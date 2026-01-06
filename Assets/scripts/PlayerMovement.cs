using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private float forwardSpeed = 5f;
    [SerializeField] private float miniSpeed = 1f;
    [SerializeField] private float tiltAngle = 35f;
    [SerializeField] private float tiltSmooth = 5f;

    [SerializeField] private GameObject backwardicon;
    [SerializeField] private GameObject smoke;

    [Header("Audio Settings")]
    [SerializeField] private AudioSource raumshipsound;

    private BatteryManager batterymanager;

    private bool isMoving = false;
    private float targetTiltX = 0f;

    private bool moveallowed = true;



    private void Start()
    {
        batterymanager = FindAnyObjectByType<BatteryManager>();
    }

    public void MoveChange()
    {
        moveallowed = !moveallowed;
    }

    public void MoveContinue()
    {
        moveallowed = true;
    }

    void Update()
    {
        if (!moveallowed) return;

        float horizontal = Input.GetAxis("Horizontal");  // A / D
        float vertical = Input.GetAxis("Vertical");    // W / S

        // --- 左右旋转（Yaw） ---
        transform.Rotate(Vector3.up, horizontal * rotationSpeed * Time.deltaTime);

        bool isPressingMoveKey = false;

        // --- W/S 控制 X 轴倾斜旋转 ---
        if (vertical > 0.1f)        // W
        {
            targetTiltX = -tiltAngle;
        }
        else if (vertical < -0.1f)  // S
        {
            targetTiltX = tiltAngle;
        }
        else
        {
            targetTiltX = 0f;       // 松开回正
        }

        // 平滑旋转到目标角度
        float currentX = transform.localEulerAngles.x;

        // 处理 360° → -180~180°
        if (currentX > 180f) currentX -= 360f;

        float newX = Mathf.Lerp(currentX, targetTiltX, Time.deltaTime * tiltSmooth);

        transform.localRotation = Quaternion.Euler(newX, transform.localEulerAngles.y, 0f);

        // --- 按 Space 键向前移动 ---
        if (Input.GetKey(KeyCode.Space))
        {
            Vector3 direction;

            if(Input.GetKey(KeyCode.LeftShift))
            {
                direction = Vector3.back;
                if(backwardicon != null)
                {
                    backwardicon.SetActive(true);
                }
            }
            else
            {
                direction = Vector3.forward;
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                if (backwardicon != null)
                {
                    backwardicon.SetActive(false);
                }
            }

            if(batterymanager.IsLowBattery())
            {
                transform.Translate(direction * miniSpeed * Time.deltaTime, Space.Self);
            }
            else
            {
                transform.Translate(direction * forwardSpeed * Time.deltaTime, Space.Self);
            }

            isPressingMoveKey = true;
        }

        if (isPressingMoveKey)
        {
            if (!isMoving)
            {
                if (raumshipsound != null)
                    raumshipsound.Play();

                batterymanager.UseBattery();
                //smoke.SetActive(true);

                isMoving = true;
            }
        }
        else
        {
            if (isMoving)
            {
                if (raumshipsound != null)
                    raumshipsound.Stop();

                batterymanager.StopBattery();
                //smoke.SetActive(false);

                isMoving = false;
            }
        }
    }

    public IEnumerator SpeedBoost(float boostDistance, float boostDuration)
    {
        float elapsed = 0f;

        // 记录起点和终点（朝 forward 冲）
        Vector3 start = transform.position;
        Vector3 end = start + transform.forward * boostDistance;


        while (elapsed < boostDuration)
        {
            elapsed += Time.deltaTime;

            // 平滑移动（可改成 SmoothStep 或曲线）
            float t = elapsed / boostDuration;
            t = Mathf.SmoothStep(0f, 1f, t);

            transform.position = Vector3.Lerp(start, end, t);

            yield return null;
        }

        Debug.Log("Dash finished.");
    }
}
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 100f;    // A/D 左右旋转速度
    [SerializeField] private float forwardSpeed = 5f;       // 空格前进速度
    [SerializeField] private float tiltAngle = 35f;         // W/S 最大倾斜角度
    [SerializeField] private float tiltSmooth = 5f;         // 倾斜的 Lerp 平滑系数

    [SerializeField] private GameObject backwardicon;


    [Header("Audio Settings")]
    [SerializeField] private AudioSource raumshipsound;

    private bool isMoving = false;
    private float targetTiltX = 0f;       // 想要到达的 X 轴旋转角

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");  // A / D
        float vertical = Input.GetAxis("Vertical");    // W / S (-1~1)

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

            transform.Translate(direction * forwardSpeed * Time.deltaTime, Space.Self);
            isPressingMoveKey = true;
        }

        // --- 统一声音逻辑 ---
        if (isPressingMoveKey)
        {
            if (!isMoving)
            {
                if (raumshipsound != null)
                    raumshipsound.Play();
                isMoving = true;
            }
        }
        else
        {
            if (isMoving)
            {
                if (raumshipsound != null)
                    raumshipsound.Stop();
                isMoving = false;
            }
        }
    }
}
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;         // 玩家或跟随目标
    public float distance = 5f;      // 相机与目标的距离
    public float height = 2f;        // 相机高度
    public float rotateSpeed = 80f;  // Q/E旋转速度
    public float smoothSpeed = 5f;   // 平滑跟随速度


    [Header("Focus Settings")]
    public float focusHeightOffset = 4f;      // 聚焦时额外抬高高度
    public float focusSmoothMultiplier = 1.5f;
    public float focusDistance = 3f;          // 聚焦时的相机距离
    public float focusPitch = 60f;            // 聚焦时固定俯视角

    private float currentAngle = 0f;
    private bool isFocusing = false;


    [Header("Zoom Settings")]
    public float zoomSpeed = 10f;
    public float minDistance = 21f;
    public float maxDistance = 34f;



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleFocusMode();
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        // --- Q/E 控制相机左右旋转（普通模式 + 聚焦模式通用）---
        if (Input.GetKey(KeyCode.Q))
            currentAngle -= rotateSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.E))
            currentAngle += rotateSpeed * Time.deltaTime;

        // 只绕 Y 轴旋转
        Quaternion rotation = Quaternion.Euler(0, currentAngle, 0);

        if (!isFocusing)
        {
            // ---------- 普通跟随模式 ----------
            // 计算相机目标位置  
            Vector3 desiredPosition = target.position + rotation * new Vector3(0, height, -distance);

            // 平滑移动
            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothSpeed);

            // 让相机朝向目标
            transform.LookAt(target.position + Vector3.up * height * 0.5f);

            // --- 鼠标滚轮控制相机距离 ---
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (Mathf.Abs(scroll) > 0.01f)
            {
                distance -= scroll * zoomSpeed;
                distance = Mathf.Clamp(distance, minDistance, maxDistance);
            }
        }
        else
        {
            // ---------- 聚焦模式 ----------
            // 你可以在类里增加这两个参数：
            // public float focusDistance = 3f;
            // public float focusPitch = 60f;

            // 使用 rotation 围绕 target 旋转，但比普通模式更近、更高
            Vector3 focusOffset = new Vector3(0, height + focusHeightOffset, -focusDistance);
            Vector3 focusPos = target.position + rotation * focusOffset;

            // 平滑靠近焦点位置
            transform.position = Vector3.Lerp(
                transform.position,
                focusPos,
                Time.deltaTime * smoothSpeed * focusSmoothMultiplier
            );

            // 固定一个俯视角度（例如 60 度），绕 Y 轴用 currentAngle，这样 Q/E 仍然生效
            float pitch = focusPitch; // 比如 60f
            Quaternion targetRot = Quaternion.Euler(pitch, currentAngle, 0f);

            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                targetRot,
                Time.deltaTime * smoothSpeed * focusSmoothMultiplier
            );
        }
    }

    public void FocusMode()
    {
        isFocusing = true;
    }

    public void ExitFocusMode()
    {
        isFocusing = false;
    }

    public void ToggleFocusMode()
    {
        isFocusing = !isFocusing;
    }
}
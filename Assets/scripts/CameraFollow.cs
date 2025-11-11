using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;         // 玩家或跟随目标
    public float distance = 5f;      // 相机与目标的距离
    public float height = 2f;        // 相机高度
    public float rotateSpeed = 80f;  // Q/E旋转速度
    public float smoothSpeed = 5f;   // 平滑跟随速度

    private float currentAngle = 0f;

    void LateUpdate()
    {
        if (target == null) return;

        // --- Q/E 控制相机左右旋转 ---
        if (Input.GetKey(KeyCode.Q))
            currentAngle -= rotateSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.E))
            currentAngle += rotateSpeed * Time.deltaTime;

        // 计算相机目标位置
        Quaternion rotation = Quaternion.Euler(0, currentAngle, 0);
        Vector3 desiredPosition = target.position + rotation * new Vector3(0, height, -distance);

        // 平滑移动
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothSpeed);

        // 让相机朝向目标
        transform.LookAt(target.position + Vector3.up * height * 0.5f);
    }
}
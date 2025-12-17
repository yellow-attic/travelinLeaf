using UnityEngine;

public class RotateZ : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 180f; // 每秒旋转角度（度）

    void Update()
    {
        transform.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);
    }
}
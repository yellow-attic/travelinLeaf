using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public float moveSpeed = 5f;

    [Header("Audio Settings")]
    [SerializeField] private AudioSource raumshipsound;
    private bool isMoving = false;


    void Update()
    {
        // --- 用 WASD 控制旋转 ---
        float horizontal = Input.GetAxis("Horizontal"); // A, D 键 默认是 -1~1
        float vertical = Input.GetAxis("Vertical");     // W, S 键 默认是 -1~1

        // 绕 Y 轴旋转（左右转向）
        transform.Rotate(Vector3.up, horizontal * rotationSpeed * Time.deltaTime);

        // 绕 X 轴旋转（上下抬头）
        transform.Rotate(Vector3.right, -vertical * rotationSpeed * Time.deltaTime, Space.Self);

        // --- 按 Space 键向前移动 ---
        if (Input.GetKey(KeyCode.Space))
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

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

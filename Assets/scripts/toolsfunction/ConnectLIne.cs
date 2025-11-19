
using UnityEngine;

public class ConnectLIne : MonoBehaviour
{
    [Header("Rotation Settings")]
    public float maxAngle = 57f;
    public float speed = 1f;

    private float randomOffset;

    [SerializeField] private bool isflip;

    [NonReorderable] public float currentangle;


    void Start()
    {
        float startAngle;

        if (isflip)
        {
            bool flip = Random.value > 0.5f;
            startAngle = flip ? -maxAngle : maxAngle;
        }
        else
        {
            startAngle = maxAngle;
        }

        randomOffset = Mathf.Asin(startAngle / maxAngle);
    }

    void Update()
    {
        // 用正弦函数产生平滑往返 [-1, 1]
        float angle = Mathf.Sin(Time.time * speed + randomOffset) * maxAngle;
        currentangle = angle;

        // 应用到本地旋转（只在 Z 轴上旋转）
        transform.localRotation = Quaternion.Euler(0, 0, angle);
    }
}

using UnityEngine;
using UnityEngine.UI;

public class BatteryManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Slider batterySlider;

    [Header("Settings")]
    [SerializeField] private float drainSpeed = 0.2f;  // 每秒消耗量 (0~1)

    void Update()
    {
        if (batterySlider == null) return;

        float vertical = Input.GetAxis("Vertical");   // W / S

        // 按住 Space 进行消耗
        if (Input.GetKey(KeyCode.Space) || Mathf.Abs(vertical) > 0.01f)
        {
            batterySlider.value -= drainSpeed * Time.deltaTime;
        }

        // 防止数值越界
        batterySlider.value = Mathf.Clamp01(batterySlider.value);
    }
}
using UnityEngine;
using UnityEngine.UI;

public class BatteryManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Slider batterySlider;

    [Header("Settings")]
    [SerializeField] private float drainSpeed = 0.2f;
    [SerializeField] private float lineengry = 0.2f;


    private bool batteryusing = false;


    void Update()
    {
        if(batteryusing)
        {
            if (batterySlider == null) return;

            batterySlider.value -= drainSpeed * Time.deltaTime;
            batterySlider.value = Mathf.Clamp01(batterySlider.value);
        }
    }

    public bool CheckBattery()
    {
        if(batterySlider.value < 1f)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void UseBattery()
    {
        batteryusing = true;
    }

    public void StopBattery()
    {
        batteryusing = false;
    }

    public void GetLineEnergy()
    {
        if (batterySlider == null) return;

        batterySlider.value += lineengry;

        batterySlider.value = Mathf.Clamp01(batterySlider.value);
    }
}
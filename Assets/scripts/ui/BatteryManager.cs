using UnityEngine;
using UnityEngine.UI;

public class BatteryManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Slider batterySlider;

    [Header("Settings")]
    [SerializeField] private float drainSpeed = 0.2f;
    [SerializeField] private float lineengry = 0.05f;
    [SerializeField] private float hinweisengry = 0.1f;
    [SerializeField] private float connectengry = 0.2f;

    private bool batteryusing = false;

    [Header("Buss")]
    [SerializeField] private GameObject Bussicon;


    void Update()
    {
        if(batteryusing)
        {
            if (batterySlider == null) return;

            batterySlider.value -= drainSpeed * Time.deltaTime;
            batterySlider.value = Mathf.Clamp01(batterySlider.value);
        }

        checkBatteryBuss();
    }

    public bool IsLowBattery()
    {
        if(batterySlider.value == 0f)
        {
            return true;
        }
        else
        {
            return false;
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

        Debug.Log("Get Energy" + lineengry);

    }

    public void GetHinweisEnergy()
    {
        if (batterySlider == null) return;

        batterySlider.value += hinweisengry;

        batterySlider.value = Mathf.Clamp01(batterySlider.value);

        Debug.Log("Get Energy" + hinweisengry);
    }

    public void GetConnectEnergy()
    {
        if (batterySlider == null) return;

        batterySlider.value += connectengry;

        batterySlider.value = Mathf.Clamp01(batterySlider.value);

        Debug.Log("Get Energy" + connectengry);
    }

    private void checkBatteryBuss()
    {
        if(batterySlider.value >= 0.9f)
        {
            Bussicon.SetActive(true);
        }
    }
}
using UnityEngine;
using UnityEngine.Rendering;


public class ShowHinweis : DistanceCheck
{
    [SerializeField] private HinweisManager hinweismanager;
    [SerializeField] private HinweisDetail hinweiscode;

    [SerializeField] private GameObject production;


    private bool isenergyget;
    private BatteryManager batterymanager;


    private void Start()
    {
        
        isenergyget = false;
        batterymanager = FindAnyObjectByType<BatteryManager>();
    }

    protected override void OnEnterRadius()
    {
        hinweismanager.AssignTexts(hinweiscode.HinweisTxt);
        hinweiscode.UnlockAchivement();

        if (!isenergyget)
        {
            batterymanager.GetHinweisEnergy();
            isenergyget = true;
        }
        production.SetActive(true);
    }

    protected override void OnExitRadius()
    {
        hinweismanager.Reset();
        production.SetActive(false);
    }

    protected override void OnInsideRadiusUpdate()
    {
        //Debug.Log("Inside...");
    }

}
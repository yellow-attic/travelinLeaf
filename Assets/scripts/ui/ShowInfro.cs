using UnityEngine;


public class ShowInfro : DistanceCheck
{
    [SerializeField] private GameObject Infropannel;


    protected override void OnEnterRadius()
    {
        //Debug.Log("A - Enter Radius");

        Infropannel.SetActive(true);
    }

    protected override void OnExitRadius()
    {
        //Debug.Log("B - Exit Radius");

        Infropannel.SetActive(false);
    }

    protected override void OnInsideRadiusUpdate()
    {
        //Debug.Log("Inside...");
    }
}
using UnityEngine;
using UnityEngine.Rendering;


public class ShowInfro : DistanceCheck
{
    [SerializeField] private GameObject Infropannel;
    [SerializeField] private GameObject targetpannel;

    [SerializeField] private GameObject production;


    private bool isenergyget;


    private void Start() {
        if (target == null) {
            target = PlayerMovement.GetPlayer();
            Debug.Log( name + " Set player as target.");
        }
    }

    protected override void OnEnterRadius()
    {
        //Debug.Log("A - Enter Radius");

        Infropannel.SetActive(false);
        targetpannel.SetActive(true);

        production.SetActive(true);
    }

    protected override void OnExitRadius()
    {
        //Debug.Log("B - Exit Radius");

        Infropannel.SetActive(false);
        targetpannel.SetActive(false);

        production.SetActive(false);
    }

    protected override void OnInsideRadiusUpdate()
    {
        //Debug.Log("Inside...");
    }
}
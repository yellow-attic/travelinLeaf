using UnityEngine;
using UnityEngine.XR;

public class ConnectManager : MonoBehaviour
{
    [SerializeField] private ConnectLIne line1;
    [SerializeField] private ConnectLIne line2;

    [SerializeField] private float connectangle;

    private ConnetTime connecttime;
    private BrokenStation currentstation;


    void Start()
    {
        connecttime = GetComponent<ConnetTime>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.C) || Raumkapsel.VR.Input.GetRightSecondaryButton())
        {
            if (!connecttime.isintime) return;

            float angleA = line1.currentangle;
            float angleB = line2.currentangle;

            if(Mathf.Abs(angleA) < connectangle && Mathf.Abs(angleB) < connectangle)
            {
                Debug.Log("Line Connect!");

                currentstation.BrokenRepair();
            }
        }
    }

    public void GetBrokenStation(BrokenStation station)
    {
        currentstation = station;
    }
}

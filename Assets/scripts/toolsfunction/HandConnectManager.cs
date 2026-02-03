using UnityEngine;

public class HandConnectManager : MonoBehaviour
{
    [SerializeField] private TrggleCheck leftcheck;
    [SerializeField] private TrggleCheck rightcheck;

    [SerializeField] private GameObject connectpanel;

    private BrokenStation currentstation;
    private bool connectallowed = false;


    void Update()  {
        if (leftcheck.handConnect && rightcheck.handConnect) {
            if(!connectallowed) {
                connectpanel.SetActive(true);
                connectpanel.GetComponent<ConnectManager>().GetBrokenStation(leftcheck.BrokStat);
                connectallowed = true;
            }
        }
    }


    public void ConnectpanelReset() {
        connectallowed = false;
    }
}
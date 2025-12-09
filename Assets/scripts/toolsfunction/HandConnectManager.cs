using UnityEngine;

public class HandConnectManager : MonoBehaviour
{
    [SerializeField] private TrggleCheck leftcheck;
    [SerializeField] private TrggleCheck rightcheck;

    [SerializeField] private GameObject connectpanel;

    private bool connectallowed = false;


    void Update()
    {
        if (leftcheck.handConnect && rightcheck.handConnect)
        {
            if(!connectallowed)
            {
                connectpanel.SetActive(true);
                connectallowed = true;
            }
        }
    }


    public void ConnectpanelReset()
    {
        connectallowed = false;
    }
}
using UnityEngine;

public class HandConnectManager : MonoBehaviour
{
    [SerializeField] private TrggleCheck leftcheck;
    [SerializeField] private TrggleCheck rightcheck;

    [SerializeField] private GameObject connectpanel;


    private bool printed = false;

    void Update()
    {
        if (leftcheck.handConnect && rightcheck.handConnect)
        {
            connectpanel.SetActive(true);
        }
    }
}
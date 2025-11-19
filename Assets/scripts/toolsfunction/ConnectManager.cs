using UnityEngine;

public class ConnectManager : MonoBehaviour
{
    [SerializeField] private ConnectLIne line1;
    [SerializeField] private ConnectLIne line2;


    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.C))
        {
            float angleA = line1.currentangle;
            float angleB = line2.currentangle;
        }
    }
}

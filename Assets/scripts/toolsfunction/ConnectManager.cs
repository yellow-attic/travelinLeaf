using UnityEngine;

public class ConnectManager : MonoBehaviour
{
    [SerializeField] private ConnectLIne line1;
    [SerializeField] private ConnectLIne line2;

    [SerializeField] private float connectangle;

    private ConnetTime connecttime;

    [SerializeField] private LineMergeAnimation linemerge;

    [SerializeField] private GameObject[] brokens;



    void Start()
    {
        connecttime = GetComponent<ConnetTime>();
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.C))
        {
            if (!connecttime.isintime) return;

            float angleA = line1.currentangle;
            float angleB = line2.currentangle;

            if(Mathf.Abs(angleA) < connectangle && Mathf.Abs(angleB) < connectangle)
            {
                Debug.Log("Line Connect!");

                linemerge.StartMerge();

                foreach(GameObject obj in brokens)
                {
                    obj.SetActive(false);
                }

                //engry ++
            }
        }
    }
}

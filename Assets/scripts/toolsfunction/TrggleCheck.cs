using UnityEngine;


public class TrggleCheck : MonoBehaviour
{
    [Header("Connection State")]
    [NonReorderable] public bool handConnect = false;

    public BrokenStation BrokStat;
    private bool isColliding = false;


    public bool CheckConnect()
    {
        if (isColliding)
        {
            handConnect = true;

            Debug.Log("hand connenct");

            return true;
        }

        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Broken"))
        {
            BrokStat = other.transform.parent.GetComponentInParent<BrokenStation>();
            isColliding = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Broken"))
        {
            isColliding = false;
        }
    }

    public void TriggerReset()
    {
        isColliding = false;

        handConnect = false;

        BrokStat = null;
    }
}


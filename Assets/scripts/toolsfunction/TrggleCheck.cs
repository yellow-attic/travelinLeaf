using UnityEngine;


public class TrggleCheck : MonoBehaviour
{
    [Header("Connection State")]
    [NonReorderable] public bool handConnect = false;

    private Collider _brokenCollider;
    public BrokenStation BrokStat;
    private bool isColliding = false;

    public struct ConnectInfo {
        public bool isConnected;
        public LineRenderer brokenLine;
        public BrokenStation brokenStation;
    }

    public ConnectInfo CheckConnect() {
        if (isColliding)  {
            handConnect = true;

            return new ConnectInfo { isConnected=true, brokenLine = _brokenCollider.GetComponentInParent<LineRenderer>(), brokenStation = _brokenCollider.GetComponentInParent<BrokenStation>() };
        }

        return new ConnectInfo { isConnected = false, brokenLine = null, brokenStation = null };
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Broken"))
        {
            _brokenCollider = other;
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


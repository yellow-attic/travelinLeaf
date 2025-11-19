using UnityEngine;

public abstract class DistanceCheck : MonoBehaviour
{
    public Transform target;
    public float radius = 5f;

    private bool isInside = false;

    void Update()
    {
        if (target == null) return;

        float distance = Vector3.Distance(transform.position, target.position);

        bool insideNow = distance <= radius;

        if (!isInside && insideNow)
        {
            isInside = true;
            OnEnterRadius();
        }
        else if (isInside && !insideNow)
        {
            isInside = false;
            OnExitRadius();
        }

        if (isInside)
        {
            OnInsideRadiusUpdate();
        }
    }

    protected abstract void OnEnterRadius();
    protected abstract void OnExitRadius();

    protected virtual void OnInsideRadiusUpdate() { }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
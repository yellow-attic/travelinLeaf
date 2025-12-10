using UnityEngine;


public class CreateLineCapsuleCollider : MonoBehaviour
{

    [SerializeField] private LineRenderer targetLine;
    [SerializeField] private float capsuleRadius = 0.1f;
    [SerializeField] private bool isTrigger = true;


    [ContextMenu("Generate Capsule Colliders For Line")]
    private void GenerateCapsuleColliders()
    {
        targetLine = GetComponent<LineRenderer>();

        if (targetLine == null)
        {
            Debug.LogError("targetLine not be assigned！");
            return;
        }

        CleanupOldColliders();

        int count = targetLine.positionCount;
        for (int i = 0; i < count - 1; i++)
        {
            Vector3 p1 = targetLine.GetPosition(i);
            Vector3 p2 = targetLine.GetPosition(i + 1);

            GameObject capsuleObj = new GameObject($"Capsule_{i}");
            capsuleObj.transform.parent = this.transform;

            CapsuleCollider col = CreateCapsuleBetweenPoints(capsuleObj, p1, p2, capsuleRadius);
            col.isTrigger = isTrigger;
        }
    }


    private CapsuleCollider CreateCapsuleBetweenPoints(GameObject host, Vector3 p1, Vector3 p2, float radius)
    {
        CapsuleCollider col = host.AddComponent<CapsuleCollider>();

        Vector3 dir = p2 - p1;
        float length = dir.magnitude;
        Vector3 center = (p1 + p2) * 0.5f;

        host.transform.position = center;

        host.transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);

        col.radius = radius;
        col.height = length + radius * 2f;

        col.direction = 1;

        return col;
    }


    [ContextMenu("Clean Capsule Colliders For Line")]
    private void CleanupOldColliders()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Transform child = transform.GetChild(i);
            if (child.name.StartsWith("Capsule_"))
            {
                DestroyImmediate(child.gameObject);
            }
        }
    }
}

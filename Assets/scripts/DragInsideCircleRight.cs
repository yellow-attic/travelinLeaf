using UnityEngine;

public class DragInsideCircleLeft : MonoBehaviour
{
    [Header("Circle Settings")]
    [SerializeField] private Transform center;
    [SerializeField] private float radius = 3f;

    [Header("Connection State")]
    [SerializeField] private TrggleCheck trigglecheck;

    private Camera cam;
    private bool isDragging = false;


    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (trigglecheck.handConnect)
            return;

        if (Input.GetMouseButtonDown(1)) isDragging = true;
        if (Input.GetMouseButtonUp(1))
        {
            isDragging = false;
            trigglecheck.CheckConnect();
        }

        if (isDragging)
        {
            DragMove();
        }
    }

    void DragMove()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Vector3.Distance(cam.transform.position, center.position);
        Vector3 targetPos = cam.ScreenToWorldPoint(mousePos);

        Vector3 offset = targetPos - center.position;

        if (offset.magnitude > radius)
            offset = offset.normalized * radius;

        transform.position = center.position + offset;
    }

    private void OnDrawGizmosSelected()
    {
        if (center != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(center.position, radius);
        }
    }
}
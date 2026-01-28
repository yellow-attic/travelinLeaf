using TMPro;
using UnityEngine;

public class DragInsideCircleRight : MonoBehaviour
{
    [Header("Circle Settings")]
    [SerializeField] private Transform center;
    [SerializeField] private float radius = 3f;

    [Header("Connection State")]
    [SerializeField] private TrggleCheck trigglecheck;

    [Header("Animation")]
    [SerializeField] private Animator handanim;

    private Camera cam;
    private bool isDragging = false;

    [SerializeField] private Vector3 startpos;


    void Start()
    {
        cam = Camera.main;
    }

    private void OnEnable()
    {
        isDragging = false;
        transform.localPosition = startpos;
        trigglecheck.TriggerReset();
    }

    void Update() {
        if (trigglecheck.handConnect)
            return;

        if (Input.GetMouseButtonDown(0) || Raumkapsel.VR.Input.GetLeftGripPressed()) isDragging = true;
        
        if (Input.GetMouseButtonUp(0) || Raumkapsel.VR.Input.GetLeftGripReleased()) {
            isDragging = false;
            if(trigglecheck.CheckConnect())
            {
                handanim.SetTrigger("IsCatched");
            }
            else
            {
                handanim.SetTrigger("IsGrapping");
            }
        }

        if (isDragging)
        {
            DragMove();
        }
    }

    void DragMove() {
        // VR control code
        if (Raumkapsel.VR.Configuration.IsVrActive()) {
            transform.localPosition += Raumkapsel.VR.Input.GetLeftHandDelta() * 4.0f;
            return;
        }

        // regular PC control code
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
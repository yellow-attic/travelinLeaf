using UnityEngine;
using UnityEngine.XR;

public class DragInsideCircleLeft : MonoBehaviour
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


    void Update()
    {
        InputDevice rightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        bool gripDown = (rightHand.TryGetFeatureValue(CommonUsages.gripButton, out bool pressed) && pressed);
        bool gripReleased = !gripDown && isDragging;

        if (trigglecheck.handConnect)
            return;

        //if (Input.GetMouseButtonDown(1)) isDragging = true;
        if (gripDown) isDragging = true;
        //if (Input.GetMouseButtonUp(1))
        if (gripReleased)
            {
            isDragging = false;
            if (trigglecheck.CheckConnect())
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
        //Vector3 mousePos = Input.mousePosition;
        //mousePos.z = Vector3.Distance(cam.transform.position, center.position);
        //Vector3 targetPos = cam.ScreenToWorldPoint(mousePos);

        //Vector3 targetPos = VrOrigin.GetLocalRightHandPosition() * 50.0f;

        //Vector3 offset = targetPos - center.position;

        //if (offset.magnitude > radius)
        //    offset = offset.normalized * radius;

        //transform.position = center.position + offset;
        transform.localPosition += VrOrigin.GetRightHandDelta() * 4.0f;
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
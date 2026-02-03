using UnityEngine;

public class DragHand : MonoBehaviour {
    enum State {
        Idle,
        Dragging,
        Connected,
    }

    private State _state = State.Idle;
    private LineRenderer _connectedLine;

    [Header("Circle Settings")]
    [SerializeField] private Transform center; // arm root position
    [SerializeField] private float radius = 3f; // arm length from center

    [Header("Connection State")]
    [SerializeField] private TrggleCheck trigglecheck;

    [Header("Animation")]
    [SerializeField] private Animator handanim;

    [SerializeField] private Vector3 _resetPosition = Vector3.zero;

    private void OnEnable() {
        // reset hand
        _state = State.Idle;
        transform.localPosition = _resetPosition;
        trigglecheck.TriggerReset();
    }

    void Update() {
        if (!trigglecheck.handConnect) {
            if (Input.GetMouseButtonDown(0)) _state = State.Dragging;
            if (Input.GetMouseButtonUp(0)) {
                if (trigglecheck.CheckConnect().isConnected) {
                    handanim.SetTrigger("IsCatched");
                    _state = State.Connected;
                    _connectedLine = trigglecheck.CheckConnect().brokenLine;
                }
                else {
                    handanim.SetTrigger("IsGrapping");
                    _state = State.Idle;
                }
            }
        }

        if (_state == State.Dragging) {
            _drag();
        }

        if (_state == State.Connected) {
            Debug.Assert(trigglecheck.handConnect);
            _grab();
        }
    }

    private void _drag() {
        Camera cam = Camera.main;

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Vector3.Distance(cam.transform.position, center.position);
        Vector3 targetPos = cam.ScreenToWorldPoint(mousePos);

        Vector3 offset = targetPos - center.position;

        if (offset.magnitude > radius)
            offset = offset.normalized * radius;

        transform.position = center.position + offset;
    }

    private void _grab() {
        Debug.Assert(_connectedLine != null, "Connected LineRenderer is null in grab state.");
        Debug.Assert(_connectedLine.positionCount == 2, "Connected LineRenderer does not have 2 positions.");
        transform.position = _connectedLine.GetPosition(1);
    }

    private void OnDrawGizmosSelected() {
        if (center != null) {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(center.position, radius);
        }
    }
}
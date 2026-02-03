using UnityEngine;
using UnityEngine.Rendering;

public class DragHand : MonoBehaviour {
    enum State {
        Idle,
        Dragging,
        Connected,
        Reset, // move arms back to reset position
    }

    enum Hand {
        Left = 0,
        Right = 1,
    }

    private State _state = State.Idle;
    private TrggleCheck.ConnectInfo _connectionInfo;

    [SerializeField] private Hand _hand;

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
            if (Input.GetMouseButtonDown((int)_hand)) _state = State.Dragging;
            if (Input.GetMouseButtonUp((int)_hand)) {
                if (trigglecheck.CheckConnect().isConnected) {
                    handanim.SetTrigger("IsCatched");
                    _state = State.Connected;
                    _connectionInfo = trigglecheck.CheckConnect();
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

            if (_connectionInfo.brokenStation.isFixed()) {
                _state = State.Reset;
            }
        }

        if (_state == State.Reset) {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, _resetPosition, Time.deltaTime * 1.5f);
            if (Vector3.Distance(transform.localPosition, _resetPosition) < 0.01f) {
                transform.localPosition = _resetPosition;
            }
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
        Debug.Assert(_connectionInfo.brokenLine != null, "Connected LineRenderer is null in grab state.");
        Debug.Assert(_connectionInfo.brokenLine.positionCount == 2, "Connected LineRenderer does not have 2 positions.");

        Vector3 target = _connectionInfo.brokenStation.mergedLine.center();
        if (Vector3.Distance(transform.position, target) > 0.1f) {
            transform.position = _connectionInfo.brokenLine.GetPosition(1);
        }

    }

    private void OnDrawGizmosSelected() {
        if (center != null) {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(center.position, radius);
        }
    }
}
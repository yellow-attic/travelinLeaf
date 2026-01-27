using UnityEngine;
using UnityEngine.XR;

using Hand = UnityEngine.XR.XRNode;

public class VrOrigin : MonoBehaviour {

    private static VrOrigin _instance;

    public static Hand LeftHand = XRNode.LeftHand;
    public static Hand RightHand = XRNode.RightHand;

    struct ControllerState {
        public Vector3 position;
        public Vector3 delta;
        public Vector2 axis;
        public bool triggerButton;
        public bool gripButton;
        public bool primaryButton;
        public bool secondaryButton;
    }

    private static ControllerState _leftControllerState;
    private static ControllerState _rightControllerState;

    void Start() {
        Debug.Assert(_instance == null);
        _instance = this;
    }

    public static Vector3 GetHandDelta(Hand hand) {
        return hand == LeftHand ? _leftControllerState.delta : _rightControllerState.delta;
    }
    public static Vector3 GetLeftHandDelta() => _leftControllerState.delta;
    public static Vector3 GetRightHandDelta() => _rightControllerState.delta;

    public static Vector2 GetLeftAxis() => _leftControllerState.axis;
    public static Vector2 GetRightAxis() => _rightControllerState.axis;

    public static bool GetLeftTrigger() => _leftControllerState.triggerButton;
    public static bool GetRightTrigger() => _rightControllerState.triggerButton;

    public static bool GetLeftPrimaryButton() => _leftControllerState.primaryButton;
    public static bool GetLeftSecondaryButton() => _leftControllerState.secondaryButton;

    public static bool GetRightPrimaryButton() => _rightControllerState.primaryButton;
    public static bool GetRightSecondaryButton() => _leftControllerState.secondaryButton;

    
    private void Update() {
        InputDevice rightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        InputDevice leftHand = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);

        updateState(leftHand, ref _leftControllerState);
        updateState(rightHand, ref _rightControllerState);
    }

    private void updateState(InputDevice hand, ref ControllerState prev) {
        ControllerState state = new ControllerState();
        state.position = hand.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 position) ? position : Vector3.zero;
        state.delta = state.position - prev.position;
        state.triggerButton = hand.TryGetFeatureValue(CommonUsages.triggerButton, out bool trigger) && trigger;
        state.gripButton = hand.TryGetFeatureValue(CommonUsages.gripButton, out bool grip) && grip;
        state.primaryButton = hand.TryGetFeatureValue(CommonUsages.primaryButton, out bool primary) && primary;
        state.secondaryButton = hand.TryGetFeatureValue(CommonUsages.secondaryButton, out bool secondary) && secondary;
        prev = state;
    }
}

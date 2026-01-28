using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR;

namespace Raumkapsel.VR {

    using Hand = UnityEngine.XR.XRNode;

    public static class Configuration {
        public static bool IsVrActive() {
            return (Application.platform == RuntimePlatform.Android);
        }
    }

    public class Input : MonoBehaviour {

        private static Input _instance;

        public static Hand LeftHand = XRNode.LeftHand;
        public static Hand RightHand = XRNode.RightHand;

        struct ControllerState {
            public enum ButtonState {
                Pressed, Released, Up, Down,
            }
            public Vector3 position;
            public Vector3 delta;
            public Vector2 axis;
            public bool triggerButton;
            public bool gripButton;
            public ButtonState gripButtonState;
            public bool primaryButton;
            public bool secondaryButton;
            // joystick as buttons
            public ButtonState stickButtonLeft;
            public ButtonState stickButtonRight;
        }

        private static ControllerState _leftControllerState;
        private static ControllerState _rightControllerState;

        void Start() {
            Debug.Assert(_instance == null);
            _instance = this;

            Debug.Log("VR Input is " + (Configuration.IsVrActive() ? "active" : "inactive"));
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

        public static bool GetLeftGrip() => _leftControllerState.gripButton;
        public static bool GetRightGrip() => _rightControllerState.gripButton;

        public static bool GetLeftGripReleased() => _leftControllerState.gripButtonState == ControllerState.ButtonState.Released;
        public static bool GetRightGripReleased() => _rightControllerState.gripButtonState == ControllerState.ButtonState.Released;

        public static bool GetLeftGripPressed() => _leftControllerState.gripButtonState == ControllerState.ButtonState.Pressed;
        public static bool GetRightGripPressed() => _rightControllerState.gripButtonState == ControllerState.ButtonState.Pressed;


        public static bool GetLeftPrimaryButton() => _leftControllerState.primaryButton;
        public static bool GetLeftSecondaryButton() => _leftControllerState.secondaryButton;

        public static bool GetRightPrimaryButton() => _rightControllerState.primaryButton;
        public static bool GetRightSecondaryButton() => _leftControllerState.secondaryButton;

        public static bool GetLeftStickButtonLeftPressed() => _leftControllerState.stickButtonLeft == ControllerState.ButtonState.Pressed;
        public static bool GetLeftStickButtonRightPressed() => _leftControllerState.stickButtonRight == ControllerState.ButtonState.Pressed;


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
            state.gripButtonState = state.gripButton && !prev.gripButton ? ControllerState.ButtonState.Pressed :
                              !state.gripButton & prev.gripButton ? ControllerState.ButtonState.Released :
                              state.gripButton ? ControllerState.ButtonState.Down :
                              ControllerState.ButtonState.Up;
            state.primaryButton = hand.TryGetFeatureValue(CommonUsages.primaryButton, out bool primary) && primary;
            state.secondaryButton = hand.TryGetFeatureValue(CommonUsages.secondaryButton, out bool secondary) && secondary;

            state.axis = hand.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 axis) ? axis : Vector2.zero;

            // compute stick axis as buttons
            state.stickButtonRight = (state.axis.x > 0.5f && prev.axis.x < 0.2f) ? ControllerState.ButtonState.Pressed : ControllerState.ButtonState.Up;
            state.stickButtonLeft = (state.axis.x < -0.5f && prev.axis.x > -0.2f) ? ControllerState.ButtonState.Pressed : ControllerState.ButtonState.Up;

            prev = state;
        }
    }
}
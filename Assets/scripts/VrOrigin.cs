using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class VrOrigin : MonoBehaviour {

    private static VrOrigin _instance;

    private Vector3 _leftHand, _rightHand;
    private Vector3 _leftHandDelta, _rightHandDelta;

    void Start() {
        Debug.Assert(_instance == null);
        _instance = this;
    }

    public static Vector3 GetLocalLeftHandPosition() {
        InputDevice leftHand = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        Vector3 leftHandPosition = leftHand.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 position) ? position : Vector3.zero;

        return _instance.transform.InverseTransformPoint(leftHandPosition);
    }

    public static Vector3 GetLocalRightHandPosition() {
        InputDevice rightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        Vector3 rightHandPosition = rightHand.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 position) ? position : Vector3.zero;

        return _instance.transform.InverseTransformPoint(rightHandPosition);
    }

    public static Vector3 GetLeftHandDelta() {
        return _instance._leftHandDelta;
    }

    public static Vector3 GetRightHandDelta() {
        return _instance._rightHandDelta;
    }

    bool _leftTrigger;
    bool _rightTrigger;

    bool _leftPrimaryButton;
    bool _leftSecondaryButton;
    bool _rightPrimaryButton;
    bool _rightSecondaryButton;

    Vector2 _leftAxis;
    Vector2 _rightAxis;

    public static Vector2 GetLeftAxis() => _instance._leftAxis;
    public static Vector2 GetRightAxis() => _instance._rightAxis;

    public static bool GetLeftTrigger() => _instance._leftTrigger;
    public static bool GetRightTrigger() => _instance._rightTrigger;

    public static bool GetLeftPrimaryButton() => _instance._leftPrimaryButton;
    public static bool GetLeftSecondaryButton() => _instance._leftSecondaryButton;
    public static bool GetRightPrimaryButton() => _instance._rightPrimaryButton;
    public static bool GetRightSecondaryButton() => _instance._rightSecondaryButton;

    private void Update() {
        InputDevice rightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        InputDevice leftHand = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);

        Vector3 rightHandPosition = rightHand.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 positionr) ? positionr : Vector3.zero;
        Vector3 leftHandPosition = leftHand.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 positionl) ? positionl : Vector3.zero;

        _leftHandDelta = leftHandPosition - _leftHand;
        _rightHandDelta = rightHandPosition - _rightHand;

        _leftHand = leftHandPosition;
        _rightHand = rightHandPosition;

        _leftTrigger = leftHand.TryGetFeatureValue(CommonUsages.triggerButton, out bool pressedl) && pressedl;
        _rightTrigger = rightHand.TryGetFeatureValue(CommonUsages.triggerButton, out bool pressedr) && pressedr;

        _leftAxis = leftHand.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 axisl) ? axisl : Vector2.zero;
        _rightAxis = rightHand.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 axisr) ? axisr : Vector2.zero;

        _leftSecondaryButton = (leftHand.TryGetFeatureValue(CommonUsages.secondaryButton, out bool pressedls) && pressedls);
        _leftPrimaryButton = (leftHand.TryGetFeatureValue(CommonUsages.primaryButton, out bool pressedlp) && pressedlp);

        _rightSecondaryButton = (rightHand.TryGetFeatureValue(CommonUsages.secondaryButton, out bool pressedrs) && pressedrs);
        _rightPrimaryButton = (rightHand.TryGetFeatureValue(CommonUsages.primaryButton, out bool pressedrp) && pressedrp);
    }
}

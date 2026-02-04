using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControls : MonoBehaviour {
    
    public static float NavigateHorizontal() {
        return Input.GetKey(KeyCode.A) ? -1.0f : Input.GetKey(KeyCode.D) ? 1.0f : 0.0f; 
    }

    public static float NavigateVertical() {
        return Input.GetKey(KeyCode.S) ? -1.0f : Input.GetKey(KeyCode.W) ? 1.0f : 0.0f;
    }

    public static float ZoomView() {
        return Input.GetKey(KeyCode.DownArrow) ? -1.0f : Input.GetKey(KeyCode.UpArrow) ? 1.0f : 0.0f;
    }

    public static float ViewRotation() {
        return Input.GetKey(KeyCode.Q) ? -1.0f : Input.GetKey(KeyCode.E) ? 1.0f : 0.0f;
    }

    public static float Move() {
        return Input.GetKey(KeyCode.Space) ? 1.0f : 0.0f;
    }

    public static bool InvertMove() {
        return Input.GetKey(KeyCode.LeftShift);
    }

    public static bool GetConnectAction() {
        return Input.GetKey(KeyCode.C);
    }

    public static bool ToolCycleForward() {
        return Input.GetKeyDown(KeyCode.P);
    }

    public static bool ToolCycleBackward() {
        return Input.GetKeyDown(KeyCode.O);
    }

    public static bool ToggleInfoPanel() {
        return Input.GetKeyDown(KeyCode.Tab);
    }
}

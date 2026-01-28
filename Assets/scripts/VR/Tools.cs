using UnityEngine;

namespace Raumkapsel.VR {

    public enum Tool {
        _Min,
        //Uv,
        Staub,
        Blitz,
        Arms,
        //Sett,
        _Max,
    }

    public class Tools : MonoBehaviour {
        private static Tools _instance;

        private int _currentTool;
        private int _nextTool;
    
        public void cycleToolForward() => _nextTool = Mathf.Min(_currentTool + 1, (int)Tool._Max - 1);
        public void cycleToolBackward() => _nextTool = Mathf.Max(_currentTool - 1, (int)Tool._Min + 1);
        
        // allows polling for single frame tool change event
        public static bool GetToolPressed(Tool tool) => _instance._currentTool != _instance._nextTool && (Tool)_instance._nextTool == tool;

        private void Start() {
            Debug.Assert(_instance == null, "Multiple Tools instances detected!");
            _instance = this;
        }

        private void Update() {
            _currentTool = _nextTool;

            // enable editor testing
            if (!VR.Configuration.IsVrActive()) {
                if (UnityEngine.Input.GetKeyUp(KeyCode.O))
                    cycleToolBackward();
                if (UnityEngine.Input.GetKeyUp(KeyCode.P))
                    cycleToolForward();

                Debug.Log($"Current tool: {(Tool)_currentTool}, Next tool: {(Tool)_nextTool}");
            }

            if (VR.Input.GetLeftStickButtonLeftPressed())
                cycleToolBackward();

            if (VR.Input.GetLeftStickButtonRightPressed())
                cycleToolForward();
        }
    }
}
using UnityEngine;

namespace Leave {

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

        public void cycleForward() => _nextTool = Mathf.Min(_currentTool + 1, (int)Tool._Max - 1);
        public void cycleBackward() => _nextTool = Mathf.Max(_currentTool - 1, (int)Tool._Min + 1);

        // allows polling for single frame tool change event
        public static bool GetToolPressed(Tool tool) => _instance._currentTool != _instance._nextTool && (Tool)_instance._nextTool == tool;

        private void Start() {
            Debug.Assert(_instance == null, "Multiple Tools instances detected!");
            _instance = this;
        }

        private void Update() {
            _currentTool = _nextTool;

            // enable editor testing
            if (InputControls.ToolCycleBackward())
                cycleBackward();
            if (InputControls.ToolCycleForward())
                cycleForward();

            //Debug.Log($"Current tool: {(Tool)_currentTool}, Next tool: {(Tool)_nextTool}");

        }
    }
}
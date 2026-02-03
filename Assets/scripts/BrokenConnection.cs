using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenConnection : MonoBehaviour {

    [SerializeField] Line _brokenLineA;
    [SerializeField] Line _brokenLineB;
    [SerializeField] Line _mergedLine;

    [ContextMenu("Randomize Break Point")]
    private void _randomizeBreakPoint() {

    }
}

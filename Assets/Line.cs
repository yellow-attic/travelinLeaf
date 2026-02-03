using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Line : MonoBehaviour {

    public Transform start;
    public Transform end;

    public void applyPositions() {
        if (start) 
           GetComponent<LineRenderer>().SetPosition(0, start.position);
        if (end)
            GetComponent<LineRenderer>().SetPosition(1, end.position);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenStation : MonoBehaviour {

    //[SerializeField] private Line _lineA;
    //[SerializeField] private Line _lineB;
    //[SerializeField] private Line _mergedLine;

    private LineMergeAnimation linemerge;
    private BatteryManager batterymanager;
    private bool isconnected;

    private void Start() {
        batterymanager = FindAnyObjectByType<BatteryManager>();
        linemerge = GetComponent<LineMergeAnimation>();
        isconnected = false;
    }

    public bool isFixed() {
        return linemerge.isMergingFinished();
    }

    [ContextMenu("Random Break Point")]
    private void randomBreakPoint() {
        Line brokenLineA = GetComponent<LineMergeAnimation>().getLineA().GetComponent<Line>();
        Line brokenLineB = GetComponent<LineMergeAnimation>().getLineB().GetComponent<Line>();
        Line mergedLine = GetComponent<LineMergeAnimation>().getMergedLine().GetComponent<Line>();

        LineRenderer mergedLineRenderer = GetComponent<LineMergeAnimation>().getMergedLine();

        brokenLineA.start = mergedLine.start;
        brokenLineB.start = mergedLine.end;

        // now set random mid points
        Vector3 breakPointA = Vector3.Lerp(mergedLine.start.position, mergedLine.end.position, 0.35f);
        Vector3 breakPointB = Vector3.Lerp(mergedLine.start.position, mergedLine.end.position, 0.65f);

        brokenLineA.transform.GetChild(0).position = breakPointA;
        brokenLineB.transform.GetChild(0).position = breakPointB;

        brokenLineA.end = brokenLineA.transform.GetChild(0);
        brokenLineB.end = brokenLineB.transform.GetChild(0);
    }

    public void BrokenRepair() {
        if (isconnected) return;

        if (linemerge != null)
            linemerge.play();

        batterymanager.GetConnectEnergy();
        isconnected = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenStation : MonoBehaviour {
    public Line mergedLine;
    public Line lineA;
    public Line lineB;

    private LineMergeAnimation linemerge;
    private BatteryManager batterymanager;
    private bool isconnected;

    private void Start() {
        batterymanager = FindAnyObjectByType<BatteryManager>();
        linemerge = GetComponent<LineMergeAnimation>();
        isconnected = false;

        // TODO: remove when all prefabs updated
        if (mergedLine == null) return;

        // start disabled
        mergedLine.gameObject.SetActive(false);

        // start broken parts enabled
        lineA.gameObject.SetActive(true);
        lineB.gameObject.SetActive(true);
    }

    public void setRepairedConnection() {
        mergedLine.gameObject.SetActive(true);
        lineA.gameObject.SetActive(false);
        lineB.gameObject.SetActive(false);
    }

    public bool isFixed() {
        return mergedLine.gameObject.activeSelf;
    }

    [ContextMenu("Random Break Point")]
    private void randomBreakPoint() {
        LineRenderer mergedLineRenderer = mergedLine.renderer();

        lineA.start = mergedLine.start;
        lineB.start = mergedLine.end;

        // now set random mid points
        Vector3 breakPointA = Vector3.Lerp(mergedLine.start.position, mergedLine.end.position, 0.35f);
        Vector3 breakPointB = Vector3.Lerp(mergedLine.start.position, mergedLine.end.position, 0.65f);

        lineA.end.position = breakPointA;
        lineB.end.position = breakPointB;
    }

    public void BrokenRepair() {
        if (isconnected) return;

        if (linemerge != null)
            linemerge.play();

        batterymanager.GetConnectEnergy();
        isconnected = true;
    }
}

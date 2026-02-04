using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BrokenStation : MonoBehaviour {
    [Header("Broken Line Transforms")]
    [SerializeField] private Transform _brokenStart;
    [SerializeField] private Transform _brokenEnd;
    [SerializeField] private Transform _lights;

    public Line mergedLine;
    public Line lineA;
    public Line lineB;

    private LineMergeAnimation linemerge;
    private BatteryManager batterymanager;
    private bool isconnected = false;

    private void Start() {
        batterymanager = FindAnyObjectByType<BatteryManager>();
        linemerge = GetComponent<LineMergeAnimation>();

        // TODO: remove when all prefabs updated
        if (mergedLine == null) return;

        // properly sets start+end for broken lines
        applyBrokenConnections();

        // start merged line disabled
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
    private void applyBrokenConnections() {
        transform.position = Vector3.Lerp(_brokenStart.position, _brokenEnd.position, 0.5f);

        // prepare merged line
        mergedLine.start = _brokenStart;
        mergedLine.end = _brokenEnd;
        mergedLine.applyPositions();

        LineRenderer mergedLineRenderer = mergedLine.renderer();

        lineA.start = mergedLine.start;
        lineB.start = mergedLine.end;

        // now set random mid points
        Vector3 breakPointA = Vector3.Lerp(mergedLine.start.position, mergedLine.end.position, 0.35f);
        Vector3 breakPointB = Vector3.Lerp(mergedLine.start.position, mergedLine.end.position, 0.65f);

        lineA.end.position = breakPointA;
        lineB.end.position = breakPointB;

        lineA.applyPositions();
        lineB.applyPositions();

        // set point light position
        _lights.position = mergedLine.center() + Vector3.up;
    }

    public void startRepair() {
        if (isconnected) return;

        if (linemerge != null)
            linemerge.play();

        batterymanager.GetConnectEnergy();
        isconnected = true;
    }
}

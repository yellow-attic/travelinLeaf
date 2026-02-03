using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenStation : MonoBehaviour {

    [Header("Broken Parts")]
    [SerializeField] private GameObject[] brokens;

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

    public void BrokenRepair() {
        if (isconnected) return;

        if (linemerge != null)
            linemerge.play();

        if (brokens != null)
        {
            foreach (GameObject obj in brokens)
            {
                obj.SetActive(false);
            }
        }

        batterymanager.GetConnectEnergy();

        isconnected = true;
    }
}

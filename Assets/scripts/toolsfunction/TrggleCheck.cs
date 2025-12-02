using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrggleCheck : MonoBehaviour
{
    [Header("Connection State")]
    [NonReorderable] public bool handConnect = false;

    private bool isColliding = false;


    public void CheckConnect()
    {
        if (isColliding)
        {
            handConnect = true;

            Debug.Log("hand connenct");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        isColliding = true;
    }


    private void OnTriggerExit(Collider other)
    {
        isColliding = false;
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSupplyer : MonoBehaviour
{
    public PowerRequirer interactiveObject;

    public bool isBatteryPlugged;

    private void FixedUpdate()
    {
        if(isBatteryPlugged == true)
        {

            interactiveObject.SetActive(true);
        }
    }





}

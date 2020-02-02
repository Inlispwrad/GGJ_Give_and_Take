using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPedestal : MonoBehaviour
{
    public SpriteRenderer batterySprite;
    private InteractiveObject interact;

    private void Start()
    {
        interact = GetComponent<InteractiveObject>();
    }


    private void FixedUpdate()
    {
        if (interact.partsInserted.Count == 0)
        {
            batterySprite.enabled = false;
        }
        else
        {
            batterySprite.enabled = true;
        }
    }

    public bool IsPluggedBattery()
    {
        return (interact.partsInserted.Count != 0);
    }
}

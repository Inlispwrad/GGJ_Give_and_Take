using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObject
{
    public Collider2D hitBox;

    public int reqGear = 0;
    public int reqSpring = 0;
    public int reqBattery = 0;
    private bool activated = false;

    GameObject[] cosmetics;
    int cosmeticsSize;

    public BaseObject(int Gear, int Spring, int Battery, Collider2D collider, bool broken, GameObject[] gameObjects, int size)
    {
        reqGear = Gear;
        reqSpring = Spring;
        reqBattery = Battery;
        hitBox = collider;
        activated = !broken;
        cosmetics = gameObjects;
        cosmeticsSize = size;

        //SpriteRenderer[] cosmetics = hitBox.GetComponentInParent<Transform>().gameObject.GetComponentsInChildren<SpriteRenderer>();
        foreach (GameObject cosmetic in cosmetics)
        {
            if (cosmetic.CompareTag("Cosmetic"))
            {
                cosmetic.GetComponent<SpriteRenderer>().enabled = activated;
            }
        }
    }

    public void Activate()
    {
        activated = true;

        //SpriteRenderer[] cosmetics = hitBox.GetComponentInParent<Transform>().gameObject.GetComponentsInChildren<SpriteRenderer>();
        foreach (GameObject cosmetic in cosmetics)
        {
            if(cosmetic.CompareTag("Cosmetic"))
            {
                cosmetic.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }

    public int[] Deactivate()
    {
        activated = false;

        //SpriteRenderer[] cosmetics = hitBox.GetComponentInParent<Transform>().gameObject.GetComponentsInChildren<SpriteRenderer>();
        foreach (GameObject cosmetic in cosmetics)
        {
            if (cosmetic.CompareTag("Cosmetic"))
            {
                cosmetic.GetComponent<SpriteRenderer>().enabled = false;
            }
        }

        return GetReq();
    }

    public int[] GetReq()
    {
        int[] output = new int[3];
        output[0] = reqGear;
        output[1] = reqSpring;
        output[2] = reqBattery;
        return output;
    }

    public bool getStatus()
    {
        return activated;
    }

    public Vector3 getPos()
    {
        return hitBox.bounds.center;
    }
}

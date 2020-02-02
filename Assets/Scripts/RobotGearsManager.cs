using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotGearsManager : MonoBehaviour
{
    public Gear gear1;
    public Gear gear2;
    public Gear backGear1;
    public Gear backGear2;
    private int currentExsistence = 0;


    public void GearExsistence(int numGear)
    {
        gear1.gameObject.SetActive(false);
        gear2.gameObject.SetActive(false);
        backGear1.gameObject.SetActive(false);
        backGear2.gameObject.SetActive(false);
        if (numGear == 0)
        {
            return;
        }
        if (numGear > 0)
        {
            gear1.gameObject.SetActive(true);
        }
        if (numGear > 1)
        {
            gear2.gameObject.SetActive(true);
        }
        if (numGear > 2)
        {
            backGear2.gameObject.SetActive(true);
        }
        if (numGear > 3)
        {
            backGear1.gameObject.SetActive(true);
        }
        currentExsistence = numGear;
    }
}

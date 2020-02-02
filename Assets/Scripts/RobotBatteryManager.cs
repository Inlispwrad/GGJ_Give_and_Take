using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBatteryManager : MonoBehaviour
{
    public Battery mainBattery;
    public Battery subBattery;



    public void BatteryExistence(int numBattery)
    {
        mainBattery.gameObject.SetActive(false);
        subBattery.gameObject.SetActive(false);
        if (numBattery > 0)
            mainBattery.gameObject.SetActive(true);
        if (numBattery > 1)
            subBattery.gameObject.SetActive(true);
    }
    public bool CheckExistenceOfBattery(int index) // 0 and 1
    {
        if (index == 0)
        {
            return mainBattery.IsActive();
        }
        if (index == 1)
        {
            return subBattery.IsActive();
        }
        return false;
    }

    public void UseBatteryBurst(int index) // 0 and 1
    {
        if (CheckExistenceOfBattery(index))
        {
            if (index == 0)
                mainBattery.Burst();
            else
                subBattery.Burst();
        }
    }

    public void AddBattery()
    {
        if (mainBattery.IsActive() == false)
        {
            mainBattery.Reset();
        }
        else if (subBattery.IsActive() == false)
        {
            subBattery.Reset();
        }
    }

    public int NumActiveBattery()
    {
        int count = 0;
        if (subBattery.IsActive())
            count++;
        if (mainBattery.IsActive())
            count++;
        return count;
    }
}

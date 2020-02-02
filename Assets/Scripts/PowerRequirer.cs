using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerRequirer : MonoBehaviour
{
    private bool isActive;

    public void SetActive(bool active)
    {
        isActive = active;
    }
    public bool IsActive()
    {
        return isActive;
    }
}

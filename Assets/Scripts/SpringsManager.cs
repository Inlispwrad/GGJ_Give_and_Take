using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringsManager : MonoBehaviour
{
    public GameObject frontSpring1;
    public GameObject frontSpring2;
    public GameObject backSpring1;
    public GameObject backSpring2;
    private int currentExistence;

    public void SpringsExistence(int numSpring)
    {
        if (numSpring == currentExistence)
            return;
        frontSpring2.SetActive(false);
        frontSpring1.SetActive(false);
        backSpring1.SetActive(false);
        backSpring2.SetActive(false);
        if (numSpring > 0)
            frontSpring2.SetActive(true);
        if (numSpring > 1)
            frontSpring1.SetActive(true);
        if (numSpring > 2)
            backSpring1.SetActive(true);
        if (numSpring > 3)
            backSpring2.SetActive(true);
    }
}

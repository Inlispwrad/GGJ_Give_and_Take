using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconNumberManager : MonoBehaviour
{
    public List<SpriteRenderer> icons;

    public int currentExistence = -1;

    public void IconsExistence(int numIcons)
    {
        if (numIcons == currentExistence)
            return;
        for (int i = 0; i < icons.Count; i++)
        {
            if (numIcons > i)
            {
                icons[i].gameObject.SetActive(true);
            }
            else
            {
                icons[i].gameObject.SetActive(false);
            }
        }
        currentExistence = numIcons;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPressHint : MonoBehaviour
{
    public bool isGive; // if false, then it is taken

    public SpriteRenderer hint;
    public Sprite giveHint;
    public Sprite takeHint;


    public string typeOfPart;
    public Sprite gear;
    public Sprite spring;
    public Sprite battery;
    public SpriteRenderer partSr;


    private void Update()
    {
        if (isGive)
        {
            hint.sprite = takeHint;
        }
        else
        {
            hint.sprite = giveHint;
        }

        if (typeOfPart == "Gear" && partSr.sprite != gear)
        {
            partSr.sprite = gear;
        }
        if (typeOfPart == "Spring" && partSr.sprite != spring)
        {
            partSr.sprite = spring;
        }
        if (typeOfPart == "Battery" && partSr.sprite != battery)
        {
            partSr.sprite = battery;
        }
    }

}

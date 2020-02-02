using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{

    public bool isMain;
    Animator anim;
    SpriteRenderer sr;

    private void Start()
    {
        anim = this.GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        anim.SetBool("isMain", isMain);
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Stop"))
        {
            sr.enabled = false;
        }
        else
        {
            sr.enabled = true;
        }
    }

    public bool IsActive()
    {
        return sr.isVisible;
    }

    public void Lost()
    {
        sr.enabled = false;
    }


    public void Reset()
    {
        anim.SetBool("isBursting", false);
    }

    public void Burst()
    {
        anim.SetBool("isBursting", true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{

    public float targetDistanceX;
    public float targetDistanceY;


    private InteractiveObject interact;

    private Animator anim;

    


    // Start is called before the first frame update
    void Start()
    {
        interact = GetComponent<InteractiveObject>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {

        if (interact.isActive == true)
        {
            anim.SetBool("isActive", true);
            Active();
        }
        else
        {
            anim.SetBool("isActive", false);
        }
    }

    void Active()
    {
        //Having target, lunch it.
        if (interact.IsPlayerTouchingTrigger())
        {
            anim.SetBool("havingTarget", true);
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("JumpPad_WindUp") == false)
            {
                PlayerController player = GameManager.GetPlayer();
                float g = Mathf.Abs(Physics2D.gravity.y);
                float t = Mathf.Sqrt(targetDistanceY * Game2D.METER / g);
                player.AddForceOnPlayer(targetDistanceX * Game2D.METER / t, t * g);
            }
        }
        else
        {
            anim.SetBool("havingTarget", false);
        }
    }
}

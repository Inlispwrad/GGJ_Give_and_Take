using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundingBox : MonoBehaviour
{
    private bool isGround = false;
    Collider2D box;
    private void Start()
    {
        box = this.GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        if (box.IsTouchingLayers())
        {
            isGround = true;
            Debug.Log("IsTouchedGround");
        }
        else
        {
            isGround = false;
            Debug.Log("IsNotTouchingGround");
        }
    }



    public bool IsGround()
    {
        return isGround;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGround = true;
        }
    }
}

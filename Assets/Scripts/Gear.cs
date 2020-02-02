using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    [SerializeField]
    private float speedOfRotation; // In degree per sec

    public bool isActive = false;
    public bool isReversed = false;
    Animator animator;
    SpriteRenderer sr;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update() // Graphic Updating
    {
        animator.SetBool("isActive", isActive);
        animator.SetBool("isReversed", isReversed);

        animator.speed = speedOfRotation/360;
    }

    public void ChangeRotatingSpeed(float degreePerSec)
    {

        if (degreePerSec > 0)
            isReversed = false;
        else
            isReversed = true;

        speedOfRotation = Mathf.Abs(degreePerSec);
    }
}

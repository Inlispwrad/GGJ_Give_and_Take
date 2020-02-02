using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private InteractiveObject interact;
    public SpriteRenderer gearSr;
   

    public Vector2 end;
    public Vector2 start;
    private Vector2 fixedPos;

    public float restTime;
    private float restTimer;
    private bool inStation = false;
    public float tripTime;

    public bool isForward = true;




    // Start is called before the first frame update
    void Start()
    {
        interact = GetComponent<InteractiveObject>();
        fixedPos = new Vector2(transform.position.x, transform.position.y);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (interact.isActive)
        {
            gearSr.enabled = true;
            trip();
        }
        else
        {
            gearSr.enabled = false;
        }
    }

    void trip()
    {
        if (isForward)
        {
            Vector3 targetPos = new Vector3(fixedPos.x + end.x, fixedPos.y + end.y, 1);
            transform.position = Vector3.MoveTowards(transform.position, targetPos, (1 / tripTime) * Time.fixedDeltaTime);
            if (Mathf.Abs(transform.position.x - targetPos.x) < 0.02f && Mathf.Abs(transform.position.y - targetPos.y) < 0.02f)
            {
                if (inStation == false)
                {
                    inStation = true;
                    restTimer = 0;

                }
                if (inStation)
                {
                    restTimer += Time.fixedDeltaTime;
                }
                if (restTimer >= restTime)
                {
                    isForward = false;
                    inStation = false;
                    restTimer = 0;
                }
            }
        }
        else
        {
            Vector3 targetPos = new Vector3(fixedPos.x + start.x, fixedPos.y + start.y, 1);
            transform.position = Vector3.MoveTowards(transform.position, targetPos, (1 / tripTime) * Time.fixedDeltaTime);
            if (Mathf.Abs(transform.position.x - targetPos.x) < 0.02f && Mathf.Abs(transform.position.y - targetPos.y) < 0.02f)
            {
                if (inStation == false)
                {
                    inStation = true;
                    restTimer = 0;

                }
                if (inStation)
                {
                    restTimer += Time.fixedDeltaTime;
                }
                if (restTimer >= restTime)
                {
                    isForward = true;
                    inStation = false;
                    restTimer = 0;
                }
            }
        }
    }
}

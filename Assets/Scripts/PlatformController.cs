using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour 
{

    public SpriteRenderer gearSr;


    [Header("Movement Parameters")]
    public int reqGear = 0;
    public int reqSpring = 0;
    public int reqBattery = 0;

    public float endX;
    public float endY;

    public Transform start;
    public Transform end;

    public float speed = 0.005f;
    public int Stoptime = 90;

    public GameObject battery;
    public GameObject gear;

    [Header("Hidden Parameters")]
    [SerializeField]
    //private bool working = false;


    public InteractiveObject interact;




    public BaseObject baseObj;
    private Vector2 velocity;
    private Vector3 currPosition;
    //private Collider2D hitBox;

    bool forward = true;
    bool stop = false;
    int stopCount = 0;
    int stopCooldown = 0;
    int initialCountdown = 10;

    int direction = 1;

//----------------------------------------------------------------------------------------------------------------------\\

    Vector2 getPosition(Transform input)
    {
        float x = input.position.x;
        float y = input.position.y;
        Vector2 output = new Vector2(x, y);
        return output;
    }

    public int[] GetReq()
    {
        int[] output = new int[3];
        output[0] = reqGear;
        output[1] = reqSpring;
        output[2] = reqBattery;
        return output;
    }

    void UpdateValues()
    {
        currPosition = baseObj.getPos();
    }

    void Move()
    {
        if (interact.isActive) // Active 
        {
            gearSr.enabled = true;
            if (initialCountdown > 0)
                initialCountdown--;

            if (stop)
            {
                stopCount++;
                if (stopCount >= Stoptime)
                {
                    stop = false;
                    stopCount = 0;
                    stopCooldown = 60;
                }
            }

            else
            {
                if (forward)
                {
                    //Debug.Log("Going up");
                    velocity = Vector2.MoveTowards(currPosition, getPosition(end), speed);
                    velocity.x = velocity.x - currPosition.x;
                    velocity.y = velocity.y - currPosition.y;
                }

                else
                {
                    //Debug.Log("Going down");
                    velocity = Vector2.MoveTowards(currPosition, getPosition(start), speed);
                    velocity.x = velocity.x - currPosition.x;
                    velocity.y = velocity.y - currPosition.y;
                }


                if (Mathf.Abs(currPosition.x - getPosition(end).x) < 0.1f && Mathf.Abs(currPosition.y - getPosition(end).y) < 0.1f)
                {
                    if (stopCooldown <= 0)
                    {
                        stop = true;
                        forward = false;
                        direction = -1;
                    }

                    else
                    {
                        stopCooldown--;
                    }
                }

                if (Mathf.Abs(currPosition.x - getPosition(start).x) < 0.1f && Mathf.Abs(currPosition.y - getPosition(start).y) < 0.1f)
                {
                    if (stopCooldown <= 0 && initialCountdown <= 0)
                    {
                        stop = true;
                        forward = true;
                        direction = 1;
                    }

                    else
                    {
                        stopCooldown--;
                    }
                }

                transform.Translate(velocity * Time.deltaTime);
                //transform.position = new Vector2(transform.position.x + speed * velocity.x, transform.position.y + speed * velocity.y);

                float magnitude = Mathf.Sqrt(Mathf.Pow(velocity.x, 2) + Mathf.Pow(velocity.y, 2));

                gear.GetComponent<Gear>().ChangeRotatingSpeed(direction * magnitude * 360);
            }
        }
        else
        {
            gearSr.enabled = false;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        interact = GetComponent<InteractiveObject>();
        Collider2D hitBox = GetComponentInChildren<BoxCollider2D>();
        GameObject[] cosmetics = new GameObject[2];
        cosmetics[0] = gear;
        cosmetics[1] = battery;
        baseObj = new BaseObject(reqGear, reqSpring, reqBattery, hitBox, true, cosmetics, 2);

        if (Mathf.Abs(endY) < 0.01f || Mathf.Abs(endX) < 0.01f)
        {
            start.position = baseObj.getPos();
            end.position = new Vector3(baseObj.getPos().x + endX, baseObj.getPos().y + endY, baseObj.getPos().z);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        UpdateValues();
        Move();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Character Property

    public GroundingBox GroundingBox;

    public int numGear;
    public int numSpring;
    public int numBattery;
    public int weight; // It will affect on the max speed and jump height of the character. 1 gear = 1, 2 spring = 1, 1 battery = 2;

    //Robot Forming Level
    int arms = 0; // only from 0 to 2;
    int legs = 0; // only from 0 to 2;
    bool head = false;

    //Basic Movement Parameters
    [Header("Movement Parameters")]
    public float maxSpeedPerGear = 2; // the maximum velocity (distance/1sec) per 1 gear.
    public float accelerationTime = 0.5f; //
    public float deaccelerationMeterPerSec = 2f; 
    public float jumpHeight; //
    [Header("Hidden Parameters")]
    [SerializeField]
    private float currentSpeed = 0;
    private float inheritingSpeed = 0;
    private float groundHeight = 0.2f;


    //grounding
    int groundTime = 0;
    bool onGround = false;
    bool prevGround = false;

    bool tempOnGround = false;
    bool stopGroundCheck = false;

    bool isGrounding = false;

    bool ground = false;
    //Position Vectors
    Vector2 bottomLeftCorner;
    Vector2 bottomRightCorner;


    //Basic System 
    Collider2D hitBox;
    Rigidbody2D rg2d;
    Animator anim;
    public GameObject avatar;
    RobotGearsManager gears;
    SpringsManager springs;
    RobotBatteryManager batteries;


    // Start is called before the first frame update
    void Start()
    {
        rg2d = GetComponent<Rigidbody2D>();
        hitBox = GetComponentInChildren<Collider2D>();
        anim = GetComponentInChildren<Animator>();
        gears = GetComponentInChildren<RobotGearsManager>();
        springs = GetComponentInChildren<SpringsManager>();
        batteries = GetComponentInChildren<RobotBatteryManager>();
    }

    private void FixedUpdate()
    {
        ValueUpdate();

        Actions();

        AnimValueUpdate();
    }

    private void ValueUpdate()
    {
        weight = numGear + numSpring/(int)2 + numBattery * 2;
        accelerationTime = 0.75f / (1 + numBattery);

        bottomLeftCorner.x = hitBox.bounds.center.x - hitBox.bounds.extents.x + 1;
        bottomLeftCorner.y = hitBox.bounds.center.y - hitBox.bounds.extents.y;

        bottomRightCorner.x = hitBox.bounds.center.x + hitBox.bounds.extents.x - 1;
        bottomRightCorner.y = hitBox.bounds.center.y - hitBox.bounds.extents.y;
    }

    private void AnimValueUpdate()
    {
        //Moving
        anim.SetFloat("currentSpeed", Mathf.Abs(currentSpeed));
        if (Mathf.Abs(currentSpeed) > 0)
        {
            float moveAnimSpeedRatio = (Mathf.Abs(currentSpeed)/0.64f);
            anim.speed = moveAnimSpeedRatio;
        }
        else
        {
            anim.speed = 1;
        }
        //Falling
        anim.SetFloat("VerticalVelocity", rg2d.velocity.y);


        //OnGround
        anim.SetBool("OnGround", GroundingBox.IsGround());


        //ActionId
        anim.SetInteger("numGear", numGear);

        if (anim.GetInteger("ActionId") == 6 && anim.GetCurrentAnimatorStateInfo(0).IsName("Robot_Jump_Fall") == true)
        {
            anim.SetInteger("ActionId", 0);
        }
        if (GroundingBox.IsGround() == true)
        { 
            if (numGear == 0 && anim.GetCurrentAnimatorStateInfo(0).IsName("Robot_Idle_0Gear"))
            {
                anim.SetInteger("ActionId", 0);
            }
            else if (numGear < 5 && numGear >= 3 && anim.GetCurrentAnimatorStateInfo(0).IsName("Robot_Idle_4Gears"))
            {
                anim.SetInteger("ActionId", 2);
            }
            else if (numGear > 0 && numGear <= 2 && anim.GetCurrentAnimatorStateInfo(0).IsName("Robot_Idle_2Gears"))
            {
                anim.SetInteger("ActionId", 1);
            }
        }
        else
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Robot_Jump_Start"))
            {
                anim.SetInteger("ActionId", 6);
                anim.speed = 1 - (0.5f/3) * (numSpring - 1);
            }
        }
        gears.GearExsistence(numGear);
        springs.SpringsExistence(numSpring);
        batteries.BatteryExistence(numBattery);
    }

    void Actions()
    {
        Move();
        Jump();
    }

    // Actions
    private void Move()
    {
        if (GroundingBox.IsGround() == true)
        {

            inheritingSpeed = Mathf.Lerp(inheritingSpeed, 0, (1/2) * Time.fixedDeltaTime);
        }

        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.05f)
        {
            if (numGear != 0)
            {
                int horz = (Input.GetAxis("Horizontal") > 0) ? 1 : -1;
                float amountOfSpeed = maxSpeedPerGear / ((weight/2 < 1)?1:weight/2) * numGear*(1+numBattery/2) * Game2D.METER;
                //New Speed = [previous speed] + [amount of speed increase]
                currentSpeed = currentSpeed + amountOfSpeed * ((1 /accelerationTime) * Time.fixedDeltaTime) * horz;
                if (Mathf.Abs(currentSpeed) > amountOfSpeed)
                {
                    currentSpeed = amountOfSpeed * horz;
                }

                //Change Facing
                if (avatar != null)
                {
                    avatar.transform.localScale = new Vector3(horz,1,1);
                }
            }
          
        }
        else
        {
            float newSpeed;
            float amountOfSpeedDecrease = deaccelerationMeterPerSec * weight * Game2D.METER * Time.fixedDeltaTime;
            if ((Mathf.Abs(currentSpeed) - amountOfSpeedDecrease) < 0)  
            {
                newSpeed = 0;
            }
            else
            {
                newSpeed = currentSpeed - (amountOfSpeedDecrease * (currentSpeed > 0 ? 1 : -1));
            }
            currentSpeed = newSpeed;
        }
        if (numGear != 0)
        {
            if (Mathf.Abs(rg2d.velocity.x) < 0.02f)
            { 
                inheritingSpeed = 0;
            }
            rg2d.velocity = new Vector2(currentSpeed + inheritingSpeed, rg2d.velocity.y);
        }
    }

    private void Jump()
    {
        if (numSpring != 0)
        {
            if(Input.GetAxis("Jump")  > 0.99f && GroundingBox.IsGround())
            {
                float g = Mathf.Abs(Physics2D.gravity.y);
                float t = Mathf.Sqrt((jumpHeight*numSpring*Game2D.METER)/g); //Original formula will be t = 2h/g, but we only want 1 body unit of height.
                //Debug.Log(t);
                float initialVerticleVelocity = g * t;
                rg2d.velocity = new Vector2(rg2d.velocity.x, initialVerticleVelocity);

                anim.SetInteger("ActionId", 5);
            }
        }
    }

    private void CheckGround()
    {

        tempOnGround = Physics2D.Raycast(bottomLeftCorner, Vector2.down, groundHeight)
               || Physics2D.Raycast(bottomRightCorner, Vector2.down, groundHeight);
        if (!stopGroundCheck)
        {
            prevGround = onGround;

            //Character Actions
            if (!prevGround && tempOnGround || groundTime > 0)
            {
                groundTime++;
            }

            if (groundTime == 20 && tempOnGround)
            {
                onGround = true;
            }

            else if (groundTime >= 20)
                groundTime = 0;

            if (tempOnGround)
                Move();

            if (onGround)
            {
                Jump();
            }
        }
        else
        {
            Move();
            Jump();
        }

        //stopGroundCheck = false;
    }

    public void OnPlatform()
    {
        stopGroundCheck = true;
        Debug.Log("OnPlatform");
    }

    public void OffPlatform()
    {
        stopGroundCheck = false;
        Debug.Log("OffPlatform");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log("triggered");
       if(collision.CompareTag("Gear"))
        {
            if (numGear < 4)
            {
                numGear++;
                Destroy(collision.gameObject);
            }
        }

        if (collision.CompareTag("Spring"))
        {
            if (numSpring < 4)
            {
                numSpring++;
                Destroy(collision.gameObject);
            }
        }

        if (collision.CompareTag("Battery"))
        {
            if (numBattery < 4)
            {
                numBattery++;
                Destroy(collision.gameObject);
            }
        }

        if (collision.CompareTag("baseObject"))
        {
            //Debug.Log("module");
            //stopGroundCheck = true;

            if (Input.GetAxis("Interact") > 0f)
            {

                BaseObject baseObj = collision.GetComponentInParent<PlatformController>().baseObj;

                if (!baseObj.getStatus())
                {
                    int[] reqs = baseObj.GetReq();

                    if (numGear >= reqs[0] && numSpring >= reqs[1] && numBattery >= reqs[2])
                    {
                        numGear = numGear - reqs[0];
                        numSpring = numSpring - reqs[1];
                        numBattery = numBattery - reqs[2];
                        baseObj.Activate();
                    }
                }
            }

            if (Input.GetAxis("Take") > 0)
            {
                BaseObject baseObj = collision.GetComponentInParent<PlatformController>().baseObj;
                if (baseObj.getStatus())
                {
                    int[] add = baseObj.Deactivate();
                    numGear = numGear + add[0];
                    numSpring = numSpring + add[1];
                    numBattery = numBattery + add[2];
                }
            }
        }
    }

    public void AddForceOnPlayer(float vX, float vY)
    {
        inheritingSpeed = vX;
        rg2d.velocity = new Vector2(vX, vY);
    }


    public int GetNumOfParts(string partsType)
    {
        switch (partsType)
        {
            case "Gear":return numGear;
            case "Spring":return numSpring;
            case "Battery":return numBattery;
            default: return 0;
        }
    }
    public void AddParts(string partsType)
    {
        switch (partsType)
        {
            case "Gear": numGear += ((numGear + 1 > 4)?0 : 1);break;
            case "Spring": numSpring += ((numSpring + 1 > 4) ? 0 : 1);break;
            case "Battery": numBattery += ((numBattery + 1 > 2) ? 0 : 1);break;
            default: return;
        }
    }
    public void LostParts(string partsType)
    {
        switch (partsType)
        {
            case "Gear": numGear -= ((numGear - 1 < 0) ? 0 : 1); break;
            case "Spring": numSpring -= ((numSpring - 1 < 0) ? 0 : 1); break;
            case "Battery": numBattery -= ((numBattery - 1 < 0) ? 0 : 1); break;
            default: return;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    [Header("Laser Anime")]
    public GameObject laserPrefab;

    [Header("stuff")]
    public bool isActive;

    public BatteryPedestal batteryPedestal;



    public GameObject laserBase;
    public GameObject laserHead;
    public GameObject laserSprites;

    private Vector3 spritesPos;

    private GameObject head;
    private DeathBox deathBox;
    private BoxCollider2D boxCollider;

    private Vector3 posBase;
    private Vector3 posHead;

    float dx;
    float dy;

    float distance;
    float direction;

    private void ValueUpdate()
    {
        posBase = laserBase.transform.position;
        posHead = laserHead.transform.position;

        dx = posHead.x - posBase.x;
        dy = posHead.y - posBase.y;

        distance = Mathf.Sqrt(Mathf.Pow(dx, 2) + Mathf.Pow(dy, 2));
        direction = Mathf.Atan(-dx / dy) * Mathf.Rad2Deg;


        //deathBox.transform.position = new Vector3(posBase.x + dx/2, posBase.y - dy/2, 1);
        deathBox.transform.position = new Vector3(posBase.x + dx / 2, posBase.y + dy / 2, 1);
    }

    private void Sprites()
    {
        float numSprite = distance / 0.16f;
        int numSpriteFloor = Mathf.FloorToInt(distance / 0.16f);
        float offset = 0.16f - (distance / 0.16f - numSpriteFloor) * 0.16f;

        Vector3 pos;
        SpriteRenderer temp;
        for (int i = 0; i < numSpriteFloor; i++)
        {
            pos = new Vector3(posBase.x, 0.06f + posBase.y + 0.16f * i, 0);
            temp = new SpriteRenderer();
            Instantiate(laserPrefab, pos, Quaternion.identity, laserSprites.transform);
        }

        pos = new Vector3(posBase.x, 0.06f + posBase.y + 0.16f * numSpriteFloor - offset, 0);
        temp = new SpriteRenderer();
        Instantiate(laserPrefab, pos, Quaternion.identity, laserSprites.transform);

        laserSprites.transform.RotateAround(posBase, new Vector3(0, 0, 1), direction);
    }

    // Start is called before the first frame update
    void Start()
    {
        head = transform.gameObject;
        deathBox = head.GetComponentInChildren<DeathBox>();
        boxCollider = deathBox.GetComponentInChildren<BoxCollider2D>();
        spritesPos = laserSprites.transform.position;

        ValueUpdate();

        boxCollider.size = new Vector2(0.1f, distance);
        //boxCollider.transform.position = new Vector3(posBase.x, posBase.y, 0);
        boxCollider.transform.RotateAround(deathBox.transform.position, new Vector3(0, 0, 1), direction);

        Sprites();
    }

    private void FixedUpdate()
    {
        if (batteryPedestal.IsPluggedBattery())
        {
            isActive = true;
        }
        else
        {
            isActive = false;
        }
        if (isActive)
        {
            laserSprites.SetActive(true);
            deathBox.gameObject.SetActive(true);
        }

        else
        {
            laserSprites.SetActive(false);
            deathBox.gameObject.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        //ValueUpdate();
    }
}

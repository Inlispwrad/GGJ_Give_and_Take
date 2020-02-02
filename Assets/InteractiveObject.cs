using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InteractiveObject : MonoBehaviour
{
    public Vector2 offset;
    public float triggerWidth;
    public float triggerHeight;
    private bool playerIsTouching = false;

    public List<RobotParts> partsRequired = new List<RobotParts>();
    public List<RobotParts> partsInserted = new List<RobotParts>();

    public KeyPressHint hintPrefab;
    private List<KeyPressHint> hints = new List<KeyPressHint>();

    public bool isActive = false;



    private BoxCollider2D triggerBox;
    private void Start()
    {
        triggerBox = this.gameObject.AddComponent<BoxCollider2D>();
        triggerBox.isTrigger = true;
        triggerBox.offset = offset;
        triggerBox.size = new Vector2(triggerWidth, triggerHeight);
    }

    private void FixedUpdate()
    {
        if (partsRequired.Count > 0)
        {
            isActive = false;
        }
        else
        {
            isActive = true;
        }
        if (playerIsTouching == true)
        {
            RespawnHints();
            PlayerController player = GameManager.GetPlayer();
            if (Input.GetAxis("Interact") > 0.1f)
            {
                if (partsRequired.Count > 0)
                {
                    if (player.GetNumOfParts(partsRequired[0].partsType) > 0)
                    {
                        player.LostParts(partsRequired[0].partsType);
                        partsInserted.Add(partsRequired[0]);
                        partsRequired.Remove(partsRequired[0]);
                    }
                }
            }
            if (Input.GetAxis("Take") > 0.1f)
            {
                if (partsInserted.Count > 0)
                {
                    if (player.GetNumOfParts(partsInserted[0].partsType) < 4)
                    {
                        if (partsInserted[0].partsType != "Battery" || (partsInserted[0].partsType == "Battery" && player.numBattery < 2))
                        {
                            player.AddParts(partsInserted[0].partsType);
                            partsRequired.Add(partsInserted[0]);
                            partsInserted.Remove(partsInserted[0]);
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            playerIsTouching = true;
            RespawnHints();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        playerIsTouching = true;
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
           
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("Player Out");
            for (int i = 0; i < hints.Count;)
            {
                GameObject temp = hints[i].gameObject;
                hints.Remove(hints[i]);
                Destroy(temp);
            }
            playerIsTouching = false;

        }
    }

    public void RespawnHints()
    {
        if (hints.Count > 0)
        {
            for (int i = 0; i < hints.Count;)
            {
                GameObject temp = hints[i].gameObject;
                hints.Remove(hints[i]);
                Destroy(temp);
            }
        }
        if (partsRequired.Count > 0)
        {
            for (int i = 0; i < partsRequired.Count; i++)
            {
                Vector3 offset = new Vector3(transform.position.x, transform.position.y - 0.20f, 1);
                KeyPressHint hint = Instantiate(hintPrefab, offset, Quaternion.identity, this.transform).GetComponent<KeyPressHint>();
                hint.isGive = true;
                hint.typeOfPart = partsRequired[i].partsType;
                hints.Add(hint);
            }
        }
        if (partsInserted.Count > 0)
        {
            for (int i = 0; i < partsInserted.Count; i++)
            {
                Vector3 offset = new Vector3(transform.position.x, transform.position.y - 0.20f, 1);
                KeyPressHint hint = Instantiate(hintPrefab, offset, Quaternion.identity, this.transform).GetComponent<KeyPressHint>();
                hint.isGive = false;
                hint.typeOfPart = partsInserted[i].partsType;
                hints.Add(hint);
            }
        }
    }
    public bool IsPlayerTouchingTrigger()
    {
        return playerIsTouching;
    }
}

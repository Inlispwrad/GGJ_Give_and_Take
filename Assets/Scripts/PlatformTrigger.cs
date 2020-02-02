using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTrigger : MonoBehaviour
{
    public Transform platform;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponentInParent<PlayerController>().gameObject.transform.SetParent(platform);
        collision.GetComponentInParent<PlayerController>().OnPlatform();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //if (collision.GetComponentInParent<PlayerControl>() == null)
        
        collision.GetComponentInParent<PlayerController>().gameObject.transform.SetParent(null);
        collision.GetComponentInParent<PlayerController>().OffPlatform();
    }
}

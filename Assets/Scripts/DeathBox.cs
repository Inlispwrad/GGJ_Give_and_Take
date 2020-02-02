using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBox : MonoBehaviour
{
    public Transform respawnPt;

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
        //Debug.Log("respawn");
        Transform player = collision.GetComponentInParent<Rigidbody2D>().gameObject.transform;
        Vector2 respawn;
        //respawn = Vector2.MoveTowards(player.TransformVector(player.position), respawnPt.position, Vector2.Distance(respawnPt.position, player.position));
        //respawn.x = respawn.x + player.position.x;
        //respawn.y = respawn.y + player.position.y;
        //player.position.x = respawnPt.position.x;

        //Debug.Log("Respawn Pt: " + respawn.x + " ," + respawn.y);
        Application.LoadLevel(Application.loadedLevel);

        respawn = player.InverseTransformPoint(respawnPt.position);
        player.Translate(respawn);
    }
}

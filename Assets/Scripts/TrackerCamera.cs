using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackerCamera : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset = Vector3.zero;
    public float t;

    private void Update()
    {
        Vector2 targetPos = Vector2.Lerp(this.transform.position, player.transform.position + offset, (1 / t) * Time.deltaTime);
        transform.position = new Vector3(targetPos.x, targetPos.y, -10);
    }
}

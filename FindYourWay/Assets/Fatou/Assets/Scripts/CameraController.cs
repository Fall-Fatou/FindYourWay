using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform player;

    private float yOffset = 4f;
    private float zOffset = -8f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;    
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y + yOffset, player.position.z + zOffset);
    }
}

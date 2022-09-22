using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    Transform playerPos;
    Vector3 camPos;
    // Start is called before the first frame update
    void Start()
    {
        playerPos = FindObjectOfType<Player>().transform;
        camPos = new Vector3(10, 4, 5);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerPos.position + camPos;
    }
}

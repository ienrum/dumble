using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCon : MonoBehaviour
{
    Transform playerPos;
    Player player;
    public GameObject enemyPrefab;
    float time = 0f;
    Vector3 camPos;

    float sign = 1;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerPos = player.gameObject.transform;
        camPos = new Vector3(10, 4, 7);
    }

    // Update is called once per frame
    void Update()
    {
        if(playerPos == null)
            playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        if(player == null)
            player = FindObjectOfType<Player>();

        followToPlayer(1);
        time += Time.deltaTime;

    }

    public void followToPlayer(int speed)
	{
        Vector3 temp2 = playerPos.position + new Vector3(10, 4, 7);

        transform.position = Vector3.Lerp(transform.position, temp2, Time.deltaTime * 3f * speed);
    }
}

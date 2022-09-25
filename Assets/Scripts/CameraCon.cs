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
        player = FindObjectOfType<Player>();
        playerPos = FindObjectOfType<Player>().transform;
        camPos = new Vector3(10, 4, 5);
    }

    // Update is called once per frame
    void Update()
    {
        if(playerPos == null)
            playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        if(player == null)
            player = FindObjectOfType<Player>();
        Vector3 temp2 = playerPos.position + new Vector3(10, 4, 5); 

        transform.position = Vector3.Lerp(transform.position, temp2, Time.deltaTime * 3f);
        
        time += Time.deltaTime;

        if (Enemy.dieCnt != 0 && time > 1.5f && !player.Died)
        {

            GameObject temp = Instantiate(enemyPrefab);
            Enemy.dieCnt -= 1;

            temp.transform.position = transform.GetChild(0).transform.position;
            time = 0;
        }
    }
}

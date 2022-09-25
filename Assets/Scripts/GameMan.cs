using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMan : MonoBehaviour
{
    GameObject insEnemy;
    GameObject insPlayer;
    public GameObject player;
    public GameObject floorZone;
    GameObject insFloorZone;
    public GameObject enemyZone;
    GameObject insEnemyZone;
    public GameObject enemy;
    GameObject inRoll;
    public GameObject roll;

    int first = 0;
    public Camera cam;

    public Material[] Mat;
    string[,] colorLis = new string[,]{
        { "937DC2", "C689C6" , "FFABE1", "FFE6F7" },
        { "F96666","674747","829460","EEEEEE"},
        { "FD841F","E14D2A","CD104D","9C2C77"},
        { "FFE9A0","367E18","F57328","CC3636"},
        {"B7C4CF","EEE3CB","D7C0AE","967E76" },
        {"1C6758","3D8361","D6CDA4","EEF2E6" },
        {"16213E","0F3460","533483","E94560" }
    };
    int[,] arr = new int[2, 3] { { 1, 2, 3 }, { 4, 5, 6 } };

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.3f;
        cam = GameObject.FindObjectOfType<Camera>();
        insFloorZone = GameObject.FindGameObjectWithTag("FloorZone");
        insEnemyZone = GameObject.FindGameObjectWithTag("EnemyZone");
        insPlayer = GameObject.FindGameObjectWithTag("Player");
        insEnemy = GameObject.FindGameObjectWithTag("Enemy");
        inRoll = GameObject.FindGameObjectWithTag("Roll");
        restart();
    }

    // Update is called once per frame
    void Update()
    {

        
            Time.timeScale = 1.3f;
        if (insFloorZone == null)
            insFloorZone = GameObject.FindGameObjectWithTag("FloorZone");
        if (insEnemyZone == null)
            insEnemyZone = GameObject.FindGameObjectWithTag("EnemyZone");
        if (insPlayer == null)
            insPlayer = GameObject.FindGameObjectWithTag("Player");
        if (insEnemy == null)
            insEnemy = GameObject.FindGameObjectWithTag("Enemy");
        if (inRoll == null)
            inRoll = GameObject.FindGameObjectWithTag("Roll");
        if (Input.GetKeyDown(KeyCode.R) || first < 1)
        {
            restart();
            first += 1;
        }
    }

    void restart()
	{
        enemyReset();
        rollReset();
        playerReset();
        mapReset();
        colorReset();
    }
    void rollReset()
	{
        Destroy(inRoll);
        Instantiate(roll);
	}
    void enemyReset()
	{
        Destroy(insEnemyZone);
        Instantiate(enemyZone);
    }

    void playerReset()
	{
        Destroy(insPlayer);
        
        Instantiate(player);
        
    }

    void mapReset()
	{
        Destroy(insFloorZone);
        
        Instantiate(floorZone);
    }

    void colorReset()
	{
        int rand = Random.Range(0, colorLis.GetLength(0));
        for (int i = 0; i < Mat.Length; i++)
		{
            Color color;
            ColorUtility.TryParseHtmlString("#"+colorLis[rand,i%4], out color);
            Mat[i].color = color;
            Camera.main.backgroundColor = color;
        }
    }

}

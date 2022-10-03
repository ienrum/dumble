using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    TextMeshProUGUI[] text = new TextMeshProUGUI[5];

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
        {"16213E","0F3460","533483","E94560" },
        {"222831","393E46","00ADB5","EEEEEE" },
        {"F9ED69","F08A5D","B83B5E","6A2C70" },
        {"F38181","FCE38A","EAFFD0","95E1D3" },
        {"08D9D6","252A34","FF2E63","EAEAEA" },
        {"364F6B","3FC1C9","F5F5F5","FC5185" },
        {"2B2E4A","E84545","903749","53354A" },
        {"212121","323232","0D7377","14FFEC" },
        {"E23E57","88304E","522546","311D3F" },
        {"F8EDE3","BDD2B6","A2B29F","798777" },
        {"00B8A9","F8F3D4","F6416C","FFDE7D" },
        {"2D4059","EA5455","F07B3F","FFD460" },
        {"98DDCA","D5ECC2","D5ECC2","FFAAA7" },
        {"999B84","D8AC9C","EFD9D1","F4EEED" },
        {"37E2D5","590696","C70A80","FBCB0A" },
        {"181818","8758FF","5CB8E4","F2F2F2" },
        {"411530","D1512D","F5C7A9","F5E8E4" },
        {"483838","42855B","90B77D","D2D79F" },

    };
    int[,] arr = new int[2, 3] { { 1, 2, 3 }, { 4, 5, 6 } };

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        
        cam = GameObject.FindObjectOfType<Camera>();
        insFloorZone = GameObject.FindGameObjectWithTag("FloorZone");
        insEnemyZone = GameObject.FindGameObjectWithTag("EnemyZone");
        insPlayer = GameObject.FindGameObjectWithTag("Player");
        insEnemy = GameObject.FindGameObjectWithTag("Enemy");
        inRoll = GameObject.FindGameObjectWithTag("Roll");

        text = FindObjectsOfType<TextMeshProUGUI>();
        restart();
    }

    // Update is called once per frame
    void Update()
    {
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

    public void restart()
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
        float maximum = -1;
        Color textColor = Color.white;
        int rand = Random.Range(0, colorLis.GetLength(0));
        for (int i = 0; i < Mat.Length; i++)
		{
            Color color;
            
            Color backColor;
            ColorUtility.TryParseHtmlString("#"+colorLis[rand,i%4], out color);
            
            ColorUtility.TryParseHtmlString("#" + colorLis[rand, 1], out backColor);

            Vector4 ok = new Vector4(color.r - backColor.r, color.g - backColor.g, color.b - backColor.b, color.a - backColor.a);
            Vector4 ok2 = new Vector4((color.r * Color.white.r - backColor.r), (color.g * Color.white.g - backColor.g), (color.b * Color.white.b - backColor.b), (color.a * Color.white.a - backColor.a));
            Vector4 ok3 = new Vector4((color.r * Color.black.r - backColor.r), (color.g * Color.black.g - backColor.g), (color.b * Color.black.b - backColor.b), (color.a * Color.black.a - backColor.a));

            maximum = Mathf.Max(maximum,ok2.magnitude);
            maximum = Mathf.Max(maximum, ok.magnitude);

            if (maximum < ok3.magnitude)
			{
                maximum = ok3.magnitude;
                textColor = color;
			}

            
            Mat[i].color = color;
            Camera.main.backgroundColor = backColor;
			foreach (var txt in text)
			{
                txt.color = textColor; 
			}
        }
    }

}

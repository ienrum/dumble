using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StartSc : MonoBehaviour
{
    bool first = true;
    Touch touch;
    GameMan gameMan;
    public int bestScore = 0;

    TextMeshProUGUI bestScoreText;

    // Start is called before the first frame update
    void Start()
    {
        bestScore = PlayerPrefs.GetInt("BestScore");

        bestScoreText = GameObject.FindGameObjectWithTag("BestScore").GetComponent<TextMeshProUGUI>();


        bestScoreText.text = "BEST " + bestScore;
        gameMan = FindObjectOfType<GameMan>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
            touch = Input.GetTouch(0);
        if ((touch.phase == TouchPhase.Ended || Input.GetAxisRaw("Jump") != 0) && first)
        {
            SceneManager.LoadScene("Game");
            first = false;
        }
    }
}

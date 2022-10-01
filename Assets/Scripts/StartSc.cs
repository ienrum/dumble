using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class StartSc : MonoBehaviour
{
    bool first = true;
    Touch touch;
    GameMan gameMan;
    public int bestScore = 0;
    public PlayStoreLeaderBoad restartButton;

    TextMeshProUGUI bestScoreText;

    // Start is called before the first frame update
    void Start()
    {
        bestScore = PlayerPrefs.GetInt("BestScore");

        bestScoreText = GameObject.FindGameObjectWithTag("BestScore").GetComponent<TextMeshProUGUI>();


        bestScoreText.text = "BEST " + bestScore;
        gameMan = FindObjectOfType<GameMan>();
    }
    public void touched()
	{
        SceneManager.LoadScene("Game");
    }
}

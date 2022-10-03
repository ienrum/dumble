using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    Rigidbody rigi;
    public bool ifOnTheFloor = false;
    public bool getIfOnTheFloor() { return ifOnTheFloor; }
    public void setIfOnTheFloor(bool arg) { ifOnTheFloor = arg; }

    public bool isJumping = false;
    public bool getIsJumping() { return isJumping; }
    public void setIsJumping(bool arg) { isJumping = arg; }

    const float jumpPower = 11.5f;
    const float flipPower = 250f;
    public float energy = 0;
    float prevScore = 0;

    Touch touch;
    // parent 해제후 놓을 공간
    public GameObject floorPrefab = null;

    public health health;

    TextMeshProUGUI text;
    TextMeshProUGUI bestText;
    TextMeshProUGUI bonusText;

    TextMeshProUGUI firstText1;
    TextMeshProUGUI firstText2;

    public GameObject restartUi;

    public GameObject bulletPrefab;

    static int bestScore_PP = 0;
    public int curScore = 0;
    bool isDied = false;
    float bonusTime = 0f;
    float flipTime = 0;

    float colorCnt = 0;

    public int bonusScore = 0;
    int resScore = 0;

    AudioSource dieSound;
    AudioSource healthSound;

    public bool getisDied() { return isDied; }
    public void setisDied(bool arg) { isDied = arg; }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.3f;

        rigi = GetComponent<Rigidbody>();
        text = FindObjectOfType<TextMeshProUGUI>();
        bestText = GameObject.FindGameObjectWithTag("BestScore").GetComponent<TextMeshProUGUI>();
        text = GameObject.FindGameObjectWithTag("Score").GetComponent<TextMeshProUGUI>();
        bonusText = GameObject.FindGameObjectWithTag("Bonus").GetComponent<TextMeshProUGUI>();
        firstText1 = GameObject.FindGameObjectWithTag("FirstText1").GetComponent<TextMeshProUGUI>();
        firstText2 = GameObject.FindGameObjectWithTag("FirstText2").GetComponent<TextMeshProUGUI>();
        healthSound = GameObject.FindGameObjectWithTag("HealthSound").GetComponent<AudioSource>();

        dieSound = GameObject.FindGameObjectWithTag("DieSound").GetComponent<AudioSource>();

        bestScore_PP = PlayerPrefs.GetInt("BestScore");
        setScoreToTextUIString(bestText, bestScore_PP, "BEST ");

        if(PlayerPrefs.GetInt("BestScore") < 3)
		{
            firstText1.enabled = true;
            firstText2.enabled = true;
		}
        //PlayerPrefs.SetInt("BestScore", 0);
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<MeshRenderer>().material.SetColor("_EmissionColor",Color.black);
        scoring();
        inputActing();
        isFlip();

        if (bonusScore != 0)
        {
            bonusText.text = "" + bonusScore;
            resScore += bonusScore;
            bonusScore = 0;
            StartCoroutine(FadeScore());
        }
        
    }
    IEnumerator FadeScore()
    {
        yield return new WaitForSeconds(1f);
        bonusText.text = "";
        
    }

    void scoring()
	{
        curScore = getCurScore();

        setScoreToTextUIString(text, curScore+resScore, "");

        if (bestScore_PP < curScore + resScore)
        {
            bestScore_PP = curScore + resScore;
            setScoreToTextUIString(bestText, bestScore_PP, "BEST ");
        }
        PlayerPrefs.SetInt("BestScore", bestScore_PP);
    }
    void inputActing()
	{
        if (!getisDied() && (isButtonClicked() || Input.touchCount > 0 || Input.GetAxisRaw("Horizontal") !=0))
        {
            if (getIfOnTheFloor())
                doJump();
            else 
                doFlip();
        }
    }
    int getCurScore()
	{
        return (int)Mathf.Floor(transform.position.z) - 5;
    }
    void setScoreToTextUIString(TextMeshProUGUI ui, int score,string preWord)
	{
        ui.text = preWord + score;
	}
    // 덤블링함수
    void doJump()
	{
        Vector3 jumpDir = (Vector3.up + transform.up).normalized;
        rigi.velocity = (jumpDir * jumpPower);
        setIfOnTheFloor(false);
        setIsJumping(true);
	}
    void doFlip()
	{
        if (touch.position.x > Screen.width / 2 || Input.GetAxisRaw("Horizontal") > 0)
		{
            transform.Rotate(Vector3.right * flipPower * Time.deltaTime);
            rigi.angularVelocity = Vector3.right * 2f;
        }
            
        else if (Input.GetAxisRaw("Horizontal") < 0 ||touch.position.x < Screen.width / 2)
		{
            transform.Rotate(-Vector3.right * flipPower * Time.deltaTime);
            rigi.angularVelocity = Vector3.right * -2f;
        }
    }

    void isFlip()
	{
        
        if(Input.touchCount > 0 || Input.GetAxisRaw("Horizontal") != 0)
		{
            flipTime += Time.deltaTime;
 
            if(flipTime > 0.07f)
			{
                GameObject temp = Instantiate(bulletPrefab);
                temp.transform.position = transform.position;
                temp.transform.rotation = transform.rotation;

                flipTime = 0;
            }
        }
    }
    bool isButtonClicked()
	{
        bool touchFlag = false;
        
        if (Input.touchCount > 0)
		{
            touch = Input.GetTouch(0);
            touchFlag = touch.phase == TouchPhase.Began;
        }
        bool ok = Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow);
        return ok || touchFlag;
    }
    // Die 함수 죽을때 주는 함수
    public void DoDie()
	{
        dieSound.Play();
        setIfOnTheFloor(false);
        health.cnt--;
        rigi.velocity = Vector3.up * 11;
        if (health.cnt != 0)
		{
            return;
		}
        setisDied(true);
        scattering();
        GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(2).gameObject.SetActive(true);
        firstText1.enabled = false;
        firstText2.enabled = false;
    }
    void scattering()
	{
        Collider[] ok = GetComponentsInChildren<Collider>();
        foreach (Collider o in ok)
        {
            o.isTrigger = false;
        }
        Rigidbody[] ok2 = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody o in ok2)
        {
            o.isKinematic = false;
            o.transform.parent = GameObject.FindGameObjectWithTag("EnemyZone").transform;
        }
    }
	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Door")
		{
            creatingNewFloor(other);
		}
        if (other.tag == "Heart")
            healthSound.Play();

    }
    void creatingNewFloor(Collider other)
	{
        GameObject temp = GameObject.Instantiate(floorPrefab);
        temp.transform.parent = GameObject.FindGameObjectWithTag("FloorZone").transform;
        temp.transform.position = other.transform.parent.GetChild(5).position;
        other.gameObject.GetComponent<BoxCollider>().enabled = false;
    }
    // 바닥에 닿으면 덤블링 가능하다.
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "Roll" || collision.gameObject.tag == "Obs")
        {
            setIfOnTheFloor(true);
            setIsJumping(false);
        }
    }
}

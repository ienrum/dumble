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

    const float jumpPower = 560f;
    const float flipPower = 100f;
    public float energy = 0;

    Touch touch;
    // parent 해제후 놓을 공간
    public GameObject floorPrefab = null;

    TextMeshProUGUI text;
    TextMeshProUGUI bestText;
    public GameObject restartUi;

    static int bestScore_PP = 0;
    int curScore = 0;
    bool isDied = false;

    public bool getisDied() { return isDied; }
    public void setisDied(bool arg) { isDied = arg; }

    // Start is called before the first frame update
    void Start()
    {
        rigi = GetComponent<Rigidbody>();
        text = FindObjectOfType<TextMeshProUGUI>();
        bestText = GameObject.FindGameObjectWithTag("BestScore").GetComponent<TextMeshProUGUI>();
        text = GameObject.FindGameObjectWithTag("Score").GetComponent<TextMeshProUGUI>();

        bestScore_PP = PlayerPrefs.GetInt("BestScore");
        setScoreToTextUIString(bestText, bestScore_PP, "BEST ");
    }

    // Update is called once per frame
    void Update()
    {
        scoring();
        inputActing();        
    }
	void scoring()
	{
        curScore = getCurScore();

        setScoreToTextUIString(text, curScore, "");

        if (bestScore_PP < curScore)
        {
            bestScore_PP = curScore;
            setScoreToTextUIString(bestText, bestScore_PP, "BEST ");
        }
        PlayerPrefs.SetInt("BestScore", bestScore_PP);
    }
    void inputActing()
	{
        if (!getisDied() && isButtonClicked())
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
        Vector3 jumpDir = (Vector3.up * 2 + transform.up).normalized;
        rigi.AddForce(jumpDir * jumpPower);
        setIfOnTheFloor(false);
        setIsJumping(true);
	}
    void doFlip()
	{
        if (touch.position.x > Screen.width / 2 || Input.GetKeyDown(KeyCode.RightArrow))
            rigi.AddTorque(transform.right * flipPower);
        else if(Input.GetKeyDown(KeyCode.LeftArrow))
            rigi.AddTorque(-transform.right * flipPower);
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
        setisDied(true);
        scattering();
        GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(2).gameObject.SetActive(true);
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

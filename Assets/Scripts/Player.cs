using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    Rigidbody rigi;
    public bool canDumbling = false;
    public bool jumping = false;
    float jumpPower = 560f;
    public float energy = 0;
    // parent 해제후 놓을 공간
    public Transform freeZone = null;
    public GameObject floorPrefab = null;
    public Transform floorFreeZone = null;

    TextMeshProUGUI text;
    TextMeshProUGUI bestText;

    static int bestScore = 0;
    int score = 0;
    public bool Died = false;

    AudioSource dieSound;
    AudioSource jumpSound;
    AudioSource landSound;
    // Start is called before the first frame update
    void Start()
    {
        
        rigi = GetComponent<Rigidbody>();
        text = FindObjectOfType<TextMeshProUGUI>();
        bestText = GameObject.FindGameObjectWithTag("BestScore").GetComponent<TextMeshProUGUI>();
        text = GameObject.FindGameObjectWithTag("Score").GetComponent<TextMeshProUGUI>();

        dieSound = GameObject.FindGameObjectWithTag("DieSound").GetComponent<AudioSource>();
        jumpSound = GameObject.FindGameObjectWithTag("JumpSound").GetComponent<AudioSource>();
        landSound = GameObject.FindGameObjectWithTag("LandSound").GetComponent<AudioSource>();

        bestScore = PlayerPrefs.GetInt("BestScore");
        bestText.text = "BEST "+bestScore;
    }

    // Update is called once per frame
    void Update()
    {
        score = ((int)Mathf.Floor(transform.position.z) - 5);
        text.text = "" + score;
        if (bestScore < score)
        {
            bestScore = score;
            bestText.text = "BEST " + bestScore;
        }
            
        if (!Died)
            Dumbling();
        PlayerPrefs.SetInt("BestScore", bestScore);
    }
    // 바닥에 닿으면 덤블링 가능하다.
	private void OnCollisionEnter(Collision collision)
	{
        if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "Roll" || collision.gameObject.tag == "Obs")
        {
            canDumbling = true;
            jumping = false;
            landSound.Play();
        }
    }
    // 덤블링함수
	void Dumbling()
	{
        Touch touch;
        // 바닥에 닿으면
        if (canDumbling)
		{
            float jumpFlag = Input.GetAxisRaw("Jump");

            // 버튼이 눌리면 에너지를 모으고

            if (jumpFlag > 0 || Input.touchCount > 0)
			{
                energy += Time.deltaTime / 2; 
                
            }
                
            else if((jumpFlag == 0 || Input.touchCount == 0)&& energy != 0) // 버튼이 떨어지면 에너지만큼 점프를 뛴다.
			{
                energy = Mathf.Clamp(energy, 0, 3) + 0.9f;
                Vector3 jumpDir = (Vector3.up * 2 + transform.up).normalized;
                rigi.AddForce(jumpDir * jumpPower * energy);
                rigi.AddTorque(Vector3.right * jumpPower * energy);
                energy = 0f;
                canDumbling = false;
                jumping = true;
                jumpSound.Play();
            }                
        }
	}
    // Die 함수 죽을때 주는 함수
    public void Die()
	{
        dieSound.Play();
        // 자식들의 parent 를 플레이어가 아닌곳으로 이동한후에 중력을 줘서 흩어지게함
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
        Died = true;
    }

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Door")
		{
            GameObject temp = GameObject.Instantiate(floorPrefab);
            temp.transform.parent = GameObject.FindGameObjectWithTag("FloorZone").transform;
            temp.transform.position = other.transform.parent.GetChild(5).position;
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
		}
	}
}

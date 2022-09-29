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
    // parent ������ ���� ����
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
    // �ٴڿ� ������ ���� �����ϴ�.
	private void OnCollisionEnter(Collision collision)
	{
        if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "Roll" || collision.gameObject.tag == "Obs")
        {
            canDumbling = true;
            jumping = false;
            landSound.Play();
        }
    }
    // �����Լ�
	void Dumbling()
	{
        Touch touch;
        // �ٴڿ� ������
        if (canDumbling)
		{
            float jumpFlag = Input.GetAxisRaw("Jump");

            // ��ư�� ������ �������� ������

            if (jumpFlag > 0 || Input.touchCount > 0)
			{
                energy += Time.deltaTime / 2; 
                
            }
                
            else if((jumpFlag == 0 || Input.touchCount == 0)&& energy != 0) // ��ư�� �������� ��������ŭ ������ �ڴ�.
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
    // Die �Լ� ������ �ִ� �Լ�
    public void Die()
	{
        dieSound.Play();
        // �ڽĵ��� parent �� �÷��̾ �ƴѰ����� �̵����Ŀ� �߷��� �༭ ���������
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody rigi;
    bool canDumbling = false;
    public bool jumping = false;
    float jumpPower = 560f;

    float dieTime = 0f;
    float time = 0f;
    public GameObject enemyPrefab;
    // parent 해제후 놓을 공간
    
    public bool Died = false;

    bool flag = false;
    Transform freeZone;
    public static int dieCnt = 0;
    Player player;
    Vector3 playerPos;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        rigi = GetComponent<Rigidbody>();
        freeZone = GameObject.FindGameObjectWithTag("EnemyZone").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Died)
            Dumbling();
        
        if(rigi.velocity.z >=0 && rigi.velocity.z < 0.2f)
		{
            dieTime += Time.deltaTime;
            if(dieTime > 2f && !Died)
                Die();
		}

        if (Input.GetKeyDown(KeyCode.R))
		{
            dieCnt = 0;
            Die();
        }
            

    }
    // 바닥에 닿으면 덤블링 가능하다.
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "Roll") 
        {
            canDumbling = true;
            jumping = false;
            flag = true;
        }
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().Die();
            
        }
    }
    // 덤블링함수
    void Dumbling()
    {
        // 바닥에 닿으면
        if (canDumbling)
        {
            
            if (transform.eulerAngles.x > 60 && transform.eulerAngles.x < 100)
			{
                Vector3 jumpDir = (Vector3.up * 2 + transform.up).normalized;
                rigi.AddForce(jumpDir * jumpPower * 0.9f);
                rigi.AddTorque(Vector3.right * jumpPower * 2f);
                canDumbling = false;
                jumping = true;
            }
        }
    }
    // Die 함수 죽을때 주는 함수
    public void Die()
    {
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
        dieCnt += 1;
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
	{

        yield return new WaitForSeconds(2f);
        enemyPrefab.SetActive(false);

    }
}

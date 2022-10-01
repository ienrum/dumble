using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool ifOnTheFloor = false;
    public bool getIfOnTheFloor() { return ifOnTheFloor; }
    public void setIfOnTheFloor(bool arg) { ifOnTheFloor = arg; }

    public bool died = false;

    AudioSource dieSound;
    int time = 0;
    protected Rigidbody rigi;
    float jumpForce = 300f;
    // Start is called before the first frame update
    protected void Start()
    {
        rigi = GetComponent<Rigidbody>();
        dieSound = GameObject.FindGameObjectWithTag("DieSound").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
        if(dieSound == null)
            dieSound = GameObject.FindGameObjectWithTag("DieSound").GetComponent<AudioSource>();
        time += (int)Time.deltaTime;
        if(time % 4 == 0 && getIfOnTheFloor() && !died)
		{
            doJump();
		}
    }
	private void OnCollisionEnter(Collision collision)
	{
        GameObject obj = collision.gameObject;
        if (obj.tag == "Floor")
        {
            setIfOnTheFloor(true);
        }
        else if (obj.tag == "Roll" || obj.tag == "Obs")
		{
            Vector3 flipAngleY = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y+180, transform.eulerAngles.z);
            transform.rotation = Quaternion.Euler(flipAngleY);
            setIfOnTheFloor(true);
        }
        else if(obj.tag == "Player" && !died)
		{
            obj.GetComponent<Player>().DoDie();
		}
    }
	public void doJump()
	{
        rigi.AddForce((Vector3.up + transform.forward * 0.3f).normalized * jumpForce * Random.Range(1f,2f));
        setIfOnTheFloor(false);
    }
    // Die 함수 죽을때 주는 함수
    public void Die(bool soundCheck =true)
    {
        died = true;
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
            o.useGravity = true;
            o.transform.parent = GameObject.FindGameObjectWithTag("EnemyZone").transform;
        }
        if(soundCheck)
            dieSound.Play();
        
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
	{
        yield return new WaitForSeconds(2f);
        Debug.Log(transform.childCount);
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        gameObject.SetActive(false);
    }

	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "SetFalseZone")
		{
            Die(false);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody rigi;
    bool canDumbling = false;
    float jumpPower = 560f;
    float energy = 0;
    // parent 해제후 놓을 공간
    public Transform freeZone = null;
    // Start is called before the first frame update
    void Start()
    {
        rigi = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Dumbling();
    }
    // 바닥에 닿으면 덤블링 가능하다.
	private void OnCollisionEnter(Collision collision)
	{
        if (collision.gameObject.tag == "Floor")
        {
            canDumbling = true;
        }
    }
    // 덤블링함수
	void Dumbling()
	{
        // 바닥에 닿으면
		if (canDumbling)
		{
            float jumpFlag = Input.GetAxisRaw("Jump");
            // 버튼이 눌리면 에너지를 모으고
            if (jumpFlag > 0)
                energy += Time.deltaTime;
            else if(jumpFlag == 0 && energy != 0) // 버튼이 떨어지면 에너지만큼 점프를 뛴다.
			{
                energy = Mathf.Clamp(energy, 0, 1) + 0.9f;
                Vector3 jumpDir = (Vector3.up * 2 + transform.up).normalized;
                rigi.AddForce(jumpDir * jumpPower * energy);
                rigi.AddTorque(Vector3.right * jumpPower * energy);
                energy = 0f;
                canDumbling = false;
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
            o.transform.parent = freeZone;
        }
    }
}

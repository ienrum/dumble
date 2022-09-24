using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody rigi;
    bool canDumbling = false;
    float jumpPower = 560f;
    float energy = 0;
    // parent ������ ���� ����
    public Transform freeZone = null;
    public GameObject floorPrefab = null;
    public Transform floorFreeZone = null;
    public Transform continuePos = null;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.3f;
        rigi = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Dumbling();
    }
    // �ٴڿ� ������ ���� �����ϴ�.
	private void OnCollisionEnter(Collision collision)
	{
        if (collision.gameObject.tag == "Floor")
        {
            canDumbling = true;
        }
    }
    // �����Լ�
	void Dumbling()
	{
        // �ٴڿ� ������
		if (canDumbling)
		{
            float jumpFlag = Input.GetAxisRaw("Jump");
            // ��ư�� ������ �������� ������
            if (jumpFlag > 0)
                energy += Time.deltaTime/2;
            else if(jumpFlag == 0 && energy != 0) // ��ư�� �������� ��������ŭ ������ �ڴ�.
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
    // Die �Լ� ������ �ִ� �Լ�
    public void Die()
	{
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
            o.transform.parent = freeZone;
        }
    }

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Door")
		{


            GameObject temp = GameObject.Instantiate(floorPrefab);
            temp.transform.parent = floorFreeZone;
            temp.transform.position = other.transform.parent.GetChild(5).position;
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
		}
	}
}

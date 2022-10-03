using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
	public GameObject bulletPrefab;
    // Start is called before the first frame update
    void Start()
    {
		StartCoroutine(ToTrigger());
    }

	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player" && tag != "item1")
		{
			for(int i = 0; i <= 360; i+=8)
			{
				GameObject temp = Instantiate(bulletPrefab);
				temp.transform.position = gameObject.transform.position;
				temp.transform.Rotate(Vector3.right * i);
					
			}
			gameObject.SetActive(false);
		}
		else if (other.gameObject.tag == "Player" && tag == "item1")
		{
			for (int i = 0; i <= 360; i += 72)
			{
				GameObject temp = Instantiate(bulletPrefab);
				temp.transform.position = GameObject.FindGameObjectWithTag("SheildZone").transform.position;
				temp.transform.Rotate(Vector3.right * i);
				temp.transform.parent = GameObject.FindGameObjectWithTag("SheildZone").transform;

			}
			gameObject.SetActive(false);
			
		}
	}
	IEnumerator ToTrigger()
	{
		yield return new WaitForSeconds(0.5f);
		GetComponent<Collider>().isTrigger = true;
		GetComponent<Rigidbody>().isKinematic = true;
	}
}

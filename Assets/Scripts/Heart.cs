using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    health health;
	private void Start()
	{
		health = FindObjectOfType<health>();
		StartCoroutine(ToTrigger());
	}
	// Update is called once per frame
	void Update()
    {
        transform.Rotate(Vector3.up * 10);
    }

	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			health.cnt = 3;
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

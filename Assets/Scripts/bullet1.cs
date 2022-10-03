using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet1 : MonoBehaviour
{
	public void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Enemy")
		{
			gameObject.SetActive(false);
		}
	}
}

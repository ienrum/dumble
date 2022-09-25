using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHead : MonoBehaviour
{
	Enemy player;
	// Start is called before the first frame update
	void Start()
	{
		player = transform.parent.GetComponent<Enemy>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Floor")
		{
			player.Die();
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHead : MonoBehaviour
{
	Enemy enemy;
	// Start is called before the first frame update
	void Start()
	{
		enemy = transform.parent.GetComponent<Enemy>();
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Player" && !enemy.died)
		{
			GetComponent<Collider>().enabled = false;
			enemy.Die();
		}
	}
}

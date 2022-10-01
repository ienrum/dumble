using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : Enemy
{
	float speed = 0.1f;

	private void Update()
	{
		goForward();
	}
	void goForward()
	{
		transform.Translate(Vector3.forward * Random.Range(0.07f,0.1f));
	}

	private void OnCollisionEnter(Collision collision)
	{
		GameObject obj = collision.gameObject;

		if (obj.tag == "Player" && !died)
		{
			obj.GetComponent<Player>().DoDie();
		}
	}
}

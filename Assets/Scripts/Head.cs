using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = transform.parent.GetComponent<Player>();
    }

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Floor")
		{
			player.Die();
		}
	}
}

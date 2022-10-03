using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheildZone : MonoBehaviour
{
	GameObject playerObj;
	int prevCount = 0;
	private void Start()
	{
		playerObj = FindObjectOfType<Player>().gameObject;
	}
	// Update is called once per frame
	void Update()
    {
		if(playerObj == null)
			playerObj = FindObjectOfType<Player>().gameObject;
		transform.position = playerObj.transform.position;
        transform.Rotate(Vector3.right * 10f);

		if(transform.childCount != prevCount)
		{
			for (int i = prevCount; i < transform.childCount; i++)
			{
				Transform tempTr = transform.GetChild(i).transform;
				tempTr.Translate(tempTr.forward * 2.5f);
			}
			prevCount = transform.childCount;
		}
    }
}

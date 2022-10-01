using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAni : MonoBehaviour
{
    Player player;
    bool rotatingOk = true;
    Rigidbody rigi;
    int rand;
    int[] lis = { -1, 1 };

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        rigi = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
		if (player.getIsJumping())
		{
            //transform.Rotate(Vector3.up * lis[rand]);
            rotatingOk = true;
        }
		else if(rotatingOk)
		{
            //transform.rotation = Quaternion.Euler(Vector3.up * lis[rand]);
            rigi.angularVelocity = rigi.angularVelocity * 0f;
            rotatingOk = false;
            rand = Random.Range(0, 2);
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAni : MonoBehaviour
{
    Enemy player;
    bool rotatingOk = true;
    Rigidbody rigi;

    float rand;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Enemy>();
        rigi = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.jumping)
        { 
            transform.Rotate(Vector3.up);

            rotatingOk = true;
        }
        else if (rotatingOk)
        {
            transform.rotation = Quaternion.Euler(Vector3.zero);
            rigi.angularVelocity = rigi.angularVelocity * 0.3f;
            rotatingOk = false;
            rand = Random.Range(-1f, 1f);
        }

    }
}

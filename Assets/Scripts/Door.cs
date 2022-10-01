using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform[] Poses;

    public GameObject obs;
    public GameObject enemy;
    public GameObject flyingEnemy;
    public Transform ObsZone;
    // Start is called before the first frame update
    void Start()
    {
        int posRand = Random.Range(0, 4);
        int sizeRand = Random.Range(1, 3);

        GameObject tempEnemy = GameObject.Instantiate(enemy);
        GameObject temp = GameObject.Instantiate(obs);
        GameObject tempFlyingEnemy = GameObject.Instantiate(flyingEnemy);

        tempEnemy.transform.position = Poses[posRand].position;
        tempEnemy.transform.parent = GameObject.FindGameObjectWithTag("FloorZone").transform;

        tempFlyingEnemy.transform.position = Poses[posRand].position + Vector3.up * Random.Range(4,8);
        tempFlyingEnemy.transform.parent = GameObject.FindGameObjectWithTag("FloorZone").transform;

        temp.transform.localScale *= sizeRand;
        temp.transform.position = Poses[posRand].position;
        temp.transform.parent = GameObject.FindGameObjectWithTag("FloorZone").transform;
    }
}

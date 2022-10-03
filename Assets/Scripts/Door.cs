using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    static int cnt = 0;
    public Transform[] Poses;

    public GameObject obs;
    public GameObject enemy;
    public GameObject flyingEnemy;
    public GameObject ItemPrefab;
    public Transform ObsZone;
    public GameObject item1Prefab;
    public GameObject heartPrefab;
    // Start is called before the first frame update
    void Start()
    {
        cnt += 1;
        int posRand = Random.Range(0, 4);
        int sizeRand = Random.Range(1, 3);
        int cntRand = Random.Range(1, 2);

        
        GameObject temp = GameObject.Instantiate(obs);
        GameObject tempFlyingEnemy = GameObject.Instantiate(flyingEnemy);
        

        for(int i = 1; i <= cntRand; i++)
		{
            float sizeRand2 = Random.Range(1, 2.1f);
            GameObject tempEnemy = GameObject.Instantiate(enemy);
            tempEnemy.transform.position = Poses[posRand].position + Vector3.forward * i * -1f + Vector3.up * 2f;
            tempEnemy.transform.parent = GameObject.FindGameObjectWithTag("FloorZone").transform;
            tempEnemy.transform.localScale *= sizeRand2;
        }

        tempFlyingEnemy.transform.position = Poses[posRand].position + Vector3.up * Random.Range(4,8);
        tempFlyingEnemy.transform.parent = GameObject.FindGameObjectWithTag("FloorZone").transform;

        temp.transform.localScale *= sizeRand;
        temp.transform.position = Poses[posRand].position;
        temp.transform.parent = GameObject.FindGameObjectWithTag("FloorZone").transform;

        if(cnt % 2 == 0)
		{
            int posRand2 = Random.Range(0, 4);
            float rand = Random.Range(1, 5);
            GameObject tempItem = GameObject.Instantiate(ItemPrefab);
            tempItem.transform.position = Poses[(posRand2 + 3) % 4].position + Vector3.forward * 3 + Vector3.up * rand;
            tempItem.transform.parent = GameObject.FindGameObjectWithTag("FloorZone").transform;
        }

        if ((cnt+1) % 5 == 0)
        {
            int posRand2 = Random.Range(0, 4);
            float rand = Random.Range(1, 5);
            GameObject tempItem = GameObject.Instantiate(item1Prefab);
            tempItem.transform.position = Poses[(posRand2 + 1) % 4].position + Vector3.forward * 3 + Vector3.up * rand;
            tempItem.transform.parent = GameObject.FindGameObjectWithTag("FloorZone").transform;
        }

        if ((cnt + 2) % 7 == 0)
        {
            int posRand2 = Random.Range(0, 4);
            float rand = Random.Range(1, 5);
            GameObject tempItem = GameObject.Instantiate(heartPrefab);
            tempItem.transform.position = Poses[(posRand2 + 2) % 4].position + Vector3.forward * 3 + Vector3.up * rand;
            tempItem.transform.parent = GameObject.FindGameObjectWithTag("FloorZone").transform;
        }

    }
}

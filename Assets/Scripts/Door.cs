using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform[] Poses;

    public GameObject obs;
    public Transform ObsZone;
    // Start is called before the first frame update
    void Start()
    {
        int rand = Random.Range(0, 4);

        GameObject temp = GameObject.Instantiate(obs);
        temp.transform.position = Poses[rand].position;
        temp.transform.parent = ObsZone;
    }
}

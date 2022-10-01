using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyZoneClear : MonoBehaviour
{
    float time = 0;
    int prev = 0;
    // Update is called once per frame
    void Update()
    {

        if (prev != transform.childCount)
        {
            time += Time.deltaTime;
            if (time > 3)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    if (transform.GetChild(i).gameObject.activeSelf)
                        transform.GetChild(i).gameObject.SetActive(false);
                }
                prev = transform.childCount;
            }
        }
        else
            time = 0;


    }
}

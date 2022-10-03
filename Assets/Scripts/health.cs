using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class health : MonoBehaviour
{
    Transform playerTr;
    public int cnt = 3;
    int prev = 3;
    // Start is called before the first frame update
    void Start()
    {
        playerTr = FindObjectOfType<Player>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (null == playerTr)
            playerTr = FindObjectOfType<Player>().transform;
        transform.position = playerTr.position + new Vector3(0,2,0);

        if(cnt < prev)
		{
            transform.GetChild(cnt).gameObject.SetActive(false);
            prev = cnt;
		}
        else if(cnt > prev)
		{
            for(int i=prev;i<cnt;i++)
                transform.GetChild(i).gameObject.SetActive(true);
            prev = cnt;
		}
    }
}

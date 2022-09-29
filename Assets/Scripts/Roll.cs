using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roll : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

	private void OnCollisionEnter(Collision collision)
	{
        if (collision.gameObject.tag == "Obs")
            StartCoroutine(Fade());
    }


    IEnumerator Fade()
    {
        yield return new WaitForSeconds(2f);
        Transform resetTrans = FindObjectOfType<Camera>().transform.GetChild(0);
        gameObject.transform.position = resetTrans.position;
        gameObject.transform.rotation = Quaternion.Euler(new Vector3(0,0,90));
    }
}

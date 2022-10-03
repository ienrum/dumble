using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    Player playerScript;
    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(Fade());
    }
	private void Update()
	{
        if(playerScript == null)
		{
            playerScript = FindObjectOfType<Player>();
		}
        transform.Translate(transform.forward * 0.4f);
    }
    IEnumerator Fade()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
	public void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Enemy")
		{
            gameObject.SetActive(false);
		}
	}


}

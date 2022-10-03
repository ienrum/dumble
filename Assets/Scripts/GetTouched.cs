using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GetTouched : MonoBehaviour
{
    Button button;
    
    CameraCon cameraCon;
    AdMobAdsManager adMobAdsManager;

    

    // Start is called before the first frame update
    void Start()
    {
        button = FindObjectOfType<Button>();
        
        cameraCon = FindObjectOfType<CameraCon>();
        adMobAdsManager = FindObjectOfType<AdMobAdsManager>();
    }

	private void Update()
	{
        if (Input.GetKeyDown(KeyCode.R))
            toMainScene();
	}

	public void toMainScene()
	{
        SceneManager.LoadScene("Start");
    }
}

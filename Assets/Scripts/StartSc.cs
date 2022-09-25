using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSc : MonoBehaviour
{
    bool first = true;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Jump") != 0 && first)
        {
            SceneManager.LoadScene("Game");
            first = false;
        }
    }
}

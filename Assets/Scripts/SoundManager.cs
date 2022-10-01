using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public GameObject dieSoundPrefab;
    public GameObject jumpSoundPrefab;
    public GameObject landSoundPrefab;

    AudioSource dieSound;
    AudioSource jumpSound;
    AudioSource landSound;

    Player playerScript;

    bool didAlreadyLandSoundPlaying = false;
    bool getDidAlreadyLandSoundPlaying() { return didAlreadyLandSoundPlaying; }
    void setDidAlreadyLandSoundPlaying(bool arg) { didAlreadyLandSoundPlaying = arg; }

    bool didAlreadyJumpSoundPlaying = false;
    bool getDidAlreadyJumpSoundPlaying() { return didAlreadyJumpSoundPlaying; }
    void setDidAlreadyJumpSoundPlaying(bool arg) { didAlreadyJumpSoundPlaying = arg; }

    bool didAlreadyDieSoundPlaying = false;
    bool getDidAlreadyDieSoundPlaying() { return didAlreadyDieSoundPlaying; }
    void setDidAlreadyDieSoundPlaying(bool arg) { didAlreadyDieSoundPlaying = arg; }
	// Start is called before the first frame update

	void Start()
    {
        dieSound = Instantiate(dieSoundPrefab).GetComponent<AudioSource>();
        jumpSound = Instantiate(jumpSoundPrefab).GetComponent<AudioSource>();
        landSound = Instantiate(landSoundPrefab).GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerScript == null)
            playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (!playerScript.getIsJumping() && !getDidAlreadyLandSoundPlaying())
		{
            landSound.Play();
            Debug.Log("Land");
            setDidAlreadyLandSoundPlaying(true);
            setDidAlreadyJumpSoundPlaying(false);
            
        }
		if (playerScript.getisDied() && !getDidAlreadyDieSoundPlaying())
        {
            dieSound.Play();
            Debug.Log("Died");
            setDidAlreadyDieSoundPlaying(true);
        }
        if(playerScript.getIsJumping() && !getDidAlreadyJumpSoundPlaying())
		{
            jumpSound.Play();
            Debug.Log("Jump");
            setDidAlreadyJumpSoundPlaying(true);
            setDidAlreadyLandSoundPlaying(false);
        }
    }
}

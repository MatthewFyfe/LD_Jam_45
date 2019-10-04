using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainGameLoop : MonoBehaviour
{
	public Canvas mainMenuScreen;
	public AudioSource cameraMusic;

    // Start is called before the first frame update
    void Start()
    {
    	// Hide menu at start (for some reason hiding it in editor breaks stuff)
        mainMenuScreen.enabled = false;
        // Enable time and music if it was somehow disabled at scene start
        Time.timeScale = 1;
        cameraMusic.Play();

    }

    // Update is called once per frame (independent of timeScale)
    void Update()
    {
  		if (Input.GetKeyDown("space"))
        {
            print("space key was pressed");
        }

        // Toggle menu UI and pause / unpause game physics
        if (Input.GetKeyDown("escape"))
        {
            mainMenuScreen.enabled = (mainMenuScreen.enabled == false);
         	
         	if(Time.timeScale == 1)
         	{
         		Time.timeScale = 0;
         		cameraMusic.Pause();
         	}
         	else
         	{
         		Time.timeScale = 1;
         		cameraMusic.Play();
         	}
        }
    }

    // Physics updates (frozen while timeScale == 0)
    void FixedUpdate()
    {

    }

}

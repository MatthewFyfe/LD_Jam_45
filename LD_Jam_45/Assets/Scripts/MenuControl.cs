using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
	public Canvas mainMenuScreen;
    public GameObject mainGameLoop;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Unpause the game and continue
    public void Continue()
    {
		//Debug.Log("Continue");
        
    }

    // Reset the game to initial configuration
    public void Restart()
    {
		//Debug.Log("Restart");
		SceneManager.LoadScene("FirstScene", LoadSceneMode.Single);
    }

    // Quit the game
    public void Exit()
    {
    	Application.Quit();
    	//Debug.Log("Exiting game...");
    }
}

using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour 
{
    public bool isPaused = false;
    public string levelToBeLoaded;

	void Start ()
    {
	
	}
	
	void Update () 
    {
	    if(Input.GetKeyDown(KeyCode.P))
        {
            isPaused = !isPaused;
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1;
            Application.LoadLevel(levelToBeLoaded);
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 1;
            Application.LoadLevel("Level_Select");
        }

        if (isPaused)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
	}
}

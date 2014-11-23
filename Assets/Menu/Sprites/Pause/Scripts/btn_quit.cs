using UnityEngine;
using System.Collections;

public class btn_quit : MonoBehaviour {

    public string levelLoadQuit;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	    
	}

    void OnClick()
    {
        //print("quit");
        Time.timeScale = 1;
        Application.LoadLevel(levelLoadQuit);
    }
}

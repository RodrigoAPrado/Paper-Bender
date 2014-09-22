using UnityEngine;
using System.Collections;

public class button_Quit : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnClick()
    {
        Application.Quit();
    }
}

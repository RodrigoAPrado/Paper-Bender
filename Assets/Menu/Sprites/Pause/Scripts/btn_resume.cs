using UnityEngine;
using System.Collections;

public class btn_resume : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnClick()
    {
        //print("resume");
        Time.timeScale = 1;
        GameObject.Destroy(transform.parent.gameObject);
    }
}

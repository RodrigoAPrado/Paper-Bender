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
        GameObject.Instantiate(Resources.Load("UI Button Pause") as GameObject, new Vector3(9999, 0, 0), new Quaternion(0, 0, 0, 0));
        GameObject.Destroy(transform.parent.gameObject);
    }
}

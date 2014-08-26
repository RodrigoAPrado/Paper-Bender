using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {


	Transform character;
	// Use this for initialization
	void Start () {
		character = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter2D(Collider2D hit)
	{
		Camera.main.transform.parent = character;
	}
	void OnTriggerExit2D(Collider2D hit)
	{
		Camera.main.transform.parent = null;
	}

}

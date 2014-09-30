using UnityEngine;
using System.Collections;

public class NextStage : MonoBehaviour {

	public string nextStage;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnMouseDown()
	{
		print ("NextStage");
		Application.LoadLevel(nextStage);
	}
}

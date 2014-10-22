using UnityEngine;
using System.Collections;

public class PaperBallActivate : MonoBehaviour {

	public GameObject paper;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(paper.activeSelf)
		{
			GetComponent<Rigidbody2D>().isKinematic = false;
		}
	}
}

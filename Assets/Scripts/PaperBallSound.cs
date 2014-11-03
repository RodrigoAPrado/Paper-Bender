using UnityEngine;
using System.Collections;

public class PaperBallSound : MonoBehaviour {

	float volume;
	float value;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(rigidbody2D.velocity.x > 0.5 || rigidbody2D.velocity.x < -0.5)
		{
			volume = 1;
			
			audio.volume = volume;
			audio.Play();
		}
		else
		{
			audio.volume = 0;
			audio.Stop();
		}
	
	}
}

using UnityEngine;
using System.Collections;

public class PaperBallSound : MonoBehaviour {

	float volume;
	float value;

    bool isRolling = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(rigidbody2D.velocity.x > 0.5 || rigidbody2D.velocity.x < -0.5)
		{
			volume = 0.5f;
			
			audio.volume = volume;

            if(!isRolling)
            {
    			audio.Play();
                isRolling = true;
            }
		}
		else
		{
			audio.volume = 0;
			audio.Stop();

            isRolling = false;
		}
	
	}
}

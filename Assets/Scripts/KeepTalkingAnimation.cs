using UnityEngine;
using System.Collections;

public class KeepTalkingAnimation : MonoBehaviour {

	public Texture[] frames;
	float timer;
	public float maxTimer;
	int currentFrame;
	GUITexture gTexture;
	// Use this for initialization
	void Start () {
		gTexture = GetComponent<GUITexture>();
	}

	void OnEnable()
	{
		currentFrame = 0;
	}
	// Update is called once per frame
	void Update () {
		gTexture.texture = frames[currentFrame];

		timer += Time.deltaTime;
		if(timer > maxTimer)
		{
			timer = 0;
			currentFrame ++;
			if(currentFrame >= frames.Length)
			{
				currentFrame = 0;
			}
		}
	}
}

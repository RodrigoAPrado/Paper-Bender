using UnityEngine;
using System.Collections;

public class PaperDurexChecker : MonoBehaviour {

	public bool durexOn;
	public GameObject durexSpriteBended;
	public GameObject durexSpriteUnbended;
	GameObject paperBended;
	GameObject paperUnbended;
	// Use this for initialization
	void Start () {
		paperBended = transform.FindChild("PaperBended").gameObject;
		paperUnbended = transform.FindChild("PaperUnbended").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if(paperBended.activeSelf)
		{
			if(durexOn)
				durexSpriteBended.SetActive(true);
			else
				durexSpriteBended.SetActive(false);
		}
		else
		{
			durexSpriteBended.SetActive(false);
		}
		if(paperUnbended.activeSelf)
		{
			if(durexOn)
				durexSpriteUnbended.SetActive(true);
			else
				durexSpriteUnbended.SetActive(false);
		}
		else
		{
			durexSpriteUnbended.SetActive(false);
		}
	}
}

using UnityEngine;
using System.Collections;

public class NPCDialogBox : MonoBehaviour {

	GUITexture gTexture;
	GUIText gText;
	int currentLetter;
	public float timerEnd;
	float timer;
	public enum NPCBoxType
	{
		Text,
		Box,
		Button,
		Avatar
	}
	string sTest = "What is this?\nWhat is a little villager doing 'round here?"; 
	string fullString = "What is this?\nWhat is a little villager doing 'round here?";
	string displayString = "";
	/*+
		"\nYou know you're gonna cook inside those \nfancy clothes of yours, right?";*/
	public NPCBoxType boxType = NPCBoxType.Box;

	// Use this for initialization
	void Start () {

		if(boxType == NPCBoxType.Text)
			gText = GetComponent<GUIText>();
		else
			gTexture = GetComponent<GUITexture>();

	}
	
	// Update is called once per frame
	void Update () {
		switch(boxType)
		{
			case NPCBoxType.Box:
				gTexture.pixelInset = new Rect((-Screen.width/2) + (Screen.width/30), (-Screen.height/2), Screen.width - (Screen.width/15), (Screen.height/5));
			break;
			case NPCBoxType.Avatar:
				gTexture.pixelInset = new Rect((-Screen.width/2) + (Screen.width/16), (-Screen.height/2) + Screen.height/18, Screen.width/7, Screen.width/7);
			break;
			case NPCBoxType.Text:
			//gText.pixelOffset = new Vector2((-Screen.width/2) + (Screen.width/4),(-Screen.height/2) + (Screen.height/4.6f));
			gText.pixelOffset = new Vector2((-Screen.width/2) + (Screen.width/4),(-Screen.height/2) + (Screen.height/6));
				gText.text = displayString;
				gText.fontSize = (int) Screen.width/28;
			break;
		}
		LetterAppearance();
	}
	void LetterAppearance()
	{
		if(currentLetter < fullString.Length)
		{
			displayString = fullString.Remove(currentLetter);
			timer += Time.deltaTime;
			if(timer > timerEnd)
			{
				timer = 0;
				currentLetter ++;
			}
		}
		else
		{
			displayString = fullString;
		}
	}
}

using UnityEngine;
using System.Collections;

public class NPCDialogController : MonoBehaviour {

	GUITexture gTextureBox;
	GUITexture gTextureAvatar;
	GUIText gText;
	int currentLetter;
	public float timerEnd;
	float timer;
	string fullString = "What is this?\nWhat is a little villager doing 'round here?";
	string displayString = "";
	public bool onChat;
	/*+
		"\nYou know you're gonna cook inside those \nfancy clothes of yours, right?";*/

	// Use this for initialization
	void Start () {

		gText = transform.FindChild("Text").gameObject.GetComponent<GUIText>();
		gTextureAvatar = transform.FindChild("Avatar").gameObject.GetComponent<GUITexture>();
		gTextureBox = transform.FindChild("Box").gameObject.GetComponent<GUITexture>();
	}
	
	// Update is called once per frame
	void Update () {
		Chatting();
		if(onChat)
		{
			gText.gameObject.SetActive(true);
			gTextureBox.gameObject.SetActive(true);
			gTextureAvatar.gameObject.SetActive(true);
			LetterAppearance();
		}
		else
		{
			gText.gameObject.SetActive(false);
			gTextureBox.gameObject.SetActive(false);
			gTextureAvatar.gameObject.SetActive(false);
			displayString = fullString.Remove(0);
			currentLetter = 0;
			timer = 0;
		}
	}
	void Chatting()
	{
		gTextureBox.pixelInset = new Rect((-Screen.width/2) + (Screen.width/30), (-Screen.height/2), Screen.width - (Screen.width/15), (Screen.height/5));
		gTextureAvatar.pixelInset = new Rect((-Screen.width/2) + (Screen.width/16), (-Screen.height/2) + Screen.height/18, Screen.width/7, Screen.width/7);
		gText.pixelOffset = new Vector2((-Screen.width/2) + (Screen.width/4),(-Screen.height/2) + (Screen.height/6));
		gText.text = displayString;
		gText.fontSize = (int) Screen.width/28;
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

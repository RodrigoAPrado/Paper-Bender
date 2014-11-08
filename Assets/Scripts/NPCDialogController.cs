using UnityEngine;
using System.Collections;

public class NPCDialogController : MonoBehaviour {

	GUITexture gTextureBox;
	GUITexture gTextureAvatar;
	GUIText gText;
	int currentLetter;
	public float timerEnd;
	float timer;
	int currentLine;
	string[] dialogueScript;
	string fullString = "";
	string displayString = "";
	public bool onChat;
	public Texture[] avatarImage;
	public enum allAvatar
	{
		Abutre,
		Coruja,
		Corvo,
		Xerife,
		Aldeao
	}
	public allAvatar thisAvatar = allAvatar.Abutre;
	/*+
		"\nYou know you're gonna cook inside those \nfancy clothes of yours, right?";*/

	// Use this for initialization
	void Start () {
		dialogueScript = new string[0];
		currentLine = 0;
		gText = transform.FindChild("Text").gameObject.GetComponent<GUIText>();
		gTextureAvatar = transform.FindChild("Avatar").gameObject.GetComponent<GUITexture>();
		gTextureBox = transform.FindChild("Box").gameObject.GetComponent<GUITexture>();
	}
	
	// Update is called once per frame
	void Update () {
		Chatting();
		if(onChat)
		{
			CheckAvatarImage();
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
			if(fullString.Length > 0)
			{
				displayString = fullString.Remove(0);
			}
			currentLetter = 0;
			currentLine = 0;
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
	void CheckAvatarImage()
	{
		switch(thisAvatar)
		{
		case allAvatar.Abutre:
			gTextureAvatar.texture = avatarImage[0];
			break;
		case allAvatar.Coruja:
			gTextureAvatar.texture = avatarImage[1];
			break;
		case allAvatar.Corvo:
			gTextureAvatar.texture = avatarImage[2];
			break;
		case allAvatar.Xerife:
			gTextureAvatar.texture = avatarImage[3];
			break;
		case allAvatar.Aldeao:
			gTextureAvatar.texture = avatarImage[4];
			break;
		}
	}
	public void SetDialogueScript(string[] dialogue, allAvatar dialogueAvatar)
	{
		dialogueScript = new string[dialogue.Length];
		for(int i = 0; i < dialogue.Length; i++)
		{
			dialogueScript[i] = dialogue[i];
		}
		currentLine = 0;
		thisAvatar = dialogueAvatar;
		onChat = true;
	}
	void LetterAppearance()
	{
		
		if(currentLine >= dialogueScript.Length)
		{
			onChat = false;
			return;
		}
		fullString = dialogueScript[currentLine];
		if(Input.GetMouseButtonDown(0))
		{
			if(gTextureBox.HitTest(Input.mousePosition))
			{
				if(currentLetter < fullString.Length)
				{
					currentLetter = fullString.Length;
				}
				else
				{
					currentLine ++;
					currentLetter = 0;
					return;
				}
			}
		}
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

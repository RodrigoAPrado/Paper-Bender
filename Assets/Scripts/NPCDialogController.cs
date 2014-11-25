using UnityEngine;
using System.Collections;

public class NPCDialogController : MonoBehaviour {

	GUITexture gTextureBox;
	GUITexture gTextureAvatar;
	GUITexture gTextureKeepTalking;
	GUITexture gTextureEndTalking;
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
	AudioClip[] avatarClip;
	float lastVolume;
	bool volumeMixed = false;
	public enum allAvatar
	{
		Abutre,
		Coruja,
		Corvo,
		Xerife,
		Aldeao,
		Gigante
	}
	public allAvatar thisAvatar = allAvatar.Abutre;
	allAvatar[] avatarLineUp;
	/*+
		"\nYou know you're gonna cook inside those \nfancy clothes of yours, right?";*/

	// Use this for initialization
	void Start () {
		dialogueScript = new string[0];
		avatarLineUp = new allAvatar[0];
		avatarClip = new AudioClip[0];
		currentLine = 0;
		gText = transform.FindChild("Text").gameObject.GetComponent<GUIText>();
		gTextureAvatar = transform.FindChild("Avatar").gameObject.GetComponent<GUITexture>();
		gTextureBox = transform.FindChild("Box").gameObject.GetComponent<GUITexture>();
		gTextureKeepTalking = transform.FindChild("KeepTalking").gameObject.GetComponent<GUITexture>();
		gTextureEndTalking = transform.FindChild("EndTalking").gameObject.GetComponent<GUITexture>();
	}
	
	// Update is called once per frame
	void Update () {
		Chatting();
		if(onChat)
		{
            if(!volumeMixed)
            {
				lastVolume = FindObjectOfType<SoundManager>().maxVol;
				if(lastVolume >= 0.1)
					FindObjectOfType<SoundManager>().ChangeVolume(0.1f);
                Debug.Log("VAI SOM");
                Debug.Log(lastVolume);
				volumeMixed = true;
            }

			CheckAvatarImage();
			gText.gameObject.SetActive(true);
			gTextureBox.gameObject.SetActive(true);
			gTextureAvatar.gameObject.SetActive(true);
			if(currentLetter == fullString.Length && currentLine < dialogueScript.Length - 1)
				gTextureKeepTalking.gameObject.SetActive(true);
			else
				gTextureKeepTalking.gameObject.SetActive(false);
			if(currentLetter == fullString.Length && currentLine >= dialogueScript.Length - 1)
				gTextureEndTalking.gameObject.SetActive(true);
			else
				gTextureEndTalking.gameObject.SetActive(false);
			LetterAppearance();
		}
		else
		{
			volumeMixed = false;
			gText.gameObject.SetActive(false);
			gTextureBox.gameObject.SetActive(false);
			gTextureAvatar.gameObject.SetActive(false);
			gTextureKeepTalking.gameObject.SetActive(false);
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
		gTextureKeepTalking.pixelInset = new Rect((Screen.width/2) - (Screen.width/8), (-Screen.height/2) + Screen.height/25, Screen.width/23, Screen.width/23);
		gTextureEndTalking.pixelInset = new Rect((Screen.width/2) - (Screen.width/8), (-Screen.height/2) + Screen.height/25, Screen.width/23, Screen.width/23);
		gText.pixelOffset = new Vector2((-Screen.width/2) + (Screen.width/4),(-Screen.height/2) + (Screen.height/5.5f));
		gText.text = displayString;
		gText.fontSize = (int) Screen.width/29;
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
		case allAvatar.Gigante:
			gTextureAvatar.texture = avatarImage[5];
			break;
		}
	}
	public void SetDialogueScript(string[] dialogue, allAvatar[] dialogueAvatar, AudioClip[] dialogueClips)
	{
		dialogueScript = new string[dialogue.Length];
		for(int i = 0; i < dialogue.Length; i++)
		{
			dialogueScript[i] = dialogue[i];
		}
		avatarLineUp = new allAvatar[dialogueAvatar.Length];
		for(int j = 0; j < dialogue.Length; j++)
		{
			avatarLineUp[j] = dialogueAvatar[j];
		}
		avatarClip = new AudioClip[dialogueClips.Length];
		for(int k = 0; k < dialogueClips.Length; k++)
		{
			avatarClip[k]= dialogueClips[k];
		}
		print (dialogueAvatar[0]);
		currentLine = 0;
		fullString = dialogueScript[currentLine];
		thisAvatar = avatarLineUp[currentLine];
		audio.PlayOneShot(avatarClip[currentLine]);
		onChat = true;
	}
	void LetterAppearance()
	{
		
		if(currentLine >= dialogueScript.Length)
		{
			onChat = false;
            Debug.Log("VOLTA SOM");
            Debug.Log(lastVolume);
			FindObjectOfType<SoundManager>().ChangeVolume(lastVolume);

			return;
		}
		fullString = dialogueScript[currentLine];
		thisAvatar = avatarLineUp[currentLine];
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
					audio.Stop();
					currentLine ++;
					if(currentLine < avatarClip.Length)
					{	
						audio.PlayOneShot(avatarClip[currentLine]);
					}
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

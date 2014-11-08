using UnityEngine;
using System.Collections;

public class StartTalk : MonoBehaviour {

	NPCDialogController nPC;
	public string[] thisText;
	public NPCDialogController.allAvatar thisAvatar;
	// Use this for initialization
	void Start () {
		nPC = GameObject.FindGameObjectWithTag("ChatBox").GetComponent<NPCDialogController>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void SetTalkOn()
	{
		nPC.SetDialogueScript(thisText, thisAvatar);
	}
}

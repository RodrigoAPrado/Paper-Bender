using UnityEngine;
using System.Collections;

public class StartTalk : MonoBehaviour {

	NPCDialogController nPC;
	public string[] thisText;
	public NPCDialogController.allAvatar[] thisAvatar;
	SpriteRenderer sRenderer;
	BoxCollider2D bCollider;
	// Use this for initialization
	void Start () {
		nPC = GameObject.FindGameObjectWithTag("ChatBox").GetComponent<NPCDialogController>();
		bCollider = GetComponent<BoxCollider2D>();
		sRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(nPC.onChat)
		{
			bCollider.enabled = false;
			sRenderer.enabled = false;
		}
		else
		{
			bCollider.enabled = true;
			sRenderer.enabled = true;
		}
	}
	public void SetTalkOn()
	{
		nPC.SetDialogueScript(thisText, thisAvatar);
	}
}

using UnityEngine;
using System.Collections;

public class StartTalk : MonoBehaviour {

	NPCDialogController nPC;
	public string[] thisText;
	public NPCDialogController.allAvatar[] thisAvatar;
	public AudioClip[] thisAudioClips;
	SpriteRenderer sRenderer;
	BoxCollider2D bCollider;
	public GameObject nPCAvatar;
	public LayerMask player;
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
			if(nPCAvatar != null)
			{
				nPCAvatar.GetComponent<Animator>().SetBool("Talk", true);
			}
			bCollider.enabled = false;
			sRenderer.enabled = false;
		}
		else
		{
			if(nPCAvatar != null)
			{
				nPCAvatar.GetComponent<Animator>().SetBool("Talk", false);
			}
			if(!Physics2D.OverlapCircle(transform.position, 5, player))
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
	}
	public void SetTalkOn()
	{
		nPC.SetDialogueScript(thisText, thisAvatar, thisAudioClips);
	}
}

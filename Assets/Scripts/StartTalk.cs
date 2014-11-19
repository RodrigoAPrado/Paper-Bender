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
	Transform playerNPC;
	public bool ignoreScale;
	int scale;
	float nPCAvatarScale;
	// Use this for initialization
	void Start () {
		scale = 1;
		if(nPCAvatar != null)
		{
			if(nPCAvatar.transform.localScale.x < 0)
				nPCAvatarScale = -nPCAvatar.transform.localScale.x;
			else
				nPCAvatarScale = nPCAvatar.transform.localScale.x;
		}
		playerNPC = GameObject.FindGameObjectWithTag("Player").transform;
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
				if(nPCAvatar.transform.position.x >= playerNPC.position.x)
				{
					if(scale == 1)
					{
						scale = -1;
					}
				}
				if(nPCAvatar.transform.position.x < playerNPC.position.x)
				{
					if(scale == -1)
					{
						scale = 1;
					}
				}
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
		if(nPCAvatar != null)
		{
			nPCAvatar.transform.localScale = new Vector2(nPCAvatarScale * scale, nPCAvatar.transform.localScale.y);
		}
	}
	public void SetTalkOn()
	{
		nPC.SetDialogueScript(thisText, thisAvatar, thisAudioClips);
	}
}

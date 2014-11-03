using UnityEngine;
using System.Collections;

public class SwitchController : MonoBehaviour {
	
	public Sprite[] sprites;
	public DoorController door;
	public bool rotateAPlataform;
	public Transform right;
	public Transform left;
	bool locked = true;
	bool wasLocked;
	bool playerSound;
	bool check;
	public LayerMask weights;
	public bool forceOpen;
	public SeesawController sC;
	
	public AudioClip switchOn;
	public AudioClip switchOff;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
		locked = !Physics2D.OverlapArea(new Vector2(left.position.x, left.position.y), new Vector2(right.position.x, right.position.y), weights);
		if(!check)
		{
			wasLocked = locked;
			playerSound = true;
			check = true;
		}
		if(wasLocked != locked)
		{
			playerSound = false;
			wasLocked = locked;
		}
		if(!playerSound)
		{
			audio.PlayOneShot(switchOn, 1);
			playerSound = true;
		}
		if(locked && !forceOpen)
		{
			GetComponent<SpriteRenderer>().sprite = sprites[0];

			if(!rotateAPlataform)
			{
				if(!door.doorOpen)
					return;
				door.doorOpen = false;
				door.moving = true;
				return;
			}
		}
		else
		{
			GetComponent<SpriteRenderer>().sprite = sprites[1];

			if(!rotateAPlataform)
			{
				if(door.doorOpen)
					return;
				door.doorOpen = true;
				door.moving = true;
				return;
			}
		}
		sC.rotate = !locked;
	}
}

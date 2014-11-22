using UnityEngine;
using System.Collections;

public class AtlasTempleDoor : MonoBehaviour {

	public Transform entranceLeft;
	public Transform entranceRight;
	public Transform exitLeft;
	public Transform exitRight;
	public LayerMask player;
	public DoorController door;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(!door.doorOpen)
		{
			if(Physics2D.OverlapArea(entranceLeft.position, entranceRight.position, player))
			{
				door.doorOpen = true;
				door.moving = true;
			}
		}
		else
		{
			if(Physics2D.OverlapArea(exitLeft.position, exitRight.position, player))
			{
				door.doorOpen = false;
				door.moving = true;
			}
		}
	}
}

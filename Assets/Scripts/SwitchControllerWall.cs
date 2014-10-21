using UnityEngine;
using System.Collections;

public class SwitchControllerWall : MonoBehaviour {

	public DoorController door;
	public bool rotateAPlataform;
	bool locked = true;
	public GameObject weight;
	public bool forceOpen;
	public SeesawController sC;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		locked = !weight.activeSelf;
		if(locked && !forceOpen)
		{
			
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

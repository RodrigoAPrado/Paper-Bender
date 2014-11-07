using UnityEngine;
using System.Collections;

public class TemplePuzzle : MonoBehaviour {


	public GameObject[] puzzle;
	public DoorController door;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < puzzle.Length; i++)
		{

			if(!puzzle[i].activeSelf)
			{
				if(!door.doorOpen)
					return;
				door.doorOpen = false;
				door.moving = true;
				return;
			}
		}
		if(door.doorOpen)
			return;
		door.doorOpen = true;
		door.moving = true;
	}
}

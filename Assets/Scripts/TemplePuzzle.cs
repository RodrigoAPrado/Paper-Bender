using UnityEngine;
using System.Collections;

public class TemplePuzzle : MonoBehaviour {


	public GameObject[] puzzle;
	public GameObject door;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < puzzle.Length; i++)
		{

			if(!puzzle[i].activeSelf)
			{
				door.SetActive(true);
				return;
			}
		}
		door.SetActive(false);
	}
}

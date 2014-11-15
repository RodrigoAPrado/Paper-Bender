using UnityEngine;
using System.Collections;

public class FileManager : MonoBehaviour {
	
	public GameObject[] fileEmpty;
	public GameObject[] fileSave;

	// Use this for initialization
	void Start () {
		for(int i = 0; i <= 2; i++)
		{
			int index = i+1;
			fileSave[i].SetActive(ES2.Load<bool>("file" + index.ToString() + ".txt?tag=init"));
			fileEmpty[i].SetActive(!ES2.Load<bool>("file" + index.ToString() + ".txt?tag=init"));
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

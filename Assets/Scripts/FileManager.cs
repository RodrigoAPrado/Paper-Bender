using UnityEngine;
using System.Collections;

public class FileManager : MonoBehaviour {
	
	public GameObject[] fileEmpty;
	public GameObject[] fileSave;
	public GameObject[] fileErase;
	public int chosenSave;

	// Use this for initialization
	void Start () {
		CheckFiles();
	}
	
	// Update is called once per frame
	public void CheckFiles()
	{
		for(int i = 0; i <= 2; i++)
		{
			int index = i+1;
			print (ES2.Load<bool>("file" + index.ToString() + ".txt?tag=init"));
			fileSave[i].SetActive(ES2.Load<bool>("file" + index.ToString() + ".txt?tag=init"));
			fileEmpty[i].SetActive(!ES2.Load<bool>("file" + index.ToString() + ".txt?tag=init"));
		}
	}
	public void CheckErase()
	{
		for(int i = 0; i <= 2; i++)
		{
			int index = i+1;
			print (ES2.Load<bool>("file" + index.ToString() + ".txt?tag=init"));
			fileErase[i].SetActive(ES2.Load<bool>("file" + index.ToString() + ".txt?tag=init"));
		}
	}
}

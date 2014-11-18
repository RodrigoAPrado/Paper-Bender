using UnityEngine;
using System.Collections;

public class StageLoader : MonoBehaviour {

	int stageLoader;
	[SerializeField]string[] scenesToLoad;
	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(gameObject);
		stageLoader = ES2.Load<int>("currentSave.txt");
		int currentProgress = ES2.Load<int>("file" + stageLoader.ToString() + ".txt?tag=gProgEvent");
		if(currentProgress == 0)
		{
			currentProgress = 1;
		}
		switch(currentProgress)
		{
			default:
			print ("Current is " + currentProgress.ToString());
			Application.LoadLevel(scenesToLoad[0]);
			break;
			case 1:
			print ("Current is " + currentProgress.ToString());
			Application.LoadLevel(scenesToLoad[1]);
			break;
			case 2:
			print ("Current is " + currentProgress.ToString());
			Application.LoadLevel(scenesToLoad[2]);
			break;
			case 3:
			print ("Changed back to 2");
			ES2.Save(2, "file" + stageLoader.ToString() + ".txt?tag=gProgEvent");
			Application.LoadLevel(scenesToLoad[2]);
			break;

		}
	}
	
	// Update is called once per frame
}

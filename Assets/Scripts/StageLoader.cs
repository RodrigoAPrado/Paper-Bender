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
			print (ES2.Load<int>("file" + stageLoader.ToString() + ".txt?tag=curW"));
			//print ("Error");

			if(currentProgress == 10 || currentProgress == 11)
			{
				ES2.Save(9, "file" + stageLoader.ToString() + ".txt?tag=gProgEvent");
			}
			print ("Current is " + currentProgress.ToString());
			print (ES2.Load <int>("file" + stageLoader.ToString() + ".txt?tag=uestA"));
			Application.LoadLevel(scenesToLoad[9 + ES2.Load<int>("file" + stageLoader.ToString() + ".txt?tag=curW")]);
			break;
			case 0:
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
		case 4:
			print ("Current is " + currentProgress.ToString());
			Application.LoadLevel(scenesToLoad[4]);
			break;
		case 5:
			print ("Current is " + currentProgress.ToString());
			Application.LoadLevel(scenesToLoad[5]);	
			break;
		case 6:
			print ("Current is " + currentProgress.ToString());
			Application.LoadLevel(scenesToLoad[6]);	
			break;
		case 7:
			print ("Current is " + currentProgress.ToString());
			Application.LoadLevel(scenesToLoad[7]);	
		break;
		case 8:
			print ("Changed back to 7");
			ES2.Save(7, "file" + stageLoader.ToString() + ".txt?tag=gProgEvent");
			Application.LoadLevel(scenesToLoad[7]);
			break;
		}
	}
	
	// Update is called once per frame
}

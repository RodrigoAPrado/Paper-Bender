using UnityEngine;
using System.Collections;

public class SaveController : MonoBehaviour {

	public string variable;
	public GameObject[] gameObjectsToActivate;
	public GameObject[] gameObjectsToDeactivate;
	/*public*/ int uestProgress;
	// Use this for initialization
	void Start () {
		int stageLoader = ES2.Load<int>("currentSave.txt");
		int currentProgress = ES2.Load<int>("file" + stageLoader.ToString() + ".txt?tag=gProgEvent");
		//int 
		uestProgress = ES2.Load<int>("file" + stageLoader.ToString() + ".txt?tag=" + variable);
		if(variable == "uesB")
		{
			switch(uestProgress)
			{
			case 0:

				break;
			case 1:
				gameObjectsToActivate[0].SetActive(true);
				break;
			case 2:
				gameObjectsToActivate[0].SetActive(true);
				break;
			case 3:
				gameObjectsToActivate[0].SetActive(true);
				gameObjectsToActivate[1].SetActive(true);
				break;
			case 4:
				gameObjectsToActivate[0].SetActive(true);
				break;
			case 5:
				gameObjectsToActivate[0].SetActive(true);
				gameObjectsToActivate[1].SetActive(true);
				break;
			case 6:
				gameObjectsToActivate[0].SetActive(true);
				gameObjectsToActivate[1].SetActive(true);
				break;
			case 7:
				gameObjectsToActivate[0].SetActive(true);
				gameObjectsToActivate[1].SetActive(true);
				gameObjectsToActivate[2].SetActive(true);
				if(currentProgress <= 13)
				{
					ES2.Save(14, "file" + stageLoader.ToString() + ".txt?tag=gProgEvent");
				}
				break;

			}
			return;
		}
		switch(uestProgress)
		{
		case 0:
			break;
		case 1:
			gameObjectsToActivate[0].SetActive(true);
			gameObjectsToActivate[1].SetActive(true);
			gameObjectsToDeactivate[0].SetActive(false);
			break;
		case 2:
			gameObjectsToActivate[2].SetActive(true);
			gameObjectsToActivate[3].SetActive(true);
			gameObjectsToDeactivate[1].SetActive(false);
			break;
		case 3:
			gameObjectsToDeactivate[0].SetActive(false);
			gameObjectsToDeactivate[1].SetActive(false);
			gameObjectsToDeactivate[3].SetActive(false);
			gameObjectsToActivate[6].SetActive(true);
			gameObjectsToActivate[7].SetActive(true);
			gameObjectsToActivate[8].SetActive(true);
			gameObjectsToActivate[9].SetActive(true);
			gameObjectsToActivate[10].SetActive(true);
			if(currentProgress <= 12)
			{
				ES2.Save(13, "file" + stageLoader.ToString() + ".txt?tag=gProgEvent");
			}
			break;
		case 4:
			gameObjectsToActivate[4].SetActive(true);
			gameObjectsToActivate[5].SetActive(true);
			gameObjectsToDeactivate[2].SetActive(false);
			break;
		case 5:
			gameObjectsToDeactivate[0].SetActive(false);
			gameObjectsToDeactivate[2].SetActive(false);
			gameObjectsToDeactivate[3].SetActive(false);
			gameObjectsToActivate[6].SetActive(true);
			gameObjectsToActivate[7].SetActive(true);
			gameObjectsToActivate[8].SetActive(true);
			gameObjectsToActivate[11].SetActive(true);
			gameObjectsToActivate[12].SetActive(true);
			if(currentProgress <= 12)
			{
				ES2.Save(13, "file" + stageLoader.ToString() + ".txt?tag=gProgEvent");
			}
			break;
		case 6:
			gameObjectsToDeactivate[1].SetActive(false);
			gameObjectsToDeactivate[2].SetActive(false);
			gameObjectsToDeactivate[3].SetActive(false);
			gameObjectsToActivate[6].SetActive(true);
			gameObjectsToActivate[9].SetActive(true);
			gameObjectsToActivate[10].SetActive(true);
			gameObjectsToActivate[11].SetActive(true);
			gameObjectsToActivate[12].SetActive(true);
			if(currentProgress <= 12)
			{
				ES2.Save(13, "file" + stageLoader.ToString() + ".txt?tag=gProgEvent");
			}
			break;
		case 7:
			gameObjectsToDeactivate[0].SetActive(false);
			gameObjectsToDeactivate[1].SetActive(false);
			gameObjectsToDeactivate[2].SetActive(false);
			gameObjectsToDeactivate[3].SetActive(false);
			gameObjectsToActivate[6].SetActive(true);
			gameObjectsToActivate[7].SetActive(true);
			gameObjectsToActivate[8].SetActive(true);
			gameObjectsToActivate[9].SetActive(true);
			gameObjectsToActivate[10].SetActive(true);
			gameObjectsToActivate[11].SetActive(true);
			gameObjectsToActivate[12].SetActive(true);
			if(currentProgress <= 12)
			{
				ES2.Save(13, "file" + stageLoader.ToString() + ".txt?tag=gProgEvent");
			}
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

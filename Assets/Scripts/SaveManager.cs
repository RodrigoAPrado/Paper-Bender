using UnityEngine;
using System.Collections;

public class SaveManager : MonoBehaviour {

	//File 1
	bool initiate_1;
	int currentWorld_1;
	int gameProgress_Event_1;
	int gameProgress_Stages_1;
	int uestleinA_1;
	int uestleinB_1;

	//File 2
	bool initiate_2;
	int currentWorld_2;
	int gameProgress_Event_2;
	int gameProgress_Stages_2;
	int uestleinA_2;
	int uestleinB_2;

	//File 3
	bool initiate_3;
	int currentWorld_3;
	int gameProgress_Event_3;
	int gameProgress_Stages_3;
	int uestleinA_3;
	int uestleinB_3;

	//Manager
	int currentSave;

	// Use this for initialization
	void Start () {
		for(int i = 1; i <= 3; i++)
		{
			string fileName = "file" + i.ToString() + ".txt";
			if(ES2.Exists(fileName))
			{
				//LoadSave(fileName, i);
			}
			else
			{
				CreateSave(fileName, i);
			}
		}
	}
	
	// Update is called once per frame
	/*void LoadSave(string fileName, int identifier)
	{
		switch(identifier)
		{
		case 1:
			initiate_1 = ES2.Load<bool>(fileName + "?tag=init");
			currentWorld_1 = ES2.Load<int>(fileName + "?tag=curW");
			gameProgress_Event_1 = ES2.Load<int>(fileName + "?tag=gProgEvent");
			gameProgress_Stages_1 = ES2.Load<int>(fileName + "?tag=gProgStages");
			uestleinA_1 = ES2.Load<int>(fileName + "?tag=uestA");
			uestleinB_1 = ES2.Load<int>(fileName + "?tag=uesB");
			break;
		case 2:
			initiate_2 = ES2.Load<bool>(fileName + "?tag=init");
			currentWorld_2 = ES2.Load<int>(fileName + "?tag=curW");
			gameProgress_Event_2 = ES2.Load<int>(fileName + "?tag=gProgEvent");
			gameProgress_Stages_2 = ES2.Load<int>(fileName + "?tag=gProgStages");
			uestleinA_2 = ES2.Load<int>(fileName + "?tag=uestA");
			uestleinB_2 = ES2.Load<int>(fileName + "?tag=uesB");
			break;
		case 3:
			initiate_3 = ES2.Load<bool>(fileName + "?tag=init");
			currentWorld_3 = ES2.Load<int>(fileName + "?tag=curW");
			gameProgress_Event_3 = ES2.Load<int>(fileName + "?tag=gProgEvent");
			gameProgress_Stages_3 = ES2.Load<int>(fileName + "?tag=gProgStages");
			uestleinA_3 = ES2.Load<int>(fileName + "?tag=uestA");
			uestleinB_3 = ES2.Load<int>(fileName + "?tag=uesB");
			break;
		}

	}*/
	void CreateSave(string fileName, int identifier)
	{
		print ("ok");
		ES2.Save(false, fileName + "?tag=init");
		ES2.Save(0, fileName + "?tag=curW");
		ES2.Save(0, fileName + "?tag=gProgEvent");
		ES2.Save(0, fileName + "?tag=gProgStages");
		ES2.Save(0, fileName + "?tag=uestA");
		ES2.Save(0, fileName + "?tag=uesB");
		//LoadSave(fileName, identifier);
	}
}

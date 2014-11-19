using UnityEngine;
using System.Collections;

public class SaveLoad : MonoBehaviour {

	public int thisSave;
	FileManager fM;
	UILabel world;
	UILabel percent;
	GameObject LoadScreen;
	GameObject FilesScreen;
	GameObject CreateScreen;
	GameObject EraseScreen;
	GameObject ConfirmEraseScreen;
	int maxStages = 10;
	public bool load;
	// Use this for initialization
	void Start () {
		fM = GameObject.Find ("FileManager").gameObject.GetComponent<FileManager>();
		world = transform.FindChild("Save").transform.FindChild("World").GetComponent<UILabel>();
		percent = transform.FindChild("Save").transform.FindChild("Percent").GetComponent<UILabel>();
		int i = ES2.Load<int>("file" + thisSave.ToString() + ".txt?tag=gProgStages");
		percent.text = ((i*100)/maxStages).ToString() + "%";
		int j = ES2.Load<int>("file" + thisSave.ToString() + ".txt?tag=curW");
		switch(j)
		{
		case 0:
			world.text = "Tempou";
			break;
		case 1:
			world.text = "Greslein";
			break;
		case 2:
			world.text = "Uestlein";
			break;
		case 3:
			world.text = "Mauntein";
			break;
		case 4:
			world.text = "Suampelein";
			break;
		}
		LoadScreen = transform.parent.transform.parent.FindChild("Confirm").gameObject;
		FilesScreen = transform.parent.transform.parent.FindChild("Files").gameObject;
		CreateScreen = transform.parent.transform.parent.FindChild("Create").gameObject;
		EraseScreen = transform.parent.transform.parent.FindChild("Erase").gameObject;
		ConfirmEraseScreen = transform.parent.transform.parent.FindChild("ConfirmErase").gameObject;
	}
	
	// Update is called once per frame
	void OnClick()
	{
		print (ES2.Load<int>("file" + thisSave.ToString() + ".txt?tag=gProgStages"));
		
		print (ES2.Load<int>("file" + thisSave.ToString() + ".txt?tag=gProgEvent"));
		if(load)
		{
	        if(ES2.Load<bool>("file" + thisSave.ToString() + ".txt?tag=init"))
			{
				fM.chosenSave = thisSave;
				FilesScreen.SetActive(false);
				LoadScreen.SetActive(true);
			}
			else
			{
				fM.chosenSave = thisSave;
				FilesScreen.SetActive(false);
				CreateScreen.SetActive(true);
				/*
				print ("ok");
				ES2.Save(true,"file" + thisSave.ToString() + ".txt?tag=init");
				ES2.Save(0,"file" + thisSave.ToString() + ".txt?tag=curW");
				ES2.Save(0,"file" + thisSave.ToString() + ".txt?tag=gProgEvent");
				ES2.Save(0,"file" + thisSave.ToString() + ".txt?tag=gProgStages");
				ES2.Save(0,"file" + thisSave.ToString() + ".txt?tag=uestA");
				ES2.Save(0,"file" + thisSave.ToString() + ".txt?tag=uesB");
				*/
			}
		}
		else
		{
			fM.chosenSave = thisSave;
			EraseScreen.SetActive(false);
			ConfirmEraseScreen.SetActive(true);
		}
		//fM.CheckFiles();
	}
}

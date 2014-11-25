using UnityEngine;
using System.Collections;

public class ConfirmSave : MonoBehaviour {

	
	GameObject LoadScreen;
	GameObject FilesScreen;
	GameObject CreateScreen;
	GameObject EraseScreen;
	GameObject ConfirmEraseScreen;
	FileManager fM;
	public enum saveButtons
	{
		ConfirmCreate,
		CancelCreate,
		ConfirmLoad,
		CancelLoad,
		EnterErase,
		ExitErase,
		ConfirmErase,
		CancelErase
	}
	public saveButtons buttonType;
	// Use this for initialization
	void Start () {
		fM = GameObject.Find ("FileManager").gameObject.GetComponent<FileManager>();
		LoadScreen = GameObject.Find("Anchor").transform.FindChild("Confirm").gameObject;
        FilesScreen = GameObject.Find("Anchor").transform.FindChild("Files").gameObject;
        CreateScreen = GameObject.Find("Anchor").transform.FindChild("Create").gameObject;
        EraseScreen = GameObject.Find("Anchor").transform.FindChild("Erase").gameObject;
        ConfirmEraseScreen = GameObject.Find("Anchor").transform.FindChild("ConfirmErase").gameObject;
        //ConfirmEraseScreen = transform.parent.transform.parent.FindChild("ConfirmErase").gameObject;
	}
	
	// Update is called once per frame
	void OnClick()
	{
		switch(buttonType)
		{
		case saveButtons.ConfirmCreate:
			ES2.Save(true,"file" + fM.chosenSave.ToString() + ".txt?tag=init");
			ES2.Save(0,"file" + fM.chosenSave.ToString() + ".txt?tag=curW");
			ES2.Save(0,"file" + fM.chosenSave.ToString() + ".txt?tag=gProgEvent");
			ES2.Save(0,"file" + fM.chosenSave.ToString() + ".txt?tag=gProgStages");
			ES2.Save(0,"file" + fM.chosenSave.ToString() + ".txt?tag=uestA");
			ES2.Save(0,"file" + fM.chosenSave.ToString() + ".txt?tag=uesB");
			fM.CheckFiles();
			print ("GameLoaded");
			ES2.Save(fM.chosenSave,"currentSave.txt");
			ES2.Save(0.1f, "musicTime.txt");
            Application.LoadLevel("StageLoader");
			break;
		case saveButtons.CancelCreate:
			FilesScreen.SetActive(true);
			CreateScreen.SetActive(false);
			break;
		case saveButtons.ConfirmLoad:
			ES2.Save(fM.chosenSave,"currentSave.txt");
			Application.LoadLevel("StageLoader");
			break;
		case saveButtons.CancelLoad:
			FilesScreen.SetActive(true);
			LoadScreen.SetActive(false);
			break;
		case saveButtons.EnterErase:
			FilesScreen.SetActive(false);
			EraseScreen.SetActive(true);
			fM.CheckErase();
		break;
		case saveButtons.ExitErase:
			FilesScreen.SetActive(true);
			EraseScreen.SetActive(false);
			fM.CheckFiles();
		break;
		case saveButtons.ConfirmErase:
			ES2.Save(false,"file" + fM.chosenSave.ToString() + ".txt?tag=init");
			ConfirmEraseScreen.SetActive(false);
			EraseScreen.SetActive(true);
			fM.CheckErase();
			break;
		case saveButtons.CancelErase:
			ConfirmEraseScreen.SetActive(false);
			EraseScreen.SetActive(true);
			break;
		}
	}
}

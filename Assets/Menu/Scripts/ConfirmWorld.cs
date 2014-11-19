using UnityEngine;
using System.Collections;

public class ConfirmWorld : MonoBehaviour {

	
	
	GameObject levelSelect;
	GameObject confirmScreen;
	FileManager fM;
	WorldManager wM;
	public enum worldButtons
	{
		Confirm,
		Cancel
	}
	public worldButtons buttonType;
	// Use this for initialization
	void Start () {
		wM = GameObject.Find ("WorldManager").GetComponent<WorldManager>();
		levelSelect = transform.parent.transform.parent.FindChild("LevelSelect").gameObject;
		confirmScreen = transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void OnClick()
	{
		switch(buttonType)
		{
		case worldButtons.Confirm:
			int stageLoader = ES2.Load<int>("currentSave.txt");
			int stage = ES2.Load<int>("currentWorldSelected.txt");
			//int currentWorld = ES2.Load<int>("file" + stageLoader.ToString() + ".txt?tag=curW");
			ES2.Save(stage, "file" + stageLoader.ToString() + ".txt?tag=curW");
			int currentProgress = ES2.Load<int>("file" + stageLoader.ToString() + ".txt?tag=gProgEvent");
			if(currentProgress <= 8)
			{
				ES2.Save(9, "file" + stageLoader.ToString() + ".txt?tag=gProgEvent");
			}
			Time.timeScale = 1;
			Application.LoadLevel(wM.worlds[stage]);
			break;
		case worldButtons.Cancel:
			levelSelect.SetActive(true);
			confirmScreen.SetActive(false);
			break;
		}
	}
}

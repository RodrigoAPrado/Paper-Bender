using UnityEngine;
using System.Collections;

public class EventTrigger : MonoBehaviour {

	bool start;
	StartTalk sT;
	public GameObject[] gOActivate;
	public bool changeSaveProgress;
	public int saveProgressToChange;
	public bool changeStageProgress;
	public int stageProgressToChange;
	// Use this for initialization
	void Start () {
		sT = GetComponent<StartTalk>();
		int stageLoader = ES2.Load<int>("currentSave.txt");
		int currentProgress = ES2.Load<int>("file" + stageLoader.ToString() + ".txt?tag=gProgEvent");
		if(currentProgress >= saveProgressToChange)
		{
			start = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(start)
		{
			if(gOActivate.Length > 0)
			{
				for(int i = 0; i < gOActivate.Length; i++)
				{
					gOActivate[i].SetActive(true);
				}
			}
			gameObject.SetActive(false);
		}
	}
	void OnTriggerEnter2D(Collider2D hit)
	{
		if(hit.gameObject.tag == "Player" && !start)
		{
			int stageLoader = ES2.Load<int>("currentSave.txt");
			int currentProgress = ES2.Load<int>("file" + stageLoader.ToString() + ".txt?tag=gProgEvent");
			if(changeSaveProgress)
			{
				if(currentProgress < saveProgressToChange)
				{
					ES2.Save(saveProgressToChange, "file" + stageLoader.ToString() + ".txt?tag=gProgEvent");
				}
			}
			sT.SetTalkOn();
			start = true;
		}
	}
}

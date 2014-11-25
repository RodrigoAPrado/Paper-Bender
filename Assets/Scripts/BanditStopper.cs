using UnityEngine;
using System.Collections;

public class BanditStopper : MonoBehaviour {

	public Transform rightPosition;
	public LayerMask banditLayer;
	public GameObject bandit;
	public GameObject banditDefeated;
	public GameObject sheriff;
	public GameObject eventBubble;
	bool clear;
	// Use this for initialization
	void Start () 
	{
		int stageLoader = ES2.Load<int>("currentSave.txt");
		int currentProgressB = ES2.Load<int>("file" + stageLoader.ToString() + ".txt?tag=gProgEvent");
		if(currentProgressB >= 16)
		{
			bandit.SetActive(false);
			banditDefeated.SetActive(true);
			sheriff.SetActive(true);
			eventBubble.SetActive(true);
			clear = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
		if(clear)
			return;
		if(Physics2D.OverlapArea(transform.position, rightPosition.position, banditLayer))
		{
			bandit.GetComponent<BanditControllerNeo>().enabled = false;
			int stageLoader = ES2.Load<int>("currentSave.txt");
			int currentProgressB = ES2.Load<int>("file" + stageLoader.ToString() + ".txt?tag=gProgEvent");
			banditDefeated.transform.position = bandit.transform.position;
			bandit.SetActive(false);
			banditDefeated.SetActive(true);
			if(currentProgressB < 16)
			{
				ES2.Save(16, "file" + stageLoader.ToString() + ".txt?tag=gProgEvent");
				sheriff.SetActive(true);
				eventBubble.SetActive(true);
				clear = true;
			}
		}
	}
}

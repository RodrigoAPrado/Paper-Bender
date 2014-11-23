using UnityEngine;
using System.Collections;

public class StageManagerMaster : MonoBehaviour {

	public StageManager[] sM;
	// Use this for initialization
	void Start () {
		int i = 0;
		if(GameObject.FindGameObjectWithTag("NextStageNote") != null)
		{
			i = GameObject.FindGameObjectWithTag("NextStageNote").GetComponent<NextStage>().nextStageStartingPosition;
			GameObject.Destroy(GameObject.FindGameObjectWithTag("NextStageNote").gameObject);
			print(i);
		}
		if(GameObject.FindGameObjectWithTag("NextStageClear") != null)
		{
			i = GameObject.FindGameObjectWithTag("NextStageClear").GetComponent<ClearBandit>().nextStageStartingPosition;
			GameObject.Destroy(GameObject.FindGameObjectWithTag("NextStageClear").gameObject);
			print(i);
		}
		CallStageManager(i);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void CallStageManager(int i)
	{
		sM[i].SetStart();
	}
}

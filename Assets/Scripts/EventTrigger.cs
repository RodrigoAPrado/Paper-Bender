using UnityEngine;
using System.Collections;

public class EventTrigger : MonoBehaviour {

	bool start;
	StartTalk sT;
	public GameObject[] gOActivate;
	// Use this for initialization
	void Start () {
		sT = GetComponent<StartTalk>();
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
			sT.SetTalkOn();
			start = true;
		}
	}
}

using UnityEngine;
using System.Collections;

public class NextStage : MonoBehaviour {

	public string nextStage;
	public int nextStageStartingPosition;
	ChangeScene cSc;
	public LayerMask player;
	//remover essas aqui depois
	int checkLevel;
	public int addLevel;
	// Use this for initialization
	void Start () {
		if(Camera.main.GetComponent<ChangeScene>() != null)
		{
			cSc= Camera.main.GetComponent<ChangeScene>();
		}
		else
		{
			print ("null");
		}
		GetComponent<SpriteRenderer>().enabled = false;
		GetComponent<BoxCollider2D>().enabled = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(cSc != null){
			if(cSc.begin)
			{
				if(Physics2D.OverlapCircle(transform.position, 3, player))
				{
					GetComponent<SpriteRenderer>().enabled = true;
					GetComponent<BoxCollider2D>().enabled = true;
				}
				else
				{
					GetComponent<SpriteRenderer>().enabled = false;
					GetComponent<BoxCollider2D>().enabled = false;
				}
			}
			else
			{
				GetComponent<SpriteRenderer>().enabled = false;
				GetComponent<BoxCollider2D>().enabled = false;
			}
		}
	}
	public void Clicked()
	{
		checkLevel = PlayerPrefs.GetInt("DevelopLevel");
		if(checkLevel < addLevel)
		{
			PlayerPrefs.SetInt("DevelopLevel", addLevel);
		}
		gameObject.tag = "NextStageNote";
		DontDestroyOnLoad(gameObject);
		if(cSc!= null)
			cSc.ChangeThisScene(nextStage);
		else
			Application.LoadLevel(nextStage);
	}
}

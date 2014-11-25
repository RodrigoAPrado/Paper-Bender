using UnityEngine;
using System.Collections;

public class ClearBandit : MonoBehaviour 
{
    public GameObject bandidao;
	public string variable;
	public int stage;
	public string nextStage;
	public int nextStageStartingPosition;
	ChangeScene cSc;
	bool changeS;
	public bool uest7;
	public GameObject arrowEnable;
	void Start () 
    {
		if(Camera.main.GetComponent<ChangeScene>() != null)
		{
			cSc= Camera.main.GetComponent<ChangeScene>();
		}
		else
		{
			print ("null");
		}
		int stageLoader = ES2.Load<int>("currentSave.txt");
		if(uest7)
		{
			int currentProgressB = ES2.Load<int>("file" + stageLoader.ToString() + ".txt?tag=gProgEvent");
			if(currentProgressB >= 15)
			{
				bandidao.transform.FindChild("Avatar").GetComponent<SpriteRenderer>().color = new Color(0,1,1,0.5f);
			}
			return;
		}
		int currentProgress = ES2.Load<int>("file" + stageLoader.ToString() + ".txt?tag=" + variable);
		switch(stage)
		{
		case 1:
			if(currentProgress == 1 || currentProgress == 3 || currentProgress == 5 || currentProgress == 7)
			{
				bandidao.transform.FindChild("Avatar").GetComponent<SpriteRenderer>().color = new Color(0,1,1,0.5f);
			}
			break;
		case 2:
			if(currentProgress == 2 || currentProgress == 3 || currentProgress == 6 || currentProgress == 7)
			{
				bandidao.transform.FindChild("Avatar").GetComponent<SpriteRenderer>().color = new Color(0,1,1,0.5f);
			}
			break;
		case 3:
			if(currentProgress == 4 || currentProgress == 5 || currentProgress == 6 || currentProgress == 7)
			{
				bandidao.transform.FindChild("Avatar").GetComponent<SpriteRenderer>().color = new Color(0,1,1,0.5f);
			}
			break;
		default:
			print ("level nao declarado ou diferente de 1, 2 e 3");
			break;
		}
		//GetComponent<SpriteRenderer>().enabled = false;
		//GetComponent<BoxCollider2D>().enabled = false;
	}
	
	void Update () 
    {
		if(Input.GetKeyDown(KeyCode.B))
		{
			GameObject.Destroy(bandidao);
		}
		/*if(cSc != null){
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
		}*/
        if (bandidao == null && !changeS)
        { 
			int stageLoader = ES2.Load<int>("currentSave.txt");
			int currentProgress = ES2.Load<int>("file" + stageLoader.ToString() + ".txt?tag=" + variable);
			int currentStageProgress = ES2.Load<int>("file" + stageLoader.ToString() + ".txt?tag=gProgStages");
			changeS = true;
			if(uest7)
			{
				int currentProgressB = ES2.Load<int>("file" + stageLoader.ToString() + ".txt?tag=gProgEvent");
				if(currentProgressB < 15)
				{
					ES2.Save<int>(15, "file" + stageLoader.ToString() + ".txt?tag=gProgEvent");
				}
				arrowEnable.SetActive(true);
				return;
			}
			switch(stage)
			{
			case 1:
				if(currentProgress != 1 && currentProgress != 3 && currentProgress != 5 && currentProgress != 7)
				{
					currentProgress += 1;
					ES2.Save(currentProgress, "file" + stageLoader.ToString() + ".txt?tag=" + variable);
					ES2.Save(currentStageProgress + 1, "file" + stageLoader.ToString() + ".txt?tag=gProgStages");
				}
				break;
			case 2:
				if(currentProgress != 2 && currentProgress != 3 && currentProgress != 6 && currentProgress != 7)
				{
					currentProgress += 2;
					ES2.Save(currentProgress, "file" + stageLoader.ToString() + ".txt?tag=" + variable);
					ES2.Save(currentStageProgress + 1, "file" + stageLoader.ToString() + ".txt?tag=gProgStages");
				}
				break;
			case 3:
				if(currentProgress != 4 && currentProgress != 5 && currentProgress != 6 && currentProgress != 7)
				{
					currentProgress += 4;
					ES2.Save(currentProgress, "file" + stageLoader.ToString() + ".txt?tag=" + variable);
					ES2.Save(currentStageProgress + 1, "file" + stageLoader.ToString() + ".txt?tag=gProgStages");
				}
				break;
			default:
				print ("level nao declarado ou diferente de 1, 2 e 3");
				break;
			}
			gameObject.tag = "NextStageClear";
			DontDestroyOnLoad(gameObject);
			if(cSc!= null)
				cSc.ChangeThisScene(nextStage);
			else
				Application.LoadLevel(nextStage);
        }
	}
}

using UnityEngine;
using System.Collections;

public class BandaidScript : MonoBehaviour {

	public int mustBeThis;
	public int toChangeStage;
	public int toChangeProgress;
	SpriteRenderer sprite;
	public LayerMask playerMask;
	// Use this for initialization
	void Start () {
		sprite = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		int stageLoader = ES2.Load<int>("currentSave.txt");
		int currentProgressB = ES2.Load<int>("file" + stageLoader.ToString() + ".txt?tag=gProgEvent");
		if(currentProgressB == mustBeThis)
		{
			sprite.enabled = true;
			if(Physics2D.OverlapCircle(transform.position, 0.5f, playerMask))
			{
				ES2.Save(toChangeProgress, "file" + stageLoader.ToString() + ".txt?tag=gProgEvent");
				ES2.Save(toChangeStage, "file" + stageLoader.ToString() + ".txt?tag=gProgStages");
				GameObject.Destroy(gameObject);
			}
		}
		else
		{
			sprite.enabled = false;
		}

	
	}
}

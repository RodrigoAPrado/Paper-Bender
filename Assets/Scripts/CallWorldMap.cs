using UnityEngine;
using System.Collections;

public class CallWorldMap : MonoBehaviour {

	public GameObject particleObj;
	public Transform leftObj;
	public Transform rightObj;
	public LayerMask player;

	// Use this for initialization
	void Start () {
		particleObj.renderer.sortingLayerName = "Particles";
	}
	
	// Update is called once per frame
	void Update () {
		if(Physics2D.OverlapArea(leftObj.position, rightObj.position, player))
		{
			GetComponent<BoxCollider2D>().enabled = true;
			GetComponent<SpriteRenderer>().enabled = true;
			return;
		}
		GetComponent<BoxCollider2D>().enabled = false;
		GetComponent<SpriteRenderer>().enabled = false;
	}
	public void LoadWorldMapSelect()
	{
		Application.LoadLevelAdditive("Level_Select");
	}
}

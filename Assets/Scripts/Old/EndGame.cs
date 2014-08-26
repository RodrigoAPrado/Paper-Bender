using UnityEngine;
using System.Collections;

public class EndGame : MonoBehaviour {


	public GameObject pD;
	public GameObject blackScreen;
	public LayerMask player;
	float opacity = 0;
	public int nextStage;
	public Vector2 playerNextPosition;
	bool endstage;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(Physics2D.OverlapCircle(transform.position, 2, player) || Input.GetKeyDown(KeyCode.E))
		{
			endstage = true;
		}
		if(endstage)
		{
			pD.SetActive(false);
			opacity += Time.deltaTime;
			blackScreen.GetComponent<SpriteRenderer>().color = new Color (1, 1, 1, opacity);
			if(opacity > 1)
			{
				PlayerPrefs.SetInt("currentStage", nextStage);
				PlayerPrefs.SetInt("cameraPosition", 0);
				PlayerPrefs.SetFloat("pointX", playerNextPosition.x);
				PlayerPrefs.SetFloat("pointY", playerNextPosition.y);
				Application.LoadLevel(nextStage);
			}
		}
	}
}

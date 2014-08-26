using UnityEngine;
using System.Collections;

public class CheckPointController : MonoBehaviour {

	public Transform upper;
	public Transform lower;
	public LayerMask player;
	public bool passed;
	public GameObject[] toDelete;
	public Transform playerSpawn;
	public int cameraPosition;
	public int currentStage;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(passed)
		{
			return;
		}
		if(Physics2D.OverlapArea(upper.position, lower.position, player))
		{
			passed = true;
			GetComponent<TextMesh>().color = new Color(1,1,1);
			PlayerPrefs.SetInt("currentStage", currentStage);
			PlayerPrefs.SetInt("cameraPosition", cameraPosition);
			PlayerPrefs.SetFloat("pointX", playerSpawn.position.x);
			PlayerPrefs.SetFloat("pointY", playerSpawn.position.y);
			print (PlayerPrefs.GetInt("currentStage"));
			print (PlayerPrefs.GetInt("cameraPosition"));
			print (PlayerPrefs.GetFloat("pointX"));
			print (PlayerPrefs.GetFloat("pointY"));


		}
	}
}

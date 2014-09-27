using UnityEngine;
using System.Collections;

public class PaperToBeCut : MonoBehaviour {

	public Transform leftEntrance;
	public Transform rightEntrance;
	public Transform leftExit;
	public Transform rightExit;

	public LayerMask scissors;

	public GameObject[] paperSpawn;

	bool entrance;
	bool exit;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Physics2D.OverlapArea(leftEntrance.position, rightEntrance.position, scissors))
		{
			entrance = true;
			return;
		}
		if(Physics2D.OverlapArea(leftExit.position, rightExit.position, scissors))
		{
			exit = true;
			return;
		}
		if(entrance && exit)
		{
			for(int i = 0; i < paperSpawn.Length; i++)
			{
				paperSpawn[i].SetActive(true);
			}
			GameObject.Destroy(gameObject);
			return;
		}
	}
}

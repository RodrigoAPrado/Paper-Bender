using UnityEngine;
using System.Collections;

public class StageManager : MonoBehaviour {
	
	public Transform[] cameraEdges;
	public Transform startingPosition;
	public int cameraPosition;
	public bool followCharacter;
	PositionDetect pD;
	bool dontProceed;
	public bool flipChar;

	// Use this for initialization
	void Start () {
		if(GameObject.FindGameObjectWithTag("PositionDetect").GetComponent<PositionDetect>() != null)
		{
			pD = GameObject.FindGameObjectWithTag("PositionDetect").GetComponent<PositionDetect>();
		}
		else
		{
			dontProceed = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void SetStart()
	{
		if(GameObject.FindGameObjectWithTag("PositionDetect").GetComponent<PositionDetect>() != null)
		{
			pD = GameObject.FindGameObjectWithTag("PositionDetect").GetComponent<PositionDetect>();
		}
		else
		{
			dontProceed = true;
		}
		if(dontProceed)
			return;
		for(int i = 0; i < cameraEdges.Length; i ++)
		{
			cameraEdges[i].gameObject.SetActive(true);
		}
		if(GameObject.FindGameObjectWithTag("Player").transform != null)
		{
			Transform player = GameObject.FindGameObjectWithTag("Player").transform;
			player.transform.position = startingPosition.position;
 			Camera.main.GetComponent<CameraMovement>().MoveCamera(pD.cameraPositions[cameraPosition].x, pD.cameraPositions[cameraPosition].y, pD.cameraPositions[cameraPosition].z, followCharacter, false, cameraPosition);
			if(flipChar)
				player.GetComponent<MovePlayer>().Flip();
		}
		return;
	}
}

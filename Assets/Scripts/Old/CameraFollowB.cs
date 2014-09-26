using UnityEngine;
using System.Collections;

public class CameraFollowB : MonoBehaviour {


	public LayerMask player;
	public Transform upperLeft;
	public Transform lowerRight;
	PositionDetect pD;
	public GameObject toDisable;
	public GameObject toDisableB;
	public GameObject toEnableA;
	public GameObject toEnableB;
	public GameObject toEnableC;
	public int cameraPositionChange;
	public bool followCharacter;
	public bool stage3Follow;
	// Use this for initialization
	void Start () {
		pD = GameObject.FindGameObjectWithTag("PositionDetect").GetComponent<PositionDetect>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Physics2D.OverlapArea(new Vector2(upperLeft.position.x, upperLeft.position.y), new Vector2(lowerRight.position.x, lowerRight.position.y), player))
		{
			if(toDisable)
				toDisable.SetActive(false);
			if(toDisableB)
				toDisableB.SetActive(false);
			if(toEnableA)
				toEnableA.SetActive(true);
			if(toEnableB)
				toEnableB.SetActive(true);
			if(toEnableC)
				toEnableC.SetActive(true);
			Camera.main.GetComponent<CameraMovement>().MoveCamera(pD.cameraPositions[cameraPositionChange].x, pD.cameraPositions[cameraPositionChange].y, pD.cameraPositions[cameraPositionChange].z, followCharacter, stage3Follow, cameraPositionChange);
			/*Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(pD.cameraPositions[cameraPositionChange].x, pD.cameraPositions[cameraPositionChange].y, -10), 1);
			Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, pD.cameraPositions[cameraPositionChange].z, 1);*/
				//Camera.main.transform.position = new Vector3(pD.cameraPositions[cameraPositionChange].x, pD.cameraPositions[cameraPositionChange].y, -10);
			//Camera.main.orthographicSize = pD.cameraPositions[cameraPositionChange].z;
			
			gameObject.SetActive(false);
		}
	}
}

using UnityEngine;
using System.Collections;

public class BeginGame : MonoBehaviour {

	public PositionDetect pD;
	public GameObject blackScreen;
	float opacity = 1;
	int cameraToLoad;
	public GameObject[] cameraEdges;
	// Use this for initialization
	void Start () {
		cameraToLoad = PlayerPrefs.GetInt("cameraPosition");
		pD = GameObject.FindGameObjectWithTag("PositionDetect").GetComponent<PositionDetect>();
		print(pD.cameraPositions[cameraToLoad]);
		Camera.main.transform.position = new Vector3(pD.cameraPositions[cameraToLoad].x, pD.cameraPositions[cameraToLoad].y, -10);
		Camera.main.orthographicSize = pD.cameraPositions[cameraToLoad].z;
		pD.halt = true;
		int currentStage = PlayerPrefs.GetInt("currentStage");
		if(currentStage == 1)
			CameraStage1();
		if(currentStage == 2)
			CameraStage2();
		if(currentStage == 3)
			CameraStage3();
	}
	void CameraStage1()
	{
		switch(cameraToLoad)
		{
			case 0:
				cameraEdges[1].SetActive(true);
			break;
			case 2:
				cameraEdges[3].SetActive(true);
				cameraEdges[4].SetActive(true);
			break;
			case 4:
				cameraEdges[7].SetActive(true);
				cameraEdges[8].SetActive(true);
			break;
		}
	}
	
	void CameraStage2()
	{
		switch(cameraToLoad)
		{
		case 0:
			cameraEdges[1].SetActive(true);
			break;
		case 2:
			cameraEdges[3].SetActive(true);
			cameraEdges[4].SetActive(true);
			cameraEdges[6].SetActive(true);
			break;
		case 4:
			cameraEdges[7].SetActive(true);
			cameraEdges[8].SetActive(true);
			break;
		}
	}
	
	void CameraStage3()
	{
		switch(cameraToLoad)
		{
		case 0:
			cameraEdges[1].SetActive(true);
			break;
		case 1:
			cameraEdges[0].SetActive(true);
			cameraEdges[2].SetActive(true);
			cameraEdges[8].SetActive(true);
			break;
		case 4:
			cameraEdges[7].SetActive(true);
			break;
		case 9:
			cameraEdges[17].SetActive(true);
			cameraEdges[18].SetActive(true);
			cameraEdges[19].SetActive(true);
			break;
		}
	}
	// Update is called once per frame
	void Update () {
		pD.halt = true;
		if(!blackScreen)
		{
			pD.halt = false;
			GameObject.Destroy(gameObject);
		}
		opacity -= Time.deltaTime;
		blackScreen.GetComponent<SpriteRenderer>().color = new Color (1, 1, 1, opacity);
		if(opacity < 0)
		{
			pD.halt = false;
			GameObject.Destroy(gameObject);
		}
	}
	
}

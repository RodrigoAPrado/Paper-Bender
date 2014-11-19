using UnityEngine;
using System.Collections;

public class ExitWorldMap : MonoBehaviour {

	public GameObject worldMapScreen;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnClick()
	{
		Time.timeScale = 1;
		GameObject.Destroy(worldMapScreen);
	}
}

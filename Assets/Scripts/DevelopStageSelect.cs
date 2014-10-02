using UnityEngine;
using System.Collections;

public class DevelopStageSelect : MonoBehaviour {

	public int levelValue;
	int checkValue;
	public string levelName;
	bool available;
	// Use this for initialization
	void Start () {
		checkValue = PlayerPrefs.GetInt("DevelopLevel");
		if(checkValue >= levelValue)
			available = true;
		if(!available)
			GetComponent<SpriteRenderer>().color = new Color(1,1,1,0.3f);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.U))
			available = true;
	}
	void OnMouseDown()
	{
		if(!available)
			return;
		Application.LoadLevel(levelName);
	}
}

using UnityEngine;
using System.Collections;

public class PersistentObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(gameObject);
	}
	
	void OnLevelWasLoaded(int scene)
	{
		if(Application.loadedLevelName != "Menu" && Application.loadedLevelName != "Level_Select" && Application.loadedLevelName != "Save_Select")
		{
			Destroy(gameObject);
		}
	}
}

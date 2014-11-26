using UnityEngine;
using System.Collections;

public class PersistentObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if(GameObject.FindGameObjectWithTag("MenuMusic") != null)
		{
			GameObject.Destroy(gameObject);
		}
		else
		{
			gameObject.tag = "MenuMusic";
			DontDestroyOnLoad(gameObject);
		}

	}
	
	void OnLevelWasLoaded(int scene)
	{
		if(Application.loadedLevelName != "Menu" && Application.loadedLevelName != "Level_Select" && Application.loadedLevelName != "Save_Select")
		{
			Destroy(gameObject);
		}
	}
}

using UnityEngine;
using System.Collections;

public class LoadLevel : MonoBehaviour 
{
    public string level;

	void Start () 
    {
	
	}
	
	void Update () 
    {
	
	}

    void OnClick()
    {
        Application.LoadLevel(level);
    }
}

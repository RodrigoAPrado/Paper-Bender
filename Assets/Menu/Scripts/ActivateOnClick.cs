using UnityEngine;
using System.Collections;

public class ActivateOnClick : MonoBehaviour 
{
    public GameObject target;

	void Start () 
    {
	    
	}
	
	void Update () 
    {
	    
	}

    void OnClick()
    {
        print("lalal");
        target.SetActive(true);
    }
}

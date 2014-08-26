using UnityEngine;
using System.Collections;

public class buttonOptions : MonoBehaviour 
{
    public GameObject options;

	void Start () 
    {
	
	}
	
	void Update () 
    {
	
	}

    void OnClick()
    {
        //Instantiate(options);
        options.SetActive(true);
    }
}

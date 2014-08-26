using UnityEngine;
using System.Collections;

public class buttonReturnOptions : MonoBehaviour 
{

	void Start () 
    {
	
	}
	
	void Update () 
    {
	
	}

    void OnClick()
    {
        //GameObject.Destroy(transform.parent.gameObject);
        transform.parent.gameObject.SetActive(false);
    }
}

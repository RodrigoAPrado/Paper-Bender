using UnityEngine;
using System.Collections;

public class btn_options : MonoBehaviour {

    //public Transform prefabOptions;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnClick()
    {
        //print("options");
        //CRIAR O OPTIONS
        GameObject.Instantiate(Resources.Load("UI Options") as GameObject, new Vector3(9999, 0, 0), new Quaternion(0, 0, 0, 0));
    }
}

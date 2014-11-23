using UnityEngine;
using System.Collections;

public class btn_pause : MonoBehaviour {

    //public Transform prefabPause;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnClick()
    {
        //print("pause");
        //CRIAR O PAUSE
        Time.timeScale = 0;
        GameObject.Instantiate(Resources.Load("UI Pause") as GameObject, new Vector3(9999, 0, 0), new Quaternion(0, 0, 0, 0));
        GameObject.Destroy(transform.parent.gameObject);
    }
}

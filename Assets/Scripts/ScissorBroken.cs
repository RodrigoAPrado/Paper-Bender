using UnityEngine;
using System.Collections;

public class ScissorBroken: MonoBehaviour {

	public GameObject repairPart;
	public GameObject normalScissor;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(repairPart.activeSelf)
		{
			normalScissor.SetActive(true);
			GameObject.Destroy(gameObject);
		}
	}
}

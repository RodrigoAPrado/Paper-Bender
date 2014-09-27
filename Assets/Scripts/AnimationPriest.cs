using UnityEngine;
using System.Collections;

public class AnimationPriest : MonoBehaviour {

	MovePlayer mP;
	public bool test;
	// Use this for initializafition
	void Start () {
		mP = transform.parent.gameObject.GetComponent<MovePlayer>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

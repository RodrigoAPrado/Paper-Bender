using UnityEngine;
using System.Collections;

public class IndoorSoundManager : MonoBehaviour {

	public Transform limit;
	public bool indoors;
	public LayerMask player;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		indoors = Physics2D.OverlapArea(transform.position, limit.position, player);
	}
}

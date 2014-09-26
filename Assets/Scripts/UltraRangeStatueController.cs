using UnityEngine;
using System.Collections;

public class UltraRangeStatueController : MonoBehaviour {

	CameraMovement cM;
	[SerializeField] int cameraRestriction;
	public CircleCollider2D range;
	public BoxCollider2D ultraRange;
	// Use this for initialization
	void Start () {
		cM = Camera.main.GetComponent<CameraMovement>();
	}
	
	// Update is called once per frame
	void Update () {
		if(cameraRestriction == cM.currentCameraPosition)
		{
			range.enabled = false;
			ultraRange.enabled = true;
		}
		if(cameraRestriction != cM.currentCameraPosition)
		{
			ultraRange.enabled = false;
			range.enabled = true;
		}
	}
}

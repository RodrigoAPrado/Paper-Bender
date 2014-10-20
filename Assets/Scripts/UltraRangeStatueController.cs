using UnityEngine;
using System.Collections;

public class UltraRangeStatueController : MonoBehaviour {

	CameraMovement cM;
	[SerializeField] int cameraRestriction;
	public CircleCollider2D range;
	public BoxCollider2D ultraRange;
	public Transform left;
	public Transform right;
	public LayerMask player;
	bool working;
	public SpriteRenderer light;
	float lightTime;
	bool increase;
	// Use this for initialization
	void Start () {
		cM = Camera.main.GetComponent<CameraMovement>();
		lightTime = 0;
		Color c = new Color(1,1,1,lightTime);
		light.color = c;
	}
	
	// Update is called once per frame
	void Update () {
		if(Physics2D.OverlapArea(left.position, right.position, player))
   	    {
			range.enabled = false;
			ultraRange.enabled = true;
			working = true;
		}
		else
		{
			ultraRange.enabled = false;
			range.enabled = true;
			working = false;
		}
		if(working)
		{
			if(increase)
			{
				if(lightTime < 1)
					lightTime += Time.deltaTime;
				else
					increase = false;
			}
			else
			{
				if(lightTime > 0)
					lightTime -= Time.deltaTime;
				else
					increase = true;
			}
		}
		else
		{
			if(lightTime > 0)
				lightTime -= Time.deltaTime * 0.5f;
			else
				lightTime = 0;
		}
		Color c = new Color(1,1,1,lightTime);
		light.color = c;
		/*if(cameraRestriction == cM.currentCameraPosition)
		{
			range.enabled = false;
			ultraRange.enabled = true;
		}
		if(cameraRestriction != cM.currentCameraPosition)
		{
			ultraRange.enabled = false;
			range.enabled = true;
		}*/
	}
}

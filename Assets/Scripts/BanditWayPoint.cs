using UnityEngine;
using System.Collections;

public class BanditWayPoint : MonoBehaviour {

	public Transform wayPointRight;
	public Transform wayPointLeft;
	public bool dontStopHere;
	public int baseHeightLeft;
	public int baseHeightRight;
	public GameObject[] obstacles;
	public int[] heightValues;
	public bool[] heightForLeft;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public int CheckHeight(bool left)
	{
		int trueHeight;
		if(left)
		{
			trueHeight = baseHeightLeft;
			for(int i = 0; i < obstacles.Length; i ++)
			{
				if(obstacles[i].activeSelf)
				{
					if(heightForLeft[i])
					{
						trueHeight += heightValues[i];
					}
				}
			}
		}
		else
		{
			trueHeight = baseHeightRight;
			for(int i = 0; i < obstacles.Length; i ++)
			{
				if(obstacles[i].activeSelf)
				{
					if(!heightForLeft[i])
					{
						trueHeight += heightValues[i];
					}
				}
			}
		}
		return trueHeight;
	}
}

using UnityEngine;
using System.Collections;

public class BanditWayPoint : MonoBehaviour {

	public Transform[] wayPointsRight;
	public bool[] wayPointRightDistanceJump;
	public Transform[] wayPointsLeft;
	public bool[] wayPointLeftDistanceJump;

	//public Transform wayPointRight;
    //public Transform wayPointLeft;
	public bool dontStopHere;
	
	public Transform[] autoNextWayPoints;
	public bool[] nextWayPointJump;
	public bool[] nextWayDistanceJump;
	[HideInInspector] public Transform autoNextWayPoint;
	[HideInInspector] public bool jumpHere;
	[HideInInspector] public bool distanceJump;

	public Transform[] originWayPoint;
	public int[] originWayPointBaseHeight;

	public GameObject[] obstacles;
	public int[] heightValues;
	public bool[] heightForLeft;
	public int maxHeight;
	public bool multipleAutoFromOrigin;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public int CheckHeight(bool left, Transform currentWayPoint)
	{
		int trueHeight = 0;
		bool autoSelected = false;
		for(int j = 0; j < originWayPoint.Length; j ++)
		{
			if(originWayPoint[j] == currentWayPoint && !autoSelected)
			{
				trueHeight = originWayPointBaseHeight[j];
				if(multipleAutoFromOrigin){
					int testHeight = 7;
					if(autoNextWayPoints.Length > 0)
					{
						if(autoNextWayPoints[j].position.x > transform.position.x)
						{
							testHeight = autoNextWayPoints[j].GetComponent<BanditWayPoint>().CheckHeight(true, transform);
						}
						else
						{
							testHeight = autoNextWayPoints[j].GetComponent<BanditWayPoint>().CheckHeight(false, transform);
						}
					}
					print (testHeight);
					if(testHeight < 7){
						autoNextWayPoint = autoNextWayPoints[j];
						if(nextWayPointJump.Length > 0)
							jumpHere = nextWayPointJump[j];
						if(nextWayDistanceJump.Length > 0)
							distanceJump = nextWayDistanceJump[j];
						autoSelected = true;
					}
				}
				else
				{
					if(autoNextWayPoints.Length > 0)
						autoNextWayPoint = autoNextWayPoints[j];
					if(nextWayPointJump.Length > 0)
						jumpHere = nextWayPointJump[j];
					if(nextWayDistanceJump.Length > 0)
						distanceJump = nextWayDistanceJump[j];
					autoSelected = true;
				}
			}
		}
		if(left)
		{
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
		if(maxHeight > 0)
		{
			if(trueHeight > maxHeight)
			{
				trueHeight = maxHeight;
			}
		}
		return trueHeight;
	}
}

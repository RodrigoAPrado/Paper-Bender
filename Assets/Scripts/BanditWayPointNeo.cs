using UnityEngine;
using System.Collections;

public class BanditWayPointNeo : MonoBehaviour {
	
	//public Transform wayPointRight;
    //public Transform wayPointLeft;
	public Transform[] wayPoints;
	public int[] wayPointsBaseHeightFromHere;
	public bool[] isWayPointLeft;
	public bool[] doesWayPointNeedJump;
	public bool[] isTheJumpADistanceJump;
	public float[] wayPointDistanceForce;
	public int[] wayPointPreference;

	public Transform[] wayPoint0Obstacles;
	public int[] wayPoint0ObstaclesHeights;

	public Transform[] wayPoint1Obstacles;
	public int[] wayPoint1ObstaclesHeights;

	public Transform[] wayPoint2Obstacles;
	public int[] wayPoint2ObstaclesHeights;

	public Transform[] wayPoint3Obstacles;
	public int[] wayPoint3ObstaclesHeights;

	public Transform[] wayPoint4Obstacles;
	public int[] wayPoint4ObstaclesHeights;

	public Transform[] wayPoint5Obstacles;
	public int[] wayPoint5ObstaclesHeights;

	public Transform[] wayPoint6Obstacles;
	public int[] wayPoint6ObstaclesHeights;

	public Transform[] wayPoint7Obstacles;
	public int[] wayPoint7ObstaclesHeights;

	public bool dontStopHere;
	public int maxHeight;
	public int increaseUltraHeightBy;
	int ultraHeight;
	// Use this for initialization
	void Start () 
	{
		ultraHeight = 7 + increaseUltraHeightBy;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public int CheckHeight(int index)
	{
		int obstacleHeight = CheckObstacles(index);
		int trueHeight = wayPointsBaseHeightFromHere[index] + obstacleHeight;
		if(maxHeight > 0)
		{
			if(trueHeight > maxHeight)
				trueHeight = maxHeight;
		}
		return trueHeight;
	}
	public int CheckHeightIndex(bool isLeft, Transform lastPoint)
	{
		bool startHeight = false;
		int height = 0;
		int preference = 0;
		int index = -1;
		for(int i = 0; i < isWayPointLeft.Length; i++)
		{
			if(isWayPointLeft[i] == isLeft)
			{
				int obstacleHeight = CheckObstacles(i);
				int trueHeight = wayPointsBaseHeightFromHere[i] + obstacleHeight;
				if(maxHeight > 0)
				{
					if(trueHeight > maxHeight)
						trueHeight = maxHeight;
				}
				if(trueHeight < ultraHeight)
				{
					if(!startHeight)
					{
						height = trueHeight;
						startHeight = true;
						preference = wayPointPreference[i];
						index = i;
					}
					else
					{
						if(lastPoint != null)
						{
							if(wayPoints[i].gameObject == lastPoint.gameObject)
							{
								if(preference <= 0)
								{
									if(trueHeight < height)
									{
										height = trueHeight;
										preference = wayPointPreference[i];
										index = i;
									}
								}
							}
							else
							{
								if(preference < wayPointPreference[i])
								{
									height = trueHeight;
									preference = wayPointPreference[i];
									index = i;
								}
								else
								{
									if(preference == wayPointPreference[i])
									{
										if(trueHeight < height)
										{
											height = trueHeight;
											preference = wayPointPreference[i];
											index = i;
										}
									}
								}
							}

						}
						else
						{
							if(preference < wayPointPreference[i])
							{
								height = trueHeight;
								preference = wayPointPreference[i];
								index = i;
							}
							else
							{
								if(preference == wayPointPreference[i])
								{
									if(trueHeight < height)
									{
										height = trueHeight;
										preference = wayPointPreference[i];
										index = i;
									}
								}
							}
						}
					}
				}
			}
		}
		return index;
	}
	int CheckObstacles(int index)
	{
		Transform[] indexedObstacles = new Transform[0];
		int[] indexedObstaclesHeight = new int[0];
		int value = 0;
		switch(index)
		{
		case 0:
			indexedObstacles = wayPoint0Obstacles;
			indexedObstaclesHeight = wayPoint0ObstaclesHeights;
			break;
		case 1:
			indexedObstacles = wayPoint1Obstacles;
			indexedObstaclesHeight = wayPoint1ObstaclesHeights;
			break;
		case 2:
			indexedObstacles = wayPoint2Obstacles;
			indexedObstaclesHeight = wayPoint2ObstaclesHeights;
			break;
		case 3:
			indexedObstacles = wayPoint3Obstacles;
			indexedObstaclesHeight = wayPoint3ObstaclesHeights;
			break;
		case 4:
			indexedObstacles = wayPoint4Obstacles;
			indexedObstaclesHeight = wayPoint4ObstaclesHeights;
			break;
		case 5:
			indexedObstacles = wayPoint5Obstacles;
			indexedObstaclesHeight = wayPoint5ObstaclesHeights;
			break;
		case 6:
			indexedObstacles = wayPoint6Obstacles;
			indexedObstaclesHeight = wayPoint6ObstaclesHeights;
			break;
		case 7:
			indexedObstacles = wayPoint7Obstacles;
			indexedObstaclesHeight = wayPoint7ObstaclesHeights;
			break;
		}
		if(indexedObstacles.Length > 0)
		{
			for(int i = 0; i < indexedObstacles.Length; i++)
			{
				if(indexedObstacles[i].gameObject.activeSelf)
				{
					value += indexedObstaclesHeight[i];
				}
			}
			return value;
		}
		else
		{
			return 0;
		}
	}
	/*
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
	}*/
}

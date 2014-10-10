	using UnityEngine;
using System.Collections;

public class BanditController : MonoBehaviour {


	//public Transform[] wayPoints;
	//public bool[] wayPointsStop;

	public LayerMask wayPointsLayer;

	public LayerMask playerObstacleLayer;
	public LayerMask playerLayer;

	public Transform currentWayPoint;
	BanditWayPoint bWP;

	public float jumpSpeedMod;
	public float speedMod;
	float speed;
	bool left;
	bool detectSide;
	bool isOnStart;
	bool walk;

	int storedHeight;

	float jumpCounter;
	float jumpSpeed;

	[SerializeField] float range;

	public Transform wayPointCheck; 

	//Ground
	public LayerMask groundLayer;
	public Transform groundCheckLeft;
	public Transform groundCheckRight;
	bool grounded;
	bool jumping;
	bool groundOnLeft;
	bool groundOnRight;
	float groundSpeed;
	bool distanceJump;
	bool detectDistanceJump;
	//
	Transform player;

	GameObject charAvatar;

	Vector2[] currentPosition;
	int positionIndex = 0;

	float timer = 0;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").transform;
		bWP = currentWayPoint.GetComponent<BanditWayPoint>();
		charAvatar = transform.FindChild("Avatar").gameObject;
		currentPosition = new Vector2[3];
		//walk = true;
		//isOnStart = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(storedHeight > 0 && !jumping)
		{
			Jump(false);
		}
		if(positionIndex >= 3)
		{
			positionIndex = 0;
		}
		currentPosition[positionIndex] = transform.position;

		if(currentPosition[0] == currentPosition[1] && currentPosition[1] == currentPosition[2])
		{
			walk = false;
			isOnStart = true;
			detectSide = false;
		}

		if(walk)
		{
			if(!detectSide)
			{
				if(transform.position.x > currentWayPoint.position.x)
					left = true;
				else
					left = false;
				detectSide = true;
			}
			if(left)
				speed = -1;
			else
				speed = 1;
			if(isOnStart)
			{
				if(!Physics2D.OverlapCircle(wayPointCheck.position, 0.1f, wayPointsLayer))
				{
					isOnStart = false;
				}
			}
			else
			{
				if(Physics2D.OverlapCircle(wayPointCheck.position, 0.1f, wayPointsLayer))
				{
					Collider2D[] collidersDetected = Physics2D.OverlapCircleAll(wayPointCheck.position, 0.1f, wayPointsLayer);
					bool skip = false; 
					for(int j = 0; j< collidersDetected.Length; j++)
					{
						if(collidersDetected[j].gameObject != currentWayPoint.gameObject)
						{
							skip = true;
						}
					}
					if(!skip)
					{
						if(currentWayPoint.gameObject.tag == "BanditWayPointEnd")
						{
							GameObject.Destroy(gameObject);
						}
						isOnStart = true;
						detectSide = false;
						if(bWP.dontStopHere)
						{
							if(bWP.autoNextWayPoint != null)
							{
								if(bWP.jumpHere)
								{
									Jump(true);
								}
								currentWayPoint = bWP.autoNextWayPoint;
								bWP = currentWayPoint.GetComponent<BanditWayPoint>();
							}
							return;
						}
						else
						{
							if(Physics2D.OverlapCircle(transform.position, range * 0.33F, playerLayer))
							{
								
								if(player.transform.position.x < transform.position.x)
								{
									DecideWayPoint(true);
									return;
								}
								if(player.transform.position.x > transform.position.x)
								{
									DecideWayPoint(false);
									return;
								}
							}
							else
							{
								walk = false;
							}
						}

					}

					//walk = false;
					//detectSide = false;
				}
			}
		}
		if(!walk)
		{
			DetectPlayer();
			speed = 0;
		}
		groundOnLeft = Physics2D.OverlapCircle(groundCheckLeft.position, 0.2f, groundLayer);
		groundOnRight = Physics2D.OverlapCircle(groundCheckRight.position, 0.2f, groundLayer);

		if(groundOnLeft || groundOnRight)
			grounded = true;
		else
			grounded = false;



		if(grounded)
			groundSpeed = 5;
		else
			groundSpeed = 1;

		if(jumping)
		{
			if(distanceJump)
			{
				groundSpeed = 5;
			}
			else
			{
				groundSpeed = 2;
			}
			if(jumpCounter < 0.8f)
			{
				jumpCounter += Time.deltaTime;
			}
		}

		if(grounded && jumping && jumpCounter > 0.8f)
		{
			distanceJump = false;
			jumping = false;
			jumpCounter = 0;
		}

		rigidbody2D.velocity = new Vector2(speed * groundSpeed * speedMod,rigidbody2D.velocity.y);

		//print (speed*groundSpeed*speedMod);
		/*if(isWayPointLeft[currentWayPoint])
		{
			if(transform.position.x > wayPoints[currentWayPoint].position.x)
			{
				speed = -1;

			}
			else
			{
				if(wayPointsStop[currentWayPoint])
				{
					speed = 0;
				}
				else
				{
					currentWayPoint --;
					if(currentWayPoint < 0)
					{
						currentWayPoint = wayPoints.Length - 1;
					}
				}
			}
		}
		else
		{
			if(transform.position.x < wayPoints[currentWayPoint].position.x)
			{
				speed = 1;
			}
			else
			{
				if(wayPointsStop[currentWayPoint])
				{
					speed = 0;
				}
				else
				{
					currentWayPoint ++;
					if(currentWayPoint >= wayPoints.Length)
					{
						currentWayPoint = 0;
					}
				}
			}
		}
		*/
	}
	void Jump(bool calculate)
	{
		if(jumping)
			return;
		/*if(bWP.autoNextWayPoint.position.y > currentWayPoint.position.y)
		{
			float dif = bWP.autoNextWayPoint.position.y - currentWayPoint.position.y;
			jumpSpeed = dif * jumpSpeedMod;
		}*/
		int height = 0;
		if(storedHeight > 0)
		{
			height = storedHeight;
			storedHeight = 0;
		}
		else
		{
			if(!calculate)
				return;
			if(bWP.autoNextWayPoint.position.x > currentWayPoint.position.x)
			{
				 height = bWP.autoNextWayPoint.GetComponent<BanditWayPoint>().CheckHeight(true, currentWayPoint);
			}
			else
			{
				height = bWP.autoNextWayPoint.GetComponent<BanditWayPoint>().CheckHeight(false, currentWayPoint);
			}
		}
		if(height <= 0)
		{
			return;
		}
		jumpSpeed = height * jumpSpeedMod;
		//print(height);
		//print (jumpSpeed);
		if(bWP.distanceJump || detectDistanceJump)
			distanceJump = true;
		detectDistanceJump = false;
		rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpSpeed);
		jumping = true;
	}
	void DetectPlayer()
	{
		if(Physics2D.OverlapCircle(transform.position, range, playerLayer))
		{

			RaycastHit2D resultUp = Physics2D.Raycast(transform.position, Vector2.up, range, playerObstacleLayer);
			if(player.transform.position.x < transform.position.x)
			{
				RaycastHit2D resultDUp = Physics2D.Raycast(transform.position, new Vector2(-1,1), range, playerObstacleLayer);
				RaycastHit2D resultDDown = Physics2D.Raycast(transform.position, new Vector2(-1,-1), range, playerObstacleLayer);
				RaycastHit2D result = Physics2D.Raycast(transform.position, -Vector2.right, range, playerObstacleLayer);
				if((result.collider == null || result.collider.tag != "Player") && (resultUp.collider == null || resultUp.collider.tag != "Player") &&(resultDUp.collider == null || resultDUp.collider.tag != "Player") && (resultDDown.collider == null || resultDDown.collider.tag != "Player"))
				{
					//print ("he's not here");
				}
				else
				{
					//print ("he's here, on my left");
					/*int height = bWP.wayPointRight.GetComponent<BanditWayPoint>().CheckHeight(true);

					if(height < 7)
						currentWayPoint = bWP.wayPointRight;
					else
						currentWayPoint = bWP.wayPointLeft;*/
					DecideWayPoint(true);
					return;
				}
			}
			if(player.transform.position.x > transform.position.x)
			{
				RaycastHit2D resultDUp = Physics2D.Raycast(transform.position, new Vector2(1,1), range, playerObstacleLayer);
				RaycastHit2D resultDDown = Physics2D.Raycast(transform.position, new Vector2(1,-1), range, playerObstacleLayer);
				RaycastHit2D result = Physics2D.Raycast(transform.position, Vector2.right, range, playerObstacleLayer);

				if((result.collider == null || result.collider.tag != "Player") && (resultUp.collider == null || resultUp.collider.tag != "Player") &&(resultDUp.collider == null || resultDUp.collider.tag != "Player") && (resultDDown.collider == null || resultDDown.collider.tag != "Player"))
				{
					//print ("he's not here");
				}
				else
				{
					//print ("he's here, on my right");
					/*int height = bWP.wayPointLeft.GetComponent<BanditWayPoint>().CheckHeight(false);
					if(height < 7)
						currentWayPoint = bWP.wayPointLeft;
					else
						currentWayPoint = bWP.wayPointRight;
					*/
					DecideWayPoint(false);
					return;
				}
			}

		}
	}
	void DecideWayPoint(bool left)
	{
		if(left)
		{
			if(bWP.wayPointsRight.Length > 0)
			{
				Transform chosenWaypoint = bWP.wayPointsRight[ChooseWayPoint(bWP.wayPointsRight, false)];
				
				int height = chosenWaypoint.GetComponent<BanditWayPoint>().CheckHeight(false, currentWayPoint);
				//print (height);
				if(height < 7)
				{
					detectDistanceJump = bWP.wayPointRightDistanceJump[ChooseWayPoint(bWP.wayPointsRight, false)];
					storedHeight = height;
					currentWayPoint = chosenWaypoint;
				}
				else
				{
					chosenWaypoint = bWP.wayPointsLeft[ChooseWayPoint(bWP.wayPointsLeft, true)];
					detectDistanceJump = bWP.wayPointLeftDistanceJump[ChooseWayPoint(bWP.wayPointsLeft, true)];
					height = chosenWaypoint.GetComponent<BanditWayPoint>().CheckHeight(true, currentWayPoint);
					storedHeight = height;
					currentWayPoint = chosenWaypoint;
				}
			}
			else
			{
				Transform chosenWaypoint = bWP.wayPointsLeft[ChooseWayPoint(bWP.wayPointsLeft, true)];
				detectDistanceJump = bWP.wayPointLeftDistanceJump[ChooseWayPoint(bWP.wayPointsLeft, true)];
				int height = chosenWaypoint.GetComponent<BanditWayPoint>().CheckHeight(true, currentWayPoint);
				storedHeight = height;
				currentWayPoint = chosenWaypoint;
			}
			bWP = currentWayPoint.GetComponent<BanditWayPoint>();
			walk = true;
			isOnStart = true;
			return;
		}
		else
		{
			if(bWP.wayPointsLeft.Length > 0)
			{
				Transform chosenWaypoint = bWP.wayPointsLeft[ChooseWayPoint(bWP.wayPointsLeft, true)];
				
				int height = chosenWaypoint.GetComponent<BanditWayPoint>().CheckHeight(true, currentWayPoint);
				//print (height);
				if(height < 7)
				{
					detectDistanceJump = bWP.wayPointLeftDistanceJump[ChooseWayPoint(bWP.wayPointsLeft, true)];
					storedHeight = height;
					currentWayPoint = chosenWaypoint;
				}
				else
				{
					chosenWaypoint = bWP.wayPointsRight[ChooseWayPoint(bWP.wayPointsRight, false)];
					detectDistanceJump = bWP.wayPointRightDistanceJump[ChooseWayPoint(bWP.wayPointsRight, false)];
					height = chosenWaypoint.GetComponent<BanditWayPoint>().CheckHeight(false, currentWayPoint);
					storedHeight = height;
					currentWayPoint = chosenWaypoint;
				}
			}
			else
			{
				Transform  chosenWaypoint = bWP.wayPointsRight[ChooseWayPoint(bWP.wayPointsRight, false)];
				detectDistanceJump = bWP.wayPointRightDistanceJump[ChooseWayPoint(bWP.wayPointsRight, false)];
				int height = chosenWaypoint.GetComponent<BanditWayPoint>().CheckHeight(false, currentWayPoint);
				storedHeight = height;
				currentWayPoint = chosenWaypoint;
			}
			bWP = currentWayPoint.GetComponent<BanditWayPoint>();
			walk = true;
			isOnStart = true;
			return;
		}
	}
	int ChooseWayPoint(Transform[] wayPoints, bool side)
	{
		int height = 0;
		int selectedWaypoint = 0;
		for(int i = 0; i < wayPoints.Length; i++)
		{
			int thisHeight = wayPoints[i].GetComponent<BanditWayPoint>().CheckHeight(side, currentWayPoint);
			if(i == 0 || thisHeight < height)
			{
				height = thisHeight;
				selectedWaypoint = i;
			}
		}
		return selectedWaypoint;
	}
}

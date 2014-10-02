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



	float speed;

	bool left;
	bool detectSide;
	bool isOnStart;
	bool walk;

	[SerializeField] float range;

	public Transform wayPointCheck; 

	//Ground
	public LayerMask groundLayer;
	public Transform groundCheckLeft;
	public Transform groundCheckRight;
	bool grounded;
	bool groundOnLeft;
	bool groundOnRight;
	float groundSpeed;

	//
	Transform player;

	GameObject charAvatar;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").transform;
		bWP = currentWayPoint.GetComponent<BanditWayPoint>();
		charAvatar = transform.FindChild("Avatar").gameObject;
		//walk = true;
		//isOnStart = true;
	}
	
	// Update is called once per frame
	void Update () {

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
					if(Physics2D.OverlapCircle(transform.position, range, playerLayer))
					{
						isOnStart = true;
						if(player.transform.position.x < transform.position.x)
						{
							currentWayPoint = bWP.wayPointRight;
							bWP = currentWayPoint.GetComponent<BanditWayPoint>();
							walk = true;
							isOnStart = true;
							return;
						}
						if(player.transform.position.x > transform.position.x)
						{
							currentWayPoint = bWP.wayPointLeft;
							bWP = currentWayPoint.GetComponent<BanditWayPoint>();
							walk = true;
							isOnStart = true;
							return;
						}
					}
					else
					{
						walk = false;
					}
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
			groundSpeed = 2;




		rigidbody2D.velocity = new Vector2(speed * groundSpeed,rigidbody2D.velocity.y);


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
					print ("he's not here");
				}
				else
				{
					print ("he's here, on my left");
					int height = bWP.wayPointRight.GetComponent<BanditWayPoint>().CheckHeight(true);
					if(height < 7)
						currentWayPoint = bWP.wayPointRight;
					else
						currentWayPoint = bWP.wayPointLeft;
					bWP = currentWayPoint.GetComponent<BanditWayPoint>();
					walk = true;
					isOnStart = true;
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
					print ("he's not here");
				}
				else
				{
					print ("he's here, on my right");
					int height = bWP.wayPointLeft.GetComponent<BanditWayPoint>().CheckHeight(false);
					if(height < 7)
						currentWayPoint = bWP.wayPointLeft;
					else
						currentWayPoint = bWP.wayPointRight;

					bWP = currentWayPoint.GetComponent<BanditWayPoint>();
					walk = true;
					isOnStart = true;
					return;
				}
			}

		}
	}
}

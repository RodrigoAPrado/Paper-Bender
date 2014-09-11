using UnityEngine;
using System.Collections;

public class MovingPlataformController : MonoBehaviour {

	public float speed;
	public Transform pointA; //Tem sempre que ser o que tem os menores valores de Transform
	public Transform pointB; //Tem sempre que ser o que tem os maiores valores de Transform
	float timer;
	bool isGoingBack;
	public bool moving;
	public bool isVertical;
	public LayerMask obstacles;
	public LayerMask player;
	public Transform upperLeft;
	public Transform upperRight;
	public Transform lowerLeft;
	public Transform lowerRight;

	public TileSpriteManager spriteManager;
	public Transform uppermostLeft;
	public Transform uppermostRight;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate()
	{
		if(isVertical)
			transform.localPosition = new Vector3(0, transform.localPosition.y, -5);
		else
			transform.localPosition = new Vector3(transform.localPosition.x, 0, -5);
		if(timer > 0)
		{
			moving = false;
			timer -= Time.deltaTime;
			return;
		}
		else
			timer = 0;
		moving = true;
		if(isGoingBack)
			MovePlataform(-1);
		else
			MovePlataform(1);
		
		CheckDestination();
	}
	void MovePlataform(int i)
	{
		if(isVertical)
		{
			if(isGoingBack && Physics2D.OverlapArea(new Vector2(lowerLeft.position.x, lowerLeft.position.y), new Vector2(lowerRight.position.x, lowerRight.position.y), player))
			{
				timer = 1;
				rigidbody2D.velocity = new Vector2(0, 0);
				return;
			}
			if(isGoingBack && Physics2D.OverlapArea(new Vector2(lowerLeft.position.x, lowerLeft.position.y), new Vector2(lowerRight.position.x, lowerRight.position.y), obstacles))
			{
				timer = 1;
				isGoingBack = false;
				rigidbody2D.velocity = new Vector2(0, 0);
				return;
			}
			if(!isGoingBack)
			{
				if(Physics2D.OverlapArea(new Vector2(upperLeft.position.x, upperLeft.position.y), new Vector2(upperRight.position.x, upperRight.position.y), player))
				{
					if(Physics2D.OverlapArea(new Vector2(uppermostLeft.position.x, uppermostLeft.position.y), new Vector2(uppermostRight.position.x, uppermostRight.position.y), obstacles))
					{
						timer = 1;
						rigidbody2D.velocity = new Vector2(0, 0);
						return;
					}
				}
				else
				{
					if(Physics2D.OverlapArea(new Vector2(upperLeft.position.x, upperLeft.position.y), new Vector2(upperRight.position.x, upperRight.position.y), obstacles))
					{
						timer = 1;
						isGoingBack = true;
						rigidbody2D.velocity = new Vector2(0, 0);
						return;
					}
				}
			}
		}


		if(!isVertical)
		{
			if(isGoingBack && Physics2D.OverlapArea(new Vector2(lowerLeft.position.x, lowerLeft.position.y), new Vector2(lowerRight.position.x, lowerRight.position.y), player))
			{
				timer = 1;
				rigidbody2D.velocity = new Vector2(0, 0);
				return;
			}
			if(isGoingBack && Physics2D.OverlapArea(new Vector2(lowerLeft.position.x, lowerLeft.position.y), new Vector2(lowerRight.position.x, lowerRight.position.y), obstacles))
			{
				timer = 1;
				isGoingBack = false;
				rigidbody2D.velocity = new Vector2(0, 0);
				return;
			}
			if(!isGoingBack)
			{
				if(Physics2D.OverlapArea(new Vector2(upperLeft.position.x, upperLeft.position.y), new Vector2(upperRight.position.x, upperRight.position.y), player))
				{
					timer = 1;
					rigidbody2D.velocity = new Vector2(0, 0);
					return;
				}
				if(Physics2D.OverlapArea(new Vector2(upperLeft.position.x, upperLeft.position.y), new Vector2(upperRight.position.x, upperRight.position.y), obstacles))
				{
					timer = 1;
					isGoingBack = true;
					rigidbody2D.velocity = new Vector2(0, 0);
					return;
				}
			}
		}

		if(isVertical)
			rigidbody2D.velocity = new Vector2(0,speed*i);
		else
			rigidbody2D.velocity = new Vector2(speed*i, 0);
	}
	void CheckDestination()
	{
		if(isGoingBack)
		{
			if(transform.position.x <= pointA.position.x && transform.position.y <= pointA.position.y)
			{
				timer = 2;
				isGoingBack = false;
				rigidbody2D.velocity = new Vector2(0, 0);
			}
		}
		else
		{
			if(transform.position.x >= pointB.position.x && transform.position.y >= pointB.position.y)
			{
				timer = 2;
				isGoingBack = true;
				rigidbody2D.velocity = new Vector2(0, 0);
			}
		}
	}
}
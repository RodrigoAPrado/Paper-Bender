using UnityEngine;
using System.Collections;

public class MovePlayer : MonoBehaviour {

	//Movement
	Vector2 charPosition;
	float mouseClicked;
	float mouseClickedHeight;
	public bool moving;
	float speed;
	[SerializeField]float moveSpeed = 5;
	float[] currentDist;
	int currentDistCounter;
	public Transform obstacleCheck;
	public Transform obstacleDetect;
	public Transform obstacleDetectUpper;
	public Transform obstacleDetectLower;
	public Transform jumpCheck;

	//Jump
	public GameObject currentGround;
	public Transform groundCheck;
	public Transform jumpLeft;
	public Transform jumpMid;
	public Transform jumpRight;
	public Transform jumpHeight;
	public bool grounded;
	public float groundRadius = 0.1f;
	public LayerMask groundLayer;
	[SerializeField]float jumpSpeed = 10;
	bool paperBall;
	public bool flipStart;

	SpriteRenderer bendZoneSprite;
	float opacity;

	//Sprite
	bool facingRight = true;
	public Transform avatarSprite;

	// Use this for initialization
	void Start () {

		bendZoneSprite = GameObject.FindGameObjectWithTag("PlayerBendZone").gameObject.GetComponent<SpriteRenderer>();
		currentDist = new float[2];
		//avatarSprite = GameObject.FindGameObjectWithTag("PlayerSprite").transform;
		if(flipStart)
			Flip();
	}


	void FixedUpdate()
	{
		if(!moving && grounded)
		{
			if(opacity < 1.6F)
			{
				opacity +=  Time.deltaTime;
			}
		}
		else
		{
			if(opacity > 0)
			{
				opacity -=  3 * Time.deltaTime;
			}
			else
			{
				opacity = 0;
			}
		}
		bendZoneSprite.color = new Color(1,1,1,opacity - 1);
		GravityModifier();

		grounded = Physics2D.OverlapCircle(new Vector2(groundCheck.position.x, groundCheck.position.y), groundRadius, groundLayer);
		if(grounded)
		{
			currentGround = Physics2D.OverlapCircle(new Vector2(groundCheck.position.x, groundCheck.position.y), groundRadius, groundLayer).gameObject;
		}
		else
		{
			currentGround = null;
		}
		if(moving)
		{

			rigidbody2D.velocity = new Vector2(moveSpeed * speed, rigidbody2D.velocity.y);
			float dist = mouseClicked - transform.position.x;
			currentDistCounter ++;
			if(currentDistCounter > 1)
			{
				currentDistCounter = 0;
			}
			currentDist[currentDistCounter] = dist;
			if(grounded){
				CheckJump();
			}
			CheckClimb(CheckObstacleHeight());
			if((dist == currentDist[0] && dist == currentDist[1]) || (mouseClicked < transform.position.x && speed > 0) || (mouseClicked > transform.position.x && speed < 0))
			{
				moving = false;
				for(int i = 0; i < 2; i++)
				{
					currentDist[i] = 0;
				}	
			}
			if(grounded && !moving)
			{
				rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
			}
		}
		if(!grounded && !moving && rigidbody2D.velocity.y == 0)
			transform.position = new Vector2(transform.position.x - (speed/10), transform.position.y);
	}
	// Update is called once per frame
	void Update () 
	{
		//print (currentGround.tag);
	}
	void CheckClimb(int jumpHeight)
	{
		if(jumpHeight <= 0)
			return;
		bool canClimb = true;
		obstacleCheck.localPosition = new Vector2 (speed * 0.96f, -0.8f + (jumpHeight * 0.32f));
		for(int i = 0; i < 5; i++)
		{
			if(Physics2D.OverlapCircle(new Vector2(obstacleCheck.position.x, obstacleCheck.position.y), 0.01f, groundLayer))
			{
				canClimb = false;
			}
			else
			{
				obstacleCheck.localPosition = new Vector2 (obstacleCheck.localPosition.x, obstacleCheck.localPosition.y + 0.32f);
			}
		}
		if(jumpHeight != 0 && jumpHeight < 7 && canClimb)
		{
			rigidbody2D.gravityScale = 1;
			//print (obstacleCheck.position);
			//gameObject.transform.position = obstacleCheck.position;
			gameObject.transform.position = new Vector2(obstacleCheck.position.x -	 (speed * 0.32f), gameObject.transform.position.y + (jumpHeight * 0.32f));
			rigidbody2D.velocity = new Vector2(0,0);
		}
		else
		{
			if(jumpHeight <= 0)
			{
				print ("There' is nothing to jump on.");
			}
			if(jumpHeight > 7)
			{
				print ("I'll not climb, this is too high.");
			}
			if(!canClimb)
			{
				print ("There's an obstacle up there, I can't clmb this.");
			}
		}
	}
	void CheckJump()
	{
		jumpCheck.localScale = new Vector2 (speed, jumpCheck.localScale.y);
		/*Physics2D.OverlapArea(new Vector2(jumpLeft.position.x, jumpLeft.position.y), new Vector2(jumpRight.position.x, jumpRight.position.y), groundLayer).collider2D.gameObject == currentGround*/
		if(!Physics2D.OverlapArea(new Vector2(jumpLeft.position.x, jumpLeft.position.y), new Vector2(jumpRight.position.x, jumpRight.position.y), groundLayer))
		{
			if(grounded && mouseClickedHeight > transform.position.y - 0.96f)
			{
				if(speed > 0)
				{
					if(mouseClicked < jumpRight.position.x)
						return;
				}
				else
				{
					if(mouseClicked > jumpRight.position.x)
						return;
				}
				rigidbody2D.gravityScale = 1;
				rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpSpeed);
			}
		}
	}
	int CheckObstacleHeight()
	{
		obstacleCheck.localPosition = new Vector2 (speed * 0.2f, -0.3F);
		obstacleDetect.localScale = new Vector2(speed, obstacleDetect.localScale.y);
		if(Physics2D.OverlapArea(new Vector2(obstacleDetectUpper.position.x, obstacleDetectUpper.position.y), new Vector2(obstacleDetectLower.position.x, obstacleDetectLower.position.y), groundLayer))
		{
			if(Physics2D.OverlapArea(new Vector2(obstacleDetectUpper.position.x, obstacleDetectUpper.position.y), new Vector2(obstacleDetectLower.position.x, obstacleDetectLower.position.y), groundLayer).gameObject == currentGround || (Physics2D.OverlapArea(new Vector2(obstacleDetectUpper.position.x, obstacleDetectUpper.position.y), new Vector2(obstacleDetectLower.position.x, obstacleDetectLower.position.y), groundLayer).gameObject.tag == "MovingPlataform" && Physics2D.OverlapArea(new Vector2(obstacleDetectUpper.position.x, obstacleDetectUpper.position.y), new Vector2(obstacleDetectLower.position.x, obstacleDetectLower.position.y), groundLayer).gameObject.GetComponent<MovingPlataformController>().moving) || (Physics2D.OverlapArea(new Vector2(obstacleDetectUpper.position.x, obstacleDetectUpper.position.y), new Vector2(obstacleDetectLower.position.x, obstacleDetectLower.position.y), groundLayer).gameObject.tag == "Floor" && Physics2D.OverlapArea(new Vector2(obstacleDetectUpper.position.x, obstacleDetectUpper.position.y), new Vector2(obstacleDetectLower.position.x, obstacleDetectLower.position.y), groundLayer).gameObject.transform.eulerAngles.z != 0) || Physics2D.OverlapArea(new Vector2(obstacleDetectUpper.position.x, obstacleDetectUpper.position.y), new Vector2(obstacleDetectLower.position.x, obstacleDetectLower.position.y), groundLayer).gameObject.tag == "DiagonalFloor")
			{
				print ("ok1");
				return 0;
			}
			if(Physics2D.OverlapArea(new Vector2(obstacleDetectUpper.position.x, obstacleDetectUpper.position.y), new Vector2(obstacleDetectLower.position.x, obstacleDetectLower.position.y), groundLayer).gameObject.tag == "PaperBall" && !paperBall)
			{
				print ("ok2");
				return 0;
			}
			else
			{
				print ("ok3");
				paperBall = false;
			}
		/*if(Physics2D.OverlapCircle(new Vector2(obstacleCheck.position.x, obstacleCheck.position.y), 0.64f, groundLayer))
		{
			if(Physics2D.OverlapCircle(new Vector2(obstacleCheck.position.x, obstacleCheck.position.y), 0.64f, groundLayer).gameObject == currentGround || (Physics2D.OverlapCircle(new Vector2(obstacleCheck.position.x, obstacleCheck.position.y), 0.64f, groundLayer).gameObject.tag == "MovingPlataform" && Physics2D.OverlapCircle(new Vector2(obstacleCheck.position.x, obstacleCheck.position.y), 0.64f, groundLayer).gameObject.GetComponent<MovingPlataformController>().moving) || (Physics2D.OverlapCircle(new Vector2(obstacleCheck.position.x, obstacleCheck.position.y), 0.64f, groundLayer).gameObject.tag == "Floor" && Physics2D.OverlapCircle(new Vector2(obstacleCheck.position.x, obstacleCheck.position.y), 0.64f, groundLayer).gameObject.transform.eulerAngles.z != 0))
			{
				return 0;
			}
			if(Physics2D.OverlapCircle(new Vector2(obstacleCheck.position.x, obstacleCheck.position.y), 0.64f, groundLayer).gameObject.tag == "PaperBall" && !paperBall)
			{
				return 0;
			}
			else
			{
				paperBall = false;
			}*/
			obstacleCheck.localPosition = new Vector2 (speed * 0.96f, -0.8f);
			if(speed == -1)
			{
				if(mouseClicked > obstacleCheck.position.x + 0.32f)
					return 0;
			}
			else
			{
				if(mouseClicked < obstacleCheck.position.x - 0.32f)
					return 0;
			}
			//int whileBreaker = 0;
			int obstacleHeight = 0;
			/*while(Physics2D.OverlapCircle(new Vector2(obstacleCheck.position.x, obstacleCheck.position.y), 0.12f, groundLayer))
			{
				obstacleHeight ++;
				obstacleCheck.localPosition = new Vector2 (obstacleCheck.localPosition.x, obstacleCheck.localPosition.y + 0.32f);
				whileBreaker ++;
				if(whileBreaker > 20)
					break;
			}*/
			for (int i = 1; i <= 7; i++)
			{
				if(Physics2D.OverlapCircle(new Vector2(obstacleCheck.position.x, obstacleCheck.position.y),  0.01f, groundLayer))
				{
					obstacleHeight = i;
				}
				obstacleCheck.localPosition = new Vector2 (obstacleCheck.localPosition.x, obstacleCheck.localPosition.y + 0.32f);
			}
			print (obstacleHeight);
			return obstacleHeight;
		}
		else
		{
			return 0;
		}

	}
	void Flip()
	{
		if(Time.timeScale == 0)
		{
			return;
		}
		Vector2 theScale = new Vector2(avatarSprite.localScale.x, avatarSprite.localScale.y);
		theScale.x *= -1;
		avatarSprite.localScale = theScale;
		facingRight = !facingRight;
	}
	void GravityModifier()
	{
		if(grounded && !moving && currentGround.tag != "MovingPlataform" && currentGround.tag != "Seesaw")
		{
			rigidbody2D.velocity = new Vector2(0,0);
			rigidbody2D.gravityScale = 0;
		}
		else
		{
			rigidbody2D.gravityScale = 1;
		}
	}
	public void MoveCharacter(float mouseInfo, float mouseHeight, bool paperBallCheck)
	{
		paperBall = paperBallCheck;
		mouseClickedHeight = mouseHeight;
		mouseClicked = mouseInfo;
		moving = true;
		GravityModifier();
		if(mouseClicked > transform.position.x)
		{
			speed = 1;
			if(!facingRight)
				Flip ();
		}
		else
		{
			speed = -1;
			if(facingRight)
				Flip ();
		}
	}
}

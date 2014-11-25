using UnityEngine;
using System.Collections;

public class MovePlayer : MonoBehaviour {

    public LayerMask ballLayer;

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
	bool dontJump;
	public Transform heightToJump;
	bool jumpedOnce;
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
	bool adjustJumpSpeed;

	SpriteRenderer bendZoneSprite;
	float opacity;

	//Sprite
	bool facingRight = true;
	public Transform avatarSprite;

	public bool bending;
	bool bended;
	float normalSpeed;
	PaperBendB paperToBend;

	//Animations
	[SerializeField] Animator anim;
	float animationCounter;
	bool jumping;
	float jumpCounter;
	NPCDialogController nPC;
	public GameObject jumpParticle;
	public GameObject fallParticle;
	bool fallTrigger;
	public GameObject staffParticle;
	bool staffTrigger;
	public GameObject bendParticle;
	// Use this for initialization
	void Start () {
		nPC = GameObject.FindGameObjectWithTag("ChatBox").GetComponent<NPCDialogController>();
		normalSpeed = 5;
		bendZoneSprite = GameObject.FindGameObjectWithTag("PlayerBendZone").gameObject.GetComponent<SpriteRenderer>();
		currentDist = new float[2];
		//avatarSprite = GameObject.FindGameObjectWithTag("PlayerSprite").transform;
		anim = avatarSprite.GetComponent<Animator>();
		/*if(flipStart)
			Flip();*/
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



		anim.SetBool("Moving", moving);

		grounded = Physics2D.OverlapCircle(new Vector2(groundCheck.position.x, groundCheck.position.y), groundRadius, groundLayer);
		if(grounded)
		{
			currentGround = Physics2D.OverlapCircle(new Vector2(groundCheck.position.x, groundCheck.position.y), groundRadius, groundLayer).gameObject;
		}
		else
		{
			fallTrigger = true;
			currentGround = null;
		}
		if(fallTrigger && grounded)
		{
			fallTrigger = false;
			Vector3[] fallParticleRotation = new Vector3[2];
			fallParticleRotation[0] = new Vector3(0, -90, 90);
			fallParticleRotation[1] = new Vector3(0, 90, -90);
			int j = 0;
			for(float i = -0.2f; i< 0.5f; i += 0.4f)
			{
				GameObject fallParticleActive = GameObject.Instantiate(fallParticle, new Vector2(groundCheck.position.x, groundCheck.position.y), fallParticle.transform.rotation) as GameObject;
				fallParticleActive.transform.eulerAngles = fallParticleRotation[j];
				fallParticleActive.GetComponent<ParticleSystem>().particleSystem.renderer.sortingLayerName = "Particles";
				fallParticleActive.GetComponent<ParticleSystem>().particleSystem.startRotation = Random.Range(0,89) + i*90;
				fallParticleActive.GetComponent<ParticleSystem>().particleSystem.startSize = Random.Range(0.7f,0.9f);
				GameObject.Destroy(fallParticleActive, 4);
				j++;
			}
		}
		if(moving)
		{
			if(jumpedOnce && !adjustJumpSpeed)
			{
				float distance = transform.position.x - mouseClicked;
				if(distance < 0)
				{
					distance *= -1;
				}
				distance = distance/0.64f;
				if(distance > 10)
					distance = 10;
				moveSpeed = 1 + (distance * 0.4f);
				adjustJumpSpeed = true;
			}
			if(!adjustJumpSpeed || grounded)
				moveSpeed = normalSpeed;
			rigidbody2D.velocity = new Vector2(moveSpeed * speed, rigidbody2D.velocity.y);
			float dist = mouseClicked - transform.position.x;
			currentDistCounter ++;
			if(currentDistCounter > 1)
			{
				currentDistCounter = 0;
			}
			currentDist[currentDistCounter] = dist;
			if(grounded)
			{
				adjustJumpSpeed = false;
				if(!dontJump)
					CheckJump();
			}
			if(!dontJump)
				CheckClimb(CheckObstacleHeight());
			if((dist == currentDist[0] && dist == currentDist[1]) || (mouseClicked < transform.position.x && speed > 0) || (mouseClicked > transform.position.x && speed < 0))
			{
				moving = false;
				jumpedOnce = false;
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
		if(bending)
		{
			if(anim.GetCurrentAnimatorStateInfo(0).IsName("Priest_StaffEnd") && !bended)
			{
				paperToBend.BendPaper();
				bended = true;
				anim.SetBool("Bend", false);
			}
			if(anim.GetCurrentAnimatorStateInfo(0).IsName("Priest_Idle") && animationCounter <= 0)
			{
				bending = false;
				bended = false;
				dontJump = false;
				jumpedOnce = false;
				adjustJumpSpeed = false;
				staffTrigger = false;
			}
		}
		if(animationCounter > 0)
		{
			animationCounter -= Time.deltaTime;
			if(animationCounter < 0)
			{
				animationCounter = 0;
			}
			if(animationCounter < 0.2f && !staffTrigger)
			{
				GameObject staffParticleActive = GameObject.Instantiate(staffParticle, new Vector2(groundCheck.position.x + (0.6f * speed), groundCheck.position.y + 1.43f), staffParticle.transform.rotation) as GameObject;
				staffParticleActive.GetComponent<ParticleSystem>().particleSystem.renderer.sortingLayerName = "Particles";
				GameObject.Destroy(staffParticleActive, 2);
                staffTrigger = true;

                //RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 1, ballLayer);

				GameObject bendParticleActive = GameObject.Instantiate(bendParticle, new Vector2(paperToBend.transform.position.x, paperToBend.transform.position.y), bendParticle.transform.rotation) as GameObject;
				bendParticleActive.GetComponent<ParticleSystem>().particleSystem.renderer.sortingLayerName = "Particles";
				GameObject.Destroy(bendParticleActive, 3);
				//paperToBend.transform.position;
			}
		}
	}
	// Update is called once per frame
	void Update () 
	{
		if(nPC.onChat || Time.timeScale == 0)
		{
			moving = false;
			jumpedOnce = false;
		}
		if(jumping)
		{
			
			if(jumpCounter > 0.5f)
			{
				if(grounded)
				{
					jumping = false;
					jumpCounter = 0;
				}
			}
			else
			{
				jumpCounter += Time.deltaTime;
			}
		}

		if(jumping)
		{
			anim.SetBool("Jump", true);
		}
		if((rigidbody2D.velocity.y < 0 && jumping) || (!grounded && !jumping))
		{
			anim.SetBool("Fall", true);
		}
		if(grounded)
		{
			anim.SetBool("Jump", false);
			anim.SetBool("Fall", false);
		}
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
				//print ("There' is nothing to jump on.");
			}
			if(jumpHeight > 7)
			{
				//print ("I'll not climb, this is too high.");
			}
			if(!canClimb)
			{
				//print ("There's an obstacle up there, I can't clmb this.");
			}
		}
	}
	void CheckJump()
	{
		jumpCheck.localScale = new Vector2 (speed, jumpCheck.localScale.y);
		/*Physics2D.OverlapArea(new Vector2(jumpLeft.position.x, jumpLeft.position.y), new Vector2(jumpRight.position.x, jumpRight.position.y), groundLayer).collider2D.gameObject == currentGround*/
		if(!jumpedOnce && !Physics2D.OverlapArea(new Vector2(jumpLeft.position.x, jumpLeft.position.y), new Vector2(jumpRight.position.x, jumpRight.position.y), groundLayer))
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
				jumpedOnce = true;
				Vector3[] jumpParticleRotation = new Vector3[3];
				jumpParticleRotation[0] = new Vector3(60, -90, 90);
				jumpParticleRotation[1] = new Vector3(90, 180, 0);
				jumpParticleRotation[2] = new Vector3(60, 90, -90);
				int j = 0;
				for(float i = -0.6f; i< 1; i += 0.6f)
				{
					GameObject jumpParticleActive = GameObject.Instantiate(jumpParticle, new Vector2(groundCheck.position.x + i, groundCheck.position.y + 0.4f), jumpParticle.transform.rotation) as GameObject;
					jumpParticleActive.transform.eulerAngles = jumpParticleRotation[j];
					jumpParticleActive.GetComponent<ParticleSystem>().particleSystem.renderer.sortingLayerName = "Particles";
					jumpParticleActive.GetComponent<ParticleSystem>().particleSystem.startRotation = Random.Range(0,89) + i*90;
					jumpParticleActive.GetComponent<ParticleSystem>().particleSystem.startSize = Random.Range(0.7f,0.9f);
					GameObject.Destroy(jumpParticleActive, 4);
					j++;
				}
			}
			return;
		}
		if(mouseClickedHeight > heightToJump.position.y + 0.32f && !jumpedOnce)
		{
            float distanceMouseClicked = mouseClicked - transform.position.x;
            if (distanceMouseClicked < 0)
                distanceMouseClicked *= -1;
            if (distanceMouseClicked > 3)
                return;
			rigidbody2D.gravityScale = 1;
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpSpeed);
			jumpedOnce = true;
			jumping = true;
			Vector3[] jumpParticleRotation = new Vector3[3];
			jumpParticleRotation[0] = new Vector3(60, -90, 90);
			jumpParticleRotation[1] = new Vector3(90, 180, 0);
			jumpParticleRotation[2] = new Vector3(60, 90, -90);
			int j = 0;
			for(float i = -0.6f; i< 1; i += 0.6f)
			{
				GameObject jumpParticleActive = GameObject.Instantiate(jumpParticle, new Vector2(groundCheck.position.x + i, groundCheck.position.y + 0.4f), jumpParticle.transform.rotation) as GameObject;
				jumpParticleActive.transform.eulerAngles = jumpParticleRotation[j];
				jumpParticleActive.GetComponent<ParticleSystem>().particleSystem.renderer.sortingLayerName = "Particles";
				jumpParticleActive.GetComponent<ParticleSystem>().particleSystem.startRotation = Random.Range(0,89) + i*90;
				jumpParticleActive.GetComponent<ParticleSystem>().particleSystem.startSize = Random.Range(0.7f,0.9f);
				GameObject.Destroy(jumpParticleActive, 4);
				j++;
			}

			return;
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
				//print ("ok1");
				return 0;
			}
			if(Physics2D.OverlapArea(new Vector2(obstacleDetectUpper.position.x, obstacleDetectUpper.position.y), new Vector2(obstacleDetectLower.position.x, obstacleDetectLower.position.y), groundLayer).gameObject.tag == "PaperBall" && !paperBall)
			{
				//print ("ok2");
				return 0;
			}
			else
			{
				//print ("ok3");
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
			//print (obstacleHeight);
			return obstacleHeight;
		}
		else
		{
			return 0;
		}

	}
	public void Flip()
	{
		if(nPC != null){
			if(Time.timeScale == 0 || nPC.onChat)
			{
				return;
			}
		}
		Vector2 theScale = new Vector2(avatarSprite.localScale.x, avatarSprite.localScale.y);
		theScale.x *= -1;
		avatarSprite.localScale = theScale;
		facingRight = !facingRight;
	}
	void GravityModifier()
	{
		if(grounded && !moving && currentGround.tag != "MovingPlataform" && currentGround.tag != "Seesaw" && currentGround.tag != "Floor")
		{
			rigidbody2D.velocity = new Vector2(0,0);
			rigidbody2D.gravityScale = 0;
		}
		else
		{
			rigidbody2D.gravityScale = 1;
		}
	}
	public void BendAnimation(PaperBendB pB)
	{
		//print ("bend");
		if(pB.canBend){
			paperToBend = pB;
			moving = false;
			rigidbody2D.velocity = new Vector2(0,0);
			dontJump = true;
			bending = true;
			anim.SetBool("Bend", true);
			animationCounter = 0.3f;

		}
	}
	public void MoveCharacter(float mouseInfo, float mouseHeight, bool paperBallCheck)
	{
		paperBall = paperBallCheck;
		mouseClickedHeight = mouseHeight;
		mouseClicked = mouseInfo;
		moving = true;
		GravityModifier();
		if(mouseClicked > transform.position.x + 0.32f)
		{
			speed = 1;
			if(!facingRight)
				Flip ();
		}
		if(mouseClicked < transform.position.x - 0.32f)
		{
			speed = -1;
			if(facingRight)
				Flip ();
		}
	}
}

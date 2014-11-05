	using UnityEngine;
using System.Collections;

public class BanditControllerNeo : MonoBehaviour {


	//public Transform[] wayPoints;
	//public bool[] wayPointsStop;

	/*Para identificar a layer de um wayPoints, usar quando chegar perto 
	 * de um waypoint para detectar se este eh o waypoint destino*/
	public LayerMask wayPointsLayer;

	/*Layer que determina o que o forasteiro ira considerar
	 quando estiver procurando pelo jogador. Ele precisa considerar
	 tambem os obstaculos que o impedem de ver um jogador, mas ele nao
	 precisa e nem deve considerar outras layers*/
	public LayerMask playerObstacleLayer;

	/*Layer especifica do player. Usada para notar se o player ainda esta
	 seguindo o forasteiro. Nao usada em outros casos. Usar a variavel de cima
	 em outros casos.*/
	public LayerMask playerLayer;

	/*Na verdade este tambem eh o waypoint destino. Usada em conjunto com a
	 layer de waypoints para detectar se este eh o waypoint em que deve parar*/
	public Transform currentWayPoint;

	Transform lastWayPoint;

	/*Guarda o script "BanditWayPointNeo" do currentWayPoint*/
	BanditWayPointNeo bWP;

	/*Modificador De Velocidade de Pulo. Modificar no Editor*/
	public float jumpSpeedMod;

	/*Modificador de Velocidade normal. Modificar no Editor*/
	public float speedMod;

	/*Velocidade, dita a direçao*/
	float speed;

	/*Bool que dita a direçao*/
	bool left;

	/*Bool que dita se a direçao ja foi escolhida*/
	bool detectSide;

	/*Bool que dita se o personagem ja chegou no destino*/
	bool isOnStart;

	/*Bool que dita se o personagem esta andando*/
	bool walk;

	/*Int que guarda altura, acho que nao precisa*/
	int storedHeight;
	/*Marcador para nao detectar o chao enquanto o personagem pula. As vezes da merda e tem q usar isso*/
	float jumpCounter;
	/*Velocidade base do pulo. Baseada na altura do obstaculo*/
	float jumpSpeed;

	/*Area de visao do Forasteiro*/
	[SerializeField] float range;

	/*Transform para checar os WayPoints. Fica bem embaixo do forasteiro e esta marcado como WPC.*/
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
	float distanceJumpSpeed;

	/*Pulos*/
	bool doJump;
	bool doDistanceJump;
	
	bool distanceJump;
	//
	/*Necessario para decidir para onde correr*/
	Transform player;

	/*Animacao*/
	GameObject charAvatar;
	[SerializeField] Animator anim;
	bool scared;
	bool isFacingRight;

	/*Serve para parar o Forasteiro se tiver algum bug. Ele detecta se a posicao do foraseiro nao muda
	 enquanto ele anda. Como deveria mudar, ele para o Forasteiro automaticamente. Vou ver de fazer
	 com que o Forasteiro volte para o ultimo WayPoint, mas duvido que eu va fazer isso*/
	Vector2[] currentPosition;
	int positionIndex = 0;

	float timer = 0;

	float debugTimmer;

	public AudioClip scaredSound;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").transform;
		bWP = currentWayPoint.GetComponent<BanditWayPointNeo>();
		charAvatar = transform.FindChild("Avatar").gameObject;
		anim = charAvatar.GetComponent<Animator>();
		currentPosition = new Vector2[3];
		lastWayPoint = null;
		isFacingRight = true;
		//walk = true;
		//isOnStart = true;
	}
	void AnimationControl()
	{
		if(grounded)
		{
			if(walk)
				anim.SetBool("Walk", true);
			else
				anim.SetBool("Walk", false);
		}
		if(scared)
			anim.SetBool("Scared", true);
		else
			anim.SetBool("Scared", false);

		if(!grounded)
			anim.SetBool("Air", true);
		else
			anim.SetBool("Air", false);
		if(jumping && rigidbody2D.velocity.y > 0)
			anim.SetBool("Jump", true);
		else
			anim.SetBool("Jump", false);
		if(walk && !scared)
		{
			if(!left && !isFacingRight)
				Flip ();
			if(left && isFacingRight)
				Flip ();
		}
		if(scared)
		{
			if(player.position.x > transform.position.x && !isFacingRight)
			{
				Flip();
			}
			if(player.position.x < transform.position.x && isFacingRight)
			{
				Flip();
			}
		}
	}
	void Flip()
	{
		if(Time.timeScale == 0)
		{
			return;
		}
		Vector2 theScale = new Vector2(charAvatar.transform.localScale.x, charAvatar.transform.localScale.y);
		theScale.x *= -1;
		charAvatar.transform.localScale = theScale;
		isFacingRight = !isFacingRight;
	}
	// Update is called once per frame
	void Update () {
		AnimationControl();
		if(doJump && !jumping)
		{
			Jump();
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
		if(scared)
		{
			if(anim.GetCurrentAnimatorStateInfo(0).IsName("Bandit_Run"))
				scared = false;
		}
		if(isOnStart)
		{
			debugTimmer = 0;
		}
		if(walk)
		{
			if(scared)
				return;
			/*if(!detectSide)
			{
				if(transform.position.x > currentWayPoint.position.x)
					left = true;
				else
					left = false;
				detectSide = true;
			}*/
			if(left)
				speed = -1;
			else
				speed = 1;
			if(debugTimmer < 8)
			{
				debugTimmer += Time.deltaTime;
			}
			else
			{
				transform.position = new Vector2(currentWayPoint.position.x, currentWayPoint.position.y + 1);
				debugTimmer = 0;
			}
			if(speed == -1)
			{
				if(transform.position.x < currentWayPoint.position.x)
				{
					speed = 0;
				}
				if(transform.position.x < currentWayPoint.position.x - 0.16)
				{
					transform.position = new Vector2(currentWayPoint.position.x, currentWayPoint.position.y + 1);
				}
			}
			else
			{
				if(transform.position.x > currentWayPoint.position.x)
				{
					speed = 0;
				}
				if(transform.position.x > currentWayPoint.position.x + 0.16)
				{
					transform.position = currentWayPoint.position;
				}
			}
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
						//detectSide = false;
						if(bWP.dontStopHere)
						{
							debugTimmer = 0;
							/*if(bWP.autoNextWayPoint != null)
							{
								if(bWP.jumpHere)
								{
									Jump(true);
								}
								currentWayPoint = bWP.autoNextWayPoint;
								bWP = currentWayPoint.GetComponent<BanditWayPoint>();
							}*/
							int index = bWP.CheckHeightIndex(left, lastWayPoint);
							if(index < 0)
								index = bWP.CheckHeightIndex(!left, lastWayPoint);
							doJump = bWP.doesWayPointNeedJump[index];
							doDistanceJump = bWP.isTheJumpADistanceJump[index];
							storedHeight = bWP.CheckHeight(index);
							left = bWP.isWayPointLeft[index];
							distanceJumpSpeed = bWP.wayPointDistanceForce[index];
							lastWayPoint = currentWayPoint;
							currentWayPoint = bWP.wayPoints[index];
							bWP = currentWayPoint.GetComponent<BanditWayPointNeo>();
							walk = true;
							isOnStart = true;
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
				groundSpeed = distanceJumpSpeed;
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
	void Jump()
	{
		/*if(bWP.autoNextWayPoint.position.y > currentWayPoint.position.y)
		{
			float dif = bWP.autoNextWayPoint.position.y - currentWayPoint.position.y;
			jumpSpeed = dif * jumpSpeedMod;
		}*/
		if(scared)
			return;
		if(jumping)
			return;

		int height = storedHeight;
		if(height <= 0)
		{
			storedHeight = 0;
			doJump = false;
			doDistanceJump = false;
			return;
		}
		jumpSpeed = height * jumpSpeedMod;
		if(doDistanceJump)
		{
			distanceJump = true;
			if(distanceJumpSpeed <= 0)
			{
				distanceJumpSpeed = 5;
			}
		}
		rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpSpeed);
		jumping = true;
		doJump = false;
		doDistanceJump = false;
		storedHeight = 0;
	}
	void DetectPlayer()
	{
		if(!grounded || jumping)
			return;
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
				/*	int height = bWP.wayPointRight.GetComponent<BanditWayPoint>().CheckHeight(true);

					if(height < 7)
						currentWayPoint = bWP.wayPointRight;
					else
						currentWayPoint = bWP.wayPointLeft;*/
					scared = true;
					audio.PlayOneShot(scaredSound);
					DecideWayPoint(false);
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
				/*	int height = bWP.wayPointLeft.GetComponent<BanditWayPoint>().CheckHeight(false);
					if(height < 7)
						currentWayPoint = bWP.wayPointLeft;
					else
						currentWayPoint = bWP.wayPointRight;
						*/
					scared = true;
					audio.PlayOneShot(scaredSound);
					DecideWayPoint(true);
					return;
				}
			}

		}
	}
	void DecideWayPoint(bool playerIsLeft)
	{

		int index = -1;
		index = bWP.CheckHeightIndex(playerIsLeft, lastWayPoint);
		if(index < 0)
			index = bWP.CheckHeightIndex(!playerIsLeft, lastWayPoint);
		doJump = bWP.doesWayPointNeedJump[index];
		doDistanceJump = bWP.isTheJumpADistanceJump[index];
		storedHeight = bWP.CheckHeight(index);
		left = bWP.isWayPointLeft[index];
		distanceJumpSpeed = bWP.wayPointDistanceForce[index];
		lastWayPoint = currentWayPoint;
		currentWayPoint = bWP.wayPoints[index];
		bWP = currentWayPoint.GetComponent<BanditWayPointNeo>();
		walk = true;
		isOnStart = true;
		return;
		/*if(left)
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
		}*/
	}
	/*int ChooseWayPoint(Transform[] wayPoints, bool side)
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
	}*/
}

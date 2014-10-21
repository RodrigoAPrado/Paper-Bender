using UnityEngine;
using System.Collections;

public class SeesawController : MonoBehaviour {
	
	public Transform seesawMiddle;
	bool playerOnTop;
	Transform playerTransform;
	MovePlayer movePlayer;
	public float rotationSpeed;
	
	public float autoRotationSpeed;
	
	public LayerMask groundLayer;
	public LayerMask detectionLayer;
	public Transform leftGroundDetector;
	public Transform leftGroundDetectorUpper;
	public Transform rightGroundDetector;
	public Transform rightGroundDetectorUpper;
	public int forcedSide;
	public Transform[] collidersDetecter;
	public float[] positionSpeedModifier;
	public Collider2D[] collidersDetected;
	public bool rotate;
	public Transform[] groundDetector;
	
	// Use this for initialization
	void Start () {
		playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
		movePlayer = playerTransform.gameObject.GetComponent<MovePlayer>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		if(rotate)
		{
			rotationSpeed = autoRotationSpeed;
			Rotating();
			return;
		}
		DetectColliders();
		/*if(Physics2D.OverlapCircleNonAlloc(new Vector2(left3.position.x, left3.position.y), 0.5f, collidersDetected, detectionLayer) > 0)
		{
			print ("Left 3");
		}*/
		/*if(Physics2D.OverlapCircle(new Vector2(left3.position.x, left3.position.y), 0.5f, detectionLayer))
		{
			print ("Left 3");
		}
		else
		{
			if(Physics2D.OverlapCircle(new Vector2(left2.position.x, left2.position.y), 0.5f, detectionLayer))
			{
				print ("Left 2");
			}
			else
			{
				if(Physics2D.OverlapCircle(new Vector2(left1.position.x, left1.position.y), 0.5f, detectionLayer))
				{
					print ("Left 1");
				}
			}
		}*/
		
		//return;
		/*if(movePlayer.currentGround == this.gameObject)
			playerOnTop = true;
		else
			playerOnTop = false;*/
		switch(forcedSide)
		{
		case 0:
			PlayerPhysics();
			break;
		default:
			ForcedPhysics();
			break;
		}
	}
	void DetectColliders()
	{
		rotationSpeed = 0;
		for(int i = 0; i < collidersDetecter.Length; i++)
		{
			collidersDetected = Physics2D.OverlapCircleAll(new Vector2(collidersDetecter[i].position.x, collidersDetecter[i].position.y), 0.4f, detectionLayer);
			for(int j = 0; j< collidersDetected.Length; j++)
			{
				if(collidersDetected[j].tag =="Player")
				{
					rotationSpeed += positionSpeedModifier[i];
				}
				if(collidersDetected[j].tag =="PaperBall")
				{
					rotationSpeed += positionSpeedModifier[i] * 3;
				}
			}
		}
		//collidersDetected = Physics2D.OverlapCircleAll(new Vector2(left3.position.x, left3.position.y), 0.5f, detectionLayer);
	}
	void ForcedPhysics()
	{
		if(forcedSide > 1)
		{
			if(!Physics2D.OverlapCircle(leftGroundDetector.position, 0.1f, groundLayer) && !Physics2D.OverlapCircle(rightGroundDetectorUpper.position, 0.1f, groundLayer))
			{
				transform.Rotate(new Vector3(0,0, -1));
			}
		}
		else
		{
			if(!Physics2D.OverlapCircle(rightGroundDetector.position, 0.1f, groundLayer) && !Physics2D.OverlapCircle(leftGroundDetectorUpper.position, 0.1f, groundLayer))
			{
				transform.Rotate(new Vector3(0,0,1));
			}
		}
	}
	void PlayerPhysics()
	{
		if(rotationSpeed > 0)
		{
			if(	!Physics2D.OverlapCircle(leftGroundDetector.position, 0.1f, groundLayer) && !Physics2D.OverlapCircle(rightGroundDetectorUpper.position, 0.1f, groundLayer))
			{
				transform.Rotate(new Vector3(0,0,rotationSpeed));
			}		
		}
		else
		{
			if(!Physics2D.OverlapCircle(rightGroundDetector.position, 0.1f, groundLayer) && !Physics2D.OverlapCircle(leftGroundDetectorUpper.position, 0.1f, groundLayer))
			{
				transform.Rotate(new Vector3(0,0,rotationSpeed));
			}
		}
		/*if(playerOnTop)
		{
			if(playerTransform.position.x < seesawMiddle.position.x)
			{
				if(Physics2D.OverlapCircle(new Vector2(leftGroundDetector.position.x, leftGroundDetector.position.y), 0.1f, groundLayer) == false)
				{
					transform.Rotate(new Vector3(0,0,rotationSpeed));
				}		
			}
			else
			{
				if(Physics2D.OverlapCircle(new Vector2(rightGroundDetector.position.x, rightGroundDetector.position.y), 0.1f, groundLayer) == false)
				{
					transform.Rotate(new Vector3(0,0,-rotationSpeed));
				}
			}
		}
		else
		{

		}*/
	}
	void Rotating()
	{
		for(int i = 0; i < groundDetector.Length; i++)
		{
			if(Physics2D.OverlapCircle(new Vector2(groundDetector[i].position.x, groundDetector[i].position.y), 0.1f, groundLayer))
				return;
		}
		transform.Rotate(new Vector3(0,0,rotationSpeed));
	}
}

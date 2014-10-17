using UnityEngine;
using System.Collections;

public class SheepController : MonoBehaviour {

	Transform playerDetect;
	Transform groundCheck;
	bool follow;
	bool walk;
	bool grounded;
	public LayerMask playerLayer;
	public LayerMask groundLayer;

	Transform player;
	MovePlayer mP;
	bool waitForIt;
	bool jumping;

	float speed;
	public float movingSpeed;
	[SerializeField] float stopRadius;
	[SerializeField] float advanceRadius;
	[SerializeField] float followRadius;
	[SerializeField] float groundRadius = 0.1f;

	// Use this for initialization
	void Start () {
		groundCheck = transform.FindChild("GroundCheck").transform;
		player = GameObject.FindGameObjectWithTag("Player").transform;
		playerDetect = transform.FindChild("CharDetectStop").transform;
		mP = player.GetComponent<MovePlayer>();
	}
	
	// Update is called once per frame
	void Update () {
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);
		if(!mP.grounded && !waitForIt)
		{
			waitForIt = true;
		}
		if(waitForIt)
		{
			if(mP.grounded && grounded)
			{

			}
		}
		if(grounded)
		{
			if(walk)
			{
				if(Physics2D.OverlapCircle(playerDetect.position, stopRadius, playerLayer) || waitForIt)
				{
					walk = false;
					return;
				}
			}
			if(follow)
			{
				if(!Physics2D.OverlapCircle(playerDetect.position, advanceRadius, playerLayer))
				{
					walk = true;
				}
				if(!Physics2D.OverlapCircle(playerDetect.position, followRadius, playerLayer))
				{
					follow = false;
				}
			}
			else
			{
				walk  = false;
				if(Physics2D.OverlapCircle(playerDetect.position, stopRadius, playerLayer))
				{
					follow = true;
				}
			}
		}
		if(!walk)
			speed = 0;
		rigidbody2D.velocity = new Vector2(speed * movingSpeed, rigidbody2D.velocity.y);
	}
}

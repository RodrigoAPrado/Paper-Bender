using UnityEngine;
using System.Collections;

public class PositionDetect : MonoBehaviour {

	// Use this for initialization

	float mousex;
	float mousey;
	public Vector2 mouseposition;
	public Vector2 charposition;
	Transform character;
	MovePlayer movePlayer;
	GameObject targetSprite;
	public LayerMask groundLayer;
	public LayerMask bendingLayer;
	public LayerMask ballLayer;
	bool paperBall;

	public bool halt;
	public bool firstFollow;

	public Vector3[] cameraPositions;
		

	void Start () {
		targetSprite = GameObject.FindGameObjectWithTag("TargetSprite").gameObject;
		character = GameObject.FindGameObjectWithTag("Player").transform;
		movePlayer = character.gameObject.GetComponent<MovePlayer>();
		Camera.main.transform.position = new Vector3(cameraPositions[0].x, cameraPositions[0].y, -10);
		Camera.main.orthographicSize = cameraPositions[0].z;
	}
	
	// Update is called once per frame
	void Update () {

		if(Time.timeScale == 0)
		{
			return;
		}

		if(halt)
		{
			return;
		}
		if(movePlayer.moving)
			targetSprite.SetActive(true);
		else
			targetSprite.SetActive(false);

		mousex = (Input.mousePosition.x);
		mousey = (Input.mousePosition.y);
		mouseposition = Camera.main.ScreenToWorldPoint(new Vector2 (mousex,mousey));
		charposition = new Vector2 (character.position.x, character.position.y);

		paperBall = false;

		if(Input.GetMouseButtonDown(0))
		{
			if(!movePlayer.grounded || movePlayer.bending)
				return;

			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 1, bendingLayer);
			if(hit.collider != null)
			{
				if(hit.collider.tag=="PaperBend" && !movePlayer.moving)
				{
					hit.collider.gameObject.GetComponent<PaperBend>().BendPaper();
					return;
				}
				if(hit.collider.tag=="PaperBendB")
				{
					//hit.collider.gameObject.GetComponent<PaperBendB>().BendPaper();
					movePlayer.BendAnimation(hit.collider.gameObject.GetComponent<PaperBendB>());
					return;
				}
			}
			hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 1, ballLayer);
			if(hit.collider != null)
			{
				paperBall = true;
			}
			targetSprite.transform.position = new Vector3(mouseposition.x, charposition.y, targetSprite.transform.position.z);
			if(Physics2D.OverlapCircle(new Vector2(targetSprite.transform.position.x, targetSprite.transform.position.y - 0.35f), 0.2f, groundLayer))
				TargetSpritePosition();
			else
				TargetSpritePositionDown();
			movePlayer.MoveCharacter(mouseposition.x, mouseposition.y, paperBall);
		}
	}
	void TargetSpritePosition()
	{
		int whileBreaker = 0;
		while(Physics2D.OverlapCircle(new Vector2(targetSprite.transform.position.x, targetSprite.transform.position.y - 0.35f), 0.2f, groundLayer))
		{
			targetSprite.transform.position = new Vector2(targetSprite.transform.position.x, targetSprite.transform.position.y + 0.1f);
			whileBreaker += 1;
			if(whileBreaker > 100)
				break;
		}
	}
	void TargetSpritePositionDown()
	{
		int whileBreaker = 0;
		while(!Physics2D.OverlapCircle(new Vector2(targetSprite.transform.position.x, targetSprite.transform.position.y - 0.35f), 0.2f, groundLayer))
		{
			targetSprite.transform.position = new Vector2(targetSprite.transform.position.x, targetSprite.transform.position.y - 0.1f);
			whileBreaker += 1;
			if(whileBreaker > 50)
				break;
		}
	}
}

using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour {

	public bool doorOpen;
	public bool moving;
	bool moveStart;
	public GameObject doorOpenned;
	public GameObject doorClosed;
	public GameObject doorMoving;
	[SerializeField] float speedMod;
	float speed;
	[SerializeField] bool horizontal;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(moving)
		{
			doorOpenned.SetActive(false);
			doorClosed.SetActive(false);
			doorMoving.SetActive(true);
			if(!moveStart)
			{
				if(doorOpen)
					doorMoving.transform.position = doorClosed.transform.position;
				else
					doorMoving.transform.position = doorOpenned.transform.position;
				moveStart = true;
			}
			if(horizontal)
			{

			}
			else
			{
				if(doorOpen)
				{
					if(doorOpenned.transform.position.y > doorClosed.transform.position.y)
					{
						speed = 1;
						if(doorMoving.transform.position.y > doorOpenned.transform.position.y)
						{
							moving = false;
							moveStart = false;
						}
					}
					else
					{
						speed = -1;
						if(doorMoving.transform.position.y < doorOpenned.transform.position.y)
						{
							moving = false;
							moveStart = false;
						}
					}
					doorMoving.rigidbody2D.velocity = new Vector2(0, speed * speedMod * Time.deltaTime);
				}
				if(!doorOpen)
				{
					if(doorOpenned.transform.position.y > doorClosed.transform.position.y)
					{
						speed = -1;
						if(doorMoving.transform.position.y < doorClosed.transform.position.y)
						{
							moving = false;
							moveStart = false;
						}
					}
					else
					{
						speed = 1;
						if(doorMoving.transform.position.y > doorClosed.transform.position.y)
						{
							moving = false;
							moveStart = false;
						}
					}
					doorMoving.rigidbody2D.velocity = new Vector2(0, speed * speedMod * Time.deltaTime);
				}
			}
		}
		else
		{
			doorOpenned.SetActive(false);
			doorClosed.SetActive(false);
			doorMoving.SetActive(false);
			if(doorOpen)
				doorOpenned.SetActive(true);
			else
				doorClosed.SetActive(true);

		}
	}
}

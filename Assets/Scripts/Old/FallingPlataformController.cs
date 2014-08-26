using UnityEngine;
using System.Collections;

public class FallingPlataformController : MonoBehaviour {
	
	public float timer;
	public float maxTimer;
	bool startCountDown;
	public Transform upperLeft;
	public Transform lowerRight;
	public LayerMask weight;
	bool fell;
	// Use this for initialization
	void Start () {
		timer = maxTimer;
	}
	
	// Update is called once per frame
	void Update () {

	}
	void FixedUpdate()
	{
		if(Physics2D.OverlapArea(new Vector2(upperLeft.position.x, upperLeft.position.y), new Vector2(lowerRight.position.x, lowerRight.position.y), weight))
			startCountDown = true;
		if(startCountDown && timer > 0)
			timer -= 1 * Time.deltaTime;
		if(timer < 0 && !fell)
		{
			GetComponent<Rigidbody2D>().isKinematic = false;
			GetComponent<Rigidbody2D>().fixedAngle = false;
			fell = true;
			return;
		}
		if(fell)
		{
			/*if(rigidbody2D.velocity.y == 0 && rigidbody2D.velocity.x == 0)
			{
				GetComponent<Rigidbody2D>().isKinematic = true;
				GetComponent<Rigidbody2D>().fixedAngle = true;
				this.enabled = false;
			}*/
		}
	}
}

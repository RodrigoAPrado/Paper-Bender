using UnityEngine;
using System.Collections;

public class FallingPlataformController : MonoBehaviour {
	
	public float timer;
	public float maxTimer;
	bool startCountDown;
	[SerializeField] Transform upperLeft;
	[SerializeField] Transform lowerRight;
	[SerializeField] Transform endLeft;
	[SerializeField] Transform endRight;
	public GameObject toReplace;
	public LayerMask weight;
	public LayerMask endLayer;
	public float maxWeight;
	bool fell;
	Collider2D[] collidersDetected;
	float currentWeight;

	// Use this for initialization
	void Start () {
		timer = maxTimer;
	}
	
	// Update is called once per frame
	void Update () {

	}
	void FixedUpdate()
	{
		currentWeight = 0;
		if(!startCountDown)
		{
			collidersDetected = Physics2D.OverlapAreaAll(upperLeft.position, lowerRight.position, weight);
			for(int i = 0; i < collidersDetected.Length; i++)
			{
				if(collidersDetected[i].tag == "Player" || collidersDetected[i].tag == "Bandit")
				{
					currentWeight += 2;
				}
				if(collidersDetected[i].tag == "Floor")
				{
					currentWeight += 10;
				}
			}
		}
		/*if(Physics2D.OverlapArea(new Vector2(upperLeft.position.x, upperLeft.position.y), new Vector2(lowerRight.position.x, lowerRight.position.y), weight))*/
		if(currentWeight > maxWeight)
			startCountDown = true;
		if(startCountDown && timer > 0)
			timer -= 1 * Time.deltaTime;
		if(timer < 0 && !fell)
		{
			GetComponent<Rigidbody2D>().isKinematic = false;
			fell = true;
			return;
		}
		if(fell)
		{
			if(Physics2D.OverlapArea(endLeft.position, endRight.position, endLayer))
			{
				if(toReplace != null)
				{
					toReplace.gameObject.SetActive(true);
					gameObject.SetActive(false);
				}
			}
			/*if(rigidbody2D.velocity.y == 0 && rigidbody2D.velocity.x == 0)
			{
				GetComponent<Rigidbody2D>().isKinematic = true;
				GetComponent<Rigidbody2D>().fixedAngle = true;
				this.enabled = false;
			}*/
		}
	}
}

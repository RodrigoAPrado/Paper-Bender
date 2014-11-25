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
	public GameObject fallParticle;

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
					Vector3[] fallParticleRotation = new Vector3[2];
					fallParticleRotation[0] = new Vector3(0, -90, 90);
					fallParticleRotation[1] = new Vector3(0, 90, -90);
					int j = 0;
					for(float i = -0.2f; i< 0.5f; i += 0.4f)
					{
						GameObject fallParticleActive = GameObject.Instantiate(fallParticle, new Vector2(transform.position.x, endLeft.position.y), fallParticle.transform.rotation) as GameObject;
						print (fallParticleActive);
						fallParticleActive.transform.eulerAngles = fallParticleRotation[j];
						fallParticleActive.GetComponent<ParticleSystem>().particleSystem.renderer.sortingLayerName = "Particles";
						fallParticleActive.GetComponent<ParticleSystem>().particleSystem.startRotation = Random.Range(0,89) + i*90;
						fallParticleActive.GetComponent<ParticleSystem>().particleSystem.startSize = Random.Range(0.9f,1.1f);
						GameObject.Destroy(fallParticleActive, 4);
						j++;
					}
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

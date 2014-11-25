using UnityEngine;
using System.Collections;

public class ParticleDustEmitter : MonoBehaviour {

	public bool isPaperBall;
	public LayerMask floorLayer;
	bool grounded;
	bool groundedCheck;
	public GameObject fallParticle;
	float timer;
	// Use this for initialization
	void Start () {
		//particleSystem.renderer.sortingLayerName = "Particles";
	}
	
	// Update is called once per frame
	void Update () {
		if(timer < 2)
		{
			timer += Time.deltaTime;
			return;
		}
		//print (transform.parent.gameObject.rigidbody2D.velocity.x);
		if(isPaperBall)
		{
			grounded = Physics2D.OverlapCircle(transform.position, 0.3f, floorLayer);
			if(!grounded && !groundedCheck)
			{
				if(!particleSystem.isStopped)
				{
					particleSystem.Stop();
					return;
				}
				groundedCheck = true;
			}
			if(groundedCheck)
			{
				if(grounded)
				{
					groundedCheck = false;
					Vector3[] fallParticleRotation = new Vector3[2];
					fallParticleRotation[0] = new Vector3(0, -90, 90);
					fallParticleRotation[1] = new Vector3(0, 90, -90);
					int j = 0;
					for(float i = -0.2f; i< 0.5f; i += 0.4f)
					{
						GameObject fallParticleActive = GameObject.Instantiate(fallParticle, new Vector2(transform.position.x, transform.position.y), fallParticle.transform.rotation) as GameObject;
						print (fallParticleActive);
						fallParticleActive.transform.eulerAngles = fallParticleRotation[j];
						fallParticleActive.GetComponent<ParticleSystem>().particleSystem.renderer.sortingLayerName = "Particles";
						fallParticleActive.GetComponent<ParticleSystem>().particleSystem.startRotation = Random.Range(0,89) + i*90;
						fallParticleActive.GetComponent<ParticleSystem>().particleSystem.startSize = Random.Range(0.7f,0.9f);
						GameObject.Destroy(fallParticleActive, 4);
						j++;
					}

				}
			}
			if(transform.parent.gameObject.rigidbody2D.velocity.x > -0.3 && transform.parent.gameObject.rigidbody2D.velocity.x < 0.3 )
			{
				if(!particleSystem.isStopped)
				{
					particleSystem.Stop();
					return;
				}
			}
			else
			{
				if(!particleSystem.isPlaying)
				{
					particleSystem.Play();
				}
			}
			float speed = 0;
			if(transform.parent.gameObject.rigidbody2D.velocity.x < 0)
				speed = transform.parent.gameObject.rigidbody2D.velocity.x * -1;
			else
				speed = transform.parent.gameObject.rigidbody2D.velocity.x;

			particleSystem.emissionRate = speed * 5;

			float mod = 0;
			if(transform.parent.gameObject.rigidbody2D.velocity.x > 0)
			{
				mod = 70;
			}
			else
			{
				mod = -70;
			}
			transform.position = new Vector2(transform.parent.transform.position.x + (mod/170), transform.parent.transform.position.y - 0.55f);
			float positionZ = transform.parent.transform.localEulerAngles.z * -1;

			transform.localEulerAngles = new Vector3(0,0, positionZ + mod);

		}
	}
}

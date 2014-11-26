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
	public ParticleSystem doorParticle;
	public ParticleSystem doorParticleDust;
	public GameObject fallParticle;
	public Transform doorDustClosePosition;
	public AudioClip audioOpen;
	// Use this for initialization
	void Start () {
		if(doorParticle != null)
		{
			doorParticle.renderer.sortingLayerName = "Particles";
		}
		if(doorParticleDust != null)
		{
			doorParticleDust.renderer.sortingLayerName = "Particles";
		}

		gameObject.AddComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if(moving)
		{
			if(doorParticle != null)
			{
				if(!doorParticle.isPlaying)
				{
					doorParticle.Play();

					if(audioOpen != null)
					{
	
						GetComponent<AudioSource>().loop = true;
						audio.PlayOneShot(audioOpen);
					}
				}
			}
			if(doorParticleDust != null)
			{
				if(!doorParticleDust.isPlaying)
					doorParticleDust.Play();
			}
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
				if(doorOpen)
				{
					if(doorOpenned.transform.position.x > doorClosed.transform.position.x)
					{
						speed = 1;
						if(doorMoving.transform.position.x > doorOpenned.transform.position.x)
						{
							moving = false;
							moveStart = false;
						}
					}
					else
					{
						speed = -1;
						if(doorMoving.transform.position.x < doorOpenned.transform.position.x)
						{
							moving = false;
							moveStart = false;
						}
					}
					doorMoving.rigidbody2D.velocity = new Vector2(speed * speedMod * Time.deltaTime, 0);
				}
				if(!doorOpen)
				{
					if(doorOpenned.transform.position.x > doorClosed.transform.position.x)
					{
						speed = -1;
						if(doorMoving.transform.position.x < doorClosed.transform.position.x)
						{
							moving = false;
							moveStart = false;
							if(fallParticle != null && doorDustClosePosition != null)
							{
								Vector3[] fallParticleRotation = new Vector3[2];
								fallParticleRotation[0] = new Vector3(0, -90, 90);
								fallParticleRotation[1] = new Vector3(0, 90, -90);
								int j = 0;
								for(float i = -0.2f; i< 0.5f; i += 0.4f)
								{
									GameObject fallParticleActive = GameObject.Instantiate(fallParticle, new Vector2(doorDustClosePosition.position.x, doorDustClosePosition.position.y), fallParticle.transform.rotation) as GameObject;
									fallParticleActive.transform.eulerAngles = fallParticleRotation[j];
									fallParticleActive.GetComponent<ParticleSystem>().particleSystem.renderer.sortingLayerName = "Particles";
									fallParticleActive.GetComponent<ParticleSystem>().particleSystem.startRotation = Random.Range(0,89) + i*90;
									fallParticleActive.GetComponent<ParticleSystem>().particleSystem.startSize = Random.Range(1.1f,1.3f);
									GameObject.Destroy(fallParticleActive, 4);
									j++;
								}
							}
						}
					}
					else
					{
						speed = 1;
						if(doorMoving.transform.position.x > doorClosed.transform.position.x)
						{
							moving = false;
							moveStart = false;
							if(fallParticle != null && doorDustClosePosition != null)
							{
								Vector3[] fallParticleRotation = new Vector3[2];
								fallParticleRotation[0] = new Vector3(0, -90, 90);
								fallParticleRotation[1] = new Vector3(0, 90, -90);
								int j = 0;
								for(float i = -0.2f; i< 0.5f; i += 0.4f)
								{
									GameObject fallParticleActive = GameObject.Instantiate(fallParticle, new Vector2(doorDustClosePosition.position.x, doorDustClosePosition.position.y), fallParticle.transform.rotation) as GameObject;
									fallParticleActive.transform.eulerAngles = fallParticleRotation[j];
									fallParticleActive.GetComponent<ParticleSystem>().particleSystem.renderer.sortingLayerName = "Particles";
									fallParticleActive.GetComponent<ParticleSystem>().particleSystem.startRotation = Random.Range(0,89) + i*90;
									fallParticleActive.GetComponent<ParticleSystem>().particleSystem.startSize = Random.Range(1.1f,1.3f);
									GameObject.Destroy(fallParticleActive, 4);
									j++;
								}
							}
						}
					}
					doorMoving.rigidbody2D.velocity = new Vector2(speed * speedMod * Time.deltaTime, 0);
				}
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
							if(fallParticle != null && doorDustClosePosition != null)
							{
								Vector3[] fallParticleRotation = new Vector3[2];
								fallParticleRotation[0] = new Vector3(0, -90, 90);
								fallParticleRotation[1] = new Vector3(0, 90, -90);
								int j = 0;
								for(float i = -0.2f; i< 0.5f; i += 0.4f)
								{
									GameObject fallParticleActive = GameObject.Instantiate(fallParticle, new Vector2(doorDustClosePosition.position.x, doorDustClosePosition.position.y), fallParticle.transform.rotation) as GameObject;
									fallParticleActive.transform.eulerAngles = fallParticleRotation[j];
									fallParticleActive.GetComponent<ParticleSystem>().particleSystem.renderer.sortingLayerName = "Particles";
									fallParticleActive.GetComponent<ParticleSystem>().particleSystem.startRotation = Random.Range(0,89) + i*90;
									fallParticleActive.GetComponent<ParticleSystem>().particleSystem.startSize = Random.Range(1.1f,1.3f);
									GameObject.Destroy(fallParticleActive, 4);
									j++;
								}
							}
						}
					}
					else
					{
						speed = 1;
						if(doorMoving.transform.position.y > doorClosed.transform.position.y)
						{
							moving = false;
							moveStart = false;
							if(fallParticle != null && doorDustClosePosition != null)
							{
								Vector3[] fallParticleRotation = new Vector3[2];
								fallParticleRotation[0] = new Vector3(0, -90, 90);
								fallParticleRotation[1] = new Vector3(0, 90, -90);
								int j = 0;
								for(float i = -0.2f; i< 0.5f; i += 0.4f)
								{
									GameObject fallParticleActive = GameObject.Instantiate(fallParticle, new Vector2(doorDustClosePosition.position.x, doorDustClosePosition.position.y), fallParticle.transform.rotation) as GameObject;
									fallParticleActive.transform.eulerAngles = fallParticleRotation[j];
									fallParticleActive.GetComponent<ParticleSystem>().particleSystem.renderer.sortingLayerName = "Particles";
									fallParticleActive.GetComponent<ParticleSystem>().particleSystem.startRotation = Random.Range(0,89) + i*90;
									fallParticleActive.GetComponent<ParticleSystem>().particleSystem.startSize = Random.Range(1.1f,1.3f);
									GameObject.Destroy(fallParticleActive, 4);
									j++;
								}
							}
						}
					}
					doorMoving.rigidbody2D.velocity = new Vector2(0, speed * speedMod * Time.deltaTime);
				}
			}
		}
		else
		{
			if(doorParticle != null)
			{
				if(!doorParticle.isStopped)
				{
					doorParticle.Stop();
					audio.Stop();

				}
			}
			if(doorParticleDust != null)
			{
				if(!doorParticleDust.isStopped)
					doorParticleDust.Stop();
			}
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

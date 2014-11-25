using UnityEngine;
using System.Collections;

public class ParticleDustEmitter : MonoBehaviour {

	public bool isPaperBall;
	public LayerMask floorLayer;
	// Use this for initialization
	void Start () {
		//particleSystem.renderer.sortingLayerName = "Particles";
	}
	
	// Update is called once per frame
	void Update () {
		//print (transform.parent.gameObject.rigidbody2D.velocity.x);
		if(isPaperBall)
		{
			if(!Physics2D.OverlapCircle(transform.position, 0.3f, floorLayer))
			{
				if(!particleSystem.isStopped)
				{
					particleSystem.Stop();
					return;
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

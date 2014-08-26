using UnityEngine;
using System.Collections;

public class RotatingPlataform : MonoBehaviour {

	public float rotationSpeed;
	public LayerMask groundLayer;
	public Transform[] groundDetector;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Rotating();
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
	
using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	bool move;
	float xN;
	float yN;
	float sizeN;
	[SerializeField] bool followChar;
	bool stage3Follow;
	Transform character;
	public int currentCameraPosition;
	// Use this for initialization
	void Start () {
		character = GameObject.FindGameObjectWithTag("Player").transform;
		if(followChar)
			sizeN = 5;
	}
	
	// Update is called once per frame
	void Update () {

		if(stage3Follow)
		{
			camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, 5, 5 * Time.deltaTime);
			transform.position = Vector3.Lerp(transform.position, new Vector3(75, character.position.y, -10), 5 * Time.deltaTime);
		}
		if(followChar)
		{
			camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, sizeN, 5 * Time.deltaTime);
			transform.position = Vector3.Lerp(transform.position, new Vector3(character.position.x, character.position.y, -10), 5 * Time.deltaTime);
		}
		if(move && !followChar && !stage3Follow)
		{
			transform.position = Vector3.Lerp(transform.position, new Vector3(xN, yN, -10), 5 * Time.deltaTime);
			camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, sizeN, 5 * Time.deltaTime);
			if(transform.position.y == yN && transform.position.x == xN && camera.orthographicSize == sizeN)
				move = false;
		}
	}
	public void MoveCamera(float x, float y, float size, bool follow, bool s3F, int cameraPos)
	{
		currentCameraPosition = cameraPos;
		stage3Follow = s3F;
		if(stage3Follow)
		{
			followChar = false;
			return;
		}
		followChar = follow;
		sizeN = size;
		if(followChar)
			return;
		xN = x;
		yN = y;
		move = true;


		//print ("X = " + x + " / Y = " + y + " / Size = " + size);
		/**/
	}
}

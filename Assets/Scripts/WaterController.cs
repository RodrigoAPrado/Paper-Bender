using UnityEngine;
using System.Collections;

public class WaterController : MonoBehaviour {

	public SpriteRenderer[] sprites;
	[SerializeField] float opacity;
	public LayerMask playerLayer;
	[SerializeField] Transform left;
	[SerializeField] Transform right;
	[SerializeField] Transform teleportBack;
	Transform player;
	// Use this for initialization
	void Start () {
		for(int i = 0; i < sprites.Length; i++)
		{
			sprites[i].color = new Color(1,1,1,opacity);
		}
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		if(Physics2D.OverlapArea(left.position, right.position, playerLayer))
		{
			player.position = teleportBack.position;
			player.GetComponent<MovePlayer>().moving = false;
		}
	}
}

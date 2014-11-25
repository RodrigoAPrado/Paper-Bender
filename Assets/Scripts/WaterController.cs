using UnityEngine;
using System.Collections;

public class WaterController : MonoBehaviour {

	public SpriteRenderer[] sprites;
	[SerializeField] float opacity;
	public LayerMask playerLayer;
	[SerializeField] Transform left;
	[SerializeField] Transform right;
	[SerializeField] Transform teleportBack;
	public Transform particleHeight;
	public ParticleSystem particleWater;
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
			if(particleWater != null && particleHeight != null)
			{
				GameObject particleWaterActive = GameObject.Instantiate(particleWater, new Vector2(player.position.x, particleHeight.position.y), particleWater.transform.rotation) as GameObject;
				GameObject.Destroy(particleWaterActive, 4);
			}
			player.position = teleportBack.position;
			player.GetComponent<MovePlayer>().moving = false;
		}
	}
}

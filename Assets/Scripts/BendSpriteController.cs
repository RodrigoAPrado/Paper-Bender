using UnityEngine;
using System.Collections;

public class BendSpriteController : MonoBehaviour {

	public SpriteRenderer[] sprites;
	float Calpha;
	bool reduce;
	float timer;
	// Use this for initialization
	void Start () {
	
	}
	void Awake()
	{
		Calpha = 0;
		reduce = false;
		timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(timer > 0)
		{
			timer -= Time.deltaTime*3;
			return;
		}
		if(reduce)
		{
			if(Calpha <= 0)
				reduce = false;
			else
				Calpha -= Time.deltaTime;
		}
		else
		{
			if(Calpha >= 1)
			{
				reduce = true;
				timer = 2;
			}
			else
			{
				Calpha += Time.deltaTime;
			}
		}
		for(int i = 0; i < sprites.Length; i++)
		{
			sprites[i].color = new Color(1, 1, 1, Calpha);
		}
	}
}

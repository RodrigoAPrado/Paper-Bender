using UnityEngine;
using System.Collections;

public class Spritetype : MonoBehaviour {

	public enum worldSelect
	{
		Blank,
		Village,
		Temple,
		Grasslands,
		Wastelands,
		Mountain,
		Swamp
	}
	public Sprite[] sprites;
	public worldSelect world = worldSelect.Blank;
	// Use this for initialization
	void Start () {
		switch(world)
		{
		case worldSelect.Blank:
			gameObject.GetComponent<SpriteRenderer>().sprite = sprites[0];
			break;
		case worldSelect.Village:
			gameObject.GetComponent<SpriteRenderer>().sprite = sprites[1];
			break;
		case worldSelect.Temple:
			gameObject.GetComponent<SpriteRenderer>().sprite = sprites[2];
			break;
		case worldSelect.Grasslands:
			gameObject.GetComponent<SpriteRenderer>().sprite = sprites[3];
			break;
		case worldSelect.Wastelands:
			gameObject.GetComponent<SpriteRenderer>().sprite = sprites[4];
			break;
		case worldSelect.Mountain:
			gameObject.GetComponent<SpriteRenderer>().sprite = sprites[5];
			break;
		case worldSelect.Swamp:
			gameObject.GetComponent<SpriteRenderer>().sprite = sprites[6];
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

using UnityEngine;
using System.Collections;

public class Spritetypefull : MonoBehaviour {


	public enum worldSelect
	{
		Blank,
		Tempou,
		Wasteland,
		Swampland,
		TempleGray,
		TempleWood,
		TempleBrown,
		TempleEarth,
		WastelandWood,
		WastelandRock,
		WastelandEarth,
		SwampLeaf,
		SwampTree,
		SwampWood,
		SwampEarth
	}
	
	public TileSpriteManager spriteManager;
	public enum linha
	{
		Primeira,
		Segunda,
		Terceira,
		Quarta
	}
	public enum coluna
	{
		Primeira,
		Segunda,
		Terceira,
		Quarta
	}
	public coluna colunaIndex;
	public linha linhaIndex;
	int tileIndex = 0;
	public worldSelect world = worldSelect.Blank;
	// Use this for initialization
	void Start () {
		switch(colunaIndex)
		{
		case coluna.Primeira:
			tileIndex += 1;
			break;
		case coluna.Segunda:
			tileIndex += 2;
			break;
		case coluna.Terceira:
			tileIndex += 3;
			break;
		case coluna.Quarta:
			tileIndex += 4;
			break;
		}
		switch(linhaIndex)
		{
		case linha.Segunda:
			tileIndex += 4;
			break;
		case linha.Terceira:
			tileIndex += 8;
			break;
		case linha.Quarta:
			tileIndex += 12;
			break;
		}
		switch(world)
		{
		case worldSelect.Blank:
			tileIndex = 0;
		break;
		case worldSelect.Wasteland:
			tileIndex += 16;
			break;
		case worldSelect.Swampland:
			tileIndex += 32;
			break;
		case worldSelect.TempleGray:
			tileIndex += 48;
			break;
		case worldSelect.TempleWood:
			tileIndex += 64;
			break;
		case worldSelect.TempleBrown:
			tileIndex += 80;
			break;
		case worldSelect.TempleEarth:
			tileIndex += 96;
			break;
		case worldSelect.WastelandWood:
			tileIndex += 112;
			break;		
		case worldSelect.WastelandRock:
			tileIndex += 128;
			break;
		case worldSelect.WastelandEarth:
			tileIndex += 144;
			break;
		case worldSelect.SwampLeaf:
			tileIndex += 160;
			break;
		case worldSelect.SwampTree:
			tileIndex += 176;
			break;
		case worldSelect.SwampWood:
			tileIndex += 192;
			break;
		case worldSelect.SwampEarth:
			tileIndex += 208;
			break;
		}
		if(spriteManager.sprites[tileIndex] == null || tileIndex >= spriteManager.sprites.Length)
			tileIndex = 0;
		//gameObject.GetComponent<SpriteRenderer>().sprite = sprites[tileIndex];
		gameObject.GetComponent<SpriteRenderer>().sprite = spriteManager.sprites[tileIndex];

		/*switch(world)
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
		}*/
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

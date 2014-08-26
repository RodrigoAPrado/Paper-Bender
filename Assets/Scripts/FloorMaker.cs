using UnityEngine;
using System.Collections;

public class FloorMaker : MonoBehaviour {
	
	public GameObject[] tiles;
	public float tileSize;
	public Vector3[] floorMap;
	// Use this for initialization
	void Start () {
		CreateFloors();
	}
	void CreateFloors()
	{
		for(int i = 0; i < floorMap.Length; i++)
		{
			int tileType = (int)floorMap[i].z - 1;
			if(tileType < 0 || tileType >= tiles.Length)
			{
				break;
			}
			GameObject tile = GameObject.Instantiate(tiles[tileType],new Vector2(tileSize * floorMap[i].x,tileSize * floorMap[i].y), tiles[tileType].transform.rotation) as GameObject;
		}
	}
	// Update is called once per frame
}

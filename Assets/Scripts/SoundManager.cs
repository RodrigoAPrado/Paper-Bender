using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
	
	public IndoorSoundManager[] iSM;
	bool indoor;
	public AudioSource indoorMusic;
	public AudioSource outdoorMusic;
	float volumeOutDoor;
	public float musicEnd;
	[SerializeField] bool startsIndoor;
	// Use this for initialization
	void Start () {
		if(startsIndoor)
			volumeOutDoor = 0;
		else
			volumeOutDoor = 1;
		indoorMusic.volume = volumeOutDoor;
		//outdoorMusic.time = 80;
	}
	
	// Update is called once per frame
	void Update () {
//		print("IndoorMusic: " + indoorMusic.time);
//		print("OutdoorMusic: " + outdoorMusic.time);
		/*if(outdoorMusic.time >= musicEnd)
		{
			outdoorMusic.time = 0;
			indoorMusic.time = 0;
		}*/
		if(indoorMusic.time > outdoorMusic.time + 0.5f || indoorMusic.time < outdoorMusic.time - 0.5f)
		{
			indoorMusic.time = outdoorMusic.time;
		}
		if(!indoor)
		{
			if(volumeOutDoor < 1)
			{
				volumeOutDoor += Time.deltaTime * 2;
			}
			else
			{
				volumeOutDoor = 1;
			}
		}
		else
		{
			if(volumeOutDoor > 0)
			{
				volumeOutDoor -= Time.deltaTime * 2;
			}
			else
			{
				volumeOutDoor = 0;
			}
		}
		indoorMusic.volume = volumeOutDoor;
		for(int i = 0; i < iSM.Length; i++)
		{
			if(iSM[i].indoors)
			{
				indoor = true;
				return;
			}
		}
		indoor = false;
	}
}

using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
	
	public IndoorSoundManager[] iSM;
	bool indoor;
	public AudioSource indoorMusic;
	public AudioSource outdoorMusic;
	public float maxVol = 1;
	public float volumeOutDoor;
	public float musicEnd;
	[SerializeField] bool startsIndoor;
	// Use this for initialization
	void Start () {

		if(ES2.Exists("MusicVol"))
		{
			maxVol = ES2.Load<float>("MusicVol");
		}

		if(startsIndoor)
			volumeOutDoor = 0;
		else
			volumeOutDoor = maxVol;
		indoorMusic.volume = volumeOutDoor;
		//outdoorMusic.time = 80;
	}

	public void ChangeVolume(float vol)
	{
		maxVol = vol;

		Object[] SourcesList = FindObjectsOfType<AudioSource>();
		
		for(int i = 0; i< SourcesList.Length; i++)
		{
			AudioSource currSource = SourcesList[i] as AudioSource;
			
			currSource.volume = vol;
		}

		ES2.Save (vol,"MusicVol");
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
			if(volumeOutDoor < maxVol)
			{
				volumeOutDoor += Time.deltaTime * 2;
			}
			else
			{
				volumeOutDoor = maxVol;
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

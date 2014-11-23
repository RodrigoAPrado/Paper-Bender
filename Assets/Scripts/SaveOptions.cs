using UnityEngine;
using System.Collections;

public class SaveOptions : MonoBehaviour {

	void Start()
	{
		if(ES2.Exists("MusicVol"))
			FindObjectOfType<UIScrollBar>().value = ES2.Load<float>("MusicVol");
	}

	public void SetCurrentMusicVolume ()
	{
		if (UIProgressBar.current != null)
		{
//			UILabel text = Mathf.RoundToInt(UIProgressBar.current.value * 100f) + "%";

//			UILabel
			Debug.Log("MUSIC VOL: " + UIProgressBar.current.value);

			if(FindObjectOfType<SoundManager>())
				FindObjectOfType<SoundManager>().ChangeVolume(UIProgressBar.current.value);

			Object[] SourcesList = FindObjectsOfType<AudioSource>();

			for(int i = 0; i< SourcesList.Length; i++)
			{
				AudioSource currSource = SourcesList[i] as AudioSource;

				currSource.volume = UIProgressBar.current.value;
			}

			ES2.Save (UIProgressBar.current.value,"MusicVol");
		}
	}
}

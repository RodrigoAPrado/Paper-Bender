using UnityEngine;
using System.Collections;

public class SoundManagerMenu : MonoBehaviour {

	public void PlaySound(AudioClip snd)
	{
		audio.PlayOneShot(snd);
	}
}

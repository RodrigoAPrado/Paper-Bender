using UnityEngine;
using System.Collections;

public class ClickSound : MonoBehaviour {

	public AudioClip clickSound;


	// Update is called once per frame
	void OnClick () {
		FindObjectOfType<SoundManagerMenu>().PlaySound(clickSound);
	}
}

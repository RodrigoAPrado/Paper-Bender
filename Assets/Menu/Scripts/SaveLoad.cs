using UnityEngine;
using System.Collections;

public class SaveLoad : MonoBehaviour {

	public int thisSave;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnClick()
	{
		if(ES2.Load<bool>("file" + thisSave.ToString() + ".txt?tag=init"))
		{
			print ("save");
		}
		else
		{
			print ("empty");
		}
	}
}

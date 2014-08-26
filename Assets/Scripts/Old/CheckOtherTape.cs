using UnityEngine;
using System.Collections;

public class CheckOtherTape : MonoBehaviour {


	public MasterPaper otherMP;
	public PaperBend thisPaperBend;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(otherMP.bended && otherMP.tape > 0)
		{
			thisPaperBend.ignoreTime = true;
		}
		else
		{
			thisPaperBend.ignoreTime = false;
		}
	}
}

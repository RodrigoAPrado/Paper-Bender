using UnityEngine;
using System.Collections;

public class _Control : MonoBehaviour 
{
    public GameObject buttonContinue;
    public GameObject buttonNewGame;
    public bool isThereAContinue = false;

    public bool isPort = false;
    public bool isEng = true;
    public int musicVol;
    public int sfxVol;


	void Start () 
    {
	    if (!isThereAContinue)
        {
            buttonNewGame.transform.position = buttonContinue.transform.position;
            buttonContinue.SetActive(false);
        }
	}
	
	void Update () 
    {
	    
	}
}

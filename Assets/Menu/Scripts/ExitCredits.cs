using UnityEngine;
using System.Collections;

public class ExitCredits : MonoBehaviour 
{
    public Transform buttons;
    public Transform title;
    public Transform credits;
    public Transform creditsBack;

	// Use this for initialization
	void Start () 
    {
        
	}
	
	// Update is called once per frame
	void Update () 
    {
        
	}

    public void OnClick()
    {
        credits.gameObject.SetActive(false);
        buttons.gameObject.SetActive(true);
        title.gameObject.SetActive(true);
        creditsBack.gameObject.SetActive(false);
    }
}

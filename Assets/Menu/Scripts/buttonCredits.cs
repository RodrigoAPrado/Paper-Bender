using UnityEngine;
using System.Collections;

public class buttonCredits : MonoBehaviour 
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
        title.gameObject.SetActive(false);
        credits.gameObject.SetActive(true);
        creditsBack.gameObject.SetActive(true); 
        buttons.gameObject.SetActive(false);
    }

}

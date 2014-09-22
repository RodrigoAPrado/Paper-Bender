using UnityEngine;
using System.Collections;

public class TheFuckingEnd : MonoBehaviour 
{
    public GameObject msgLevelClear;
    public bool msgActivate = false;
    public bool iniciarCountdown = false;
    public float count = 3;

	void Start () 
    {
	
	}
	
	void Update () 
    {
        if (iniciarCountdown)
            EndGame();
	}

    void OnTriggerEnter2D()
    {
        iniciarCountdown = true;
    }
    
    void EndGame()
    {
        if (!msgActivate)
        {
            msgActivate = !msgActivate;
            msgLevelClear.SetActive(true);
        }
        count -= Time.deltaTime;
        if (count < 0)
        {
            Application.LoadLevel("Level_Select");
        }
    }
}

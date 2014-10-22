using UnityEngine;
using System.Collections;

public class ClearBandit : MonoBehaviour 
{
    public GameObject bandidao;

	void Start () 
    {
	
	}
	
	void Update () 
    {
        if (bandidao == null)
        { 
            print("ACABOOOOU");
            Application.LoadLevel("Level_Select");
        }
	}
}

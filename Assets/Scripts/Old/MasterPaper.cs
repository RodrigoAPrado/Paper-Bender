using UnityEngine;
using System.Collections;

public class MasterPaper : MonoBehaviour {

	public int tape;
	public bool bended;
	public GameObject bendedSprite;
	public GameObject notBendedSprite;
	// Use this for initialization
	void Start () {
		if(bended)
		{
			bendedSprite.SetActive(true);
			notBendedSprite.SetActive(false);
		}
		else
		{
			notBendedSprite.SetActive(true);
			bendedSprite.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void BendPaper()
	{
		bended = !bended;
		if(bended)
		{
			bendedSprite.SetActive(true);
			notBendedSprite.SetActive(false);
		}
		else
		{
			bendedSprite.SetActive(false);
			notBendedSprite.SetActive(true);
		}
	}
}

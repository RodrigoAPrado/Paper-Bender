using UnityEngine;
using System.Collections;

public class ActivateOnClick : MonoBehaviour 
{
    public GameObject target;
	public int thisWorld;
	public bool locked;
	public Texture available;
	public Texture unavailable;
	GameObject levelSelect;
	GameObject confirmScreen;
	UITexture worldImage;

	void Start () 
    {
		levelSelect = transform.parent.gameObject;
		confirmScreen = transform.parent.transform.parent.FindChild("Confirm").gameObject;
		worldImage = transform.FindChild("Texture").GetComponent<UITexture>();
		if(locked)
		{
			worldImage.mainTexture = unavailable;
			worldImage.color = new Color(1,1,1,0.5f);
		}
		else
		{
			worldImage.mainTexture = available;
			worldImage.color = new Color(1,1,1,1);
		}
	}
	
	void Update () 
    {
	    
	}

    void OnClick()
    {
		switch(thisWorld)
		{
		case 0:
			print ("tempou");
		break;
		case 1:
			print ("grass");
		break;
		case 2:
			print ("waste");
		break;
		case 3:
			print ("mountain");
		break;
		case 4:
			print ("suamp");
		break;
		}
		//int stageLoader = ES2.Load<int>("currentSave.txt");
		ES2.Save(thisWorld, "currentWorldSelected.txt");
		levelSelect.SetActive(false);
		confirmScreen.SetActive(true);
    }
}

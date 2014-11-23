using UnityEngine;
using System.Collections;

public class ChangeScene : MonoBehaviour {

	string nextStage;
	bool change;
	public bool begin;
	public GameObject blankScreen;
	SpriteRenderer bSRenderer;
	float opacity;
	float timer;
	// Use this for initialization
	void Start () {
		if(blankScreen != null)
		{
			blankScreen.GetComponent<SpriteRenderer>().enabled = true;
			bSRenderer = blankScreen.GetComponent<SpriteRenderer>();

		}
		opacity = 1;
		timer = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if(!begin)
		{
			if(blankScreen != null)
			{
				bSRenderer.color = new Color(1,1,1,opacity);
				if(timer > 0)
				{
					timer -= Time.deltaTime;
				}
				else{
					if(opacity > 0)
					{
						opacity -= Time.deltaTime;
					}
					else
					{
                        begin = true;
                        if (GameObject.Find("UI Button Pause") != null)
                            Destroy(GameObject.Find("UI Button Pause"));

                        GameObject pauseUI = GameObject.Instantiate(Resources.Load("UI Button Pause") as GameObject, new Vector3(6666, 6666, 0), new Quaternion(0, 0, 0, 0)) as GameObject;
                        pauseUI.name = "UI Button Pause";
						opacity = 0;
					}
				}
			}
			else
			{
				begin = true;
			}
		}

		if(change == true)
		{

			if(blankScreen != null)
			{

				bSRenderer.color = new Color(1,1,1,opacity);
				if(opacity < 1)
				{
					opacity += Time.deltaTime;
				}
				else
				{
					Application.LoadLevel(nextStage);
				}
			}
			else
			{
				Application.LoadLevel(nextStage);
			}
		}
	}
	public void ChangeThisScene(string s)
	{
		nextStage = s;
		change = true;
		opacity = 0;
	}
}

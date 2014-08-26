using UnityEngine;
using System.Collections;

public class btn_Pause : MonoBehaviour 
{
    Transform sprt_Pause;
    Transform sprt_Play;

    Transform btn_Restart;
    Transform btn_Quit;
	Transform btn_pause;

    bool isPaused = false;

    public LayerMask lyr_btn_restart;
    public LayerMask lyr_btn_quit;

    int loadLevel;
    float pointX;
    float pointY;
    public Transform player;
	bool changePlayerPosition;

	void Start ()
    {
        DontDestroyOnLoad(transform.gameObject);
		btn_pause = GameObject.FindGameObjectWithTag("Pause").transform;
		btn_pause.GetComponent<SpriteRenderer>().color = new Color (1,1,1,0.4f);
		btn_pause.gameObject.SetActive(false);
		player = GameObject.FindGameObjectWithTag("Player").transform;
        /*sprt_Pause = GameObject.Find("btn_Pause").transform.FindChild("sprite_pause");
        sprt_Play = GameObject.Find("btn_Pause").transform.FindChild("sprite_play");
        sprt_Play.gameObject.SetActive(false);
		btn_pause = GameObject.Find("btn_Pause").transform;
        btn_Restart = GameObject.Find("btn_Restart").transform;
        btn_Quit = GameObject.Find("btn_Quit").transform;
        btn_Restart.gameObject.SetActive(false);
        btn_Quit.gameObject.SetActive(false);*/
	}
	
	void Update () 
    {
		
		player = GameObject.FindGameObjectWithTag("Player").transform;
		if(changePlayerPosition)
		{
			player.position = new Vector2(pointX, pointY);
			changePlayerPosition = false;
		}
		if(Input.GetKeyDown(KeyCode.P))
		{
			ClickBtnPause();
			return;
		}
		if(Input.GetKeyDown(KeyCode.R))
		{
			Time.timeScale = 1;
			isPaused = false;
			btn_pause.gameObject.SetActive(false);
			loadLevel = PlayerPrefs.GetInt("currentStage");
			pointX = PlayerPrefs.GetFloat("pointX");
			pointY = PlayerPrefs.GetFloat("pointY");
			Application.LoadLevel(loadLevel);
			changePlayerPosition = true;
			return;
		}
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			isPaused = false;
			Time.timeScale = 1;
			Application.LoadLevel(0);
			GameObject.Destroy(this);
		}
		/*RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 1, lyr_btn_pause);
        if (hit.collider != null)
        {
            if (Input.GetMouseButtonUp(0))
                ClickBtnPause();
        }
        RaycastHit2D hitR = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 1, lyr_btn_restart);
        if (hitR.collider != null)
        {
            if (Input.GetMouseButtonUp(0))
            {
                //Return from checkpoint
				ClickBtnPause();
                loadLevel = PlayerPrefs.GetInt("currentStage");
                pointX = PlayerPrefs.GetFloat("pointX");
                pointY = PlayerPrefs.GetFloat("pointY");
                Application.LoadLevel(loadLevel);
                player.position = new Vector2(pointX, pointY);
            }
        }
            RaycastHit2D hitQ = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 1, lyr_btn_quit);
        if (hitQ.collider != null)
        {
            if (Input.GetMouseButtonUp(0))
            {
				ClickBtnPause();
                Application.LoadLevel(0);
                GameObject.Destroy(this);
            }
        }*/

	}

    void ClickBtnPause()
    {

        //print(isPaused);
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0;
			btn_pause.gameObject.SetActive(true);
            /*sprt_Pause.gameObject.SetActive(false);
            sprt_Play.gameObject.SetActive(true);
            btn_Restart.gameObject.SetActive(true);
            btn_Quit.gameObject.SetActive(true);*/
        }
        else
        {
            Time.timeScale = 1;
			btn_pause.gameObject.SetActive(false);
            /*sprt_Pause.gameObject.SetActive(true);
            sprt_Play.gameObject.SetActive(false);
            btn_Restart.gameObject.SetActive(false);
            btn_Quit.gameObject.SetActive(false);*/
        }


    }
}

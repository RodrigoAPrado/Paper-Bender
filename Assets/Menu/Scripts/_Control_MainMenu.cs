using UnityEngine;
using System.Collections;

public class _Control_MainMenu : MonoBehaviour
{

    public enum MenuState
    {
        Index, Options
    }
    MenuState ActualMenuState;


    public enum OptionHovering
    {
        Play, Options, Continue, Quit, None
    }
    public OptionHovering SelectedOption;

    private RaycastHit _mouseHit;
    public Transform _lastMouseHit;
    private Vector3 _boxOriginalScale;

    void Start()
    {
        ActualMenuState = MenuState.Index;
        _lastMouseHit = GameObject.Find("btn_Quit").transform;
        _boxOriginalScale = GameObject.Find("btn_Quit").transform.FindChild("temp_sprite").localScale;
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out _mouseHit, 100))
        {

            switch (_mouseHit.transform.name)
            {
                case "btn_Play":
                    SelectedOption = OptionHovering.Play;
                    break;
                case "btn_Continue":
                    SelectedOption = OptionHovering.Continue;
                    break;
                case "btn_Options":
                    SelectedOption = OptionHovering.Options;
                    break;
                case "btn_Quit":
                    SelectedOption = OptionHovering.Quit;
                    break;
                default:
                    SelectedOption = OptionHovering.None;
                    //Todos os botão voltar ao normal
                    break;
            }
            SelectOption();
        }
        else
        {
            SelectedOption = OptionHovering.None;
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (ActualMenuState == MenuState.Options)
            {
                ActualMenuState = MenuState.Index;
                //MenuChangingAnimation();
            }

            /* if (ActualMenuState == MenuState.Index)
                 Application.Quit();*/
        }

        if (SelectedOption != OptionHovering.None)
            Hoveringthis();
        else
            LastHoveredReturnToNormal();

    }

    void SelectOption()
    {
        switch (SelectedOption)
        {
            case OptionHovering.Play:
                if (Input.GetMouseButtonUp(0))
                {
                    //Load level
                    //print("Play");
                    Application.LoadLevel(1);
					Application.LoadLevelAdditive("ControlPause");
					PlayerPrefs.SetInt("currentStage", 1);
					PlayerPrefs.SetInt("cameraPosition", 0);
				 	PlayerPrefs.SetFloat("pointX", 3.5f);
					PlayerPrefs.SetFloat("pointY", -4.5f);
                }
                break;

            case OptionHovering.Continue:
                if (Input.GetMouseButtonUp(0))
                {
                    //Load save
                }
                break;

            case OptionHovering.Options:
                if (Input.GetMouseButtonUp(0))
                {
                    //Animação pra outra tela
                }
                break;

            case OptionHovering.Quit:
                if (Input.GetMouseButtonUp(0))
                {
                    Application.Quit();
                }
                break;

            case OptionHovering.None:
                //Desativar as animações de botão;
                break;
        }
    }

    void LastHoveredReturnToNormal()
    {
        Transform sprite;
        sprite = _lastMouseHit.transform.FindChild("temp_sprite");
        sprite.localScale = _boxOriginalScale;
        GUIText text = _lastMouseHit.transform.FindChild("temp_text").guiText;
        text.fontSize = 20;
    }

    void Hoveringthis()
    {
        if (_mouseHit.transform == null)
            return;

        _lastMouseHit = _mouseHit.transform;

        GUIText text = _mouseHit.transform.FindChild("temp_text").guiText;
        Transform sprite;
        //text = _mouseHit.transform.FindChild("temp_text");
        sprite = _mouseHit.transform.FindChild("temp_sprite");

        sprite.localScale = Vector3.Lerp(_boxOriginalScale, _boxOriginalScale * 1.5F, 0.5F);
        text.fontSize = 30;

    }

}

using UnityEngine;
using System.Collections;

public class btn_restart : MonoBehaviour {

    void OnClick()
    {
        Application.LoadLevel(Application.loadedLevelName);
    }
}

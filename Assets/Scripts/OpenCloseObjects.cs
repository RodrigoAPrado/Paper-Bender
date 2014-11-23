using UnityEngine;
using System.Collections;

public class OpenCloseObjects : MonoBehaviour {

	public GameObject ObjectClose;

	public enum tabOpenCloseObj
	{
		Open,
		Close
	}

	public tabOpenCloseObj openOrClose;

	void OnClick()
	{
		if(openOrClose == tabOpenCloseObj.Close)
			ObjectClose.SetActive(false);
		else
			ObjectClose.SetActive(true);
	}
}

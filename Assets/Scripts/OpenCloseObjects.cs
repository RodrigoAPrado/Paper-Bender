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
			Destroy(ObjectClose);
		else
			ObjectClose.SetActive(true);
	}
}

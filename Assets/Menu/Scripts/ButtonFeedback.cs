using UnityEngine;
using System.Collections;

public class ButtonFeedback : MonoBehaviour {

	public Color originalColor;
	public Color newColor;

	public SpriteRenderer objRenderer;
	public UITexture objTexture;

	void Start()
	{
		if(objRenderer == null && objTexture == null)
		{
			if(GetComponent<UITexture>())
			{
				originalColor = GetComponent<UITexture>().color;
			}
			
			if(GetComponent<SpriteRenderer>())
			{
				originalColor = GetComponent<SpriteRenderer>().color;
			}
		}

		if(objRenderer != null)
			originalColor = objRenderer.color;

		if(objTexture != null)
			originalColor = objTexture.color;
	}

	void OnPress(bool isPressed)
	{
		Debug.Log("IS PRESSED: " + isPressed);

		if(objRenderer == null && objTexture == null)
		{
			if(GetComponent<UITexture>())
			{
				Debug.Log("UITEXTURE");

				newColor = new Color(originalColor.r-0.2f,originalColor.g-0.2f,originalColor.b-0.2f);

				if(isPressed)
					GetComponent<UITexture>().color = newColor;
				else
					GetComponent<UITexture>().color = originalColor;
			}

			if(GetComponent<SpriteRenderer>())
			{
				Debug.Log("SPRITE RENDERER");

				newColor = new Color(originalColor.r-0.2f,originalColor.g-0.2f,originalColor.b-0.2f);

				if(isPressed)
					GetComponent<SpriteRenderer>().color = newColor;
				else
					GetComponent<SpriteRenderer>().color = originalColor;
			}
		}

		if(objRenderer != null)
		{
			newColor = new Color(originalColor.r-0.2f,originalColor.g-0.2f,originalColor.b-0.2f);
			
			if(isPressed)
				objRenderer.color = newColor;
			else
				objRenderer.color = originalColor;
		}

		if(objTexture != null)
		{
			newColor = new Color(originalColor.r-0.2f,originalColor.g-0.2f,originalColor.b-0.2f);
			
			if(isPressed)
				objTexture.color = newColor;
			else
				objTexture.color = originalColor;
		}
	}
}

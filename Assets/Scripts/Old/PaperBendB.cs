using UnityEngine;
using System.Collections;

public class PaperBendB : MonoBehaviour {

	public GameObject thisPaper;
	public GameObject otherPaper;
	//GameObject playerCurrentGround;
	public LayerMask playerBendingZone;
	public float timeSet;
	float timeCounter;
	public bool canBend;
	public bool canBendAnywhere;
	public bool checkSprite;
	public GameObject imageRenderer;

	//Uso para papeis invertidos
	public Transform upperPaperLeft;
	public Transform lowerPaperRight;
	public LayerMask playerZone;
	bool prepareBend;
	float mousex;
	float mousey;

	public Transform bendDetectLeft;
	public Transform bendDetectRight;
	// Use this for initialization
	public Sprite[] spriteArray = new Sprite[3];
	
	void Start () {
	}
	void Update()
	{
		//playerCurrentGround = GameObject.FindGameObjectWithTag("Player").GetComponent<MovePlayer>().currentGround;
		if(bendDetectLeft || bendDetectRight == null)
		{
			canBend = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y), 0.2f, playerBendingZone);
		}
		else
		{
			canBend = Physics2D.OverlapArea(new Vector2(bendDetectLeft.position.x, bendDetectLeft.position.y), new Vector2(bendDetectRight.position.x, bendDetectRight.position.y), playerBendingZone);
		}
		if(canBend)
		{
			if(!canBendAnywhere)
				canBend = !Physics2D.OverlapArea(new Vector2(upperPaperLeft.position.x, upperPaperLeft.position.y), new Vector2(lowerPaperRight.position.x, lowerPaperRight.position.y), playerZone);
		}
		if(canBend)
			//gameObject.renderer.enabled = true;
			imageRenderer.SetActive(true);
		else
			imageRenderer.SetActive(false);
			//gameObject.renderer.enabled = false;
		if(timeSet > 0)
			timeCounter += Time.deltaTime;

		if(timeCounter > timeSet && timeSet > 0)
			BendPaper();
		if(checkSprite)
			CheckSprite();
		else
			return;
	}
	public void BendPaper()
	{

		timeCounter = 0;
		if(/*thisPaper == playerCurrentGround || otherPaper == playerCurrentGround || */!canBend)
			return;
		otherPaper.SetActive(true);
		thisPaper.SetActive(false);
	}
	void CheckSprite()
	{
		/*thisPaper.GetComponent<SpriteRenderer>().sprite = spriteArray[masterPaper.tape];
		otherPaper.GetComponent<SpriteRenderer>().sprite = spriteArray[masterPaper.tape];*/
	}
}

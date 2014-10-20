using UnityEngine;
using System.Collections;

public class PuzzleDurexControllerNeo : MonoBehaviour {
	
	public GameObject paperAMaster;
	PaperDurexChecker paperADurex;
	GameObject paperABended;
	GameObject paperAUnbended;
	public GameObject paperBMaster;
	PaperDurexChecker paperBDurex;
	GameObject paperBBended;
	GameObject paperBUnbended;
	public bool a_MustBeBended;
	public bool b_MustBeBended;
	bool paperAIndexed;
	bool paperBIndexed;
	PaperDurexChecker firstIndexed;
	PaperDurexChecker secondIndexed;
	//public MasterPaper firstToInteract;

	// Use this for initialization
	void Start () 
	{
		paperADurex = paperAMaster.GetComponent<PaperDurexChecker>();
		paperBDurex = paperBMaster.GetComponent<PaperDurexChecker>();

		paperABended = paperAMaster.transform.FindChild("PaperBended").gameObject;
		paperAUnbended = paperAMaster.transform.FindChild("PaperUnbended").gameObject;
		
		paperBBended = paperBMaster.transform.FindChild("PaperBended").gameObject;
		paperBUnbended = paperBMaster.transform.FindChild("PaperUnbended").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		CheckBend();
		if(paperAIndexed && paperBIndexed)
		{
			CheckDurex();
		}
	}
	void CheckDurex()
	{
		if(paperBDurex.durexOn || paperADurex.durexOn)
		{
			paperBDurex.durexOn = true;
			paperADurex.durexOn = true;
		}
	}
	void CheckBend()
	{
		CheckBendA();
		CheckBendB();
	}
	void CheckBendA()
	{
		if(a_MustBeBended)
		{
			if(paperABended.activeSelf)
			{
				if(!paperAIndexed)
					IndexPaper(paperADurex);
				paperAIndexed = true;
			}
			else
			{
				if(paperAIndexed)
					RemovePaper(paperADurex, paperBDurex);
				paperAIndexed = false;
			}
		}
		else
		{
			if(paperAUnbended.activeSelf)
			{
				if(!paperAIndexed)
					IndexPaper(paperADurex);
				paperAIndexed = true;
			}
			else
			{
				if(paperAIndexed)
					RemovePaper(paperADurex, paperBDurex);
				paperAIndexed = false;
			}
		}


	}
	void CheckBendB()
	{
		if(b_MustBeBended)
		{
			if(paperBBended.activeSelf)
			{
				if(!paperBIndexed)
					IndexPaper(paperBDurex);
				paperBIndexed = true;
			}
			else
			{
				if(paperBIndexed)
					RemovePaper(paperBDurex, paperADurex);
				paperBIndexed = false;

			}
		}
		else
		{
			if(paperBUnbended.activeSelf)
			{
				if(!paperBIndexed)
					IndexPaper(paperBDurex);
				paperBIndexed = true;
			}
			else
			{
				if(paperBIndexed)
					RemovePaper(paperBDurex, paperADurex);
				paperBIndexed = false;
			}
		}

	}
	void IndexPaper(PaperDurexChecker p)
	{
		if(firstIndexed != null)
		{
			firstIndexed = p;
		}
		else
		{
			secondIndexed = p;
		}
	}
	void RemovePaper(PaperDurexChecker p, PaperDurexChecker q)
	{
		if(firstIndexed == p)
		{
			if(secondIndexed != null)
			{
				if(paperADurex.durexOn && paperBDurex.durexOn)
					q.durexOn = false;
				firstIndexed = secondIndexed;
				secondIndexed = null;
			}
			else
			{
				firstIndexed = null;
			}
		}
		else
		{
			if(paperADurex.durexOn && paperBDurex.durexOn)
				q.durexOn = false;
			secondIndexed = null;
		}
	}
}

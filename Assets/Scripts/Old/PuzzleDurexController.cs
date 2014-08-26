using UnityEngine;
using System.Collections;

public class PuzzleDurexController : MonoBehaviour {

	public MasterPaper paperA;
	public MasterPaper paperB;
	public bool notBendA;
	public bool notBendB;
	public bool paperAIndexed;
	public bool paperBIndexed;
	public MasterPaper firstToInteract;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		CheckBend();
	}
	void CheckBend()
	{
		CheckBendA();
		CheckBendB();
	}
	void CheckBendA()
	{
		if(notBendA)
		{
			if(!paperA.bended)
			{
				paperAIndexed = true;
				if(firstToInteract == null)
					firstToInteract = paperA;
			}
			else
			{
				if(paperAIndexed){
					if(firstToInteract == paperA)
					{
						if(paperBIndexed)
						{
							/* 3Puzz */
							if(paperB.tape == 1 && notBendB)
							{
								paperA.tape = 1;
								paperB.tape = 0;
							}
							/* 0Puzz */
							if(paperB.tape == 2 && !notBendB)
							{
								paperA.tape = 1;
								paperB.tape = 0;
							}
							/* 1Puzz */
							if(paperA.tape == 2)
							{
								if(notBendB == notBendA)
									paperB.tape = 2;
								else
									paperB.tape = 1;
								paperA.tape = 0;
							}
							firstToInteract = paperB;
						}
						else
						{
							firstToInteract = null;
						}
					}
					else
					{

					}
				}
				paperAIndexed = false;
			}
		}
		else
		{
			if(paperA.bended)
			{
				paperAIndexed = true;
				if(firstToInteract == null)
					firstToInteract = paperA;
			}
			else
			{
				paperAIndexed = false;
				if(firstToInteract == paperA)
				{
					if(paperBIndexed)
					{
						/* 2Puzz */
						if(paperA.tape == 1)
						{

							paperA.tape = 0;
							if(notBendB == notBendA)
								paperB.tape = 1;
							else
								paperB.tape = 2;
						}
						/* 0Puzz*/
						if(paperB.tape == 1 && notBendA)
						{
							paperA.tape = 2;
							paperB.tape = 0;
						}
						if(paperB.tape == 2 && !notBendA)
						{
							paperA.tape = 2;
							paperB.tape = 0;
						}
						firstToInteract = paperB;
					}
					else
					{
						firstToInteract = null;
					}
				}
				else
				{

				}
			}
		}
	}
	void CheckBendB()
	{
		if(notBendB)
		{
			if(!paperB.bended)
			{
				paperBIndexed = true;
				if(firstToInteract == null)
					firstToInteract = paperB;
			}
			else
			{
				if(paperBIndexed)
				{
					if(firstToInteract == paperB)
					{
						if(paperAIndexed)
						{
							/* 3Puzz */
							if(paperA.tape == 1 && notBendA)
							{
								paperB.tape = 1;
								paperA.tape = 0;
							}
							/* 0Puzz*/
							if(paperA.tape == 2 && !notBendA)
							{
								paperB.tape = 1;
								paperA.tape = 0;
							}
							/* 1Puzz */
							if(paperB.tape == 2)
							{
								if(notBendB == notBendA)
									paperA.tape = 2;
								else
									paperA.tape = 1;
								paperB.tape = 0;
							}
							firstToInteract = paperA;
						}
						else
						{
							firstToInteract = null;
						}
					}
					else
					{

					}
				}
				paperBIndexed = false;
			}
		}
		else
		{
			if(paperB.bended)
			{
				paperBIndexed = true;
				if(firstToInteract == null)
					firstToInteract = paperB;
			}
			else
			{
				paperBIndexed = false;
				if(firstToInteract == paperB)
				{
					if(paperAIndexed)
					{
						/* 2Puzz */
						if(paperB.tape == 1)
						{
							paperB.tape = 0;
							if(notBendB == notBendA)
								paperA.tape = 1;
							else
								paperA.tape = 2;
						}
						/* 0Puzz*/
						if(paperA.tape == 1 && notBendA)
						{
							paperB.tape = 2;
							paperA.tape = 0;
						}
						if(paperA.tape == 2 && !notBendA)
						{
							paperB.tape = 2;
							paperA.tape = 0;
						}
						firstToInteract = paperA;
					}
					else
					{
						firstToInteract = null;
					}
				}
			}
		}
	}
}

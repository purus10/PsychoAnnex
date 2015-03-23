using UnityEngine;
using System.Collections;

public class E_Controls : MonoBehaviour {

	E_Equip list;
	PC_Main[] search;
	int i;

	void Start()
	{
		search = GameObject.FindObjectsOfType(typeof(PC_Main)) as PC_Main[];
	}
	
	void FixedUpdate () 
	{
		list = GetComponent<E_Equip>();
		if (list.gearID == false)
		{
			if (Input.GetKeyDown(GameInformer.Left)) 
			{
				i = (i+1) % search.Length;
				ShiftCharacters(i);
			}
			
			if (Input.GetKeyDown(GameInformer.Right)) 
			{
				if (i > 0) i = (i-1) % search.Length;
				else i = search.Length-1;
				ShiftCharacters(i);
			}
			
			if (Input.GetKeyDown(KeyCode.Escape)) Application.LoadLevel(GameInformer.previous);
		}
	}

	void ShiftCharacters (int j)
	{
		E_Status selected = GetComponent<E_Status>();
		selected.Id = search[j].ID;
	}
}
								
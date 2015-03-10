using UnityEngine;
using System.Collections;

public class E_Controls : MonoBehaviour {

//	E_Equip list;
//	PC_Stat[] search;
	int i;

	/*void Start()
	{
		search = GameObject.FindObjectsOfType(typeof(PC_Stat)) as PC_Stat[];
	}*/
	
	void FixedUpdate () 
	{
	/*	list = GetComponent<E_Equip>();
		if (list.gearID == false)
		{
			if (Input.GetKeyDown(PC_Control.Left)) 
			{
				i = (i+1) % search.Length;
				ShiftCharacters(i);
			}
			
			if (Input.GetKeyDown(PC_Control.Right)) 
			{
				if (i > 0) i = (i-1) % search.Length;
				else i = search.Length-1;
				ShiftCharacters(i);
			}*/
			
			if (Input.GetKeyDown(KeyCode.Escape)) Application.LoadLevel(GameInformer.previous);
		//}
	}

/*	void ShiftCharacters (int j)
	{
		E_Status selected = GetComponent<E_Status>();
		selected.Id = search[j].ID;
	}*/
}
								
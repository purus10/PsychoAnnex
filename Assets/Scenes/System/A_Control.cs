using UnityEngine;
using System.Collections;

public class A_Control : MonoBehaviour {
	
	A_Display main;
	public PC_Main[] search;
	A_Node[] a_nodes;
	int i;
	
	void Start()
	{
		search = GameObject.FindObjectsOfType(typeof(PC_Main)) as PC_Main[];
		a_nodes = GameObject.FindObjectsOfType(typeof(A_Node)) as A_Node[];
		main = GetComponent<A_Display>();

	}
	void FixedUpdate () 
	{
			if (Input.GetKeyDown(KeyCode.Z)) 
			{
				i = (i+1) % search.Length;
				ShiftCharacters(i);
			}
			if (Input.GetKeyDown(KeyCode.X)) 
			{
				if (i > 0) i--;
				else i = search.Length-1;
				ShiftCharacters(i);
			}
			if (Input.GetKeyDown(KeyCode.Escape)) Application.LoadLevel(GameInformer.previous);
	}
	void ShiftCharacters (int j)
	{
		main.Id = search[j].ID;
		main.player = search[j].gameObject;
		foreach (A_Node a in a_nodes)
		{
			a.Switch();
		}
	}
}

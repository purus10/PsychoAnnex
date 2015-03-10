using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Database;

public class A_Node : MonoBehaviour {

	public GameObject[] nodes;
	public bool[] selectable;
	public bool[] unlocked;

	public GameObject main;
	public Abilities ability;

	public int Ccost, Ecost, Tcost, Lcost;
	
	public string abilityname;
	public int type;
	public string description, cost;

	A_Display display;
	
	void Start () 
	{
		GameInformer.Tranquality = 20;
		display = main.GetComponent<A_Display>();

		ability = new Abilities();
		ability.name = abilityname;
		ability.type = type;
		ability.description = description;
	}

	public void UnlockPaths(int id)
	{
		if (nodes.Length > 0) foreach (GameObject n in nodes)
		{
			A_Node node = n.GetComponent<A_Node>();
			node.selectable[id-1] = true;
			n.GetComponent<Renderer>().material.color = Color.yellow;
		}
		unlocked[id-1] = true;
		GetComponent<Renderer>().material.color = Color.green;
		Clearinfo();
	}

	public void Clearinfo()
	{
		if (display.node == null) 
		{
			display.selected = "";
			display.description = "";
			display.price = "";
		}
	}

	void OnMouseEnter()
	{
		display.description = description;
		display.selected = abilityname;
		display.price = cost;
	}

	void OnMouseExit()
	{
		Clearinfo();
	}

	void OnMouseDown()
	{
		if (unlocked[display.Id-1] == false && selectable[display.Id-1] == true)
		{
		display.node = this.gameObject;
		display.selected = abilityname;
		display.description = description;
		}
	}
}

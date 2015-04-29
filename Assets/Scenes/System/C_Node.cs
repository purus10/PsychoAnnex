using UnityEngine;
using System.Collections;

public class C_Node : MonoBehaviour {
	
	// 0 = Beat 1 = Acc 2 = Item

	public GameObject[] Nodes;
	public string Name, Description;
	public GameObject main;
	public int Type, ID, Cost, Essence_Type;
	private string[] Essence_Names = new string[4]{"Cruelty","Tranquility","Empathy","Luck"};
	//for Node state: 0 == not selectable or unlocked, 1 == selectable, 2 == unlocked
	public int[] Node_State = new int[7];
	A_Display display;
	
	void FixedUpdate() 
	{
		display = main.GetComponent<A_Display>();
		if (Node_State[display.Id-1] == 2) GetComponent<Renderer>().material.color = Color.green;
		else if (Node_State[display.Id-1] == 1) GetComponent<Renderer>().material.color = Color.yellow;
		else  GetComponent<Renderer>().material.color = Color.grey;
	}
	public void UnlockPaths(int id)
	{
		if (Nodes.Length > 0) foreach (GameObject n in Nodes)
		{
			A_Node node = n.GetComponent<A_Node>();
			if (node != null && node.Node_State[display.Id-1] == 0) 
			{
				if (node.abilityname[id] == "") node.SetNode(display.Id-1);
				node.Node_State[display.Id-1] = 1;
				n.GetComponent<Renderer>().material.color = Color.yellow;
			}
			else {
				C_Node c_node = n.GetComponent<C_Node>();
				if (c_node != null && c_node.Node_State[display.Id-1] == 0) 
				{
					c_node.Node_State[display.Id-1] = 1;
					n.GetComponent<Renderer>().material.color = Color.yellow;
				}
				else {
					G_Node g_node = n.GetComponent<G_Node>();
					if (g_node != null && g_node.Node_State[display.Id-1] == 0) 
					{
						g_node.Node_State[display.Id-1] = 1;
						n.GetComponent<Renderer>().material.color = Color.yellow;
					}
				}
			}
		}
		Node_State[id] = 2;
		GetComponent<Renderer>().material.color = Color.green;
		Clearinfo();
	}
	public void SetNode(int t)
	{
		if (t == 0) display.stat.Beat++;
		if (t == 1) display.stat.second_acc = true;
		if (t == 2) display.stat.fourth_item = true;
		display.node = null;
		UnlockPaths(ID);
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
		if (Node_State[display.Id-1] > 0 && display.selected == "")
		{
			display.description = Description;
			display.selected = Name;
			display.price = Essence_Names[Essence_Type]+" "+Cost.ToString();
		}
	}
	void OnMouseExit()
	{
		Clearinfo();
	}
	void OnMouseDown()
	{
		if (Node_State[display.Id-1] == 1 && display.stat.ID-1 == ID)
		{
			display.node = this.gameObject;
		}
	}
}

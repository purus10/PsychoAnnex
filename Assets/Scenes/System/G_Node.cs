using UnityEngine;
using System.Collections;

public class G_Node : MonoBehaviour {

	// 0 = Earth 1 = Fire 2 = Wind 3 = Thunder 4 = Wicked
	// 5 = Divine 6 = Water 7 = Nature 8 = Day 9 = Night

	public string Name, Description;
	public GameObject main;
	public int Type;
	public GameObject[] Nodes;
	//for Node state: 0 == not selectable or unlocked, 1 == selectable, 2 == unlocked
	public int[] Node_State = new int[7];
	int id = 11;
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
				if (node.abilityname[id-1] == "") node.SetNode(display.Id-1);
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
		Node_State[id-1] = 2;
		GetComponent<Renderer>().material.color = Color.green;
		Clearinfo();
	}
	public void SetNode(int t)
	{

		DoorManager.Door[Type]++;
		id = display.Id-1;
		display.node = null;
		UnlockPaths(id);
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
		display.price = "1 "+Name+" key";
		}
	}
	void OnMouseExit()
	{
		Clearinfo();
	}
	void OnMouseDown()
	{
		if (Node_State[display.Id-1] == 1 && display.node == null)
		{
			display.node = this.gameObject;
		}
	}
}

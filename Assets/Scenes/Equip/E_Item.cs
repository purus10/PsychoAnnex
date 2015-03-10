using UnityEngine;
using System.Collections;
using Database;

public class E_Item : MonoBehaviour {
	
	public Rect Box,Title,Item1,Item2,Item3,Item4;
	public Rect Item1Button,Item2Button,Item3Button,Item4Button;
	GUIStyle smallFont;
	E_Status selected;
	E_Equip list;
	
	string ItemCheck(int i)
	{
		if (selected.stat.items[i] != null && selected.stat.items[i].name != "None") return selected.stat.items[i].name;
		else return "";
	}
	
	void Start()
	{
		smallFont = new GUIStyle();
		smallFont.fontSize = 20;
		smallFont.normal.textColor = Color.white;
	}
	
	void FixedUpdate()
	{
		selected = GetComponent<E_Status>();
		list = GetComponent<E_Equip>();
	}
	
	void MakeList(int i)
	{
		if (list.gearID == false && list.items == false)
		{
			list.magicnum = i;
			item gen = new item();
			gen.name = "None";
			list.E_items.Add(gen);
			
			foreach( item I in ItemList.items)
			{
				if (selected.stat.items[i] != null) selected.stat.items[i].equipped = false;
				if (I.type == 1 && I.equipped == false) list.E_items.Add(I);
			}
		}
		list.items = true;
		list.gearID = true;
	}

	void OnGUI () {
		
		GUI.Box(Box,"");
		GUI.Label(Title,"Items", smallFont);
		
		GUI.Label(Item1,"Slot 1:", smallFont);
		if (GUI.Button(Item1Button,ItemCheck(0))) 
			if(selected.stat.items[0] == null || selected.stat.items[0].name != "Pills") MakeList(0);
		
		if (selected.stat.items.Length > 1)
		{
			GUI.Label(Item2,"Slot 2:", smallFont);
			if (GUI.Button(Item2Button, ItemCheck(1))) MakeList(1);
		}
		
		if (selected.stat.items.Length > 2)
		{
			GUI.Label(Item3,"Slot 3:", smallFont);
			if (GUI.Button(Item3Button, ItemCheck(2))) MakeList(2);
		}
		
		if (selected.stat.items.Length > 3)
		{
			GUI.Label(Item4,"Slot 4:", smallFont);
			if (GUI.Button(Item4Button, ItemCheck(3))) MakeList(3);
		}
		
	}
}

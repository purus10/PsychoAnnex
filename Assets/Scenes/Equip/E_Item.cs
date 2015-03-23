using UnityEngine;
using System.Collections;
using Database;

public class E_Item : MonoBehaviour {
	
	public Rect Box,Title,Item1,Item2,Item3,Item4;
	public Rect[] item, item_button;
	public Rect Item1Button,Item2Button,Item3Button,Item4Button;
	public GUIStyle smallFont;
	E_Status selected;
	E_Equip list;
	
	string ItemCheck(int i)
	{
		if (selected != null && selected.stat.items[i] != null && selected.stat.items[i].name != "None") return selected.stat.items[i].name;
		else return "";
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
			Item gen = new Item();
			gen.name = "None";
			list.E_items.Add(gen);
			
			foreach( Item I in ItemList.items)
			{
				if (selected.stat.items[i] != null) selected.stat.items[i].equipped = false;
				if (I.type != 2 && I.equipped == false)  list.E_items.Add(I);
			}
		}
		list.items = true;
		list.gearID = true;
	}

	void OnGUI () {
		
		GUI.Box(Box,"");
		GUI.Label(Title,"Items", smallFont);

		for (int i =0;i < item.Length;i++)
		{
			GUI.Label(item[i],"Slot "+ (i+1)+":", smallFont);
			if (GUI.Button(item_button[i],ItemCheck(i))) 
				if(ItemCheck(i) != "Pills") MakeList(i);
		}
	}
}

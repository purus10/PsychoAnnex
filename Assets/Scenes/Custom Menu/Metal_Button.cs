using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Database;

public class Metal_Button : MonoBehaviour {
	
	public List <Item> metals = new List<Item>();
	public Item[] metal = new Item[3];
	Selected_Button selected;
	Custom_Acc ca;
	Custom_Wep cw;
	public Rect Box,metallist;
	public Rect[] metalname;
	public Rect[] button;
	public int magicnum;

	string MetalName(int i)
	{
		if (metal[i] != null) return metal[i].name;
		else return "";
	}
	
	void Start()
	{
		selected = GetComponent<Selected_Button>();
		ca = GetComponent<Custom_Acc>();
		cw = GetComponent<Custom_Wep>();
	}

	void MakeList(int i, bool weapon)
	{
		//Gets all metals in Item List;
		metals.Clear();
		for(int m = 0; m < ItemList.items.Count;m++) 
		{
			if (ItemList.items[m].type == 2) metals.Add(ItemList.items[m]);
		}

		if (weapon == false && selected.acc.metal[i] == null ||  weapon == true && selected.wep.metal[i] == null) 
			// removes any previously selected metal in the slot;
		for (int x = 0; x < metals.Count; x++) 
		{
			if (metals[x] == metal[i])
			{
				metals[x].amount++;
				metal[i] = null;
				if (weapon) cw.weight -= cw.WeightIncrease(metals[x].name);
				if (!weapon) ca.StatDecrease(metals[x]);
			}
		}
		magicnum = i;
		selected.ID = 1;
	}
	
	void Equip(int m, int i)
	{
		if (metals[i].amount >= 1)
		{
			metal[m] = metals[i];
			metals[i].amount--;
			if (selected.wep != null) cw.weight += cw.WeightIncrease(metals[i].name);
			if (selected.acc != null) ca.StatIncrease(metals[i]);
			selected.ID = 0;	
		}
	}
	
	void OnGUI()
	{
		GUI.Box(Box, "Metals");
		for(int i = 0; i < metalname.Length;i++)
		{
			GUI.Box(metalname[i],"Metal" + (i+1));
			if (GUI.Button(button[i], MetalName(i)))
			{
				if (selected.wep != null || selected.acc != null)
				{
					if (selected.ID != 1 && selected.wep.metal[i] == null || selected.ID != 1 && selected.acc.metal[i] == null)
					{
						if (selected.wep != null) MakeList(i, true);
						if (selected.acc != null) MakeList(i,false);
					}
				}
			}
		}
		
		if(selected.ID == 1)
		{
			for(int i = 0; i < metals.Count; i++)
			{
				if (GUI.Button(new Rect (metallist.x, (metallist.height * i) + metallist.y + 30, metallist.width, metallist.height), metals[i].name + " " + metals[i].amount))
					Equip(magicnum,i);
			}
			if (GUI.Button(new Rect (metallist.x, (metallist.height * metals.Count) + metallist.y + 30, metallist.width, metallist.height), "None")) selected.ID = 0;
		}
	}
}

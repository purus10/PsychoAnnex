using UnityEngine;
using System.Collections;
using Database;

public class Metal_Button : MonoBehaviour {

	MetalList list;
	Custom_Acc ca;
	Custom_Wep cw;
	Selected_Button selected;
	public Rect Box, metal1, metal2, metal3,metallist;
	public Rect button1, button2, button3;
	int magicnum;

	public Item[] metal = new Item[3];
	
	void Start()
	{
		selected = GetComponent<Selected_Button>();
		list = GetComponent<MetalList>();
		ca = GetComponent<Custom_Acc>();
		cw = GetComponent<Custom_Wep>();
	}
	string MetalName(int i)
	{
		if (metal[i] != null) return metal[i].name;
		else return "";
	}
	void MakeList(int i, bool weapon)
	{
		if (weapon == false && selected.acc.metal[i] == null ||  weapon == true && selected.wep.metal[i] == null) 
		for (int x = 0; x < list.metals.Count; x++) 
		{
				if (list.metals[x] == metal[i])
			{
				list.metals[x].amount++;
				metal[i] = null;
				if (weapon) cw.weight -= cw.WeightIncrease(list.metals[x].name);
				if (!weapon) ca.StatDecrease(list.metals[x]);
			}
		}
		magicnum = i;
		list.ID = 1;
	}

	void Equip(int m, int i)
	{
		if (list.metals[i].amount >= 1)
		{
			metal[m] = list.metals[i];
			list.metals[i].amount--;
			if (selected.wep != null) cw.weight += cw.WeightIncrease(list.metals[i].name);
			if (selected.acc != null) ca.StatIncrease(list.metals[i]);
			list.ID = 0;			
		}
	}

	void OnGUI()
	{
		GUI.Box(Box, "Metals");

		GUI.Box(metal1, "Metal 1");
		if(GUI.Button(button1, MetalName(0)))
		{
			if (list.ID != 1)
			{
			if (selected.wep != null) MakeList(0, true);
			if (selected.acc != null) MakeList(0,false);
			}
		}
		GUI.Box(metal2, "Metal 2");
		if(GUI.Button(button2, MetalName(1)))
		{
			if (list.ID != 1)
			{
			if (selected.wep != null) MakeList(1, true);			
			if (selected.acc != null) MakeList(1,false);
			}
		}
		GUI.Box(metal3, "Metal 3");
		if(GUI.Button(button3, MetalName(2)))
		{
			if (list.ID != 1)
			{
			if (selected.wep != null) MakeList(2, true);		
			if (selected.acc != null) MakeList(2,false);
			}
		}
		if(list.ID == 1)
		{
			for(int i = 0; i < list.metals.Count; i++)
			{
				if (GUI.Button(new Rect (metallist.x, (metallist.height * i) + metallist.y + 30, metallist.width, metallist.height), list.metals[i].name + " " + list.metals[i].amount))
				Equip(magicnum,i);
			}
			if (GUI.Button(new Rect (metallist.x, (metallist.height * list.metals.Count) + metallist.y + 30, metallist.width, metallist.height), "None")) list.ID = 0;
		}
	}
}

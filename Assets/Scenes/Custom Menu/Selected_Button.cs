using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Database;

public class Selected_Button : MonoBehaviour {
	//1 = name, 2 = brawns/damage, 3 = tenacity/hit, 4 = courage/max
	public TextMesh[] change = new TextMesh[4], display = new TextMesh[21];
	static public int type;
	public int ID;
	static public string selected;
	public Rect box, button, selectedlist, listbox, Scroller, ScrollView;
	private float vScroolbarValue;
	public Vector2 scrollposition = Vector2.zero;
	public List <Item> S_wep = new List<Item>();
	public List <Item> S_acc = new List<Item>();
	Metal_Button metals;
	public Item acc;
	public Item wep;
	
	void Start () 
	{
		metals = GetComponent<Metal_Button>();
		if (selected == "Weapon") 
		for (int i = 0; i < (display.Length - 3); i++) display[i].GetComponent<Renderer>().enabled = true;

		if (selected == "Accessory") 
			for (int i = 18; i < (display.Length); i++) display[i].GetComponent<Renderer>().enabled = true;
		display[0].GetComponent<Renderer>().enabled = true;
	}
	
	string SelectedName()
	{
		if (wep != null && type == 1) return wep.name;
		else if (acc != null && type == 2) return "Accessory";
		else return "";
	}
	
	void Update()
	{
		for (int i = 0; i < change.Length; i++)
		{
			change[i].GetComponent<Renderer>().enabled = true;
			if (wep != null)
			{
				if (i == 0) change[i].text = wep.name;
				if (i == 1) change[i].text = wep.damage.ToString();
				if (i == 2) change[i].text = wep.hit.ToString();
				if (i == 3) change[i].text = wep.max.ToString();
			}else if (acc != null)
			{
				if (i == 0) change[i].text = acc.name;
				if (i == 1) change[i].text = acc.Brawns.ToString();
				if (i == 2) change[i].text = acc.Tenacity.ToString();
				if (i == 3) change[i].text = acc.Courage.ToString();
			} else{
				change[i].GetComponent<Renderer>().enabled = false;
			}
		}
	}
	
	void MakeList()
	{
		if (type == 1) 
		{
			foreach (Item w in WeaponsList.weapons) 
			if (w.equipped == false && w.type != 5) S_wep.Add(w);

		}else{ 
			foreach (Item a in AccList.Accessories) 
			if (a.equipped == false) S_acc.Add(a);
		}
	}
	
	void MakeSelection(int i, int t)
	{
		ClearMetals();
		if (t == 1) 
		{
			Custom_Wep cw = GetComponent<Custom_Wep>();
			wep = S_wep[i];
			cw.weight = wep.weight;
			cw.DisplayWeight();
			for (int m = 0; m < metals.metal.Length;m++) 
				if (metals.metal[m] != null && wep.metal[m] == null) cw.weight += cw.WeightIncrease(metals.metal[m].name);
			else if (wep.metal[m] != null) metals.metal[m] = wep.metal[m];
		} else wep = null;
		
		if (t == 2) acc = S_acc[i];
		else acc = null;
		
		S_wep.Clear();
		S_acc.Clear();
		ID = 0;
	}
	
	
	void GuiSetup(int i)
	{
		selectedlist = new Rect(0,(selectedlist.height * i) + 2,120,30f);
		if (selectedlist.y <= 567f) listbox.height = selectedlist.y;
		ScrollView.height = selectedlist.y;
	}

	void ClearMetals()
	{
		for(int i = 0; i < metals.metal.Length;i++)
		{
			metals.metal[i] = null;
		}
	}
	
	void OnGUI()
	{
		if (type == 1 || type == 2)
		{
			GUI.Box(box, selected);
			
			if(GUI.Button(button, SelectedName()))
			{
				if (ID != 4)
				{
					ID = 4;
					MakeList();
				}
			}
			
			scrollposition = GUI.BeginScrollView(Scroller,scrollposition,ScrollView);
			if(ID == 4 && type == 1)
			{

				/* Creates Button for every wep on list*/
				for(int i = 0; i < S_wep.Count; i++) 
				{
					GuiSetup(i);
					if (GUI.Button(selectedlist, S_wep[i].name)) MakeSelection(i,1);
				}
				/* Creates None Button*/
				if (GUI.Button(new Rect (selectedlist.x, selectedlist.y + 30, selectedlist.width, selectedlist.height), "None"))
				{
					for (int t = 0; t < change.Length; t++) change[t].GetComponent<Renderer>().enabled = false;
					MakeSelection(0,0);
				}
			}
			
			if(ID == 4 && type == 2)
			{
				/* Creates Button for every acc on list*/
				for(int i = 0; i < S_acc.Count; i++) 
				{
					GuiSetup(i);
					if (GUI.Button(selectedlist, S_acc[i].name)) MakeSelection(i,2);
				}
				/* Creates None Button*/
				if (GUI.Button(new Rect (selectedlist.x, selectedlist.y + 30, selectedlist.width, selectedlist.height), "None"))
				{
					for (int t = 0; t < change.Length; t++) change[t].GetComponent<Renderer>().enabled = false;
					MakeSelection(0,0);
				}
			}
			GUI.EndScrollView();
		}
	}
}

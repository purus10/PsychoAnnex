using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Database;

public class Ability_Menu : MonoBehaviour {

	public Rect Item_button, List_button;
	public Rect[] Command_box, Give_box, Equip_button, Unequip_button, Give_button, Drop_button,Player_button,slot_button;
	public List <Ability> ability_list = new List<Ability>();
	int menu_show = 0,j = 0;
	PC_Main my;
	Rect ItemSet(float x, float y, float w, float h, int i)
	{
		return new Rect (x + (w * (i % 2)), y + (h * (i/2)), w, h);
	}
	Rect AbilitySet(int i)
	{
		return new Rect(2.6f,(List_button.height * i) + 2,425.3f,40f);
	}
	string AbilityName(int i)
	{
		if (my.ability[i] != null) 
			return my.ability[i].name;
		else return "Empty";
	}
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Q))
			if (menu_show == 0)
		{
			menu_show = 1;
			my = GameInformer.target.GetComponent<PC_Main>();
		} else menu_show = 0;
	}
	void CreateList(int i)
	{
		ability_list.Clear();
		Ability gen = new Ability();
		gen.name = "None";
		ability_list.Add(gen);
		if (i == 0)
		{
			foreach (Ability a in my.abilities)
			{
				if (a.equipped == false) ability_list.Add(a);
			}
		}else{
			foreach (Ability a in my.abilities)
			{
				if (a.equipped == false && a.type == 1) ability_list.Add(a);
			}
		}
	}
	void Equip(int i, int s)
	{
		my.ability[s].equipped = false;
		ability_list[i].equipped = true;
		my.ability[s] = ability_list[i];
	}
	void OnGUI () 
	{
		UnityEngine.Cursor.visible = true;
		if (menu_show > 0)
		{
			for (int i = 0; i < my.ability.Length;i++)
				if (GUI.Button(ItemSet(Item_button.x,Item_button.y,Item_button.width,Item_button.height,i), AbilityName(i)))
			{
				CreateList(i);
				menu_show = 2;
				j = i;
			}
		}
		if (menu_show == 2)
		{
			for (int i = 0; i < ability_list.Count;i++)
			{
				if (GUI.Button(AbilitySet(i), ability_list[i].name))
				{
					Equip(i,j);
				}
			}
		}
	}
}

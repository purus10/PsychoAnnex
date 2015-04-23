using UnityEngine;
using System.Collections;
using Database;

public class Item_Menu : MonoBehaviour {

	public Rect Item_button;
	public Rect[] Command_box, Give_box, Equip_button, Unequip_button, Give_button, Drop_button,Player_button,slot_button;
	int menu_show = 0,j = 0, pc_number = 0,selection_type;
	string command_name;
	PC_Main[] search;
	PC_Main my;
	Rect ItemSet(float x, float y, float w, float h, int i)
	{
		return new Rect (x + (w * (i % 2)), y + (h * (i/2)), w, h);
	}
	Rect CharacterSet(float x, float y, float w, float h, int i)
	{
		return new Rect (x, y + (h * i), w, h);
	}
	string ItemName(int i)
	{
		if (my.items[i] != null) 
			return my.items[i].name;
		else return "";
	}
	string AccName(int i, int k)
	{
		if (search[i].acc[k] != null) 
			return search[i].acc[k].name;
		else return "Empty";
	}
	string WepName(int i, int k)
	{
		if (search[i].wep[k] != null) 
			return search[i].wep[k].name;
		else return "Empty";
	}
	void Update()
	{
		if (Input.GetKeyDown(GameInformer.Deselect))
			if (menu_show == 0)
		{
			menu_show = 1;
			my = GameInformer.target.GetComponent<PC_Main>();
		} else menu_show = 0;
	}
	void Give(int i)
	{
		for (int k = 0; k < search[i].items.Length;k++)
		{
			if (search[i].items[k] == null) 
			{
				search[i].items[k] = my.items[j];
				my.items[j] = null;
			}

		}
	}
	void Equip(int i, int s)
	{
		if (my.items[j].type == 3)
		{
			if (search[i].acc[s] == null)
			{
				search[i].acc[s] = my.items[j];
				my.items[j] = null;
			}else {
				Item  acc_hold = new Item();
				acc_hold = search[i].acc[s];
				search[i].acc[s] = my.items[j];
				my.items[j] = acc_hold;
			}
		}else if (my.items[j].type == search[i].ID+3)
		{
			if (search[i].wep[s] == null)
			{
				search[i].wep[s] = my.items[j];
				my.items[j] = null;
			}else {
				Item  wep_hold = new Item();
				wep_hold = search[i].wep[s];
				search[i].wep[s] = my.items[j];
				my.items[j] = wep_hold;
			}
		}
	}
	void OnGUI () 
	{
		UnityEngine.Cursor.visible = true;
		if (menu_show > 0)
		{
			for (int i = 0; i < my.items.Length;i++)
				if (GUI.Button(ItemSet(Item_button.x,Item_button.y,Item_button.width,Item_button.height,i), ItemName(i)))
			{
				j = i;
				if (my.items[i] != null) menu_show = 2;
			}
		}
		if (menu_show > 1)
		{
			GUI.Box(Command_box[j],"");
			if (my.items[j].type < 3) command_name = "Use";
			else command_name = "Equip";
			if (GUI.Button(Equip_button[j], command_name)) 
			{
				menu_show = 3;
				selection_type = 1;
			}
			if (GUI.Button(Give_button[j], "Give")) 
			{
				menu_show = 3;
				selection_type = 2;
			}
			if (GUI.Button(Unequip_button[j], "Unequip")) menu_show = 3;
			if (GUI.Button(Drop_button[j], "Drop")) print ("drop");
		}
		if (menu_show > 2)
		{
			search = GameObject.FindObjectsOfType(typeof(PC_Main)) as PC_Main[];
			for (int i = 0; i < search.Length; i++)
			{
				if (GUI.Button(CharacterSet(Player_button[j].x,Player_button[j].y,Player_button[j].width,Player_button[j].height,i), search[i].Name))
				{
					if (selection_type == 1 && search[i] != my) Give(i);
					else if (selection_type == 2 && command_name == "Equip") 
					{
						pc_number = i;
						menu_show = 4;
					} else my.items[j].CastItem(j,my.items[j],my,search[i]);
					if (my.items[j] == null) menu_show = 1;
				}
			}
		}
		if (menu_show == 4)
		{
			if (my.items[j].type == 3)
			{
				for (int k = 0; k < search[pc_number].acc.Length; k++)
				{
					if (GUI.Button(CharacterSet(slot_button[j].x,slot_button[j].y,slot_button[j].width,slot_button[j].height,k), AccName(pc_number,k)))
						Equip(pc_number,k);
				}
			}else if (my.items[j].type == search[pc_number].ID+3)
			{
				for (int k = 0; k < search[pc_number].wep.Length; k++)
				{
					if (GUI.Button(CharacterSet(slot_button[j].x,slot_button[j].y,slot_button[j].width,slot_button[j].height,k), AccName(pc_number,k)))
						Equip(pc_number,k);
				}
			}
		}
	}
}

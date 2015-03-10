using UnityEngine;
using System.Collections;
using Database;

public class E_Gear : MonoBehaviour {

	public Rect Box, Text, Main, Off, Acc1, Acc2, Blessing;
	public Rect MainButton, OffButton, Acc1Button, Acc2Button, BlessingButton, Blessing2Button;
	
	GUIStyle smallFont = new GUIStyle();
	public Texture2D[] potrait;
	
	E_Status selected;
	E_Equip list;
	PC_Main equip;

	#region Naming string
	string WepName(int i)
	{
		if (equip != null)
		{
		if (equip.wep[i] != null && equip.wep[i].name != "None") 
			return equip.wep[i].name;
		else return "";
		} else return "";
	}
	
	string AccName(int i)
	{
		if (equip != null)
		{
		if (equip.acc[i] != null && equip.acc[i].name != "None") 
			return equip.acc[i].name;
		else return "";
		} else return "";
	}
	
	string BlessingName(int i)
	{
		if (equip != null)
		{
		if (equip.ability[i] != null && equip.ability[i].name != "None") 
			return equip.ability[i].name;
		else return "";
		} else return "";
	}
	#endregion	
	void Start()
	{
		smallFont.fontSize = 20;
		smallFont.normal.textColor = Color.white;
	}

	void FixedUpdate()
	{
		selected = GetComponent<E_Status>();
		list = GetComponent<E_Equip>();
		equip = selected.selected.GetComponent<PC_Main>();
	}
	#region Making List
	void MakeWepList(int i)
	{
		if (list.weapons == false && list.gearID == false)
		{
			list.magicnum = i;
			if (i != 0)
			{
				weapon gen = new weapon();
				gen.name = "None";
				list.E_weapons.Add(gen);
				if (equip.wep[1] == null) equip.wep[1] = gen;
			}
			foreach( weapon w in WeaponsList.weapons)
				if (selected.Id == w.type) 
			{
				if (equip.wep[i] != null) 
				{
					equip.wep[i].equipped = false;
					list.bonus = equip.damage - equip.wep[i].damage;
					list.bonus1 = equip.hit - equip.wep[i].hit;
				}
				if (w.equipped == false) list.E_weapons.Add(w);
			}
			list.weapons = true;
			list.gearID = true;
		}
	}

	void MakeAccList(int i)
	{
		if (list.accessories == false && list.gearID == false)
		{
			list.magicnum = i;
			accessory gen = new accessory();
			gen.name = "None";
			list.E_acc.Add(gen);
			if (equip.acc[0] == null) equip.acc[0] = gen;
			if (equip.acc[1] == null) equip.acc[1] = gen;

			foreach(accessory a in AccList.Accessories)
				if (a.type == 0) 
			{
				if (equip.acc[i] != null) 
				{
					equip.acc[i].equipped = false;
					list.bonus = equip.stats[2,0] - equip.acc[i].Brawns;
					list.bonus1 = equip.stats[2,1] - equip.acc[i].Tenacity;
					list.bonus2 = equip.stats[2,2] - equip.acc[i].Courage;
				}
				if (a.equipped == false) list.E_acc.Add(a);
			}
			list.accessories = true;
			list.gearID = true;
		}
	}

	void MakeBlessList(int i)
	{
		if (list.blessings == false && list.gearID == false)
		{
			list.magicnum = i;
			Abilities gen2 = new Abilities();
			gen2.name = "None";
			list.E_abilities.Add(gen2);
			
	/*		foreach( Abilities b in equip.abilities)
				if (b.type == 1)
			{
				if (equip.ability[i] != null) equip.ability[i].equipped = false;
				if (b.equipped == false) list.E_abilities.Add(b);
			}*/
	//		if (equip.ability[i] != null) check.DeBlessings(equip.ability[i].name);
			list.blessings = true;
			list.gearID = true;
		}
	}
	#endregion
	void OnGUI()
	{
		GUI.Box(Box,"");
		if (selected != null) GUI.DrawTexture(Text, potrait[selected.Id]);

		GUI.Label(Main, "Main:", smallFont);
		if (GUI.Button(MainButton, WepName(0))) MakeWepList(0);

		GUI.Label(Off, "Off:", smallFont);
		if (GUI.Button (OffButton, WepName(1))) MakeWepList(1);

		GUI.Label(Acc1, "Acc:", smallFont);
		if (GUI.Button(Acc1Button, AccName(0))) MakeAccList(0);

		if (equip.second_acc == true)
		{
			GUI.Label(Acc2, "Acc:", smallFont);
			if (GUI.Button(Acc2Button, AccName(1))) MakeAccList(1);
		}

		GUI.Label(Blessing, "Blessing", smallFont);
		if (GUI.Button(BlessingButton, BlessingName(0))) MakeBlessList(0);

		if (equip.soul_mixture == true)
		{
			BlessingButton.width = 78.95f;
			if (GUI.Button(Blessing2Button, BlessingName(1))) MakeBlessList(1);
		}else BlessingButton.width = 157.9f;
	}
}

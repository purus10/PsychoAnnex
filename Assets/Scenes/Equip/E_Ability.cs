using UnityEngine;
using System.Collections;
using Database;

public class E_Ability : MonoBehaviour {

	public Rect Box, Title;
	public Rect[] ability,ability_button;
	
	GUIStyle smallFont;
	GUIStyle center;
	
	E_Equip list;
	E_Status selected;
	PC_Main equip;
	
	void Start()
	{
		center = new GUIStyle();
		center.alignment = TextAnchor.MiddleCenter;
		center.fontSize = 20;
		center.normal.textColor = Color.white;
		
		smallFont = new GUIStyle();
		
		smallFont.fontSize = 30;
		smallFont.normal.textColor = Color.white;
	}

	void MakeList(int i)
	{
		if (list.gearID == false && list.abilities == false)
		{
			list.magicnum = i;
			Ability gen = new Ability();
			gen.name = "None";
			list.E_abilities.Add(gen);
			
			foreach( Ability Ab in equip.abilities)
			{
				if (equip.ability[i] != null) equip.ability[i].equipped = false;
				if (Ab.equipped == false && Ab.ID != 3) list.E_abilities.Add(Ab);
			}
			list.abilities = true;
			list.gearID = true;
		}
	}
	
	void FixedUpdate()
	{
		selected = GetComponent<E_Status>();
		equip = selected.selected.GetComponent<PC_Main>();
		list = GetComponent<E_Equip>();
	}
	
	string AbilityName(int i)
	{
		if (equip != null && equip.ability[i] != null && equip.ability[i].name != "None") return equip.ability[i].name;
		else return "";
	}
	
	void OnGUI () {
		GUI.Box(Box,"");
		GUI.Label(Title,"Abilities", smallFont);

		for (int i = 0; i < ability.Length;i++)
		{
			GUI.Label(ability[i],GameInformer.A[i] + ":", center);
			if (GUI.Button(ability_button[i], AbilityName(i))) 
				if (AbilityName(i) != "Attack") MakeList(i);
		}
	}
}

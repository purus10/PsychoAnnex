using UnityEngine;
using System.Collections;
using Database;

public class E_Ability : MonoBehaviour {
	
	public Rect Box,Title,W,A,S,D;
	public Rect WButton,AButton,SButton,DButton;
	
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
				if (Ab.equipped == false) list.E_abilities.Add(Ab);
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
		
		GUI.Label(W,GameInformer.A1 + ":", center);
		GUI.Button(WButton,"Attack");
		
		GUI.Label(A, GameInformer.A2 + ":", center);
		if (GUI.Button(AButton, AbilityName(1))) MakeList(1);
		
		GUI.Label(S,GameInformer.A3 + ":", center);
		if (GUI.Button(SButton, AbilityName(2))) MakeList(2);
		
		GUI.Label(D,GameInformer.A4 + ":", center);
		if (GUI.Button(DButton, AbilityName(3))) MakeList(3);
	}
}

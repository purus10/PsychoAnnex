using UnityEngine;
using System.Collections;

public class Finish_Button : MonoBehaviour {

	public Rect Box;
	public bool final = false;
	Custom_Wep wep;
	Custom_Acc acc;
	Selected_Button selected;
	Metal_Button metals;

	void Start()
	{
		selected = GetComponent<Selected_Button>();
		wep = GetComponent<Custom_Wep>();
		acc = GetComponent<Custom_Acc>();
		metals = GetComponent<Metal_Button>();

	}

	void WeaponFinalization()
	{
		selected.wep.damage += wep.dmgbonus;
		selected.wep.hit += wep.hitbonus;
		selected.wep.weight = wep.weight;

		for(int i = 0; i < selected.wep.metal.Length;i++)
		{
			if (selected.wep.metal[i] == null && metals.metal != null)
			{
				selected.wep.metal[i] = metals.metal[i];
			}
		}
	}

	/*void AccessoyFinalization()
	{
		acc.oName = acc.acc.name;
		acc.oBrawns = acc.acc.Brawns;
		acc.oTenacity = acc.acc.Tenacity;
		acc.oCourage = acc.acc.Courage;
		if (acc.acc.metal1 == "")
		{
			acc.acc.metal1 = acc.metal1;
		}
		
		if (acc.acc.metal2 == "")
		{
			acc.acc.metal2 = acc.metal2;
		}
		
		if (acc.acc.metal3 == "")
		{
			acc.acc.metal3 = acc.metal3;
		}
	}*/

	void OnGUI()
	{
		if (final == false)
		{
		if(GUI.Button(new Rect(Screen.width/2 - Box.x + 5f,Screen.height/2 - Box.y - 30f, Box.width, Box.height - 10f), "Finish"))
		{
			if (selected.wep != null)
			{
			final = true;
			}
		}
		}

		if (final)
		{
			GUI.Box(new Rect (Screen.width/2 - Box.x, Screen.height/2 - Box.y - 81.5f, Box.width + 8.5f, Box.height + 46f), "Are you sure?");

			if(GUI.Button(new Rect(Screen.width/2 - Box.x + 5f,Screen.height/2 - Box.y - 60f, Box.width, Box.height - 10f), "Yes"))
			{
				if (selected.wep != null)
				{
					WeaponFinalization();
					final = false;
				}/*else if (acc.acc != null)
				{
					AccessoyFinalization();
					final = false;
				}*/
			}

			if(GUI.Button(new Rect(Screen.width/2 - Box.x + 5f,Screen.height/2 - Box.y - 30f, Box.width, Box.height - 10f), "No"))
			{
				final = false;
			}
			
		}
	}
}

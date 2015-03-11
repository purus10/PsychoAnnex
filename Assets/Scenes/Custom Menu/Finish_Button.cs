using UnityEngine;
using System.Collections;

public class Finish_Button : MonoBehaviour {

	public Rect Box;
	public bool final = false;
	//Custom_Wep wep = null;
	//Custom_Acc acc = null;

	void Update()
	{
	//	wep = GetComponent<Custom_Wep>();
	//	acc = GetComponent<Custom_Acc>();

	}

/*	void WeaponFinalization()
	{
		wep.oName = wep.wep.name;
		wep.wep.damage += wep.dmgbonus;
		wep.wep.hit += wep.hitbonus;
		wep.wep.weight = wep.weight;
		if (wep.wep.metal1 == "")
		{
		wep.wep.metal1 = wep.metal1;
		}

		if (wep.wep.metal2 == "")
		{
		wep.wep.metal2 = wep.metal2;
		}

		if (wep.wep.metal3 == "")
		{
		wep.wep.metal3 = wep.metal3;
		}
		wep.FinalConversion(wep.weight,wep.wep);
	}

	void AccessoyFinalization()
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
	}

	void OnGUI()
	{
		if (final == false)
		{
		if(GUI.Button(new Rect(Screen.width/2 - Box.x + 5f,Screen.height/2 - Box.y - 30f, Box.width, Box.height - 10f), "Finish"))
		{
			if (wep.wep != null || acc.acc != null)
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
				if (wep.wep != null)
				{
					WeaponFinalization();
					final = false;
				}else if (acc.acc != null)
				{
					AccessoyFinalization();
					final = false;
				}
			}

			if(GUI.Button(new Rect(Screen.width/2 - Box.x + 5f,Screen.height/2 - Box.y - 30f, Box.width, Box.height - 10f), "No"))
			{
				final = false;
			}
			
		}
	}*/
}

using UnityEngine;
using System.Collections;
using Database;

public class Custom_Wep : MonoBehaviour {
	
	//13-1 13 is the heaviest, startign weight is balanced, 1 is lightest.
	//If metal is under starting the metals bonus. If higher plus it.
	
	//0 = 1,12 = 13
	public TextMesh[] number;
	Selected_Button selected;
	
	public int weight;
	
	public int oMax;
	public string oName;
	public int oWeight;
	
	int olddmg;
	int oldhit;
	int newdmg;
	int newhit;
	public int dmgbonus;
	public int hitbonus;
	
	#region Weight Conversion
	// Use this for initialization
	public int WeightIncrease(string metal) 
	{
		switch(metal)
		{
		case "Cobolt":
			return 1;
		case "Copper":
			return 2;
		case "Bronze":
			return 0;
		case "Gleipmir":
			return -6;
		case "Lead":
			return 4;
		case "Levaithan":
			return 6;
		case "Magnesium":
			return -5;
		case "Platinum":
			return 5;
		case "Steel":
			return -1;
		case "Silver":
			return 3;
		case "Tin":
			return -2;
		case "Zinc":
			return -3;
		default:
			return 0;
		}
	}
	#endregion
	void Start()
	{
		selected = GetComponent<Selected_Button>();
	}
	public void DisplayWeight()
	{
		if (selected.wep != null) for (int i = 1; i < number.Length;i++)
		{
			if (selected.wep != null)
			{
			if (i == Mathf.Clamp(weight,1,13)) number[i-1].color = Color.red;
			else number[i].color = Color.white;

			if (i == selected.wep.weight) number[i-1].color = Color.yellow;
			else number[i].color = Color.white;
			}else number[i].color = Color.white;
		}
	}
	
	public void WepCalculation()
	{
		if (selected.wep != null)
		{
		int w = Mathf.Clamp(weight,1,13);
		olddmg = selected.wep.weight - 7;
		oldhit = (selected.wep.weight - 7) - ((selected.wep.weight - 7)*2); 
		
		newdmg =  w- 7;
		newhit = (w - 7) - ((w - 7)*2);
		
		dmgbonus = newdmg - olddmg;
		if (dmgbonus < 0 && hitbonus >= selected.wep.max) dmgbonus += selected.wep.max;
		
		hitbonus = newhit - oldhit;
		if (hitbonus < 0 && dmgbonus >= selected.wep.max) hitbonus += selected.wep.max;
		}
	}

	void FixedUpdate()
	{
		DisplayWeight();
		WepCalculation();
	}
}
	//#endregion
//}
//}

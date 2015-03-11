using UnityEngine;
using System.Collections;
using Database;

public class Custom_Acc : MonoBehaviour {

	//13-1 13 is the heaviest, 7 is balanced (0), 1 is lightest.
	//If metal is under 7 minues the metals  bonus. If higher plus it.

	public string oName;
	public int oBrawns;
	public int oTenacity;
	public int oCourage;

	public string metal1;
	public string metal2;
	public string metal3;
	
	public accessory acc;
	
	#region Stat Conversion
	// Use this for initialization
	public void StatIncrease(Item metal) 
	{
		switch(metal.name)
		{
		case "Cobolt":
			acc.Tenacity += 1;
			break;
		case "Copper":
			acc.Brawns += 1;
			break;
		case "Bronze":
			break;
		case "Gleipmir":
			acc.Tenacity += 2;
			break;
		case "Gold":
			acc.Courage += 2;
			break;
		case "Lead":
			acc.Brawns += 1;
			break;
		case "Levaithan":
			acc.Brawns += 2;
			break;
		case "Magnesium":
			acc.Tenacity += 1;
			break;
		case "Platinum":
			acc.Tenacity += 1;
			break;
		case "Steel":
			acc.Courage += 1;
			break;
		case "Silver":
			acc.Courage += 1;
			break;
		case "Tin":
			acc.Tenacity += 1;
			break;
		case "Zinc":
			acc.Brawns += 1;
			break;
		default:
			break;
		}
	}

	public void StatDecrease(Item metal) 
	{
		switch(metal.name)
		{
		case "Cobolt":
			acc.Tenacity -= 1;
			break;
		case "Copper":
			acc.Brawns -= 1;
			break;
		case "Bronze":
			break;
		case "Gleipmir":
			acc.Tenacity -= 2;
			break;
		case "Gold":
			acc.Courage -= 2;
			break;
		case "Lead":
			acc.Brawns -= 1;
			break;
		case "Levaithan":
			acc.Brawns -= 2;
			break;
		case "Magnesium":
			acc.Tenacity -= 1;
			break;
		case "Platinum":
			acc.Tenacity -= 1;
			break;
		case "Steel":
			acc.Courage -= 1;
			break;
		case "Silver":
			acc.Courage -= 1;
			break;
		case "Tin":
			acc.Tenacity -= 1;
			break;
		case "Zinc":
			acc.Brawns -= 1;
			break;
		default:
			break;
		}
	}
	#endregion
	#region Final Bonus
	void Update()
	{
	}

	#endregion
	#region ComboNames
	public void ComboNames()
	{
		if (acc != null)
		{
			if (acc.name == "Ring")
			{
			}
		}
		#endregion
	}
}

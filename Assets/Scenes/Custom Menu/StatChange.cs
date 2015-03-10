using UnityEngine;
using System.Collections;

public class StatChange : MonoBehaviour {
	
	public TextMesh Damage, Hit, Courage;
	int b1, b2, b3;
	Selected_Button selected;
	Custom_Wep cw;
	Metal_Button metal;

	void Start () 
	{
		selected = GetComponent<Selected_Button>();
		cw = GetComponent<Custom_Wep>();
		metal = GetComponent<Metal_Button>();
	}
	// Update is called once per frame
	void Update () 
	{
	if (selected.wep != null)
	{

		if (cw.dmgbonus + selected.wep.damage > selected.wep.damage)
		{
			Damage.color = Color.green;
			Damage.text = cw.dmgbonus.ToString();
			Damage.GetComponent<Renderer>().enabled = true;
		} else if (cw.dmgbonus + selected.wep.damage < selected.wep.damage)
		{
			Damage.color = Color.red;
			Damage.text = cw.dmgbonus.ToString();
			Damage.GetComponent<Renderer>().enabled = true;
		} else {
			Damage.GetComponent<Renderer>().enabled = false;
		}
		
		if (cw.hitbonus + selected.wep.hit > selected.wep.hit)
		{
			Hit.color = Color.green;
			Hit.text = cw.hitbonus.ToString();
			Hit.GetComponent<Renderer>().enabled = true;
		} else if (cw.hitbonus + selected.wep.hit < selected.wep.hit)
		{
			Hit.color = Color.red;
			Hit.text = cw.hitbonus.ToString();
			Hit.GetComponent<Renderer>().enabled = true;
		} else {
			Hit.GetComponent<Renderer>().enabled = false;
		}
	}else{
			Damage.GetComponent<Renderer>().enabled = false;
			Damage.GetComponent<Renderer>().enabled = false;
			Hit.GetComponent<Renderer>().enabled = false;
		}
	}
}

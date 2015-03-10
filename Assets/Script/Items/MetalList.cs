using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Database;

public class MetalList : MonoBehaviour {

	public List <item> metals = new List<item>();
	public int ID;
	public Custom_Wep metal = null;

	void Update()
	{

		if(Input.GetKeyDown(KeyCode.L))
		{
		metal  = GetComponent<Custom_Wep>();
		for(int x = 0; x < ItemList.items.Count; x++)
		{
			if(ItemList.items[x].type == 2)
			{
				metals.Add(ItemList.items[x]);
			}
		}
		}
	}
	

}

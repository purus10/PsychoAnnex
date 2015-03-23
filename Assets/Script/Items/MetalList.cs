using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Database;

public class MetalList : MonoBehaviour {

	public List <Item> metals = new List<Item>();
	public int ID;
	public Custom_Wep metal = null;

	void Update()
	{

		if(Input.GetKeyDown(KeyCode.L))
		{
		metal  = GetComponent<Custom_Wep>();
		for(int x = 0; x < ItemList.items.Count; x++)
		{

		}
		}
	}
	

}

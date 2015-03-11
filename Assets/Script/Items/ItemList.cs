using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Database;

public class ItemList : MonoBehaviour {

	static public List <item> items = new List<item>();
	

	void Update()
	{

		if (Input.GetKeyDown(KeyCode.A))
		{
			item check = new item();

			check.TManage("Bronze");
			check.TManage("Copper");
			check.TManage("Levaithan");
			check.TManage("Gold");
			check.TManage("Cobolt");
			check.TManage("Platinum");
			check.TManage("Lead");
			check.TManage("Silver");
			check.TManage("Steel");
			check.TManage("Tin");
			check.TManage("Zinc");
			check.TManage("Magnesium");
			check.TManage("Gleipmir");

		}
	}
}
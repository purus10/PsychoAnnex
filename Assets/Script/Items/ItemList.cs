using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Database;

public class ItemList : MonoBehaviour {

	static public List <item> items = new List<item>();

	void Awake()
	{
		item none = new item();
		none.name = "koko";
		none.amount = 4;
		none.type = 1;
		items.Add(none);

		item none2 = new item();
		none2.name = "koadsako";
		none2.amount = 4;
		none.type = 1;
		items.Add(none2);
	}

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
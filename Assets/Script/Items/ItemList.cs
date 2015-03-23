using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Database;

public class ItemList : MonoBehaviour {

	static public List <Item> items = new List<Item>();

	void Start()
	{
		if (items.Count < 2)
		{
		Add("Pistol",1,1);
		Add("Tar Water",0,2);
		Add("Pistol",1,1);
		Add("Tar Water",0,2);
		Add("Pistol",1,1);
		Add("Tar Water",0,2);
		Add("Pistol",1,1);
		Add("Tar Water",0,2);
		Add("Pistol",1,1);
		Add("Tar Water",0,2);
		Add("Pistol",1,1);
		Add("Tar Water",0,2);
		Add("Pistol",1,1);
		Add("Tar Water",0,2);
		Add("Bronze",2,15);
		Add("Copper",2,15);
		Add("Levaithan",2,15);
		Add("Gold",2,15);
		Add("Cobolt",2,15);
		Add("Platinum",2,15);
		Add("Lead",2,15);
		Add("Silver",2,15);
		Add("Steel",2,15);
		Add("Tin",2,15);
		Add("Zinc",2,15);
		Add("Magnesium",2,15);
		Add("Gleipmir",2,15);
		}
	}

	void Add(string n, int t, int a)
	{
		Item i = new Item();
		i.name = n;
		i.type = t;
		i.amount = a;

		items.Add(i);

	}
}
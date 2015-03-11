using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

	static public List <Item> Items = new List<Item>();
	static public List <accessory> Accessories = new List<accessory>();
	static public List <weapon> Weapons = new List<weapon>();

	// Use this for initialization
	void Start () {

		if (Weapons.Count == 0)
		{

		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

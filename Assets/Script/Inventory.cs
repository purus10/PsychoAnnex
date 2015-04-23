using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Database;

public class Inventory : MonoBehaviour {

	static public List <Item> Items = new List<Item>();
	static public List <Item> Accessories = new List<Item>();
	static public List <Item> Weapons = new List<Item>();

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

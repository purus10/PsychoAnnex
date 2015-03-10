using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Database;

public class AccList : MonoBehaviour {

	static public List <accessory> Accessories = new List<accessory>();
	
	void generateAcc()
	{
		accessory gen = new accessory();

		gen.name = "Ring";
		gen.Brawns = 1;

		Accessories.Add(gen);

		accessory f = new accessory();
		
		f.name = "fa";
		f.Brawns = 1000;
		f.type = 0;
		Accessories.Add(f);

	}
	
	// Use this for initialization
	void Start () {
		generateAcc();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

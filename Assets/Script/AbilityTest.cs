using UnityEngine;
using System.Collections;
using Database;

public class AbilityTest : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		PC_Main[] search = GameObject.FindObjectsOfType(typeof(PC_Main)) as PC_Main[];

		foreach(PC_Main p in search)
		{
			if (p.abilities.Count < 2)
			{
			Add ("Attack","Attack depending on player deals a diffrent secondary effect",false,0,10,1,p);
			Add ("Anima","Attack the soul of your enemy",true,2,10,1,p);
			Add ("Barrage","use remaining beats as Far range",true,0,10,3,p);
			Add ("Butterfly","Banish the weakest opponent",true,2,10,2,p);
			Add ("Chaos","Attack multiple times depending on tenacity",false,0,3,2,p);
			Add ("Eximo","Deal great amount of damage but chance of destroying your weapon",false,0,3,2,p);
			Add ("Feint","escape Close range and use remaining beats as Far range",true,0,10,2,p);
			Add ("Panacea","+1 beat",true,20000000000,2000000000000,2,p);
			}
		}
	}

	void Add(string n, string d, bool f,float x, float m, int t,PC_Main p)
	{
		Ability a = new Ability();
		a.name = n;
		a.description = d;
		a.far_range = f;
		a.max_range = x;
		a.min_range = m;
		a.type = t;
		p.abilities.Add(a);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

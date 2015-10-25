using UnityEngine;
using System.Collections;
using Database;

public class AbilityTest : MonoBehaviour {
	
	// Use this for initialization
	
	void Awake () 
	{
		PC_Main[] search = GameObject.FindObjectsOfType(typeof(PC_Main)) as PC_Main[];
		
		foreach(PC_Main p in search)
		{
			if (p.abilities.Count < 2)
			{
				// type 1 == enemy 2 == ally 3 == self ID 1 == ability 2 == blessing 3 == goddess gift
				Add ("Attack","Attack depending on player deals a diffrent secondary effect",false,0,10,1,1,p);
				/*Add ("Anima","Attack the soul of your enemy",true,2,10,1,1,p);
				Add ("Barrage","use remaining beats as Far range",true,0,10,3,1,p);
				Add ("Butterfly","Banish the weakest opponent",true,2,10,3,1,p);
				Add ("Chaos","Attack multiple times depending on tenacity",false,10000000000000000000f,0,1,1,p);
				Add ("Eximo","Deal great amount of damage but chance of destroying your weapon",false,3,0,1,1,p);
				Add ("Feint","escape Close range and use remaining beats as Far range",false,0,10,1,1,p);
				Add ("Fidelity","Recharge a Goddess Gift",true,20,0,2,1,p);
				//Add ("Hush","+1 beat",true,20000000000,2000000000000,2,1,p);
				Add ("Inquisitio","+1 beat",true,20000000000,2000000000000,2,1,p);
				//Add ("Jinx","+1 beat",true,20000000000,2000000000000,2,1,p);
				Add ("Kadabra","+1 beat",true,20000000000,2000000000000,2,1,p);
				Add ("Karma","+1 beat",false,20000000000,2000000000000,2,1,p);
				Add ("Libro","+1 beat",false,20000000000,2000000000000,2,1,p);
				Add ("Luck","+1 beat",false,20000000000,2000000000000,2,1,p);
				Add ("Oblivio","+1 beat",true,20000000000,2000000000000,2,1,p);
				Add ("Omni","+1 beat",true,20000000000,2000000000000,2,1,p);
				Add ("Panacea","+1 beat",true,20000000000,2000000000000,2,1,p);
				Add ("Provoke","+1 beat",true,20000000000,2000000000000,2,1,p);
				Add ("Pulse","+1 beat",true,20000000000,2000000000000,2,1,p);
				Add ("Rapture","+1 beat",false,20000000000,2000000000000,2,1,p);
				Add ("Verto","+1 beat",true,20000000000,2000000000000,2,1,p);
				Add ("Day","Recharge a Goddess Gift",true,20,0,2,2,p);
				Add ("Divine","Recharge a Goddess Gift",true,20,0,2,2,p);
				Add ("Earth","Recharge a Goddess Gift",false,20,0,2,2,p);
				Add ("Fire","Recharge a Goddess Gift",true,20,0,2,2,p);
				Add ("Nature","Recharge a Goddess Gift",true,20,0,2,2,p);
				Add ("Night","Recharge a Goddess Gift",true,20,0,2,2,p);
				Add ("Thunder","Recharge a Goddess Gift",true,20,0,2,2,p);
				Add ("Water","Recharge a Goddess Gift",true,20,0,2,2,p);
				Add ("Wicked","Recharge a Goddess Gift",true,20,0,2,2,p);
				Add ("Wind","Recharge a Goddess Gift",true,20,0,2,2,p);
				Add ("Aqua","Recharge a Goddess Gift",true,20,0,2,3,p);
				Add ("Attonitus","Recharge a Goddess Gift",true,20,0,2,3,p);
				Add ("Divinus","Recharge a Goddess Gift",false,20,0,2,3,p);
				Add ("Ignis","Recharge a Goddess Gift",true,20,0,2,3,p);
				Add ("Lex","Recharge a Goddess Gift",true,20,0,2,3,p);
				Add ("Maleficus","Recharge a Goddess Gift",true,20,0,2,3,p);
				Add ("Natura","Recharge a Goddess Gift",true,20,0,2,3,p);
				Add ("Nocturne","Recharge a Goddess Gift",true,20,0,2,3,p);
				Add ("Terra","Recharge a Goddess Gift",true,20,0,2,3,p);
				Add ("Ventus","Recharge a Goddess Gift",true,20,0,2,3,p);
				Passive a = new Passive();
				a.Name = "Masterful";
				p.passives.Add(a);
				Passive b = new Passive();
				b.Name = "Masterful";
				p.passives.Add(b);
				Passive c = new Passive();
				c.Name = "Masterful";
				p.passives.Add(c);*/

			}
		}
	}
	
	void Add(string n, string d, bool f,float x, float m, int t, int i,PC_Main p)
	{
		Ability a = new Ability();
		a.name = n;
		a.description = d;
		a.max_range = x;
		a.min_range = m;
		a.type = t;
		a.ID = i;
		p.abilities.Add(a);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

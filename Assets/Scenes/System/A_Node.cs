using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Database;

public class A_Node : MonoBehaviour {
	
	static public A_Node[] search;
	string [][] names = new string[][]{
		/*0*/new string[]{"Able Learner","Ingenuity","Sol"}, 
		/*1*/new string[]{"Psycho","Adapt","Leech"}, 
		/*2*/new string[]{"Theivery","Lady Luck","Master of Mockery"},  
		/*3*/new string[]{"Unlocked Mind","Brute"},
		/*4*/new string[]{"Critical Shot","Acrobat","Energy Mixture","Running Shot","Steady","With Love"},
		/*5*/new string[]{"Deft","Devil's Touch","Onslaught","Titan"},
		/*6*/new string[]{"Distilled","Guardian","Quell","Ranged Disarm"}, 
		/*7*/new string[]{"Endure","Flurry","Masterful","Sufficient"}, 
		/*8*/new string[]{"Hush","Jinx","Karma","Luck","Provoke"}, 
		/*9*/new string[]{"Chaos","Barrage","Fidelity","Omni","Rapture","Verto"},
		/*10*/new string[]{"Fire","Earth","Wind","Thunder", "Water", "Nature", "Night"}};
	int [][] ranges = new int[][]{new int[]{1,1,1,1,1,1},new int[]{1,3,2,3,1,1},new int[]{1,1,3,1,1,1,2}};
	int [][] costs = new int[][]{
		new int[]{1,1,1},
		new int[]{4,4,4},
		new int[]{3,1,2},
		new int[]{6,6},
		new int[]{2,2,3,2,2,5},
		new int[]{1,2,3,2},
		new int[]{3,5,3,2},
		new int[]{2,3,2,2},
		new int[]{3,3,2,5,3},
		new int[]{3,8,7,5,3,5},
		new int[]{2,2,2,2,2,2,2}};
	int [][] essences = new int[][]{ //0 = Cruelty, 1 = Tranquility, 2 = Empathy, 3 = Luck
		new int[]{0,1,2},
		new int[]{0,1,2},
		new int[]{3,3,0},
		new int[]{2,0},
		new int[]{0,1,2,1,1,3},
		new int[]{0,1,0,2},
		new int[]{3,3,2,1},
		new int[]{1,3,2,1},
		new int[]{0,3,2,0,0},
		new int[]{1,1,2,1,0,2},
		new int[]{0,0,1,2,1,1,3}};
	string [][] descriptions = new string[][]{
		new string[]{"+1 Brawns, for equipping only","+1 Tenacity, for equipping only","+1 Courage for equipping only"}, 
		new string[]{"Increases Tier decrease","Lowers amount of Tier lost","Increase amount absorbed when healing"}, 
		new string[]{"Higher chance of stealing","Increase chance of ability effects","Increase odd of being targeted"},  
		new string[]{"Increase damage of magical abilities","Increase damage of physical abilities"},
		new string[]{"Far Range abilities now have a chance for critical","Higher chance of dodging far range abilities","May now equip two blessings","May now aim while moving","Easier to Aim","Grants chance of Stealing"},
		new string[]{"Thrown object may now surprise","REMOVE REMOVE REMOVE","If target is defeat may attack another target with remaining beats","Cannot be graapled, unless by magical means"},
		new string[]{"REMOVE REMOVE REMOVE REMOVE","Reduces enemies chance of hitting","On a critical, attack recieves some of the damage back","Has a chance of disarming with long range weapons"}, 
		new string[]{"Chance of ignore all damage from one beat","Chance of attacking twice in one beat","Has a chance of dodging attacks when all beats are expended","When Hp = 1 has a chance to rise back to 2."}, 
		new string[]{"Suffocate an enemy","Cause a wave of supernatural fear, reducing any Targets Tier","Ignores the Defense of the Target. Among other things","Cause a Critical","The character taunts the Target"}, 
		new string[]{"strikes targets within radius multiple times.","Allows the character to use their beats in Far Range initiation.","Refresh a Goddess Gift","Increase the divine interventions", "Character spins causing damage to any Target in range","Damage all targets based on Courage in a great radius."},
		new string[]{"","","","","","",""}};
	public GameObject[] Nodes;
	private string[] Essence_Names = new string[4]{"Cruelty","Tranquility","Empathy","Luck"};
	public GameObject main;
	public int Group_type;
	public Ability ability;
	public Passive passive;
	//for Node state: 0 == not selectable or unlocked, 1 == selectable, 2 == unlocked
	public int[] Node_State = new int[7], Essence_type = new int[7], Essence_cost = new int[7], type = new int[7];
	public string[] abilityname = new string[7], description = new string[7];
	A_Display display;
	string EssenceUsed(int essence)
	{
		return Essence_Names[essence];
	}
	int AbilityCheck(int max, PC_Main my)
	{
		int c = 0;
		int n = Random.Range(0,max);
		while (c < my.abilities.Count)
		{
			c = 0;
			if ( n < max) n++;
			else n = 0;
			if (my.abilities.Count > 0)
			{
			foreach (Ability a in my.abilities) 
				if (names[Group_type][n] != a.name) c++;
			} else c = my.abilities.Count;
		}
		return n;
	}
	int PassiveCheck(int max, PC_Main my, int oftype)
	{
		int c = 0;
		int n = Random.Range(0,max);
		while (c < my.passives.Count - oftype)
		{
			c = 0;
			if (n < max) n++;
			else n = 0;
			if (my.passives.Count > 0)
			{
			foreach (Passive a in my.passives) 
				if (names[Group_type][n] != a.Name) c++;
			} else c = my.passives.Count - oftype;
		}
		return n;
	}
	void Start()
	{
		Switch();
	}
	public void Switch () 
	{
		display = main.GetComponent<A_Display>();
		if (Node_State[display.Id-1] > 0) 
		{
			if (abilityname[display.Id-1] == "") SetNode(display.Id-1); 
			if (Node_State[display.Id-1] == 1) GetComponent<Renderer>().material.color = Color.yellow;
			else if (Group_type < 8) GetComponent<Renderer>().material.color = Color.blue;
			else GetComponent<Renderer>().material.color = Color.green;
			if (Node_State[display.Id-1] == 2) UnlockPaths(display.Id);
		}else GetComponent<Renderer>().material.color = Color.grey;
		if (abilityname[display.Id-1] != "")
		{
			if (type[display.Id-1] > 0)
			{
				ability = new Ability();
				ability.name = abilityname[display.Id-1];
				ability.type = type[display.Id-1];
				ability.description = description[display.Id-1];
			}else{
				passive = new Passive();
				passive.Name = abilityname[display.Id-1];
				passive.Description = description[display.Id-1];
			}
		}
		for (int i = 0;i < GameInformer.Essence.Length;i++) GameInformer.Essence[i] = 99;
	}
	public void UnlockPaths(int id)
	{
		if (Nodes.Length > 0) foreach (GameObject n in Nodes)
		{
			A_Node node = n.GetComponent<A_Node>();
			if (node != null && node.Node_State[display.Id-1] == 0) 
			{
				if (node.abilityname[id-1] == "") node.SetNode(display.Id-1);
				node.Node_State[display.Id-1] = 1;
				n.GetComponent<Renderer>().material.color = Color.yellow;
			}
			else {
				C_Node c_node = n.GetComponent<C_Node>();
				if (c_node != null && c_node.Node_State[display.Id-1] == 0) 
				{
					c_node.Node_State[display.Id-1] = 1;
					n.GetComponent<Renderer>().material.color = Color.yellow;
				}
				else {
					G_Node g_node = n.GetComponent<G_Node>();
					if (g_node != null && g_node.Node_State[display.Id-1] == 0) 
					{
						g_node.Node_State[display.Id-1] = 1;
						n.GetComponent<Renderer>().material.color = Color.yellow;
					}
				}
			}
		}
		Node_State[id-1] = 2;
		if (Group_type < 8) GetComponent<Renderer>().material.color = Color.blue;
		else GetComponent<Renderer>().material.color = Color.green;
		Clearinfo();
	}
	public void SetNode(int t)
	{
		int i = 0;
		if (Group_type > 8) 
		{
			i = AbilityCheck(names[Group_type].Length-1,display.stat);
			type[t] = ranges[Group_type-8][i];
		} else {
			if (Group_type > 3 && Group_type < 7) i = PassiveCheck(names[Group_type].Length-1,display.stat,0);
			else if (Group_type == 7) i = PassiveCheck(names[Group_type].Length-1,display.stat,2);
			else i = Random.Range(0,names[Group_type].Length-1);
		}
		abilityname[t] = names[Group_type][i];
		description[t] = descriptions[Group_type][i];
		Essence_cost[t] = costs[Group_type][i];
		Essence_type[t] = essences[Group_type][i];
		if (type[display.Id-1] > 0)
		{
			ability = new Ability();
			ability.name = abilityname[display.Id-1];
			ability.type = type[display.Id-1];
			ability.description = description[display.Id-1];
		}else{
			passive = new Passive();
			passive.Name = abilityname[display.Id-1];
			passive.Description = description[display.Id-1];
		}
	}
	public void Clearinfo()
	{
		display.selected = "";
		display.description = "";
		display.price = "";
	}
	void OnMouseEnter()
	{
		if (Node_State[display.Id-1] > 0 && display.selected == "")
		{
		display.description = description[display.Id-1];
		display.selected = abilityname[display.Id-1];
		display.price = Essence_cost[display.Id-1] + " " + EssenceUsed(Essence_type[display.Id-1]);
		}
	}
	void OnMouseExit()
	{
		if (display.node == null)
		{
		Clearinfo();
		}
	}
	void OnMouseDown()
	{
		if (Node_State[display.Id-1] == 1 && display.node == null)
		{
			display.node = this.gameObject;
		}
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Database;

public class A_Node : MonoBehaviour {
	
	static public A_Node[] search;
	string [][] names = new string[][]{
		new string[]{"Able Learner","Ingenuity","Sol"}, 
		new string[]{"Psycho","Adapt","Leech"}, 
		new string[]{"Theivery","Lady Luck","Master of Mockery"},  
		new string[]{"Unlocked Mind","Brute"},
		new string[]{"Critical Shot","Acrobat","Energy Mixture","Running Shot","Steady","With Love"},
		new string[]{"Deft","Devil's Touch","Onslaught","Titan"},
		new string[]{"Distilled","Guardian","Quell","Ranged Disarm"}, 
		new string[]{"Endure","Flurry","Masterful","Sufficient"}, 
		new string[]{"Hush","Jinx","Karma","Luck","Provoke"}, 
		new string[]{"Chaos","Barrage","Fidelity","Omni","Rapture","Verto"}};
	int [][] ranges = new int[][]{new int[]{1,1,1,1,1,1},new int[]{1,3,2,3,1,1}};
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
		new int[]{3,8,7,5,3,5}};
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
		new int[]{1,1,2,1,0,2}};
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
		new string[]{"strikes targets within radius multiple times.","Allows the character to use their beats in Far Range initiation.","Refresh a Goddess Gift","Increase the divine interventions", "Character spins causing damage to any Target in range","Damage all targets based on Courage in a great radius."}};
	public GameObject[] nodes;
	private string[] Essence_Names = new string[4]{"Cruelty","Tranquility","Empathy","Luck"};
	public bool[] unlocked = new bool[7], selectable = new bool[7];
	public GameObject main;
	public Ability ability;
	public Passive passive;
	public int[] Essence_type = new int[7], Essence_cost = new int[7], group_type = new int[7], type = new int[7];
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
			n = Random.Range(0,max);
			foreach (Ability a in my.abilities) 
				if (names[group_type[display.Id-1]][n] != a.name) c++;
		}
		return n;
	}
	void Start () 
	{
		display = main.GetComponent<A_Display>();
		if (abilityname[display.Id-1] == "") SetNode(display.Id-1);
		if (unlocked[display.Id-1] == true) GetComponent<Renderer>().material.color = Color.green;
		for (int i = 0;i < GameInformer.Essence.Length;i++) GameInformer.Essence[i] = 99;
		for (int i = 0; i < unlocked.Length;i++)
		{
			if (type[i] > 0)
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
	}
	public void UnlockPaths(int id)
	{
		if (nodes.Length > 0) foreach (GameObject n in nodes)
		{
			A_Node node = n.GetComponent<A_Node>();
			node.selectable[display.Id-1] = true;
			n.GetComponent<Renderer>().material.color = Color.yellow;
		}
		unlocked[display.Id-1] = true;
		GetComponent<Renderer>().material.color = Color.green;
		Clearinfo();
	}
	void SetNode(int t)
	{
		int i = 0;
		if (group_type[t] > 8) 
		{
			abilityname[t] = names[group_type[t]-8][AbilityCheck(names[group_type[t]-8].Length,display.player.GetComponent<PC_Main>())];
			type[t] = ranges[group_type[t]-8][i];
		} else i = Random.Range(0,names[group_type[t]].Length);
		abilityname[t] = names[group_type[t]][i];
		description[t] = descriptions[group_type[t]][i];
		Essence_cost[t] = costs[group_type[t]][i];
		Essence_type[t] = essences[group_type[t]][i];
	}
	public void Clearinfo()
	{
		if (display.node == null) 
		{
			display.selected = "";
			display.description = "";
			display.price = "";
		}
	}
	void OnMouseEnter()
	{
		display.description = description[display.Id-1];
		display.selected = abilityname[display.Id-1];
		display.price = Essence_cost[display.Id-1] + " " + EssenceUsed(Essence_type[display.Id-1]);
	}
	void OnMouseExit()
	{
		Clearinfo();
	}
	void OnMouseDown()
	{
		if (unlocked[display.Id-1] == true)
		{
			display.node = this.gameObject;
			display.selected = abilityname[display.Id-1];
			display.description = description[display.Id-1];
		}
	}
}

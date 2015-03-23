﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Database;

public class PC_Main : MonoBehaviour {
	
	public string Name;
	public int ID, HP, Beat, Brawns, Tenacity, Courage, type;
	public float tier_count = 5;
	[HideInInspector] public int cur_hp, cur_beats, max_acc = 2, tier = 2, damage, hit, index;
	public string[,] quirk = new string[2,4];
	public int[,] stats = new int[3,3]; //[Collum,0] (0 = cur stat, 1 = anima stat, 2 = equip stat) [0,Row] (0 = brawns, 1 = tenacity, 2 = courage) 
	public float speed;
	public Transform target;
	public bool myturn;
	public List <Transform> targets = new List<Transform>();
	public List <Ability> abilities = new List<Ability>();
	public weapon[] wep = new weapon[2];
	public accessory[] acc = new accessory[2];
	public Ability[] ability = new Ability[6];
	public Item[] items = new Item[4];
	[HideInInspector] public bool tier_4, second_acc, soul_mixture, far_beats, cover;
	public NavMeshAgent agent;
	public NPC_Main NPC;
	public PC_Main PC;
	Color target_off;
	
	void Start ()
	{
		DontDestroyOnLoad(gameObject);
		SetStats();
		FirstWeapon();
		target_off = GetComponentInChildren<Renderer>().material.color;
		agent = GetComponent<NavMeshAgent>();
	}
	
	void Update()
	{
		if (ability[0] == null)
		{
			foreach (Ability a in abilities) if (a.name == "Attack") 
			{
				a.equipped = true;
				ability[0] = a;
			}
		}

		if (GameInformer.target == this.transform && GameInformer.battle == true) myturn = true;

		if (Input.GetKeyDown(GameInformer.Fight))
		{
			if (GameInformer.target == this.transform && GameInformer.stop == false) 
			{
				myturn = true;
				GameInformer.stop = true;
				GameInformer.battle = true;
			}
			if (target != null && type == 0) agent.SetDestination(target.position);
			else type = 0;
		}
		
		if (Input.GetKeyDown(KeyCode.Tab))
		{
			if (myturn == true)
			{
				targets.Clear();
				TargetType();
			}
		}
		
		if (GameInformer.stop == false)
		{
			if (Input.GetKey(GameInformer.Up)) transform.Translate(Vector3.forward * speed * Time.deltaTime);
			if (Input.GetKey(GameInformer.Left)) transform.Rotate(Vector3.down * speed * 34 * Time.deltaTime);
			if (Input.GetKey(GameInformer.Right)) transform.Rotate(Vector3.up * speed * 34 * Time.deltaTime);
			if (Input.GetKey(GameInformer.Down)) transform.Translate(-Vector3.forward * speed * Time.deltaTime);
			if (Input.GetKey(KeyCode.LeftShift)) speed = 7;
			else speed = 5;
			
		}else if(myturn == true)
		{
			for(int i = 0; i < GameInformer.A.Length;i++)
			{
				if (Input.GetKeyDown(GameInformer.A[i])) 
					if (ability[i] != null) CastAbility(ability[i]);

				if (Input.GetKeyDown(GameInformer.A[i]) && Input.GetKey(GameInformer.ItemTog)) 
					if (items[i] != null) CastItem(items[i]);
			}
			if (Input.GetKeyDown(KeyCode.Space)) EndTurn();
		}
	}
	
	void CastAbility(Ability a)
	{
		if (type != a.type) 
		{
			type = a.type;
			TargetType();
		} else {
			if (type == 1) 
			{
				float distance = Vector3.Distance(transform.position, target.position);
				if (a.far_range == false && distance > 2f) agent.SetDestination(target.position);
				else{
				NPC = target.GetComponent<NPC_Main>();
				a.OpposeCast(this,NPC);
				}
			}
			if (type == 2 || type == 3) 
			{
				PC = target.GetComponent<PC_Main>();
				a.AllyCast(this,PC);
			}
		}
	}

		void CastItem(Item i)
	{
		if (type != i.type+1 ) type = i.type+1;
		else {
			if (type == 0) 
			{
				PC = target.GetComponent<PC_Main>();
				i.CastItem(type,i,this,PC);
			}
			if (type == 2 || type == 3) 
			{
				PC = target.GetComponent<PC_Main>();
				i.CastItem(type,i,this,PC);
			}
		}
		
	}

	void FixedUpdate () 
	{
		if (tier_count <= 0f) Tier(1,0f);
		else if (tier_count >= 5f) Tier(2,5f);
		else if (tier_count >= 10f) Tier(3,10f);
		else if (tier_count >= 15f && tier_4 == true) Tier(4,15f);
	}

	public void EndTurn()
	{
		myturn = false;
		if (cur_beats < Beat) cur_beats = Beat;
		GameInformer.Idler = (GameInformer.Idler +1) % 5;
	}
	
	void SetStats()
	{
		name = Name;
		cur_hp = HP;
		cur_beats = Beat;
		
		for (int i = 0; i < stats.GetLength(1);i++)
		{
			stats[0,i] = Brawns;
			stats[1,i] = Tenacity;
			stats[2,i] = Courage;
		}
	}
	
	void FirstWeapon()
	{
		if (wep[0] == null) foreach (weapon weapon in WeaponsList.weapons)
		{
			if (ID == weapon.type)
			{
				wep[0] = weapon;
				Equip ();
			}
		}
	}
	
	public void Equip()
	{
		int d = Brawns, h = Tenacity,b = Brawns, t = Tenacity, c = Courage;

		for (int i = 0; i < wep.Length;i++)
		{
			if (wep[i] != null)
			{
				wep[i].equipped = true;
				d += wep[i].damage;
				h += wep[i].hit;
			}
		}
		for (int i = 0; i < acc.Length;i++)
		{
			if (acc[i] != null)
			{
				acc[i].equipped = true;
				b += acc[i].Brawns;
				t += acc[i].Tenacity;
				c += acc[i].Courage;
			}
		}

		damage = d;
		hit = h;
		for(int i = 0; i < stats.GetLength(0);i++)
		{
		stats[i,0] = b;
		stats[i,1] = t;
		stats[i,2] = c;
		}
	}
	
	void Tier(int t, float limit)
	{
		if (tier_count < limit) tier_count = limit;
		if (tier != t)
		{
			tier = t;
			if (tier == 1) for (int i = 0; i < stats.GetLength(0);i++) stats[0,i] = stats[0,i]/2 - (Courage/10);
			if (tier == 3 || tier == 4) for (int i = 0; i < stats.GetLength(0);i++) stats[0,i] = stats[0,i]*(Courage/10 + (tier -1));
			if (tier == 2)
			{
				stats[0,0] = Brawns;
				stats[0,1] = Tenacity;
				stats[0,2] = Courage;
			}
		}
	}
	
	void TargetType()
	{
		if (type == 0)
		{
			Cover[] search = GameObject.FindObjectsOfType(typeof(Cover)) as Cover[];
			foreach (Cover n in search) if (Physics.Raycast(n.transform.position, transform.forward, 180.0f)) targets.Add(n.transform);	
		} else if (type == 1) 
		{
			NPC_Main[] search = GameObject.FindObjectsOfType(typeof(NPC_Main)) as NPC_Main[];
			foreach (NPC_Main n in search) targets.Add(n.transform);
		} else if (type == 2)
		{
			PC_Main[] search = GameObject.FindObjectsOfType(typeof(PC_Main)) as PC_Main[];
			foreach (PC_Main n in search) targets.Add(n.transform);
		} else if (type == 3) target = transform;
		Target();
	}
	
	void Target()
	{
		if (target == null)
		{
			targets.Sort(delegate(Transform t1, Transform t2) { 
				return (Vector3.Distance(t1.position, transform.position)).CompareTo(Vector3.Distance(t2.position,transform.position));});
			if (type != 2 )target = targets[0];
			else target = targets[1];
			transform.LookAt(target);
		} 
		else if (targets.Count > 1) 
		{
			index = (index+1) % targets.Count;
			target = targets[index];
			transform.LookAt(target);
		}
		foreach (Transform n in targets) if (n == target)
		{
			if (type != 0 && n.GetComponent<Cover>() == null) n.GetComponentInChildren<Renderer>().material.color = Color.blue;
			else n.GetComponentInParent<Renderer>().material.color = Color.blue;
		}
		else {
			if (type != 0 && n.GetComponent<Cover>() != null) n.GetComponentInParent<Renderer>().material.color = n.GetComponent<Cover>().target_off;
			if (n.GetComponent<PC_Main>() != null) n.GetComponentInChildren<Renderer>().material.color = n.GetComponent<PC_Main>().target_off;
			if (n.GetComponent<NPC_Main>() != null) n.GetComponentInChildren<Renderer>().material.color = n.GetComponent<NPC_Main>().target_off;
		}
	}
}


using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Database;

public class NPC_Main : MonoBehaviour {
	
	public string Name;
	public int HP, Defense, Hit, Beat, Brawns, Tenacity, Courage, Sky_attacks, move_points, tier = 2;
	public int[] Tier_Limit = new int[3];
	[HideInInspector] public int cur_hp, cur_beats, index;
	[HideInInspector] public int[] stats = new int[3]; // marks cur (0 = brawns, 1 = tenacity, 2 = courage)
	public float kadabra_timer, speed, tier_count;
	public Transform target;
	public bool myturn, Rose_Innate, kadabra;
	public Item[] items = new Item[4];
	[HideInInspector] public List <Transform> targets = new List<Transform>();
	//[HideInInspector] public float tier_count;
	[HideInInspector] public bool seen = true, cover = false;
	public NavMeshAgent agent;
	Animator animator;

	public Color target_off;

	void OnTriggerEnter(Collider col)
	{
		PC_Main p = col.GetComponent<PC_Main>();
		if (myturn == false && GameInformer.stop == false) 
		{
			myturn = true;
			p.NPC = this;
			p.type = 1;
			p.target = transform;
			p.transform.LookAt(target);

			PC_Main[] search = GameObject.FindObjectsOfType(typeof(PC_Main)) as PC_Main[];
			foreach(PC_Main m in search) m.BattleSetup();
		}
	}

	void Awake ()
	{
		SetStats();
		target_off = GetComponentInChildren<Renderer>().material.color;
		agent = GetComponent<NavMeshAgent>();
	}

	void Update()
	{
		if (cur_hp <= 0) Death();
		if (kadabra == true)
		{
			float time = 0;
			time++;
			if (time == kadabra_timer) kadabra = false;
		}
	}
	void Death()
	{
		PC_Main[] search = GameObject.FindObjectsOfType(typeof(PC_Main)) as PC_Main[];
		foreach(PC_Main m in search)
		{
			m.target = null;
			m.targets.Clear();
			if (m.target == this.transform) m.EndTurn();
		}
		Destroy(gameObject);
	}
	void FixedUpdate () 
	{
		if (tier_count <= Tier_Limit[0]) Tier(1,Tier_Limit[0]);
		else if (tier_count >= Tier_Limit[1]) Tier(2,Tier_Limit[1]);
		else if (tier_count >= Tier_Limit[2]) Tier(3,Tier_Limit[2]);
	}
	
	void SetStats()
	{
		name = Name;
		cur_hp = HP;
		cur_beats = Beat;
		stats[0] = Brawns;
		stats[1] = Tenacity;
		stats[2] = Courage;
	}
	
	void Tier(int t, float limit)
	{
		if (tier_count < limit) tier_count = limit;
		if (tier != t)
		{
			tier = t;
			if (tier == 1) for (int i = 0; i < stats.Length;i++) stats[i] = stats[i]/2 - (Courage/10);
			if (tier == 3) for (int i = 0; i < stats.Length;i++) stats[i] = stats[i]*(Courage/10 + (tier -1));
			if (tier == 2)
			{
				stats[0] = Brawns;
				stats[1] = Tenacity;
				stats[2] = Courage;
			}
		}
	}
	
	public void TargetType(int type)
	{
		if (type == 1) 
		{
			PC_Main[] search = GameObject.FindObjectsOfType(typeof(PC_Main)) as PC_Main[];
			foreach (PC_Main n in search) targets.Add(n.transform);
		} else if (type == 2)
		{
			NPC_Main[] search = GameObject.FindObjectsOfType(typeof(NPC_Main)) as NPC_Main[];
			foreach (NPC_Main n in search) targets.Add(n.transform);
		} else if (type == 3) target = transform;
	}
	
	public void Target()
	{
		if (target == null)
		{
			float closestDistanceSqr = Mathf.Infinity;
			foreach(Transform t in targets)
			{
				Vector3 directionToTarget = t.position - transform.position;
				float dSqrToTarget = directionToTarget.sqrMagnitude;
				if(dSqrToTarget < closestDistanceSqr)
				{
					closestDistanceSqr = dSqrToTarget;
					target = t;
				}
			}
		}
	}
}

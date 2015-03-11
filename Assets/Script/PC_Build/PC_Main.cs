using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Database;

public class PC_Main : MonoBehaviour {
	
	public string Name;
	public int ID, HP, Beat, Brawns, Tenacity, Courage, type;
	[HideInInspector] public int cur_hp, cur_beats, max_acc = 2, tier = 2, damage, hit, index;
	public string[,] quirk = new string[2,4];
	public int[,] stats = new int[3,3]; // Row (0 = brawns, 1 = tenacity, 2 = courage) Collum(0 = cur stat, 1 = anima stat, 2 = equip stat) 
	public float speed;
	public Transform target;
	public bool myturn;
	public List <Transform> targets = new List<Transform>();
	public List <Ability> abilities = new List<Ability>();
	public weapon[] wep = new weapon[2];
	public accessory[] acc = new accessory[2];
	public Ability[] ability = new Ability[4];
	public item[] items = new item[4];
	[HideInInspector] public float tier_count;
	[HideInInspector] public bool tier_4, second_acc, soul_mixture, far_beats, cover;
	public NavMeshAgent agent;
	public Animator animator;
	public NPC_Main NPC;
	public PC_Main PC;
	Color target_off;
	
	void Start ()
	{
		foreach (Ability a in abilities) if (a.name == "Attack") 
		{
			ability[0] = a;
			ability[0].equipped = true;
		}
		DontDestroyOnLoad(gameObject);
		SetStats();
		FirstWeapon();
		target_off = GetComponentInChildren<Renderer>().material.color;
		animator = GetComponent<Animator>();
		agent = GetComponent<NavMeshAgent>();
	}
	
	void Update()
	{
		if (Input.GetKeyDown(GameInformer.Fight))
		{
			if (GameInformer.Idler == ID && GameInformer.stop == false) 
			{
				myturn = true;
				GameInformer.stop = true;
			}
			if (target != null && type == 0) agent.SetDestination(target.position);
		}
		
		if (Input.GetKeyDown(KeyCode.Tab))
		{
			if (myturn == true)
			{
				targets.Clear();
				TargetType();
				Target();
			}
		}
		
		if (GameInformer.stop == false)
		{
			if (Input.GetKey(GameInformer.Up)) 
			{
				transform.Translate(Vector3.forward * speed * Time.deltaTime);
				animator.SetFloat("Speed", speed);
			} else animator.SetFloat("Speed", 0);
			
			if (Input.GetKey(KeyCode.LeftShift))
			{
				speed = 7;
				animator.SetBool("Run", true);
			}else{ 
				speed = 5;
				animator.SetBool("Run", false);
			}
			
			if (Input.GetKey(GameInformer.Left)) 
			{
				transform.Rotate(Vector3.down * speed * 34 * Time.deltaTime);
				animator.SetBool("Left turn", true);
			} else animator.SetBool("Left turn", false);
			
			if (Input.GetKey(GameInformer.Right)) 
			{
				transform.Rotate(Vector3.up * speed * 34 * Time.deltaTime);
				animator.SetBool("Right turn", true);
			}else animator.SetBool("Right turn", false);
			
			if (Input.GetKey(GameInformer.Down)) transform.Translate(-Vector3.forward * speed * Time.deltaTime);
		}else if(myturn == true)
			{
				animator.SetFloat("Speed", 0);
				animator.SetBool("Run", false);
				animator.SetBool("Left turn", false);
				animator.SetBool("Right turn", false);
				
			if (Input.GetKeyDown(GameInformer.A1)) if (ability[0] != null) CastAbility(ability[0]);	
			if (Input.GetKeyDown(GameInformer.A1)) if (ability[1] != null)CastAbility(ability[1]);
			if (Input.GetKeyDown(GameInformer.A1)) if (ability[2] != null)CastAbility(ability[2]);
			if (Input.GetKeyDown(GameInformer.A1)) if (ability[3] != null)CastAbility(ability[3]);
			}
	}
	
	void CastAbility(Ability a)
	{
		if (type != a.type) type = a.type;
		else {
			if (type == 1) 
			{
				NPC = target.GetComponent<NPC_Main>();
				a.OpposeCast(this,NPC);
			}
			if (type == 2 || type == 3) 
			{
				PC = target.GetComponent<PC_Main>();
				a.AllyCast(this,PC);
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
			if (tier == 1) for (int i = 0; i < stats.GetLength(0);i++) stats[i,0] = stats[i,0]/2 - (Courage/10);
			if (tier == 3 || tier == 4) for (int i = 0; i < stats.GetLength(0);i++) stats[i,0] = stats[i,0]*(Courage/10 + (tier -1));
			if (tier == 2)
			{
				stats[0,0] = Brawns;
				stats[1,0] = Tenacity;
				stats[2,0] = Courage;
			}
		}
	}
	
	void TargetType()
	{
		if (type == 0)
		{
			Cover[] search = GameObject.FindObjectsOfType(typeof(Cover)) as Cover[];
			foreach (Cover n in search) 
			{
				if (Physics.Raycast(n.transform.position, transform.forward, 1.0f)) targets.Add(n.transform);	
			}
		} else if (type == 1) 
		{
			NPC_Main[] search = GameObject.FindObjectsOfType(typeof(NPC_Main)) as NPC_Main[];
			foreach (NPC_Main n in search) targets.Add(n.transform);
		} else if (type == 2)
		{
			PC_Main[] search = GameObject.FindObjectsOfType(typeof(PC_Main)) as PC_Main[];
			foreach (PC_Main n in search) targets.Add(n.transform);
		} else if (type == 3) target = transform;
	}
	
	void Target()
	{
		if (target == null)
		{
			targets.Sort(delegate(Transform t1, Transform t2) { 
				return (Vector3.Distance(t1.position, transform.position)).CompareTo(Vector3.Distance(t2.position,transform.position)); 
			});
			if (type != 2 )target = targets[0];
			else target = targets[1];
			transform.LookAt(target);
		} 
		else {	
			if (targets.Count > 1) index = (index+1) % targets.Count;
			target = targets[index];
			transform.LookAt(target);
		}
		foreach (Transform n in targets) if (n == target)
		{
			if (type != 0) n.GetComponentInChildren<Renderer>().material.color = Color.blue;
			else n.GetComponentInParent<Renderer>().material.color = Color.blue;
		}
		else {
			if (n.GetComponent<Cover>() != null) n.GetComponentInChildren<Renderer>().material.color = n.GetComponent<Cover>().target_off;
			if (n.GetComponent<PC_Main>() != null) n.GetComponentInChildren<Renderer>().material.color = n.GetComponent<PC_Main>().target_off;
			if (n.GetComponent<NPC_Main>() != null) n.GetComponentInChildren<Renderer>().material.color = n.GetComponent<NPC_Main>().target_off;
		}
	}
}


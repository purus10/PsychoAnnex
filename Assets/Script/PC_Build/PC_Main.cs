using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Database;

public class PC_Main : MonoBehaviour {
	
	public string Name;
	public int ID, HP, Beat, Brawns, Tenacity, Courage, type;
	public float tier_count;
	public int cur_hp, cur_beats, max_acc = 2, damage, hit, index;
	public string[,] quirk = new string[2,4];
	public int[,] stats = new int[3,3]; //[Collum,0] (0 = cur stat, 1 = anima stat, 2 = equip stat) [0,Row] (0 = brawns, 1 = tenacity, 2 = courage) 
	public float speed, rotation;
	public Transform target;
	public bool myturn, far_beats;
	public Camera AimCamera;
	public List <Transform> targets = new List<Transform>();
	public List <Ability> abilities = new List<Ability>();
	public weapon[] wep = new weapon[2];
	public accessory[] acc = new accessory[2];
	public Ability[] ability = new Ability[6];
	public Item[] items = new Item[4];
	public bool tier_4, second_acc, soul_mixture, reflect, cover, omni, battle;
	public NavMeshAgent agent;
	public NPC_Main NPC;
	public PC_Main PC;
	Color target_off;
	public int tier
	{
		get { return  1 + (int) tier_count/5; }
	}
	void Start ()
	{
		DontDestroyOnLoad(gameObject);
		SetStats();
		FirstWeapon();
		target_off = GetComponentInChildren<Renderer>().material.color;
	}
	void Update()
	{
		SetAttack();
		NPCMotions();
		if (Input.GetKeyDown(GameInformer.Fight) && battle == false) BattleSetup();
		TargetSetup();
		CharacterMotion();
		if (Input.GetKeyDown(KeyCode.Space)) EndTurn();
	}
	void CharacterMotion()
	{
		if (GameInformer.stop == false)
		{
			if (GameInformer.target == transform)
			{
				agent.Stop();
				if (Input.GetKey(GameInformer.Up)) transform.Translate(Vector3.forward * speed * Time.deltaTime);
				if (Input.GetKey(GameInformer.Left)) transform.Rotate(Vector3.down * speed * 34 * Time.deltaTime);
				if (Input.GetKey(GameInformer.Right)) transform.Rotate(Vector3.up * speed * 34 * Time.deltaTime);
				if (Input.GetKey(GameInformer.Down)) transform.Translate(-Vector3.forward * speed * Time.deltaTime);
				if (Input.GetKey(KeyCode.LeftShift)) speed = 7;
				else speed = 3;
			}
		}else if(myturn == true)
		{
			for(int i = 0; i < GameInformer.A.Length;i++)
			{
				if (Input.GetKey(GameInformer.ItemTog) && Input.GetKeyDown(GameInformer.A[i])) 
					if (items[i] != null) CastItem(i,items[i]);		
				if (!Input.GetKey(KeyCode.Q) && !Input.GetKey(GameInformer.ItemTog) && Input.GetKeyDown(GameInformer.A[i])) 
					if (ability[i] != null) 
						if (AimCamera.enabled == false || ability[i].name == "Pistol") CastAbility(ability[i]);
				if (Input.GetKey(KeyCode.Q) && Input.GetKeyDown(GameInformer.A[i]))
					if (items[i] != null)
						if (items[i].type == 1) items[i].Aim(this);
			}
		}
	}
	void TargetSetup()
	{
		if (Input.GetKeyDown(KeyCode.Tab) && type != 3) 
		{
			if (myturn == true && AimCamera.enabled == false)
			{
				targets.Clear();
				Target();
			}
		}
	}
	public void BattleSetup()
	{
		agent.Resume();
		if (target == null) RunForCover();
		if (GameInformer.target == transform && GameInformer.stop == false) 
		{
			myturn = true;
			GameInformer.stop = true;
			GameInformer.battle = true;
		}
	}
	void SetAttack()
	{
		foreach (Ability a in abilities) if (a.name == "Attack") 
		{
			a.equipped = true;
			ability[0] = a;
		}
	}
	void NPCMotions()
	{
		if (far_beats == true) transform.LookAt(target);
		if (GameInformer.target != null && GameInformer.stop == false && GameInformer.target != transform) 
		{
			agent.Resume();
			agent.stoppingDistance = 1.5f;
			agent.SetDestination(GameInformer.target.position);
		}
		if (GameInformer.target == transform && GameInformer.battle == true) myturn = true;
	}
	void RunForCover()
	{
		if (ID != 4 && ID != 6)
		{
			Target();
			Cover c = target.GetComponent<Cover>();
			if (c.selected == false) 
			{
				c.selected = true;
				c.taken = true;
			}
			if (c.selected == true) {
				agent.stoppingDistance = 0.2f;
				agent.SetDestination(target.position);
			}
			targets.Clear();
			target = null;
		}else {
			targets.Clear();
			type = 1;
			Target();
			if (target != null) target.GetComponentInChildren<Renderer>().material.color = target.GetComponent<NPC_Main>().target_off;
		}
		battle = true;
	}
	void CastAbility(Ability a)
	{
		agent.stoppingDistance = 1.5f;
		foreach (Transform n in targets)
		{
			if (n.GetComponent<Cover>() != null) n.GetComponentInParent<Renderer>().material.color = n.GetComponent<Cover>().target_off;
			if (n.GetComponent<PC_Main>() != null) n.GetComponentInChildren<Renderer>().material.color = n.GetComponent<PC_Main>().target_off;
			if (n.GetComponent<NPC_Main>() != null) n.GetComponentInChildren<Renderer>().material.color = n.GetComponent<NPC_Main>().target_off;
		}
		if (far_beats == false && type != a.type) 
		{
			targets.Clear();
			target = null;
			type = a.type;
			if (type != 3) Target();
			else {
				if (target != null) NPC = target.GetComponent<NPC_Main>();
				a.Execute(this,null,NPC);
			}
		} else {
			if (type == 1) 
			{
				float distance = Vector3.Distance(transform.position, target.position);
				if (a.far_range == false && distance > 2f) 
				{
					agent.SetDestination(target.position);
					NPC = target.GetComponent<NPC_Main>();
					StartCoroutine(CloseGap(a));
				} else if (distance <= 2f) a.OpposeCast(this,NPC);
			}
			if (type == 2) 
			{
				PC = target.GetComponent<PC_Main>();
				a.AllyCast(this,PC);
			}
			if (type == 3) 
			{
				if (target != null) NPC = target.GetComponent<NPC_Main>();
				a.OpposeCast(this,NPC);
			}
		}
	}
	IEnumerator CloseGap(Ability a)
	{
		while (Vector3.Distance(transform.position, target.position) > agent.stoppingDistance - 0.5f)
		{
			if (Vector3.Distance(transform.position, target.position) <= agent.stoppingDistance + 0.5f)
			{
			a.OpposeCast(this,NPC);
			break;
			}
			yield return null;
		}
	}
	void CastItem(int j, Item i)
	{
		if (type != 2) 
		{
			type = 2;
			targets.Clear();
			Target();
		} else {
			PC = target.GetComponent<PC_Main>();
			i.CastItem(j,i,this,PC);
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
		far_beats = false;
		if (cur_beats < Beat) cur_beats = Beat;
		GameInformer.Idler = (GameInformer.Idler +1) % 8;
		if (omni == true) ability[0].Omni(this,null,false);
		foreach (Transform n in targets)
		{
			if (n.GetComponent<Cover>() != null) n.GetComponentInParent<Renderer>().material.color = n.GetComponent<Cover>().target_off;
			if (n.GetComponent<PC_Main>() != null) n.GetComponentInChildren<Renderer>().material.color = n.GetComponent<PC_Main>().target_off;
			if (n.GetComponent<NPC_Main>() != null) n.GetComponentInChildren<Renderer>().material.color = n.GetComponent<NPC_Main>().target_off;
		}
	}
	void SetStats()
	{
		name = Name;
		cur_hp = HP;
		cur_beats = Beat;
		stats[1,0] = Brawns;
		stats[1,1] = Tenacity;
		stats[1,2] = Courage;
		for (int i = 0; i < stats.GetLength(1);i++) stats[0,i] = stats[1,i];
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
		int d = stats[1,0], h = stats[1,1],b = stats[1,0], t = stats[1,1], c = stats[1,2];
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
		for (int i = 0; i < stats.GetLength(1);i++)
			if (tier > 1) stats[0,i] = stats[0,i]*(tier-1);
		else stats[0,i] = stats[0,i]/(tier+1);
	}
	void Target()
	{
		if (type == 0)
		{
			Cover[] search = GameObject.FindObjectsOfType(typeof(Cover)) as Cover[];
			foreach (Cover n in search) if (Physics.Raycast(n.transform.position, transform.forward, 180.0f) && n.taken == false) targets.Add(n.transform);	
		} else if (type == 1) 
		{
			NPC_Main[] search = GameObject.FindObjectsOfType(typeof(NPC_Main)) as NPC_Main[];
			foreach (NPC_Main n in search) targets.Add(n.transform);
		} else if (type == 2)
		{
			PC_Main[] search = GameObject.FindObjectsOfType(typeof(PC_Main)) as PC_Main[];
			foreach (PC_Main n in search) targets.Add(n.transform);
		} else if (type == 3) target = transform;

		if (target == null)
		{
			targets.Sort(delegate(Transform t1, Transform t2) { 
				return (Vector3.Distance(t1.position, transform.position)).CompareTo(Vector3.Distance(t2.position,transform.position));});
			if (type != 2 )target = targets[0];
			else {
				target = transform;
				GetComponentInChildren<Renderer>().material.color = Color.blue;
			}
			transform.LookAt(target);
		}
		else if (targets.Count > 1) 
		{
			index = (index+1) % targets.Count;
			target = targets[index];
			if (type != 0)  transform.LookAt(target);
		}
		foreach (Transform n in targets) if (n == target)
			if (n.GetComponent<Cover>() == null) n.GetComponentInChildren<Renderer>().material.color = Color.blue;
			else n.GetComponentInParent<Renderer>().material.color = Color.blue;
		else {
			if (n.GetComponent<Cover>() != null) n.GetComponentInParent<Renderer>().material.color = n.GetComponent<Cover>().target_off;
			if (n.GetComponent<PC_Main>() != null) n.GetComponentInChildren<Renderer>().material.color = n.GetComponent<PC_Main>().target_off;
			if (n.GetComponent<NPC_Main>() != null) n.GetComponentInChildren<Renderer>().material.color = n.GetComponent<NPC_Main>().target_off;
		}
	}
}


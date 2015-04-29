using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Database;

public class PC_Main : MonoBehaviour {
	
	public string Name;
	public int ID, HP, Beat, Brawns, Tenacity, Courage, type;
	public float tier_count;
	public int cur_hp, cur_beats, max_acc = 2, damage, hit, ability_bonus,index;
	public string[,] quirk = new string[2,4];
	public int[,] stats = new int[3,3]; //[Collum,0] (0 = cur stat, 1 = anima stat, 2 = equip stat) [0,Row] (0 = brawns, 1 = tenacity, 2 = courage) 
	public float speed, rotation;
	public Transform target;
	public bool myturn, far_beats, Combat_Turn;
	public Camera AimCamera;
	public List <Transform> targets = new List<Transform>();
	public List <Ability> abilities = new List<Ability>();
	public List <Passive> passives = new List<Passive>();
	public Item[] wep = new Item[2];
	public Item[] acc = new Item[2];
	public Ability[] ability = new Ability[4];
	public Item[] items = new Item[4];
	public bool moving, Acrobat, Critical_Shot, Flurry, RunningShot, Steady, onslaught, tier_4, second_acc, fourth_item, soul_mixture, reflect, cover, omni, battle;
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
		SetAttack();
		Item i = new Item();
		i.name = "Tar Water";
		i.heal = 2;
		i.type = 0;
		i.amount = 3;
		items[2] = i;

		Item f = new Item();
		f.name = "Ring";
		f.type = 3;
		items[1] = f;

		Item g = new Item();
		g.name = "Knuckles";
		g.type = 4;
		items[0] = g;

		DontDestroyOnLoad(gameObject);
		SetStats();
		FirstWeapon();
		target_off = GetComponentInChildren<Renderer>().material.color;
	}
	void Update()
	{
		NPCMotions();
		if (Input.GetKeyDown(GameInformer.Fight) && battle == false) BattleSetup();
		TargetSetup();
		CharacterMotion();
		if (Input.GetKeyDown(KeyCode.Space) && moving == false) EndTurn();
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
		}else if(myturn == true && moving == false) {
			for(int i = 0; i < GameInformer.A.Length;i++)
			{
				if (Input.GetKey(GameInformer.ItemTog) && Input.GetKeyDown(GameInformer.A[i])) 
					if (items[i] != null) CastItem(i,items[i]);		
				if (!Input.GetKey(KeyCode.Q) && !Input.GetKey(GameInformer.ItemTog) && Input.GetKeyDown(GameInformer.A[i])) 
					if (ability[i] != null) 
						if (AimCamera.enabled == false || ability[i].name == "Pistol") CastAbility(ability[i]);
				if (Input.GetKey(KeyCode.Q) && Input.GetKeyDown(GameInformer.A[i]))
					if (items[i] != null && items[i].type == 1) items[i].Aim(this);
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
		if (ID != 4 && ID != 6 && ID != GameInformer.target.GetComponent<PC_Main>().ID)
		{
			Target();
			if (target != null)
			{
				target.GetComponentInParent<Renderer>().material.color = target.GetComponent<Cover>().target_off;
				Cover c = target.GetComponent<Cover>();
				if (c.selected == false) 
				{
					c.selected = true;
					c.taken = true;
				}
				if (c.selected == true) {
					agent.stoppingDistance = 0.2f;
					agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
					agent.SetDestination(target.position);
				}
				targets.Clear();
			}else {
				targets.Clear();
				type = 1;
				Target();
				target.GetComponentInChildren<Renderer>().material.color = target.GetComponent<NPC_Main>().target_off;
			}
		}
		battle = true;
	}
	void CastAbility(Ability a)
	{
		agent.stoppingDistance = 1.5f;
		if (cover == false) agent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
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
		} else if (type == 1) 
		{
			if (target == null) Target();
			float distance = Vector3.Distance(transform.position, target.position);
			NPC = target.GetComponent<NPC_Main>();
			if (a.far_range == true) {
				a.OpposeCast(this,NPC,distance);
				Combat_Turn = false;
			}
			else if (distance > 2f) 
			{
				agent.SetDestination(target.position);
				StartCoroutine(CloseGap(a));
			} else {
				a.OpposeCast(this,NPC,distance);
				Combat_Turn = false;
			}
		} else if (type == 2) 
		{
			PC = target.GetComponent<PC_Main>();
			a.AllyCast(this,PC);
			Combat_Turn = false;
		} else if (type == 3) 
		{
			if (target != null) NPC = target.GetComponent<NPC_Main>();
			a.OpposeCast(this,NPC,0f);
			Combat_Turn = false;
		}
	}
	IEnumerator CloseGap(Ability a)
	{
		moving = true;
		while (Vector3.Distance(transform.position, target.position) > agent.stoppingDistance - 0.5f)
		{
			if (Vector3.Distance(transform.position, target.position) <= agent.stoppingDistance + 0.5f)
			{
				moving = false;
			a.OpposeCast(this,NPC,0f);
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
		if (myturn == true)
		{
		myturn = false;
		far_beats = false;
		if (cur_beats < Beat) cur_beats = Beat;
		GameInformer.Idler = (GameInformer.Idler +1) % 7;
		if (omni == true) ability[0].Omni(this,null,false);
		if (cover == false) agent.obstacleAvoidanceType = ObstacleAvoidanceType.LowQualityObstacleAvoidance;
		foreach (Transform n in targets)
		{
			if (n.GetComponent<Cover>() != null) n.GetComponentInParent<Renderer>().material.color = n.GetComponent<Cover>().target_off;
			if (n.GetComponent<PC_Main>() != null) n.GetComponentInChildren<Renderer>().material.color = n.GetComponent<PC_Main>().target_off;
			if (n.GetComponent<NPC_Main>() != null) n.GetComponentInChildren<Renderer>().material.color = n.GetComponent<NPC_Main>().target_off;
		}
		} else GameInformer.Idler = (GameInformer.Idler +1) % 7;
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
		if (wep[0] == null) foreach (Item weapon in WeaponsList.weapons)
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
	public void Target()
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
			else n.GetComponent<Cover>().render.GetComponent<Renderer>().material.color = Color.blue;
		else {
			if (n.GetComponent<Cover>() != null) n.GetComponent<Cover>().render.GetComponent<Renderer>().material.color = n.GetComponent<Cover>().target_off;
			if (n.GetComponent<PC_Main>() != null) n.GetComponentInChildren<Renderer>().material.color = n.GetComponent<PC_Main>().target_off;
			if (n.GetComponent<NPC_Main>() != null) n.GetComponentInChildren<Renderer>().material.color = n.GetComponent<NPC_Main>().target_off;
		}
	}
}


using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Database;

public class PC_Base : MonoBehaviour 
{
	#region BaseStats
	public int MaxHP {get{return Get.Base[(int)Player,(int)Get.Stat.HpHit];}}	
	public int MaxBeat {get{return Get.Base[(int)Player,(int)Get.Stat.BeatDMG];}}
	public int MaxBrawns {get{return Get.Base[(int)Player,(int)Get.Stat.Brawns] + EquipAdd((int)Get.Stat.Brawns,5);}}
	public int MaxTenacity {get{return Get.Base[(int)Player,(int)Get.Stat.Tenacity] + EquipAdd((int)Get.Stat.Tenacity,5);}}
	public int MaxCourage {get{return Get.Base[(int)Player,(int)Get.Stat.Courage] + EquipAdd((int)Get.Stat.Courage,5);}}
	public int Hit {get{return CurStat[(int)Get.Stat.Tenacity] + EquipAdd((int)Get.Stat.HpHit,1);}}
	public int OffHit {get{return CurStat[(int)Get.Stat.Tenacity] + EquipAdd((int)Get.Stat.HpHit,0);}}
	public int Damage {get{return CurStat[(int)Get.Stat.Brawns] + EquipAdd((int)Get.Stat.BeatDMG,1);}}
	public int OffDamage {get{return CurStat[(int)Get.Stat.Brawns] + EquipAdd((int)Get.Stat.BeatDMG,0);}}
	public int Dodge {get{return SetDodge(MaxBrawns,MaxTenacity);}}
	public int Tier {get{return 2+(int)(TierCount/7);}}
	#endregion

	static public Get.GameState MyState;
	static public Get.ID TurnRoster;
	static public bool Omni;
	public TextMesh showdmg;
	public int[] CurStat = new int[5];
	public AudioClip[] MusicBeat = new AudioClip[5];
	public AudioClip[] BattleSounds;
	public int Roster, Index;
	public float TierCount, Range;
	public bool ButtonDown, WithLove, Panacea, Day, Flurry, FarBeats, InBattle;
	public Transform Target;
	public Get.ID Player;
	public Get.Element Element = Get.Element.None;
	public Get.Inflictions inflicted = Get.Inflictions.None;
	public Get.State State;
	public List <Transform> Targets = new List<Transform>();
	public List <Ability> Abilities = new List<Ability>();
	public List <Passive> Passives = new List<Passive>();
	public Item[] Equips = new Item[4];
	public Ability[] Ability = new Ability[4];
	public Ability SelectedAbility;
	public Item[] Items = new Item[4];
	public Item SelectedItem;

	public NavMeshAgent Agent;
	int EquipAdd(int Stat, int skip)
	{
		int a = 0;
		for (int i = 0; i < Equips.Length;i++) 
			if (Equips[i] != null && i != skip) a += Equips[i].Stats[Stat];
		return a;
	}
	bool EnemyFound()
	{
		bool found = false;
		NPC_Base[] search = GameObject.FindObjectsOfType(typeof(NPC_Base)) as NPC_Base[];
		foreach (NPC_Base E in search)
		{
			float distance = Vector3.Distance(E.gameObject.transform.position, transform.position);
			if (distance <= 15f) found = true;
		}
		return found;
	}
	int SetDodge(int Brawns, int Tenacity)
	{
		int bonus = 0;
		if (Player == Get.ID.Hena || Player == Get.ID.Annihilator) bonus = 2;
		else if (Player == Get.ID.Rose || Player == Get.ID.Xeres) bonus = -2;
		if (Brawns <= Tenacity) return (Brawns + Tenacity-5) + bonus;
		else return (Tenacity + Brawns-5) + bonus;
	}
	int SetStat(int Cur, int Max)
	{
		if (Cur == 0) return Max;
		else return Cur;
	}

	void Start () 
	{
		SetStats();
		Ability[0] = Get.CreateAbility("Attack",2,1.5f,2,false);
		Ability[1] = Get.CreateAbility("Pulse",2,5,40,true);
		Ability[2] = Get.CreateAbility("Day",1,100,200,true);
		Ability[3] = Get.CreateAbility("Barrage",3,5,10,true);
		Equips[0] = Get.CreateWeapon("Knuckles",0,2,2,0,5);
		Equips[1] = Get.CreateWeapon("Knuckles",0,2,2,0,5);
		Equips[2] = Get.CreateAcc("ACC",0,1,1,1);
		Equips[3] = Get.CreateAcc("ACC",0,1,1,1);
	}
	void Update () 
	{
		if (TurnRoster == Player)
		{
			if (MyState == Get.GameState.Explore)
			{
				if (Input.GetButtonDown("StartBattle") && EnemyFound() == true) StartBattle();
			} else {
				if (InBattle == true && State != Get.State.Move)
				{
					if (ButtonDown == false)
					{
						for (int i = 0; i < Get.Ability.Length;i++)
						{
							if (Input.GetButton("ItemToggle"))
							{
								if (Input.GetButtonDown(Get.Ability[i]) && Items[i] != null) StartItem(i);
							} else if (Input.GetButtonDown(Get.Ability[i]) && Ability[i] != null) StartAbility(i);
						}
					} else {
						if (Input.GetButtonDown("TargetToggle")) 
						{
							Targets.Clear();
							if (SelectedAbility != null) 
								Targetting(SelectedAbility.type);
							else Targetting(SelectedItem.type);
						}
						if (Input.GetButtonDown("Escape"))
						{
							ButtonDown = false;
							Target = null;
							Targets.Clear();
							SelectedAbility = null;
							SelectedItem = null;
						}
						if (Input.GetButtonDown("Enter"))
						{
							if (SelectedAbility != null) StartCast(false);
							else StartCast(true);
						}
					}
				}
			}
		}
	}
	void FixedUpdate()
	{
		if (Input.GetKeyDown(KeyCode.Space) && ButtonDown == false && State == Get.State.Idle || 
		    Input.GetKeyDown(KeyCode.Space) && ButtonDown == false && State == Get.State.Cover) EndTurn();
	}
	void AdjustToTier(int Tier)
	{
		for (int i = 2; i < CurStat.Length;i++)
		{
			if (Tier == 2) CurStat[i] = (Get.Base[(int)Player,i] + EquipAdd(i,5));
			else if (Tier > 2) CurStat[i] = CurStat[i]*(Tier-1);
			else if (Tier == 1) CurStat[i] = CurStat[i]/(Tier+1);
		}
	}
	void CastAbility(Ability A, Item I, int type, float min_range, float max_range)
	{
		//1 = ally, 2 = enemy, 3 = self
		if (type == 1)
		{
			if (Get.TargetDistance(transform, Target,max_range) == true) 
			{
				if (A != null) MoveThroughCover(A,null,A.min_range,A.max_range);
				else MoveThroughCover(null,I,I.min_range,I.max_range);
			}
		} else if (type == 2)
		{
			if (A != null && A.FarRange == true || I != null)
			{
				if (A != null) MoveThroughCover(A,null,A.min_range,A.max_range);
				else MoveThroughCover(null,I,I.min_range,I.max_range);
			} else {
				if (Get.TargetDistance(transform, Target, min_range) == true) 
				{
					if (A != null) ExecuteCloseRange(A);
					else I.Execute(I.Slot,I,this,Target);
				} else {
					Agent.stoppingDistance = 2f;
					Agent.SetDestination(Target.position);
					StartCoroutine(CloseRangeTrigger(A,I));
				}
			}
		} else if (A != null) ExecuteFarRange(A);
		else MoveThroughCover(null,I,I.min_range,I.max_range);
		
	}
	IEnumerator CloseRangeTrigger(Ability A, Item I)
	{
		State = Get.State.Move;
		while (Vector3.Distance(transform.position, Target.position) > Agent.stoppingDistance - 0.5f)
		{
			if (Vector3.Distance(transform.position, Target.position) <= Agent.stoppingDistance + 0.5f)
			{
				if (A != null)ExecuteCloseRange(A);
				else I.Execute(I.Slot,I,this,Target);
				break;
			}
			yield return null;
		}
	}
	void EndTurn()
	{
		if ( CurStat[(int)Get.Stat.BeatDMG] < MaxBeat) CurStat[(int)Get.Stat.BeatDMG] = MaxBeat;
		Roster = (Roster+1) % Get.Party.Length;
		TurnRoster = (Get.ID)Get.Roster[Roster];
		FarBeats = false;
		NPC_Base.MP++;
		Debug.Log("END");
		if (State == Get.State.Engaged) State = Get.State.Idle;
	}
	/****************************THE EXECUTION OF ALL ABILITIES FALL BETWEEN HERE*********************************/
	void ExecuteCloseRange(Ability A)
	{
		NPC_Base enemy = Target.gameObject.GetComponent<NPC_Base>();
		State = Get.State.Engaged;
		enemy.State = Get.State.Engaged;
		enemy.Target = transform;
		if (enemy.Innate == false && Player != Get.ID.Rose) CurStat[(int)Get.Stat.BeatDMG]--;
		Get.Execute(A,this,Target);
		enemy.Turn = true;
		if (CurStat[(int)Get.Stat.BeatDMG] == 0 && enemy.CurStat[(int)Get.Stat.BeatDMG] == 0) EndTurn();
	}
	void ExecuteFarRange(Ability A)
	{
		NPC_Base enemy = Target.gameObject.GetComponent<NPC_Base>();
		State = Get.State.Engaged;
		if (FarBeats == false) 
		{
			Get.Execute(A,this,Target);
			if (A.type != 3) EndTurn();
		} else {
			Debug.Log("yes");
			if (Player != Get.ID.Rose) CurStat[(int)Get.Stat.BeatDMG]--;
			else if (enemy.Innate == true) enemy.Innate = false;
			Get.Execute(A,this,Target);
			if (CurStat[(int)Get.Stat.BeatDMG] == 0) EndTurn();
		}
		enemy.State = Get.State.Engaged;
		enemy.Target = transform;
		enemy.Turn = true;
	}
	/*************************************************************************************************************/
	IEnumerator FarRangeTrigger(Ability A, Item I, Transform Cover)
	{
		State = Get.State.Move;
		while (Vector3.Distance(transform.position, Cover.position) > Agent.stoppingDistance - 0.5f)
		{
			if (Vector3.Distance(transform.position, Cover.position) <= Agent.stoppingDistance + 0.5f)
			{
				if (A != null) ExecuteFarRange(A);
				else I.Execute(I.Slot,I,this,Target);
				break;
			}
			yield return null;
		}
	}
	void MoveThroughCover(Ability A, Item I, float min_range, float max_range)
	{
		if (Target.transform == transform && State == Get.State.Cover)
		{
			if (A != null) ExecuteFarRange(A);
			else I.Execute(I.Slot,I,this,Target);
		} else if (Get.TargetDistance(transform, Target,max_range) == false || State != Get.State.Cover)
		{
			int coverchoice = -1;
			List <Transform> covers = new List<Transform>();
			Transform CoverToTarget = null;
			Cover[] search = GameObject.FindObjectsOfType(typeof(Cover)) as Cover[];
			foreach (Cover n in search) if (n.Status == Cover.Spot.Empty) covers.Add(n.transform);	
			covers.Sort(delegate(Transform t1, Transform t2) {return (Vector3.Distance(t1.position, Target.position)).CompareTo(Vector3.Distance(t2.position,Target.position));});
			for (int i = 0;i < covers.Count;i++)
			{
				float distance = Vector3.Distance(covers[i].position, Target.position);
				if (distance >= min_range) coverchoice = i;
			}
			if (coverchoice != -1)
			{
			CoverToTarget = covers[coverchoice];
			Agent.stoppingDistance = 0.2f;
			Agent.obstacleAvoidanceType = ObstacleAvoidanceType.MedQualityObstacleAvoidance;
			Agent.SetDestination(CoverToTarget.position);
			StartCoroutine(FarRangeTrigger(A,I,CoverToTarget));
			}
		} else if (A != null) ExecuteFarRange(A);
		else I.Execute(I.Slot,I,this,Target);
	}
	void RunForCover()
	{
		Targets.Clear();
		Targetting(0);
		if (Dodge < 5)
		{
			if (State == Get.State.Idle)
			{

				if (Targets[0] != null) 
				{
					float coverDistance = Vector3.Distance(transform.position, Targets[0].position);
					
					if (coverDistance <= Range)
					{
						Target = Targets[0];
						Cover c = Target.GetComponent<Cover>();
						c.Status = Cover.Spot.Taken;
						if (c.Status == Cover.Spot.Taken) 
						{
							Agent.stoppingDistance = 0.2f;
							Agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
						//	State = Get.State.Move;
							Agent.SetDestination(Target.position);
						}
					}
				}
			}
		}
		Targets.Clear();
		Target = null;
	}
	void SetStats()
	{
		CurStat[(int)Get.Stat.HpHit] = SetStat(CurStat[(int)Get.Stat.HpHit],MaxHP);
		CurStat[(int)Get.Stat.BeatDMG]  = SetStat(CurStat[(int)Get.Stat.BeatDMG],MaxBeat);
		CurStat[(int)Get.Stat.Brawns]  = SetStat(CurStat[(int)Get.Stat.Brawns],MaxBrawns);
		CurStat[(int)Get.Stat.Tenacity]  = SetStat(CurStat[(int)Get.Stat.Tenacity],MaxTenacity);
		CurStat[(int)Get.Stat.Courage]  = SetStat(CurStat[(int)Get.Stat.Courage],MaxCourage);
	}
	void StartAbility(int i)
	{
		print (Ability[i].Name);
		SelectedAbility = Ability[i];
		if (Target == null) Targetting(SelectedAbility.type);
		if (SelectedAbility.type == 3) CastAbility(SelectedAbility,null,SelectedAbility.type,SelectedAbility.min_range,SelectedAbility.max_range);
		else if (State != Get.State.Engaged) ButtonDown = true;
		else if (SelectedAbility.type == 2) CastAbility(SelectedAbility,null,SelectedAbility.type,SelectedAbility.min_range,SelectedAbility.max_range);
	}
	public void StartBattle()
	{
		Get.Roster[0] = (int)Player;
		Get.CreateRoster();
		RunForCover();
		MyState = Get.GameState.Battle;
	}
	void StartCast(bool item)
	{
		if (item == true)
		{
			ButtonDown = false;
			CastAbility(null,SelectedItem,SelectedItem.type,SelectedItem.min_range,SelectedItem.max_range);
			SelectedItem = null;
		} else {
			ButtonDown = false;
			CastAbility(SelectedAbility,null,SelectedAbility.type,SelectedAbility.min_range,SelectedAbility.max_range);
			SelectedAbility = null;
		}
	}
	void StartItem(int i)
	{
		print (Items[i].Name);
		SelectedItem = Items[i];
		Targetting(SelectedItem.type);
		Items[i].Slot = i;
		if (SelectedItem.type == 3) CastAbility(null,SelectedItem,SelectedItem.type,SelectedItem.min_range,SelectedItem.max_range);
		else if (State != Get.State.Engaged) ButtonDown = true;
		else if (SelectedItem.type == 2) CastAbility(null,SelectedItem,SelectedItem.type,SelectedItem.min_range,SelectedItem.max_range);
	}
	void Targetting(int Type)
	{
		if (Type == 0)
		{
			Cover[] search = GameObject.FindObjectsOfType(typeof(Cover)) as Cover[];
			foreach (Cover n in search) 
				if (Physics.Raycast(n.transform.position,transform.TransformDirection(Vector3.forward), 1f) && n.Status == Cover.Spot.Empty) Targets.Add(n.transform);	
		}else if (Type == 1)
		{
			PC_Base[] search = GameObject.FindObjectsOfType(typeof(PC_Base)) as PC_Base[];
			foreach (PC_Base n in search) Targets.Add(n.transform);	
		}else if (Type == 2)
		{
			NPC_Base[] search = GameObject.FindObjectsOfType(typeof(NPC_Base)) as NPC_Base[];
			foreach (NPC_Base n in search) Targets.Add(n.transform);	
		}else if (Type == 3) Target = transform;
		
		if (Target == null) 
		{
			Targets.Sort(delegate(Transform t1, Transform t2) {return (Vector3.Distance(t1.position, transform.position)).CompareTo(Vector3.Distance(t2.position,transform.position));});
			if (Type > 1) Target = Targets[0];
			else Target = transform;
		}else if (Targets.Count > 1) 
		{
			Index = (Index+1) % Targets.Count;
			Target = Targets[Index];
			if (Type != 0)  transform.LookAt(Target);
		}
	}
}

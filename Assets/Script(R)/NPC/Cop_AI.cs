using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Database;

public class Cop_AI : MonoBehaviour {

	public NPC_Base my;
	public int MaxHealItems,MaxAmount,HealAtPercentage;
	public float GunTimer, HealTimer, count, Range;
	public PC_Base[] search;
	public Cover[] cover;
	public bool Cover;
	public NavMeshAgent Agent;
	public int MinHeal{get{return (my.MaxHP*HealAtPercentage)/100;}}
	bool CheckForHealItem()
	{
		bool remaining = false;
		for (int i = 0; i< my.Items.Length;i++)
			if (my.Items[i] != null && my.Items[i].type == 0) remaining = true;
		return remaining;
	}
	bool HasHeal()
	{
		bool hasitem = false;
		for (int i = 0; i < my.Items.Length;i++)
			if ( my.Items[i] != null && my.Items[i].heal > 0) hasitem = true;
		return hasitem;
	}
	int Crit(PC_Base enemy)
	{
		int critical = 1;
		if (Get.HitFormula(my.CurStat[(int)Get.Stat.Courage]-10,enemy.CurStat[(int)Get.Stat.Courage]) == true) 
			critical = 1 + my.CurStat[(int)Get.Stat.Courage]/3;
		return critical;
	}

	void OnTriggerEnter(Collider col)
	{
		PC_Base target = col.GetComponent<PC_Base>();
		if (target != null)
			my.Target = target.transform;
		my.InBattle = true;
		my.Turn = true;
		if (target.InBattle == false) 
		{
			my.Opportunity = true;
			target.InBattle = true;
			target.StartBattle();
		}
	}

	void Awake () 
	{
		int SlotsFilled = Random.Range (1,Mathf.Clamp(MaxHealItems,1,3));
		for (int i = 0; i < SlotsFilled;i++)
			my.Items[i] = Get.CreateItem("Tar Water",1,1.5f,20,1,Random.Range(1,MaxAmount));
		my.Items[3] = Get.CreateItem("Pistol",2,5,20,0,1);

	}

	void Start()
	{
		search = GameObject.FindObjectsOfType(typeof(PC_Base)) as PC_Base[];
		cover = GameObject.FindObjectsOfType(typeof(Cover)) as Cover[];
	}

	void Update () 
	{
		if (my.Opportunity == true)
		{
			if (Get.TargetDistance(transform,my.Target,Range) == false)
			{
				UseGun();
				MoveToCover();
			} else {
				MoveToTarget();
			}
			my.Opportunity = false;
		} else if (my.InBattle == true)
		{
			if (my.Target == null)
				Target();
			if (my.State == Get.State.Idle)
			{
				if (my.Turn == true)
				{
					if (Get.TargetDistance(transform,my.Target,Range) == true)
						MoveToTarget();
				} else MoveToCover();
			}
			if (my.State == Get.State.Cover) count++;
			if (my.Turn == true || count >= HealTimer)
				if (my.CurStat[(int)Get.Stat.HpHit] <= MinHeal && HasHeal() == true) UseItem();
			if (my.Turn == true)
				if (my.State == Get.State.Engaged) Engaged();
			if (my.State == Get.State.Cover)
				if (count >= GunTimer) UseGun();
		}
	}

	void Attack()
	{
		PC_Base target = my.Target.GetComponent<PC_Base>();
		if (Get.HitFormula(my.Hit,target.Dodge) == true)
		{
			float dmg = (((my.Damage) * Random.Range(0.875f,1.125f))) * Crit(target);
			if (dmg > 0) 
			{
				target.CurStat[(int)Get.Stat.HpHit] -= (int)dmg;
				target.TierCount -= ((dmg % 1.5f) * Random.Range(1,1.125f)) * Crit(target);
				Get.PlayerShowNumber(0,((int)dmg).ToString(),target,false);
			}
			my.Turn = false;
		}
	}
	void Engaged()
	{
		if (Get.TargetDistance(transform,my.Target,Range) == true) 
			UseGun();
		else if (my.CurStat[(int)Get.Stat.BeatDMG] > 0)
		{
			Attack();
			my.CurStat[(int)Get.Stat.BeatDMG]--;
		}
		my.Turn = false;
	}
	void MoveToCover()
	{
		my.Targets.Clear();
		my.Target = null;
		foreach(Cover c in cover) my.Targets.Add(c.transform);
		my.Targets.Sort(delegate(Transform t1, Transform t2) {return (Vector3.Distance(t1.position, transform.position)).CompareTo(Vector3.Distance(t2.position,transform.position));});
		my.Target = my.Targets[0];
		Agent.stoppingDistance = 0.2f;
		if (Get.TargetDistance(transform,my.Targets[0],Range) == true)
			Agent.SetDestination(my.Target.position);
		my.Turn = false;
	}
	void MoveToTarget()
	{
		my.Targets.Clear();
		my.Target = null;
		foreach(PC_Base pc in search) my.Targets.Add(pc.transform);
		my.Targets.Sort(delegate(Transform t1, Transform t2) {return (Vector3.Distance(t1.position, transform.position)).CompareTo(Vector3.Distance(t2.position,transform.position));});
		my.Target = my.Targets[0];
		if (Get.TargetDistance(transform,my.Targets[0],Range) == true)
		{
		    Agent.SetDestination(my.Target.position);
			Agent.stoppingDistance = 2.0f;
			StartCoroutine("CloseRangeTrigger");
		}
	}
	IEnumerator CloseRangeTrigger()
	{
		while (Vector3.Distance(transform.position, my.Target.position) > Agent.stoppingDistance - 0.5f)
		{
			if (Vector3.Distance(transform.position, my.Target.position) <= Agent.stoppingDistance + 0.5f)
			{
				my.CurStat[(int)Get.Stat.BeatDMG]--;
				Attack();
				break;
			}
			my.State = Get.State.Engaged;
			yield return null;
		}
	}
	void Target()
	{
		foreach(PC_Base pc in search) my.Targets.Add(pc.transform);
		my.Targets.Sort(delegate(Transform t1, Transform t2) {return (Vector3.Distance(t1.position, transform.position)).CompareTo(Vector3.Distance(t2.position,transform.position));});
		my.Target = my.Targets[0];
	}
	void UseGun()
	{
		int Gunslot = -1;
		PC_Base target = my.Target.GetComponent<PC_Base>();
		for (int i = 0; i < my.Items.Length;i++)
			if (my.Items[i] != null && my.Items[i].Name == "Pistol") Gunslot = i;
		if (Gunslot != -1)
		{
			if (target.State != Get.State.Cover)
			{
				if (Get.HitFormula(my.Hit,target.Dodge) == true)
				{
					target.CurStat[(int)Get.Stat.HpHit] -= 1;
					Get.PlayerShowNumber(0,1.ToString(),target,false);
				} else Get.PlayerShowNumber(0,"Miss!",target,false);
			}
		}
		NPC_Base.MP--;
		count = 0;
	}
	void UseItem()
	{
		int bestchoice = -1;
		for (int i = 0; i < my.Items.Length;i++)
		{
			if (my.Items[i] != null && my.Items[i].heal > 0)
				if (my.Items[i].heal + my.CurStat[(int)Get.Stat.HpHit] <= my.MaxHP) 
					bestchoice = i;
		}
		if (bestchoice == -1)
		{
			for (int i = 0; i < my.Items.Length;i++)
			{
				if (my.Items[i] != null && my.Items[i].heal > 0)
					if (my.Items[i].heal + my.CurStat[(int)Get.Stat.HpHit] >= my.MaxHP) 
						bestchoice = i;
			}
		}
		if (my.Items[bestchoice] != null)
		{
			if ((my.Items[bestchoice].heal + my.CurStat[(int)Get.Stat.HpHit]) <= my.MaxHP) 
			{
				my.CurStat[(int)Get.Stat.HpHit] += my.Items[bestchoice].heal;
			}else my.CurStat[(int)Get.Stat.HpHit] = my.MaxHP;
			Get.NPCShowNumber(0,my.Items[bestchoice].heal.ToString(),my,false);
			if (my.Items[bestchoice].amount > 0) my.Items[bestchoice].amount--;
			else my.Items[bestchoice] = null;
		}
	}
}

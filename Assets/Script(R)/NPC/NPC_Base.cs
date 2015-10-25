using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Database;

public class NPC_Base : MonoBehaviour {

	static public int MP;
	public int[] CurStat = new int[5];
	public int ChallengeBonus, SkyAttacks;
	public float TierCount;
	public TextMesh showdmg;
	public AudioClip[] BattleSounds;
	public bool Innate, Provoked,Turn, InBattle, Opportunity;

	public Transform Target;
	public List <Transform> Targets = new List<Transform>();

	public Get.Inflictions inflicted = Get.Inflictions.None;
	public Get.State State;

	public Item[] Items = new Item[4];

	public int MaxHP {get{return 100;}}
	public int MaxBeat {get{return 2;}}
	public int MaxBrawns {get{return 2;}}
	public int MaxTenacity {get{return 2;}}
	public int MaxCourage {get{return 2;}}
	public int Hit {get{return CurStat[(int)Get.Stat.Tenacity];}}
	public int Damage {get{return CurStat[(int)Get.Stat.Brawns];}}
	public int Dodge {get{return SetDodge(MaxBrawns,MaxTenacity,ChallengeBonus);}}
	public int Tier {get{return 2+(int)(TierCount/7);}}
	int SetDodge(int Brawns, int Tenacity, int bonus)
	{
		if (Brawns <= Tenacity) return (Brawns + Tenacity-5) + bonus;
		else return (Tenacity + Brawns-5) + bonus;
	}

	void Start () 
	{
	}
	void Update () 
	{
	}
	public void AdjustToTier(int Tier)
	{
		for (int i = 2; i < CurStat.Length;i++)
		{
			if (Tier == 2) CurStat[i] = 2;
			else if (Tier > 2) CurStat[i] = CurStat[i]*(Tier-1);
			else if (Tier == 1) CurStat[i] = CurStat[i]/(Tier+1);
		}
	}
}

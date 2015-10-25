using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Database;

namespace Database {

	public class Get
	{
		static public int Day;
		static public bool Midnight = false;
		public enum Hour {Morning,AfterNoon,Evening,Midnight};
		public enum Element {None, Fire, Earth, Wind, Thunder, Water, Nature, Wicked, Divine, Day, Night};
		public enum GameState {Explore, Battle, Rest};
		public enum Inflictions {None,Hushed, Bleeding, Trap, Disguisted};
		public enum ID {Zen, Serenity, Sky, Hena, Rose, Annihilator, Xeres}; 
		public enum Press {None, Down, Action};
		public enum Stat {HpHit, BeatDMG, Brawns, Tenacity, Courage};
		public enum State {Idle, Battle, Move, Engaged, Cover};

		static public Hour PhaseofDay = Get.Hour.Morning;

		static public string[] Ability = new string[4] {"Ability1","Ability2","Ability3","Ability4"};
		static public PC_Base[] Party = GameObject.FindObjectsOfType(typeof(PC_Base)) as PC_Base[];
	
		//first int is the character ID, second is which type assigned 0 = HP, 1 = Beat, 2 = Brawns, 3 = Tenacity, 4 = Courage;
		static public int[,] Base = new int[7,5] {{6,2,2,3,3},{4,1,1,4,5},{6,2,2,3,5},{6,3,3,5,2},{6,1,1,6,3},{6,2,6,2,2},{6,1,2,5,3}};
		static public int[] Door = new int[11];
		static public int[] Roster = new int[7] {-1,-1,-1,-1,-1,-1,-1};
	
		static public void ApplyDamage(int dmg, PC_Base my, NPC_Base target)
		{
			target.CurStat[(int)Get.Stat.HpHit] -= (int)dmg;
			target.TierCount -= DeTier(my,dmg) * Get.Crit(my,target);
		}
		static public Ability CreateAbility(string name, int type, float minrange, float maxrange, bool farrange)
		{
			Ability create = new Ability();
			create.Name	= name;
			create.type = type;
			create.min_range = minrange;
			create.max_range = maxrange;
			create.FarRange = farrange;
			//create.description = Description(name);
			return create;
		}
		static public Item CreateAcc(string name, int type, int brawns, int tenacity, int courage)
		{
			Item create = new Item();
			create.Name	= name;
			create.type = type;
			create.Brawns = brawns;
			create.Tenacity = tenacity;
			create.Courage = courage;
			//create.description = Description(name);
			return create;
		}
		static public Item CreateItem(string name, int type, float minrange, float maxrange, int heal, int amount)
		{
			Item create = new Item();
			create.Name	= name;
			create.type = type;
			create.min_range = minrange;
			create.max_range = maxrange;
			create.heal = heal;
			create.amount = amount;
			//create.description = Description(name);
			return create;
		}
		static public Item CreateWeapon(string name, int type, int hit, int damage, int courage, int weight)
		{
			Item create = new Item();
			create.Name	= name;
			create.type = type;
			create.hit = hit;
			create.damage = damage;
			create.Courage = courage;
			create.weight = weight;
			//create.description = Description(name);
			return create;
		}
		static public int Crit(PC_Base my, NPC_Base enemy)
		{
			if (HitFormula(my.CurStat[(int)Stat.Courage]-10,enemy.CurStat[(int)Stat.Courage]) == true) return 1 + my.CurStat[(int)Stat.Courage]/3;
			else return 1;

		}
		static public float DeTier(PC_Base my,int dmg)
		{
			int multi = 1;
			float detier = (dmg % 1.5f);
			if (my.Player == Get.ID.Xeres) multi = 2;
			if (detier <= 0) return ((0.1f * multi) + Passive.Psycho(0.5f,my)) * Random.Range(1,1.125f);
			else return ((detier * multi) + Passive.Psycho(0.5f,my)) * Random.Range(1,1.125f);
		}
		static public bool HitFormula(int attack, int dodge)
		{
			attack = Random.Range (1,15 + attack);
			dodge = Random.Range (1,5 + dodge);
			return attack > dodge;
		}
		static public bool TargetDistance (Transform point,Transform target, float range)
		{
			float distance = Vector3.Distance(point.position, target.position);
			if (distance <= range) return true;
			else return false;
		}
		static public int MagicalDoor()
		{
			return Door[(int)Element.Divine] + Door[(int)Element.Water] + Door[(int)Element.Nature] + Door[(int)Element.Day] + Door[(int)Element.Night];
		}
		static public int PhysicalDoor()
		{
			return Door[(int)Element.Earth] + Door[(int)Element.Fire] + Door[(int)Element.Wind] + Door[(int)Element.Thunder] + Door[(int)Element.Wicked];
		}

		static public int PlaceCharacter(int slot)
		{
			int slots = 0;
			while (slots < Party.Length-1)
			{
				slot = Random.Range(0,7);
				for (int i = 0; i < Party.Length;i++)
				{
					if (Roster[i] == slot) slot = (slot+1) % Roster.Length;
					for (int p = 0; p < Party.Length;p++) 
						if (slot == (int)Party[p].Player) slots++;
				}
			}
			return slot;
		}
		//FUNCTIONS//
		static public void CreateRoster()
		{
			for (int i = 0; i < Roster.Length;i++)
				if (Roster[i] == -1) Roster[i] = PlaceCharacter(i);
		}
		static public void Execute(Ability A, PC_Base my, Transform target)
		{
			if (A.type != 2)
			{
				PC_Base ally = target.GetComponent<PC_Base>();
				A.Activate(my,ally,null);
			}else {
				NPC_Base enemy = target.GetComponent<NPC_Base>();
				if (HitFormula(my.Hit,enemy.Dodge) == true)
				{
					if (A.FarRange == true && A.FarCrit == false) A.Activate(my,null,enemy);
					else A.Activate(my,null,enemy);
				} else {
					NPCShowNumber(0,"Miss!",enemy,false);
				}
			}
		}
		static public void NPCShowNumber(int Hit, string dmg, NPC_Base target, bool offhand)
		{
			TextMesh number = TextMesh.Instantiate(target.showdmg,target.transform.position,target.showdmg.transform.rotation) as TextMesh;
			number.text = dmg;
			DamageNumber  num = number.GetComponent<DamageNumber>();
			num.Hit = Hit;
			num.OffHand = offhand;
		}

		static public void PlayerShowNumber(int Hit, string dmg, PC_Base target, bool offhand)
		{
			TextMesh number = TextMesh.Instantiate(target.showdmg,target.transform.position,target.showdmg.transform.rotation) as TextMesh;
			number.text = dmg;
			DamageNumber  num = number.GetComponent<DamageNumber>();
			num.Hit = Hit;
			num.OffHand = offhand;
		}

	}
	#region Abilities
	public class Ability
	{ 
		public string Name;
		public string name, description;
		public bool equipped, FarRange, FarBeats,FarCrit;
		public float max_range, min_range;
		public int type,ID;
		public float dmg;

		public void Activate (PC_Base my, PC_Base ally, NPC_Base target)
		{
			if (Name == "Attack") Attack(my,target,0);
			if (Name == "Anima") Anima(my,target);
			if (Name == "Barrage") Barrage(my);
			if (Name == "Butterfly") Butterfly();
			if (Name == "Chaos") Chaos(my);
			if (Name == "Eximo") Eximo(my,target);
			if (Name == "Feint") Feint(my);
			if (Name == "Inquisitio") Inquisitio(my,target);
			if (Name == "Jinx") Jinx(my,target);
			if (Name == "Karma") Attack(my,target,target.ChallengeBonus);
			if (Name == "Luck") Luck(my,target);
			if (Name == "Omni") Omni(my,PC_Base.Omni);
			if (Name == "Panacea") Panacea(ally);
			if (Name == "Provoke") Provoke(my,target);
			if (Name == "Pulse") Pulse(my,target);
			if (Name == "Rapture") Rapture(my);
			if (Name == "Verto") Verto(my);
			if (Name == "Day") Day(ally);
			if (Name == "Divine") Divine(my);
			if (Name == "Earth") Earth(my,target);
		}
		public void Execute(PC_Main my, PC_Main a, NPC_Main t)
		{
		/*	if (name == "Attack") Aattack(my,t,0);
			if (name == "Anima") Anima(my,t);
			if (name == "Barrage") Barrage(my);
			if (name == "Butterfly") Butterfly();
			if (name == "Chaos") Chaos(my,t);
			if (name == "Eximo") Eximo(my,t);
			if (name == "Feint") Feint(my,t);
			if (name == "Fidelity") Fidelity();
			if (name == "Hush") Hush(my,t);
			if (name == "Inquisitio") Inquisitio(my,t);
			if (name == "Jinx") Jinx(my,t);
			if (name == "Kadabra") Kadabra(my,t);
			if (name == "Karma") Karma(my,t);
			if (name == "Libro") Libro(my,t);
			if (name == "Luck") Luck(my,t);
			if (name == "Oblivio") Oblivio(my,t);
			if (name == "Omni") Omni(my,a,true);
			if (name == "Panacea") Panacea(a);
			if (name == "Provoke") Provoke(my,t);
			if (name == "Pulse") Pulse(my,t);
			if (name == "Rapture") Rapture(my,t);
			if (name == "Day") Day(a);
			if (name == "Divine") Divine(my,t);
			if (name == "Earth") Earth(my,t);
			if (name == "Fire") Fire(my,t);
			if (name == "Nature") Nature(my,t);
			if (name == "Night") Night(my,a);
			if (name == "Thunder") Thunder(my,t);
			if (name == "Water") Water(my,t);
			if (name == "Wicked") Wicked(my,t);
			if (name == "Wind") Wind(my,t);
			if (name == "Aqua") Aqua(my,t);
			if (name == "Attonitus") Attonitus(my,t);
			if (name == "Divinus") Divinus(my,t);
			if (name == "Ignis") Ignis(my,t);
			if (name == "Lex") Lex(my,t);
			if (name == "Maleficus") Maleficus(my,t);
			if (name == "Natura") Natura(my,t);
			if (name == "Nocturne") Nocturne(my,t);
			if (name == "Terra") Terra(my,t);
			if (name == "Ventus") Ventus(my,t);*/
		}
		public void Attack(PC_Base my, NPC_Base target, int karma)
		{
			int Brute = Passive.Brute(my.CurStat[(int)Get.Stat.Brawns],Get.PhysicalDoor(),my);
			dmg = (((my.Damage + Brute) * Random.Range(0.875f,1.125f)) + karma) * Get.Crit(my,target);
			if (dmg > 0) 
			{
				Get.ApplyDamage((int)dmg,my,target);
				if (my.Player == Get.ID.Annihilator) AnnAttack(my,target,karma);
				if (my.Player == Get.ID.Sky && target.CurStat[(int)Get.Stat.BeatDMG] > 0) SkyAttack(my,target,karma);
				else target.SkyAttacks = 0;
				Get.NPCShowNumber(0,((int)dmg).ToString(),target,false);
			}
			if (my.Player == Get.ID.Hena || my.WithLove == true) HenaAttack(my,target,karma);
			if (my.Player == Get.ID.Zen && Get.HitFormula(my.Hit-17,target.Dodge) == true) ZenAttack(my,target,karma,false);
			if (my.Equips[1] != null && Get.HitFormula(my.OffHit,target.Dodge) == true) OffAttack(my,target,karma);
		}
		/******************************************CHARACTER SPECIAL ATTACKS*********************************************/
		void AnnAttack(PC_Base my, NPC_Base target, int karma)
		{
			if (Get.HitFormula(my.CurStat[(int)Get.Stat.Brawns] + Passive.Brute(my.CurStat[(int)Get.Stat.Brawns],DoorManager.PhysicalDoor() * 5,my),
			               target.CurStat[(int)Get.Stat.Courage]) == true)
			{
			if (karma == 0) target.CurStat[(int)Get.Stat.BeatDMG] = 0;
			else target.CurStat[(int)Get.Stat.BeatDMG] -= target.CurStat[(int)Get.Stat.BeatDMG]/karma+1;
			}
		}
		void DoubleAttack(PC_Base my, NPC_Base target, int karma, bool offhand)
		{
			dmg = (((my.Damage + Passive.Brute(my.CurStat[(int)Get.Stat.Brawns],Get.PhysicalDoor(),my)) * Random.Range(0.875f,1.125f)) + karma) * Get.Crit(my,target);
			Get.ApplyDamage((int)dmg,my,target);
			Get.NPCShowNumber(2,((int)dmg).ToString() /*+" DOUBLE HIT!!!"*/,target,offhand);
		}
		void HenaAttack(PC_Base my, NPC_Base target, int karma)
		{
			int steal = Random.Range (0,10 + my.CurStat[(int)Get.Stat.Tenacity]);
			if (steal > 13) 
				for (int i = 0; i < target.Items.Length;i++)
					if (Random.Range(1,4) == i && target.Items[i] != null)
				{
					ItemList.items.Add(target.Items[i]);
					if (target.Items[i].type < 2 && karma == 0) 
					{
						for (int j = 0; j < my.Items.Length;j++)
						{
							if (my.Items[j] == null) my.Items[j] = target.Items[i];
							target.Items[i] = null;
						}
					}else target.Items[i] = null;
				}
		}
		void OffAttack(PC_Base my, NPC_Base target, int karma)
		{
			int Brute = Passive.Brute(my.CurStat[(int)Get.Stat.Brawns],Get.PhysicalDoor(),my);
			dmg = (((my.OffDamage + Brute) * Random.Range(0.875f,1.125f)) + karma) * Get.Crit(my,target);
			if (dmg > 0) 
			{
				Get.ApplyDamage((int)dmg,my,target);
				Get.NPCShowNumber(1,((int)dmg).ToString(),target,true);
				if (my.Player == Get.ID.Annihilator) AnnAttack(my,target,karma);
				if (my.Player == Get.ID.Zen && Get.HitFormula(my.OffHit-17,target.Dodge) == true) ZenAttack(my,target,karma,true);
				else if (my.Flurry = true && Get.HitFormula(my.OffHit-17,target.Dodge) == true) DoubleAttack(my,target,karma,true);
			}
		}
		void SkyAttack(PC_Base my, NPC_Base target, int karma)
		{
			int divide = my.CurStat[(int)Get.Stat.BeatDMG] - target.SkyAttacks - (karma/2);
			if (divide < 0) divide = 1;
			if (target.SkyAttacks > 0 && dmg > 0) 
				dmg = ((dmg/divide) * Random.Range(1,1.125f));
				target.CurStat[(int)Get.Stat.HpHit] = (int)dmg;
			target.SkyAttacks++;
		}
		void ZenAttack(PC_Base my, NPC_Base target, int karma, bool offhand)
		{
			DoubleAttack(my,target,karma,offhand);
			if (my.Flurry = true && Get.HitFormula(my.OffHit-20,target.Dodge) == true)
			{
				dmg = (((my.Damage + Passive.Brute(my.CurStat[(int)Get.Stat.Brawns],Get.PhysicalDoor(),my)) * Random.Range(0.875f,1.125f)) + karma) * Get.Crit(my,target);
				Get.ApplyDamage((int)dmg,my,target);
				Get.NPCShowNumber(3,((int)dmg).ToString() /*+" Triple HIT!!!"*/,target,offhand);
			}
		}
		/**************************************************************************************************************/
		public void Anima(PC_Base my, NPC_Base target)
		{
			target.CurStat[(int)Get.Stat.HpHit] -= ((my.CurStat[(int)Get.Stat.Tenacity] + (Passive.UnlockedMind(my.CurStat[(int)Get.Stat.Tenacity],Get.MagicalDoor(),my) + 
			                                                                               Passive.Brute(my.CurStat[(int)Get.Stat.Brawns],Get.PhysicalDoor(),my)) *2) * (int)Random.Range(1,1.125f))* Get.Crit(my,target);
			target.TierCount -= Get.DeTier(my,(int)dmg) * Get.Crit(my,target);
		}
		public void Barrage(PC_Base my)
		{
			my.FarBeats = true;
		}

		public void Butterfly()
		{
			NPC_Base[] search = GameObject.FindObjectsOfType(typeof(NPC_Base)) as NPC_Base[];
			int[] power = new int[search.Length];
			int weakest = 40;
			if (search.Length > 2)
			{
				for(int i = 0; i < power.Length;i++) power[i] = search[i].CurStat[(int)Get.Stat.Brawns] + search[i].CurStat[(int)Get.Stat.Tenacity] + search[i].CurStat[(int)Get.Stat.Courage];
				foreach (int p in power) if (p <= weakest) weakest = p;
				for(int i = 0; i < search.Length;i++)
					if (search[i].CurStat[(int)Get.Stat.Brawns] + search[i].CurStat[(int)Get.Stat.Tenacity] + search[i].CurStat[(int)Get.Stat.Courage] == weakest && search.Length == power.Length) 
				{
					GameObject.Destroy(search[i].gameObject);
					break;
				}
			}
		}

		public void Chaos(PC_Base my)
		{
			int tenacity = my.CurStat[(int)Get.Stat.Tenacity];
			while (tenacity > 0)
			{
				foreach (Transform strike in my.Targets)
				{
					
					if (Get.TargetDistance(my.transform,strike.transform,max_range) == true)
					{
						NPC_Base target = strike.GetComponent<NPC_Base>();
						if (Get.HitFormula(my.Hit,target.Dodge) == true)
						{
							dmg = my.Damage + Passive.Brute(my.CurStat[(int)Get.Stat.Brawns],Get.PhysicalDoor(),my);
							if (dmg > 0) 
							{
								dmg = ((dmg * Random.Range(1,1.125f))) * Get.Crit(my,target);
								target.CurStat[(int)Get.Stat.HpHit] -= (int)dmg;
								target.TierCount -= Get.DeTier(my,(int)dmg) * Get.Crit(my,target);
							}
						}
					}
					tenacity--;
				}
			}
		}

		public void Eximo(PC_Base my, NPC_Base target)
		{
		//	target.CurStat[(int)Get.Stat.HpHit] -= (((dmg * (int)Random.Range(1,1.125f)))*(2 + my.CurStat[(int)Get.Stat.Courage]/2))* Get.Crit(my,target);
			//int destroychance = Random.Range(0,100 + (my.wep[0].weight*10));
			//if (destroychance <= 50) my.wep[0] = null;
		}

		public void Feint(PC_Base my)
		{
			if (my.CurStat[(int)Get.Stat.BeatDMG] > 1)
			{
				my.CurStat[(int)Get.Stat.BeatDMG]--;
					Vector3 jump = new Vector3(my.transform.position.x,my.transform.position.y,my.transform.position.z + 2f);
					my.Agent.SetDestination(jump);
				for (int i = 0; i < my.Abilities.Count;i++)
					if (my.Abilities[i].FarRange == true) my.Abilities[i].FarBeats = true;
			}
		}

		public void Fidelity()
		{

		}
		public void Hush(PC_Main my, NPC_Main t)
		{
			
		}
		public void Inquisitio(PC_Base my, NPC_Base target)
		{
			if (my.CurStat[(int)Get.Stat.HpHit] > 1)
			{
				my.CurStat[(int)Get.Stat.HpHit]--;
				if (Get.TargetDistance(my.transform,target.transform,max_range) == true)
				{
					dmg = my.CurStat[(int)Get.Stat.Tenacity] * (2 + Passive.UnlockedMind(my.CurStat[(int)Get.Stat.Tenacity],Get.MagicalDoor(),my));
					if (dmg > 0)
					{
//						target.CurStat[(int)Get.Stat.HpHit] -= ((dmg * (int)Random.Range(1,1.125f))) * Get.Crit(my,target);
						target.TierCount -= Get.DeTier(my,(int)dmg) * Get.Crit(my,target);
					}
				}
			}
		}
		public void Jinx(PC_Base my, NPC_Base target)
		{
			foreach (Transform strike in my.Targets)
			{
				if (Get.TargetDistance(my.transform,strike.transform,max_range) == true)
				{
					int resist = (target.CurStat[(int)Get.Stat.Tenacity]  + target.CurStat[(int)Get.Stat.Courage])*5;
					if (Get.HitFormula(my.CurStat[(int)Get.Stat.Courage]*10,resist) == true)
					{
						target.TierCount = 0;
						target.AdjustToTier(target.Tier);
					}
				}
			}
		}

		/*public void Kadabra(PC_Main my, NPC_Main t)
		{
			NPC_Main[] search = GameObject.FindObjectsOfType(typeof(NPC_Main)) as NPC_Main[];
			foreach (NPC_Main e in search) 
			{
				e.kadabra = true;
				e.move_points = 0;
			}
			GameInformer.battle = false;
			GameInformer.stop = false;
		}*/

		public void Libro(PC_Main my, NPC_Main t)
		{
			
		}

		public void Luck(PC_Base my, NPC_Base target)
		{
			dmg = (my.Damage + Passive.Brute(my.CurStat[(int)Get.Stat.Brawns],Get.PhysicalDoor(),my)) + (my.CurStat[(int)Get.Stat.Courage]/5)*my.Tier-1;
			if (dmg > 0) 
			{
//				target.CurStat[(int)Get.Stat.HpHit] -= (dmg * (int)Random.Range(1,1.125f));
				target.TierCount -= Get.DeTier(my,(int)dmg) * Get.Crit(my,target);
			}
		}

		public void Oblivio(PC_Main my, NPC_Main t)
		{

		}

		public void Omni(PC_Base my, bool apply)
		{
			PC_Base.Omni = apply;
			if (apply == true)
				for (int i = 0; i < Get.Door.Length;i++) Get.Door[i] = Get.Door[i]*2;
			else 
				for (int i = 0; i < Get.Door.Length;i++) Get.Door[i] = Get.Door[i]/2;
		}

		public void Panacea(PC_Base target)
		{
			if (target.CurStat[(int)Get.Stat.BeatDMG] <= target.MaxBeat) target.Panacea = false;
			if (target.Panacea == false) 
			{
				target.CurStat[(int)Get.Stat.BeatDMG]++;
				target.Panacea = true;
			}
		}

		public void Provoke(PC_Base my, NPC_Base target)
		{
			if (Get.HitFormula(my.CurStat[(int)Get.Stat.Brawns],target.CurStat[(int)Get.Stat.Courage]) == true) 
				target.Target = my.transform;
		}

		public void Pulse(PC_Base my, NPC_Base target)
		{
			dmg = (my.CurStat[(int)Get.Stat.Tenacity] + Passive.UnlockedMind(my.CurStat[(int)Get.Stat.Tenacity],Get.MagicalDoor(),my)) * Random.Range(0.875f,1.125f);
			target.CurStat[(int)Get.Stat.HpHit] -= (int)dmg;
			Get.NPCShowNumber(0,((int)dmg).ToString(),target,false);
		}

		public void Rapture(PC_Base my)
		{
			foreach (Transform strike in my.Targets)
			{
				if (Get.TargetDistance(my.transform,strike.transform,max_range) == true)
				{
					NPC_Base target = strike.GetComponent<NPC_Base>();
					dmg = (my.Damage + Passive.Brute(my.CurStat[(int)Get.Stat.Brawns],Get.PhysicalDoor(),my));
					if (dmg > 0) 
					{
//						target.CurStat[(int)Get.Stat.HpHit] -= (dmg * (int)Random.Range(1,1.125f));
						target.TierCount -= Get.DeTier(my,(int)dmg) * Get.Crit(my,target);
					}
				}
			}
		}
			
		public void Aim(PC_Main my)
		{	
			if (my.AimCamera.enabled == false)
			{ 
				my.AimCamera.enabled = true;
				my.AimCamera.GetComponent<Shoot>().enabled = true;
				//my.AimCamera.GetComponent<MouseLook>().enabled = true;
			}else{
				my.AimCamera.enabled = false;
				my.AimCamera.GetComponent<Shoot>().enabled = false;
				//my.AimCamera.GetComponent<MouseLook>().enabled = false;
			}
		}
		
		public void Verto(PC_Base my)
		{
			foreach (Transform strike in my.Targets)
			{
				NPC_Base target = strike.GetComponent<NPC_Base>();
				if (Get.TargetDistance(my.transform,strike.transform,max_range) == true)
				{
					target.CurStat[(int)Get.Stat.HpHit] -= (my.CurStat[(int)Get.Stat.Courage] + (Passive.UnlockedMind(my.CurStat[(int)Get.Stat.Tenacity],Get.MagicalDoor(),my) + 
				                                                                              Passive.Brute(my.CurStat[(int)Get.Stat.Brawns],Get.PhysicalDoor(),my)) *2) * (int)Random.Range(1,1.125f);
					if (dmg > 0) 
					{
//						target.CurStat[(int)Get.Stat.HpHit] -= (dmg * (int)Random.Range(1,1.125f));
						target.TierCount -= Get.DeTier(my,(int)dmg) * Get.Crit(my,target);
					}
				}
			}
		}

		public void Day(PC_Base target)
		{
			if (target.CurStat[(int)Get.Stat.BeatDMG] >= target.MaxBeat) target.Day = false;
			if (target.Day == false) 
			{
				target.CurStat[(int)Get.Stat.BeatDMG]++;
				Debug.Log(target.CurStat[(int)Get.Stat.BeatDMG].ToString());
				target.Day = true;
			}
		}

		public void Divine(PC_Base my)
		{
			List <Transform> Party = new List<Transform>();
			foreach (Transform touched in my.Targets)
			{
				if (Get.TargetDistance(my.transform,touched.transform,max_range) == true) Party.Add(touched);
			}
			foreach (Transform player in Party)
			{
				PC_Base target = player.GetComponent<PC_Base>();
				dmg = (my.CurStat[(int)Get.Stat.Courage] + Get.Door[(int)Get.Element.Divine])/Party.Count;
//				if (dmg + target.CurStat[(int)Get.Stat.HpHit] <= target.MaxHP) target.CurStat[(int)Get.Stat.HpHit] += dmg;
//				else target.CurStat[(int)Get.Stat.HpHit] = target.MaxHP;
			}
		}

		public void Earth(PC_Base my, NPC_Base target)
		{
			if (Get.HitFormula(my.CurStat[(int)Get.Stat.Brawns],Random.Range(0,10)) == true) target.CurStat[(int)Get.Stat.BeatDMG] = 0;
			dmg = my.CurStat[(int)Get.Stat.Brawns] + DoorManager.Door[(int)Get.Element.Earth];
			if (dmg > 0) 
			{
//				target.CurStat[(int)Get.Stat.HpHit] -= (dmg * (int)Random.Range(1,1.125f));
				target.TierCount -= Get.DeTier(my,(int)dmg) * Get.Crit(my,target);
			}
		}

		/*public void Fire(PC_Base my, NPC_Base target)
		{
			dmg = my.CurStat[(int)Get.Stat.Brawns] + (my.CurStat[(int)Get.Stat.Brawns]/2) + DoorManager.Door[Get.Element.Fire];
			if (dmg > 0) 
			{
				target.CurStat[(int)Get.Stat.HpHit] -= (dmg * (int)Random.Range(1,1.125f));
				target.TierCount -= DeTier(my) * Get.Crit(my,target);
			}
		}

		public void Nature(PC_Main my, NPC_Main t)
		{
			foreach (Transform target in my.targets)
			{
				float distance = Vector3.Distance(my.transform.position, target.position);
				if (max_range >= distance)
				{
					NPC_Main tar = target.GetComponent<NPC_Main>();
					int trapchance = Random.Range(0,101 + DoorManager.Door[7]);
					if (trapchance > 50) t.move_points = 0;
					dmg = my.stats[0,2] + DoorManager.Door[7];
					if (dmg > 0) tar.cur_hp -= dmg;
				}
			}
		}

		public void Night(PC_Main my, PC_Main t)
		{
			if (t.tier == 1)
			{
				t.tier_count = 5;
			}
		}

		public void Thunder(PC_Main my, NPC_Main t)
		{
			int amount = my.stats[0,3];
			dmg = my.stats[0,2]/2 + DoorManager.Door[3];
			while (amount > 0)
			{
				foreach (Transform strike in my.targets)
				{
					float distance = Vector3.Distance(strike.position, my.transform.position);
					NPC_Main tar = strike.GetComponent<NPC_Main>();
					if (max_range >= distance)
					{
						t = tar;
						if (dmg > 0) t.cur_hp -= dmg;
						int chance = Random.Range (1,(101 + DoorManager.Door[3]));
						if (chance > 65) if (dmg > 0) t.cur_hp -= dmg;
						else amount --;
					}
				}
			}
		}

		public void Water(PC_Main my, NPC_Main t)
		{
			dmg = my.stats[0,2] + DoorManager.Door[6];
			if (dmg > 0)
			{
				float push = dmg - t.stats[0]/2;
				t.cur_hp -= dmg;
				while (max_range < push)
				{
					t.transform.LookAt(my.transform.position);
					t.transform.Translate(-Vector3.forward * 100f *Time.deltaTime);
					break;
				}
			}
		}

		public void Wicked(PC_Main my, NPC_Main t)
		{
			dmg = my.stats[0,2] + DoorManager.Door[4];
			if (dmg > 0) t.cur_hp -= dmg;
			if (dmg/4 + my.cur_hp < my.HP) my.cur_hp += dmg/4;
			else my.cur_hp = my.HP;
		}

		public void Wind(PC_Main my, NPC_Main t)
		{
			my.reflect = true;
		}

		public void Aqua(PC_Main my, NPC_Main t)
		{
			foreach (Transform enemy in my.targets)
			{
				float distance = Vector3.Distance(my.transform.position, enemy.position);
				if (max_range >= distance)
				{
					dmg = 5 + DoorManager.Door[6];
					if (dmg > 0)
					{
						float push = dmg - t.stats[0]/2;
						t.cur_hp -= dmg;
						while (distance < push)
						{
							t.transform.LookAt(my.transform.position);
							t.transform.Translate(-Vector3.forward * 100f *Time.deltaTime);
							break;
						}
					}
				}
			}
		}

		public void Attonitus(PC_Main my, NPC_Main t)
		{
			
		}

		public void Divinus(PC_Main my, NPC_Main t)
		{
			
		}

		public void Ignis(PC_Main my, NPC_Main t)
		{
			
		}

		public void Lex(PC_Main my, NPC_Main t)
		{
			
		}

		public void Maleficus(PC_Main my, NPC_Main t)
		{
			
		}

		public void Natura(PC_Main my, NPC_Main t)
		{
			
		}

		public void Nocturne(PC_Main my, NPC_Main t)
		{
			
		}

		public void Terra(PC_Main my, NPC_Main t)
		{
			
		}

		public void Ventus(PC_Main my, NPC_Main t)
		{
	
		}*/
	}

	public class Passive
	{
		public string Name;
		public string Description;


		int Increase(int increase, int skillcheck)
		{
			return increase*skillcheck;
		}
		bool TogglePassive(bool passive)
		{
			if (passive == true) return false;
			else return true;
		}
		int RangeIncrease(int skillcheck, int increase)
		{
			return increase*skillcheck;
		}
		float Adapt(float decrease, PC_Base my)
		{
			int skillcheck = Passive.SkillCheck("Adapt",my);
			return decrease*skillcheck;
		}
		static public int Brute (int Brawns, int physical, PC_Base my)
		{
			int skillcheck = Passive.SkillCheck("Brute",my);
			return physical + ((Brawns/4)*skillcheck);
		}
		bool Deft( int Tenacity, int Brawns, int Courage, PC_Base my)
		{
			int skillcheck = Passive.SkillCheck("Deft",my);
			return (Tenacity + (Brawns/2) + (Courage/2) + Random.Range(0,10))*skillcheck > Random.Range(0,30);
		}
		int EmpowerCheck(int M_Defense, PC_Base my)
		{
			int skillcheck = Passive.SkillCheck("Empower",my);
			return M_Defense*skillcheck;
		}
		bool Endure(int Brawns, PC_Base my)
		{
			int skillcheck = Passive.SkillCheck("Endure",my);
			return (Brawns/6)*skillcheck > Random.Range(0,10);
		}
		int MockeryBonus(int Courage, PC_Base my)
		{
			int skillcheck = Passive.SkillCheck("Mockery",my);
			return ((Courage/2)*5)*skillcheck;
		}
		public bool Masterful (int Tenacity, int multiple, PC_Base my)
		{
			float skillcheck = Passive.SkillCheck("Masterful",my);
			return ((Tenacity/2)*multiple)*skillcheck > Random.Range(0,15);
		}
		public int Profane(int Defense, PC_Base my)
		{
			int skillcheck = Passive.SkillCheck("Profane",my);
			return Defense*skillcheck;
		}
		static public float Psycho(float decrease, PC_Base my)
		{
			int skillcheck = Passive.SkillCheck("Psycho",my);
			return decrease*skillcheck;
		}
		public bool Sufficient(int HP, int Tenacity, PC_Base my)
		{
			int skillcheck = Passive.SkillCheck("Sufficient",my);
			if (HP == 1) return ((Tenacity/2)*5)*skillcheck > Random.Range(0,10);
			else return false;
		}
		public int Thievery (int increase, PC_Base my)
		{
			int skillcheck = Passive.SkillCheck("Thievery",my);
			return increase*skillcheck;
		}
		static public int UnlockedMind (int Tenacity, int magical, PC_Base my)
		{
			int skillcheck = Passive.SkillCheck("Unlocked Mind",my);
			return magical + ((Tenacity/4)*skillcheck);
		}
		static public int SkillCheck(string passive, PC_Base my)
		{
			int amount = 0;
			foreach (Passive p in my.Passives)
			{
				if (p.Name == passive) amount++;
			}
			return amount;
		}

		public void SetPassives(string Passive_Name, PC_Base my)
		{
		/*	string[][] names = new string[][]{new string[]{"Able Learner","Ingenuity","Sol"}};
			for (int i = 0; i < my.CurStat.Length;i++)
			{
				my.stats[1,i] = my.stats[0,i] + Increase(1,SkillCheck(names[0][i],my));
			}
			my.ability_bonus = Increase(5,SkillCheck("Lady Luck",my));

			if (Passive_Name == "Acrobat") TogglePassive(my.Acrobat);
			if (Passive_Name == "Critical Shot") TogglePassive(my.Critical_Shot);
			if (Passive_Name == "Flurry") TogglePassive(my.Flurry);
			if (Passive_Name == "Running Shot") TogglePassive(my.RunningShot);
			if (Passive_Name == "Steady") TogglePassive(my.Steady);
			if (Passive_Name == "OnSlaught") TogglePassive(my.onslaught);
			if (Passive_Name == "Energy Mixture") TogglePassive(my.soul_mixture);*/

		}
	}
/*
	#region Anima
	class Animas
	{
		static public void AnManager (string name)
		{
		}
	}
	#endregion
	#region Elements
	class Elements
	{
		static public void EManager ( string name)
		{
			PC_Target target = GameInformer.check.GetComponent<PC_Target>();

			switch (name)
			{
			case "Day":
			if (target.type == "Buff")
			{
				Day(target);
			} else {
			target.type = "Buff";
			target.choice();
			}
			break;
			case "Divine":
			break;
			case "Earth":
			break;
			case "Fire":
			break;
			case "Nature":
			break;
			case "Night":
			break;
			case "Thunder":
			break;
			case "Water":
			break;
			case "Wicked":
			break;
			case "Wind":
			break;
			}
		}

		static public void Day (PC_Target target)
		{
		/*	PC_Stat my = target.GetComponent<PC_Stat>();

			if (my.Beat <= my.MaxBeat)
			{
				my.Beat++;
			}*/
	/*	}

		static public void Divine (PC_Target target)
		{
		/*	PC_Stat my = target.GetComponent<PC_Stat>();
			float reach  = 5f;
			int amount = 0;

			foreach (Transform touched in target.targets)
			{
				float distance = Vector3.Distance(touched.position, GameInformer.check.transform.position);
				if (reach >= distance)
				{
					amount++;
				}
			}

			foreach (Transform player in target.targets)
			{
				PC_Stat tar = target.GetComponent<PC_Stat>();
				float distance = Vector3.Distance(my.transform.position, player.position);

				if (reach >= distance)
				{
					int heal = (my.Courage + DoorManager.DivineDoor)/amount;

					if (heal + tar.CurHP <= tar.MaxHP)
					{
						tar.CurHP += heal;
						Debug.Log(tar.CurHP);
					} else {
						tar.CurHP = tar.MaxHP;
						Debug.Log(tar.CurHP);
					}
				}
			}*/
	/*	}

		static public void Earth(PC_Target target)
		{
			/*NPC_Stat tar = target.target.GetComponent<NPC_Stat>();
			PC_Stat my = target.GetComponent<PC_Stat>();

			int staggerchance = (Random.Range(0,(101 + my.Tenacity)));
			if (staggerchance > 50)
			{
				tar.Beat = 0;
			}

			int damage = my.Tenacity + DoorManager.EarthDoor;
			if (damage >= 1)
			{
				tar.CurHP -= damage;
				Debug.Log (damage);
			} else {
				Debug.Log ("MIISSSS");
			}*/
	/*	}

		static public void Fire(PC_Target target)
		{
			/*PC_Stat my = target.GetComponent<PC_Stat>();
			NPC_Stat tar = target.target.GetComponent<NPC_Stat>();
			
			int damage = (my.Tenacity + my.Tenacity/2) + DoorManager.FireDoor;
			if (damage >= 1)
			{
				tar.CurHP -= damage;
				Debug.Log (damage);
			} else {
			Debug.Log ("MIISSSS");
		}*/
/*	}
	
	static public void Nature(PC_Target target)
		{
	/*		PC_Stat my = target.GetComponent<PC_Stat>();
			NPC_Stat tar = target.target.GetComponent<NPC_Stat>();

			int damage = my.Tenacity + DoorManager.NatureDoor;
			if (damage >= 1)
			{
				tar.CurHP -= damage;
				Debug.Log (damage);
			} else {
				Debug.Log ("MIISSSS");
			}*/
/*		}

		static public void Night(PC_Target target)
		{
			/*PC_Stat tar = target.GetComponent<PC_Stat>();
			
			if (tar.Tier == 1)
			{
				tar.Tier = 2;
				tar.TierCount = 0;
			}*/
/*		}

		static public void Thunder(PC_Target target)
		{
		/*	PC_Stat my = target.GetComponent<PC_Stat>();
			float reach = 4f;
			
			foreach (Transform strike in target.targets)
			{
				float distance = Vector3.Distance(strike.position, target.transform.position);
				NPC_Stat tar = strike.GetComponent<NPC_Stat>();
				
				if (reach >= distance)
				{
					int chance = Random.Range (1,(101 + (DoorManager.ThunderDoor*10)));
					if (chance > 65)
					{
						int damage = my.Tenacity + DoorManager.ThunderDoor;
						if (damage >= 1)
						{
							tar.CurHP -= damage;
							Debug.Log (damage);
						} else {
							Debug.Log ("MIISSSS");
						}
					}
				}
			}*/
	/*	}

		static public void Water(PC_Target target)
		{
			/*PC_Stat my = target.GetComponent<PC_Stat>();
			float reach = 3f;
			
			foreach (Transform strike in target.targets)
			{
				float distance = Vector3.Distance(strike.position, target.transform.position);
				NPC_Stat tar = strike.GetComponent<NPC_Stat>();
				
				if (reach >= distance)
				{
					int damage = my.Tenacity + DoorManager.WaterDoor;
					if (damage >= 1)
					{
						tar.CurHP -= damage;
						Debug.Log (damage);
					} else {
						Debug.Log ("MIISSSS");
					}
				}
			}*/
	//	}

	/*	static public void Wicked (PC_Target target)
		{
		/*	PC_Stat my = target.GetComponent<PC_Stat>();
			NPC_Stat tar = target.target.GetComponent<NPC_Stat>();

			int damage = my.Tenacity + DoorManager.WickedDoor/4;

			if (damage >= 1)
			{
				tar.CurHP -= damage;
				if (damage + my.CurHP >= my.MaxHP)
				{
					my.CurHP = my.MaxHP;
				} else {
					my.CurHP += damage;
				}
				Debug.Log (damage);
			} else {
				Debug.Log ("MIISSSS");
			}*/
//		}

	/*	static public void Wind(PC_Target target)
		{
			target.Reflect = true;
		}
	}
	#endregion
	#region GoddessGifts
	class Goddess
	{
		static public void GodManager (string name)
		{
			PC_Target target = GameInformer.check.GetComponent<PC_Target>();

			switch(name)
			{
			case "Aqua":
			Aqua (target);
			break;
			case "Attonitus":
				break;
			case "Divinus":
				break;
			case "Ignis":
			Ignis (target);
				break;
			case "Lex":
				break;
			case "Maleficus":
				break;
			case "Natura":
				break;
			case "Nocturne":
				break;
			case "Terra":
				break;
			case "Ventus":
				break;
			}
		}

		static public void Aqua (PC_Target target)
		{
			foreach (Transform enemy in target.targets)
			{

				float reach = 20f;
				NPC_Stat tar = enemy.GetComponent<NPC_Stat>();
				float distance = Vector3.Distance(target.transform.position, enemy.position);
				
				if (reach >= distance)
				{
					int damage = 2 + DoorManager.WaterDoor;
					
					if (damage > 1)
					{
						float push = 15 - tar.Brawns;
						tar.CurHP -= damage;
						while (distance < push)
						{
							Debug.Log(push);
							tar.transform.LookAt(target.transform.position);
							tar.transform.Translate(-Vector3.forward * 100f *Time.deltaTime);
							break;
						}
						Debug.Log(tar.CurHP);
					} else {
						Debug.Log("MISSS");
					}
				}
			}
		}

		static public void Attonitus (PC_Target target)
		{
			target.Attonitus = true;
		}

		static public void Divinus (PC_Target target)
		{
			target.Divinus = true;
		}

		static public void Ignis (PC_Target target)
		{
	/*		PC_Stat my = target.GetComponent<PC_Stat>();

			foreach (Transform enemy in target.targets)
			{
				float reach = 2f;
				NPC_Stat tar = enemy.GetComponent<NPC_Stat>();
				float distance = Vector3.Distance(target.transform.position, enemy.position);

				Vector3 forward = (my.transform.TransformDirection(Vector3.forward));
				
				if (reach >= distance && Physics.Raycast(tar.transform.position, forward, 100))
				{
					Debug.DrawRay(my.transform.position,tar.transform.TransformDirection(Vector3.forward), Color.red);
					int damage = (5 * my.Tier) + DoorManager.FireDoor;
					
					if (damage > 1)
					{
						tar.CurHP -= damage;
						Debug.Log(tar.CurHP);
					} else {
						Debug.Log("weak");
					}
				}
			}*/
	//	}

	/*	static public void Lex (PC_Target target)
		{
			/*PC_Stat my = target.GetComponent<PC_Stat>();

			my.Beat = my.Beat*2;
			PC_Target.Lex = true;*/
/*		}

		static public void Maleficus (PC_Target target)
		{
	/*		PC_Stat my = target.GetComponent<PC_Stat>();

			int hits = my.Courage*DoorManager.WickedDoor;

			my.StartCoroutine(States.MalState(target,hits));*/
/*		}

		static public void Natura (PC_Target target)
		{

		}

		static public void Nocturne (PC_Target target)
		{
		}

		static public void Terra (PC_Target target)
		{
		}

		static public void Ventus (PC_Target target)
		{
		}

	}
	#endregion*/
	#region Items
	public class Item
	{
		public string Name;
		public int Slot;
	public string name,a_name,description;
	public int type,heal,amount;
	public float max_range, min_range;
	public bool equipped;
	public Item[] metal = new Item[3];
	public int[] Stats = new int[5];
	public int hit,damage,max,weight;
	public int Brawns,Tenacity,Courage;
	
	//0 = healing, 1 = ally, 2 = 
	public void Execute (int slot, Item I, PC_Base my, Transform target)
	{
		
	}

	public void Aim(PC_Main my)
	{
		
		if (my.AimCamera.enabled == false)
		{
			my.AimCamera.enabled = true;
			my.AimCamera.GetComponent<Shoot>().enabled = true;
			my.AimCamera.GetComponent<MouseLook>().enabled = true;
		}else{
			my.AimCamera.enabled = false;
			my.AimCamera.GetComponent<Shoot>().enabled = false;
			my.AimCamera.GetComponent<MouseLook>().enabled = false;
		}
	}

		public void SetItem(string name, int type)
		{
			Item x = new Item();
			x.name = name;
			x.type = type;
			x.amount = 1;
			ItemList.items.Add(x);
		}
	
		public void TManage (string name) 
		{
			switch (name)
			{
			case "Cobolt":
			{
				int x = 0;
				for(int i = 0; 0 < ItemList.items.Count; i++)
				{
					if (ItemList.items[i].name == "Cobolt")
					{
						ItemList.items[i].amount++;
						break;
					}else{
						x++;
						if (x == ItemList.items.Count)
						{
							SetItem (name,2);
							break;
						}
					}
				}
			}
				break;
			case "Copper":
			{
				int x = 0;
				for(int i = 0; 0 < ItemList.items.Count; i++)
				{
					if (ItemList.items[i].name == "Copper")
					{
						ItemList.items[i].amount++;
						break;
					}else{
						x++;
						if (x == ItemList.items.Count)
						{
							SetItem (name,2);
							break;
						}
					}
				}
			}
			break;
			case "Bronze":
			{
				int x = 0;
				for(int i = 0; 0 < ItemList.items.Count; i++)
				{
				if (ItemList.items[i].name == "Bronze")
				{
					ItemList.items[i].amount++;
					break;
				}else{
					x++;
					if (x == ItemList.items.Count)
					{
							SetItem (name,2);
						break;
					}
				}
				}
			}
				break;
			case "Gleipmir":
			{
				int x = 0;
				for(int i = 0; 0 < ItemList.items.Count; i++)
				{
					if (ItemList.items[i].name == "Gleipmir")
					{
						ItemList.items[i].amount++;
						break;
					}else{
						x++;
						if (x == ItemList.items.Count)
						{
							SetItem (name,2);
							break;
						}
					}
				}
			}
				break;
			case "Gold":
			{
				int x = 0;
				for(int i = 0; 0 < ItemList.items.Count; i++)
				{
					if (ItemList.items[i].name == "Gold")
					{
						ItemList.items[i].amount++;
						break;
					}else{
						x++;
						if (x == ItemList.items.Count)
						{
							SetItem (name,2);
							break;
						}
					}
				}
			}
				break;
			case "Lead":
			{
				int x = 0;
				for(int i = 0; 0 < ItemList.items.Count; i++)
				{
					if (ItemList.items[i].name == "Lead")
					{
						ItemList.items[i].amount++;
						break;
					}else{
						x++;
						if (x == ItemList.items.Count)
						{
							SetItem (name,2);
							break;
						}
					}
				}
			}
				break;
			case "Levaithan":
			{
				int x = 0;
				for(int i = 0; 0 < ItemList.items.Count; i++)
				{
					if (ItemList.items[i].name == "Levaithan")
					{
						ItemList.items[i].amount++;
						break;
					}else{
						x++;
						if (x == ItemList.items.Count)
						{
							SetItem (name,2);
							break;
						}
					}
				}
			}
				break;
			case "Magnesium":
			{
				int x = 0;
				for(int i = 0; 0 < ItemList.items.Count; i++)
				{
					if (ItemList.items[i].name == "Magnesium")
					{
						ItemList.items[i].amount++;
						break;
					}else{
						x++;
						if (x == ItemList.items.Count)
						{
							SetItem (name,2);
							break;
						}
					}
				}
			}
				break;
			case "Platinum":
			{
				int x = 0;
				for(int i = 0; 0 < ItemList.items.Count; i++)
				{
					if (ItemList.items[i].name == "Platinum")
					{
						ItemList.items[i].amount++;
						break;
					}else{
						x++;
						if (x == ItemList.items.Count)
						{
							SetItem (name,2);
							break;
						}
					}
				}
			}
				break;
			case "Steel":
			{
				int x = 0;
				for(int i = 0; 0 < ItemList.items.Count; i++)
				{
					if (ItemList.items[i].name == "Steel")
					{
						ItemList.items[i].amount++;
						break;
					}else{
						x++;
						if (x == ItemList.items.Count)
						{
							SetItem (name,2);
							break;
						}
					}
				}
			}
				break;
			case "Silver":
			{
				int x = 0;
				for(int i = 0; 0 < ItemList.items.Count; i++)
				{
					if (ItemList.items[i].name == "Silver")
					{
						ItemList.items[i].amount++;
						break;
					}else{
						x++;
						if (x == ItemList.items.Count)
						{
							SetItem (name,2);
							break;
						}
					}
				}
			}
				break;
			case "Tin":
			{
				int x = 0;
				for(int i = 0; 0 < ItemList.items.Count; i++)
				{
					if (ItemList.items[i].name == "Tin")
					{
						ItemList.items[i].amount++;
						break;
					}else{
						x++;
						if (x == ItemList.items.Count)
						{
							SetItem (name,2);
							break;
						}
					}
				}
			}
				break;
			case "Zinc":
			{
				int x = 0;
				for(int i = 0; 0 < ItemList.items.Count; i++)
				{
					if (ItemList.items[i].name == "Zinc")
					{
						ItemList.items[i].amount++;
						break;
					}else{
						x++;
						if (x == ItemList.items.Count)
						{
							SetItem (name,2);
							break;
						}
					}
				}
			}
				break;
			case "Barrage":
				//	GetComponent<Barrage>().castBarrage();
				break;
			case "Panacea":
				break;
			default:
				break;
				
			}
		}

		static void Pills()
		{
			Debug.Log("ye");
		}
	}
	#endregion
	/*#region Weapon
	public class weapon
	{
		public string name;
		public string description;
		public bool equipped;

		public Item[] metal = new Item[3];

		public int hit;
		public int damage;
		public int max;
		public int weight;
		public int price;
		public int type;
	}
	#endregion
	#region Accessory
	public class accessory
	{
		public string name;
		public string description;
		public bool equipped;
		
		public Item[] metal = new Item[3];

		public int Damage;
		public int Hit;
		public int Brawns;
		public int Tenacity;
		public int Courage;
		public int price;
		public int type;
	}

	public class passive
	{
		public string name;
		public string description;
		public bool equipped;
	}
	#endregion
	#region States*/
	class States
	{
	/*	static public IEnumerator Suffocate (PC_Main player, NPC_Stat npc, int duration)
		{

			while (duration > 0)
			{
				if (player != null)
				{
					if (player.myturn == false)
					{
					duration--;
					Debug.Log("player lost a dur");
					}
				} else if (npc != null)
				{
					if (npc.myturn == false)
					{
					duration--;
					Debug.Log("enemy lost a dur");
					}
				}
				Debug.Log(duration);
			Debug.Log("I CANT BREATH");
			yield return null;
			}
			Debug.Log("IM DEAD");
			yield return null;
		}

/*		static public IEnumerator Immobile ( NPC_Stat npc, int stuck)
		{
			while (stuck > 0)
			{
				//player.speed = 0;
				yield return null;
			}
		}

/*		static public IEnumerator MalState (PC_Target target, int hits)
		{
			while (hits > 0)
			{
				foreach (Transform enemy in target.targets)
				{
					NPC_Stat tar = enemy.GetComponent<NPC_Stat>();

					float cap = enemy.GetComponent<NPC_Stat>().TierCount;

					if (tar != null)
					{
						if (tar.TierCount > cap)
						{
							tar.TierCount = cap;
							yield return null;
						}
					}
				}
			}
		}*/

/*		static public IEnumerator Provoked (PC_Main player, NPC_Stat npc, int duration, bool onplayer)
		{

			while (duration > 0)
			{
				if (player.myturn == true)
				{
					if (onplayer == true)
					{
						Debug.Log("IMMA GET YOU " + npc.name);
						duration--;
					}
				}

				if (npc.myturn == true)
				{
					if (onplayer == false)
					{
						Debug.Log("IMMA GET YOU " + player.name);
						duration--;
					}
				}
				Debug.Log(duration);
				Debug.Log("ENRAGED");
				yield return null;
			}
			Debug.Log("IM better now :D");
			yield return null;
		}*/
	}
}

	#endregion
//}
using UnityEngine;
using System.Collections;

namespace Database
{
	#region Abilities
	public class Abilities
	{ 
		public string name;
		public bool equipped;
		public string description;
		public int type;
	}
		/*
		#region AbilityManager
	// Gets chosen ability and target then makes check possible to execute
	/*	static public void AManage (string name) 
		{
			PC_Target target = GameInformer.check.GetComponent<PC_Target>();

			switch (name)
			{
			case "Anima":
				if (target.type == "Damage")
				{
				if (FarInit(target) == true && HitChance(target) == true)
				{
				Anima(target);
				}
				} else {
				target.type = "Damage";
				target.choice();
				}
				break;
			case "Barrage":
				Barrage(target);
				break;
			case "Butterfly":
				target.type = "Special";
				target.choice();
				Butterfly(target);
				break;
			case "Chaos":
				if (target.type == "Damage")
				{
				if (CloseInit(target) == true)
				{
				Chaos(target);
				}
				} else {
				target.type = "Damage";
				target.choice();
				}
				break;
			case "Eximo":
				if (target.type == "Damage")
				{
				if (CloseInit(target) == true && HitChance(target) == true)
				{
				Eximo(target);
				}
				} else {
				target.type = "Damage";
				target.choice();
				}
				break;
			case "Fient":
				Fient(target);
				break;
			case "Fidelity":
				if (target.type == "Buff")
				{
				target.StartCoroutine(Fidelity());
				} else {
				target.type = "Buff";
				target.choice();
				}
				break;
			case "Hush":
				if (target.type == "Damage")
				{
					Hush(target);
				} else {
				target.type = "Damage";
				target.choice();
				}
				break;
			case "Inquisito":
				if (target.type == "Damage")
				{
					Inquisito(target);
				} else {
				target.type = "Damage";
				target.choice();
				}
				break;
			case "Jinx":
				if (target.type == "Damage")
				{
					Jinx(target);
				} else {
				target.type = "Damage";
				target.choice();
				}
				break;
			case "Kadabra":
				target.StartCoroutine(Kadabra(target));
				break;
			case "Karma":
				if (target.type == "Damage")
				{
					Karma(target);
				} else {
				target.type = "Damage";
				target.choice();
				}
				break;
			case "Libro":
				if (target.type == "Damage")
				{
					Libro(target);
				} else {
				target.type = "Damage";
				target.choice();
				}
				break;
			case "Luck":
				if (target.type == "Damage")
				{
					Luck(target);
				} else {
				target.type = "Damage";
				target.choice();
				}
				break;
			case "Oblivio":
				if (target.type == "Damage")
				{
					Oblivio(target);
				} else {
				target.type = "Damage";
				target.choice();
				}
				break;
			case "Omni":
				if (target.type == "Buff")
				{
					Omni(target);
				} else {
				target.type = "Buff";
				target.choice();
				}
				break;
			case "Onslaught":
				if (target.type == "Damage")
				{
					Onslaught(target);
				}
				break;
			case "Panacea":
				if (target.type == "Buff")
				{
					Panacea(target);
				} else {
					target.type = "Buff";
					target.choice();
				}
				break;
			case "Provoke":
				if (target.type == "Damage")
				{
					Provoke(target);
				} else {
				target.type = "Damage";
				target.choice();
				}
				break;
			case "Pulse":
				if (target.type == "Damage")
				{
					Pulse(target);
				} else {
				target.type = "Damage";
				target.choice();
				}
				break;
			case "Rapture":
				if (target.type == "Damage")
				{
					if (CloseInit(target) == true)
					{
						Rapture(target);
					}
				} else {
					target.type = "Damage";
					target.choice();
				}
				break;
			case "Verto":
				if (target.type == "Damage")
				{
					Verto(target);
				} else {
				target.type = "Damage";
				target.choice();
				}
				break;
			default:
				break;
				
			}
		}
		#endregion
		#region Initations
		static public bool CloseInit(PC_Target target)
		{
			float distance = Vector3.Distance(target.target.transform.position, GameInformer.check.transform.position);
			PC_Movement start = target.gameObject.GetComponent<PC_Movement>();
			PC_Main my = target.gameObject.GetComponent<PC_Main>();
			if (distance <= 1.5f)
			{
				if (my.Beat > 0)
				{
				my.Beat--;
				return true;
				} else {
				return false;
				}
			} else {
			start.MoveStart(target.target,10f);
			return false;
			}
		}

		/*Checks if Far Range init is possible and if Far Range is beat or turn based*/
	/*	static public bool FarInit(PC_Target target)
		{
			PC_Main my = target.gameObject.GetComponent<PC_Main>();
			bool Farbeat = false;
			float distance = Vector3.Distance(target.target.transform.position, GameInformer.check.transform.position);

		/*	if (store != null)
			{
				Farbeat = target.Farbeat;
			}

			if (surprise != null)
			{
				Farbeat = target.Farbeat;
			}*/


	/*		if (distance >= 3f && distance <= 12f && Farbeat == false)
			{
				return true;
			} else if (distance >= 3f && distance <= 12f && Farbeat == true)
			{
				if(my.Beat > 0)
				{
				my.Beat--;
				return true;
				} else {
				target.Farbeat = false;
				return false;
				}
			} else {
				return false;
			}
		}

		#endregion
		#region Hitting
		//static public bool HitChance (PC_Target target)
		//{
	/*		PC_Equip weapon = target.gameObject.GetComponent<PC_Equip>();
			NPC_Stat tar = target.target.GetComponent<NPC_Stat>();
			PC_Main my = target.GetComponent<PC_Main>();

			int Attack = Random.Range (0,100 + weapon./*WepHit + my.Tenacity);
			int Dodge = Random.Range (0,100 + tar.Tenacity);
			int Beat = Random.Range(0,50 + tar.Nimble);

			if (Beat < 50)
			{
				return Attack > Dodge;
			} else {
				return false;
			}*/
		//}

		//Checks for Range & critical then returns appropriate damage
	/*	static public int DamageCal (PC_Main my, NPC_Stat tar, PC_Equip weapon, int damage, int door, int multiple, int type)
		{

			int Crit = Random.Range(0,50 + my.Courage);
			int Dodge = Random.Range(0,100 + tar.Courage);
			int weap = 0;
			bool Cshot = false;
			damage = (damage + weap + door)*multiple - tar.Defense;

			if (weapon != null)
			{
				weap = 0;
			}

			if(type == 1)
			{
				//CriticalShot store = my.GetComponent<CriticalShot>();

				if (store != null)
				{
					Cshot = store.Cshot;
				}
			}

			if (Crit > Dodge || type == 3)
			{
				if (type == 1 && Cshot == true || type == 0)
				{
				return damage * my.Tier;
				} else {
				return damage;
				}
			} else {
				return damage;
			}
			return damage;
		}*/
	/*	#endregion
		#region RefreshLoops
		//Resets the selection
		static public void AbilityClear(PC_Target target)
		{
			target.type = "";
			target.targets.Clear();
		}

		static public void EndTurnClear(NPC_Stat target, PC_Main my)
		{
			if (my.cur_beats != my.Beat)
			{
				my.cur_beats = my.Beat;
			}

			GameInformer.battlestate = 1;
		}

		static public void EndBattleClear(PC_Target target)
		{
			foreach (Transform player in target.targets)
			{
				PC_Main my = player.GetComponent<PC_Main>();
				if (my != null)
					{
				if (my.cur_beats < my.Beat)
				{
					my.cur_beats = my.Beat;
				}
				my.tier_count = 0;
				}
			}
		}

		#endregion

	#region Ability Effects

		#region Anima
		static public void Anima (PC_Target target)
		{
			PC_Main my = target.GetComponent<PC_Main>();
			NPC_Stat tar = target.target.GetComponent<NPC_Stat>();

			tar.CurHP -= my.Tenacity + (DoorManager.MagicalDoor + DoorManager.PhysicalDoor)*2;
			Debug.Log ("ANIMA");
		}
		#endregion
		#region Barrage
		static public void Barrage (PC_Target target)
		{
			target.Farbeat = true;
		}
		#endregion
		#region Butterfly
		static public void Butterfly (PC_Target target)
		{
			bool done = false;
			int amount = 0;
			int[] power = null;


			foreach (Transform chosen in target.targets)
			{
				NPC_Stat worthy = chosen.GetComponent<NPC_Stat>();

				if (worthy != null)
				{
					amount ++;
				}
			}

			power = new int[amount];
			amount = -1;
			Debug.Log (power.Length);

			if (power.Length >= 2)
			{
			foreach (Transform stored in target.targets)
			{
				NPC_Stat stat = stored.GetComponent<NPC_Stat>();

				if(stat != null)
				{
					amount ++;
					power[amount] = stat.Brawns;
				}
			}



			foreach (int find in power)
			{
				
				if(find <= 1)
				{
					foreach (Transform stored in target.targets)
					{
					NPC_Stat lowest = stored.GetComponent<NPC_Stat>();
					PC_Movement move = target.GetComponent<PC_Movement>();
					

					Transform refrence = target.gameObject.GetComponentInChildren<PC_Ref>().transform;

					if (lowest.Brawns <= 1 && done == false)
					{
					done = true;
					GameObject butterfly = GameObject.Instantiate(GameObject.Find("Butterfly"),refrence.position,refrence.rotation) as GameObject;

					move.ButterflyStart(butterfly,lowest.transform,2f);
					}
					}
				} else if(find <= 2)
				{
					foreach (Transform stored in target.targets)
					{
						NPC_Stat lowest = stored.GetComponent<NPC_Stat>();
						PC_Movement move = target.GetComponent<PC_Movement>();
						
						
						Transform refrence = target.gameObject.GetComponentInChildren<PC_Ref>().transform;
						
						if (lowest.Brawns <= 1 && done == false)
						{
							done = true;
							GameObject butterfly = GameObject.Instantiate(GameObject.Find("Butterfly"),refrence.position,refrence.rotation) as GameObject;
							
							move.ButterflyStart(butterfly,lowest.transform,2f);
						}
					}
				}
			}
			}
		}
		#endregion
		#region Chaos
/*		static public void Chaos (PC_Target target)
		{
			PC_Main my = target.GetComponent<PC_Main>();
			PC_Movement move = target.GetComponent<PC_Movement>();
			float reach = 4f;
			float distance;
			int amount = 0;
			Transform[] anim = null;

			foreach (Transform touched in target.targets)
			{
				distance = Vector3.Distance(touched.position, GameInformer.check.transform.position);
				if (reach >= distance)
				{
					amount++;
				}
			}

			anim = new Transform[amount];
			amount = -1;

			foreach (Transform stored in target.targets)
			{
				distance = Vector3.Distance(stored.position, GameInformer.check.transform.position);
				if(reach>=distance)
				{
				amount ++;
				anim[amount] = stored;
				}
			}
			
			move.ChaosStart(anim,0.2f);

			foreach (Transform strike in target.targets)
			{

				distance = Vector3.Distance(strike.position, GameInformer.check.transform.position);
				NPC_Stat tar = strike.GetComponent<NPC_Stat>();
				int tenacity = my.Tenacity/(amount +1);
				Debug.Log("ten = " + tenacity);
			
				if (reach >= distance)
				{
					while (tenacity != 0)
					{
						PC_Equip weapon = target.GetComponent<PC_Equip>(); 
						tenacity--;
						if (HitChance(target) == true)
						{
						int damage = Abilities.DamageCal(my,tar,weapon,my.Brawns,DoorManager.PhysicalDoor,1,0);
							if (damage >= 1)
							{
								tar.CurHP -= damage;
								Debug.Log (damage);
							}
						} else {
							Debug.Log ("MIISSSS");
						}
					}
				}
			}
		}*/
	/*	#endregion
		#region Eximo
		static public void Eximo (PC_Target target)
		{
			PC_Main my = target.GetComponent<PC_Main>();
			NPC_Stat tar = target.target.GetComponent<NPC_Stat>();
			PC_Equip weapon = target.GetComponent<PC_Equip>(); 

			int damage = Abilities.DamageCal(my,tar,weapon,my.Brawns,DoorManager.PhysicalDoor,3,0);
			if (damage >= 1)
			{
				tar.CurHP -= damage;
				Debug.Log (damage);
			} else {
			Debug.Log ("MIISSSS");
		}
			
		}
		#endregion
		#region Fient
		static public void Fient (PC_Target target)
		{
			float distance = Vector3.Distance(target.target.transform.position, GameInformer.check.transform.position);
			PC_Movement move = target.GetComponent<PC_Movement>();

			PC_Main my = target.gameObject.GetComponent<PC_Main>();
			if (distance <= 1.5f)
			{
				if (my.Beat > 0)
				{
					my.Beat--;
					target.Farbeat = true;
					move.FeintStart(target.target,10f);
				}
			}
		}
		#endregion
		#region Fidelity
		static public IEnumerator Fidelity ()
		{
			Debug.Log("um  yeah");
			if (Input.GetKey(PC_Control.GoddessTog) && Input.GetKeyDown(PC_Control.A1) && PC_Control.g1burnt == true)
			{
				PC_Control.g1burnt = false;
				yield return null;
			} else 
			if (Input.GetKey(PC_Control.GoddessTog) && Input.GetKeyDown(PC_Control.A2) && PC_Control.g2burnt == true)
			{
				PC_Control.g2burnt = false;
				yield return null;
			} else 
				if (Input.GetKey(PC_Control.GoddessTog) && Input.GetKeyDown(PC_Control.A3) && PC_Control.g3burnt == true)
			{
				PC_Control.g3burnt = false;
				yield return null;
			} else 
				if (Input.GetKey(PC_Control.GoddessTog) && Input.GetKeyDown(PC_Control.A4) && PC_Control.g4burnt == true)
			{
				PC_Control.g4burnt = false;
				yield return null;
			}
		}
		#endregion
		#region Hush
		static public void Hush (PC_Target target)
		{
			PC_Main my = target.GetComponent<PC_Main>();
			NPC_Stat tar = target.target.GetComponent<NPC_Stat>();

			int spellcheck = (my.Tenacity + DoorManager.MagicalDoor)*10;
			int spellresist = (tar.Tenacity + tar.Tier)*10;
			int dur = 4 - (my.Tenacity/3);

			if (spellcheck > spellresist)
			{
				tar.StartCoroutine(States.Suffocate(null,tar, dur));
			}

		}
		#endregion
		#region Inquisito
		static public void Inquisito (PC_Target target)
		{
			PC_Main my = target.GetComponent<PC_Main>();
			float reach = 10f;

			if (my.cur_hp > 1)
			{
			my.cur_hp--;
			}

			foreach (Transform strike in target.targets)
			{
				float distance = Vector3.Distance(strike.position, GameInformer.check.transform.position);
				NPC_Stat tar = strike.GetComponent<NPC_Stat>();
				
				if (reach >= distance)
				{
					int damage = Abilities.DamageCal(my,tar,null,my.Tenacity,0,(2 + DoorManager.MagicalDoor),1);
					if (damage >= 1)
					{
					tar.CurHP -= damage;
					}
				}
			}
		}
		#endregion
		#region Jinx
		static public void Jinx (PC_Target target)
		{
			PC_Main my = target.GetComponent<PC_Main>();
			float reach = 9f;

			foreach (Transform strike in target.targets)
			{
				float distance = Vector3.Distance(strike.position, GameInformer.check.transform.position);
				NPC_Stat tar = strike.GetComponent<NPC_Stat>();

				if (tar != null)
				{
				if (reach >= distance)
				{
					int spellcheck = my.Tenacity*10;

					if (spellcheck > tar.Res_SuperFear)
					{
						tar.TierCount = 0;
						tar.Tier = 1;
					}
				}
				}
			}
			
		}
		#endregion
		#region Kadabra
		static public IEnumerator Kadabra (PC_Target target)
		{
			int duration = 3;
			float wait = 10f;
			NPC_Stat tar = null;

			PC_Control.ability = false;

			foreach (Transform opponent in target.targets)
			{
				tar = opponent.GetComponent<NPC_Stat>();

				if (tar != null)
				{
					tar.myturn = false;
				}
			}

			while (duration > 0)
			{
				Debug.Log("SMOKED!!");
				yield return new WaitForSeconds(wait);
				duration--;
			}
		}
		#endregion
		#region Karma
		static public void Karma (PC_Target target)
		{
			PC_Main my = target.GetComponent<PC_Main>();
			NPC_Stat tar = target.target.GetComponent<NPC_Stat>();
			
			/*int damage = Abilities.DamageCal(my,tar,,my.Brawns,DoorManager.PhysicalDoor,1,0);
			if (damage >= 1)
			{
				tar.CurHP -= damage + tar.Defense;
				Debug.Log (damage);
			} else {
				Debug.Log ("MIISSSS");
			}*/
			
		}
	/*	#endregion
		#region Libro
		static public void Libro (PC_Target target)
		{

		}
		#endregion
		#region Luck
		static public void Luck (PC_Target target)
		{
			PC_Main my = target.GetComponent<PC_Main>();
			NPC_Stat tar = target.target.GetComponent<NPC_Stat>();
		/*	PC_Equip weapon = target.GetComponent<PC_Equip>(); 
			
			int damage = Abilities.DamageCal(my,tar,weapon,my.Brawns,DoorManager.PhysicalDoor,1,3);
			if (damage >= 1)
			{
				tar.CurHP -= (damage * my.Tier);
				Debug.Log (damage);
			} else {
				Debug.Log ("MIISSSS");
			}*/

	//	}
	/*	#endregion
		#region Oblivio
		static public void Oblivio (PC_Target target)
		{
			PC_Main my = target.GetComponent<PC_Main>();
			NPC_Stat tar = target.target.GetComponent<NPC_Stat>();

			tar.TierCount -= my.Tenacity;
			
		}
		#endregion
		#region Omni
		static public void Omni (PC_Target target)
		{
			NPC_Stat tar = target.target.GetComponent<NPC_Stat>();

			if (tar.Tier <= 2)
			{
			tar.Tier++;
			}
		}
		#endregion
		#region Onslaught
		static public void Onslaught(PC_Target target)
		{
			NPC_Stat tar = target.target.GetComponent<NPC_Stat>();
			PC_Main my = target.GetComponent<PC_Main>();
			PC_Equip weapon = target.GetComponent<PC_Equip>();
			
		/*	if (tar.Beat <= tar.MaxBeat)
			{
				tar.Beat += 1;
				AbilityClear (target);

				if (tar.CurHP <= 0)
				{
					target.AimTarget();
					float distance = Vector3.Distance(tar.transform.position,target.transform.position);
					
					if (distance <= 2f)
					{
						int damage = Abilities.DamageCal(my,tar,weapon,my.Brawns,DoorManager.PhysicalDoor,1,0);
						if (damage >= 1)
						{
							tar.CurHP -= (damage);
							Debug.Log (damage);
						} else {
							Debug.Log ("MIISSSS");
						}
					}
				}
			}*/
	/*	}
		#endregion
		#region Panacea
		static public void Panacea(PC_Target target)
		{
			/*PC_Stat tar = target.target.GetComponent<PC_Stat>();
			if (tar.Beat <= tar.MaxBeat)
			{
				tar.Beat += 1;
				AbilityClear (target);
			}*/
	/*	}
		#endregion
		#region Provoke
		static public void Provoke (PC_Target target)
		{
			/*PC_Stat my = target.GetComponent<PC_Stat>();
			NPC_Stat tar = target.target.GetComponent<NPC_Stat>();
			
			int spellcheck = (my.Courage + DoorManager.PhysicalDoor)*10;
			int spellresist = (tar.Courage + tar.Tier)*10;
			int dur = 4 - (my.Courage/3);
			
			if (spellcheck > spellresist)
			{
				tar.StartCoroutine(States.Provoked(my,tar, dur, false));
			}*/
			
	/*	}
		#endregion
		#region Pulse
		static public void Pulse (PC_Target target)
		{
			NPC_Stat tar = target.target.GetComponent<NPC_Stat>();
		/*	PC_Stat my = target.GetComponent<PC_Stat>();

			int damage = Abilities.DamageCal(my,tar,null,my.Tenacity,DoorManager.MagicalDoor,1,1);
			if (damage >= 1)
			{
				tar.CurHP -= (damage);
				Debug.Log (damage);
			} else {
				Debug.Log ("MIISSSS");
			}*/
			
	/*	}
		#endregion
		#region Rapture
		static public void Rapture (PC_Target target)
		{
			/*PC_Stat my = target.GetComponent<PC_Stat>();
			float reach = 4f;

			foreach (Transform strike in target.targets)
			{
				float distance = Vector3.Distance(strike.position, target.transform.position);
				NPC_Stat tar = strike.GetComponent<NPC_Stat>();

				if (reach >= distance)
				{
					if(tar != null)
					{
						PC_Equip weapon = target.GetComponent<PC_Equip>(); 

						if (HitChance(target) == true)
						{
							int damage = Abilities.DamageCal(my,tar,weapon,my.Brawns,DoorManager.PhysicalDoor,1,0);
							if (damage >= 1)
							{
								tar.CurHP -= damage;
								Debug.Log (damage);
							}
						} else {
							Debug.Log ("MIISSSS");
						}
					}
				}
			}*/
			
	/*	}
		#endregion
		#region Verto
		static public void Verto (PC_Target target)
		{
		/*	PC_Stat my = target.GetComponent<PC_Stat>();
			float reach = 7f;
			
			foreach (Transform strike in target.targets)
			{
				float distance = Vector3.Distance(strike.position, target.transform.position);
				NPC_Stat tar = strike.GetComponent<NPC_Stat>();
				
				if (reach >= distance)
				{
					if(tar != null)
					{
						PC_Equip weapon = target.GetComponent<PC_Equip>(); 
						
						if (HitChance(target) == true)
						{
							int damage = Abilities.DamageCal(my,tar,weapon,my.Tenacity,DoorManager.MagicalDoor + DoorManager.PhysicalDoor,1,1);
							if (damage >= 1)
							{
								tar.CurHP -= damage;
								Debug.Log (damage);
							}
						} else {
							Debug.Log ("MIISSSS");
						}
					}
				}
			}*/
			
	/*	}
	#endregion
	}
	#endregion
	#endregion
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
	public class item
	{
		public string name;
		public int type;
		public int amount;
		public bool equipped;

		public void SetItem(string name, int type)
		{
			item x = new item();
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
	#region Weapon
	public class weapon
	{
		public string name;
		public string description;
		public bool equipped;

		public item[] metal = new item[3];

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
		
		public item[] metal = new item[3];

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
	#region States
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
	#endregion
//}
#endregion
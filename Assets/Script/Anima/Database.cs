using UnityEngine;
using System.Collections;
using Database;

namespace Database
{
	#region Abilities
	public class Ability
	{ 
		public string name, description;
		public bool equipped, far_range;
		public float max_range, min_range;
		public int type,ID;
		int dmg;

		float deTier(PC_Main my)
		{
			float detier = (dmg % 1.5f);
			if (my.ID != 6) 
			{
				if (detier <= 0) return 0.1f;
				else return detier;
			}else if (detier <= 0) return 0.2f;
				else return (detier * 2);
		}

		bool FarRangeHit(float d, PC_Main my, NPC_Main t)
		{
			int my_hit = Random.Range (0,100 + my.stats[0,2]);
			int t_dodge = Random.Range (0,100 + t.stats[1]);
			if (d > max_range) return my_hit > t_dodge;
			else return false;
		}

		bool CloseRangeHit(PC_Main my, NPC_Main t)
		{
			int my_hit = Random.Range (0,100);
			int t_dodge = Random.Range (0,100 + t.stats[1]);
			return my_hit > t_dodge;
		}

		bool CritChance(PC_Main my, NPC_Main t)
		{
			int my_hit = Random.Range (0,100 + my.stats[3,0]);
			int t_dodge = Random.Range (0,100 + t.stats[1]);
			return my_hit > t_dodge;
		}

		public void AllyCast(PC_Main my, PC_Main t)
		{
			float distance = Vector3.Distance(my.transform.position, t.transform.position);
			if (distance <= max_range) Execute(my,t,null);
			my.EndTurn();
		}

		public void OpposeCast(PC_Main my, NPC_Main t, float d)
		{
			if (far_range == true) 
			{
				if (FarRangeHit(d,my,t) == true) Execute(my,null,t);
				else HUD.info = "MISS!!!";
				if (my.far_beats == false) my.EndTurn();
				else my.cur_beats--;
			}else if (my.cur_beats > 0 && my.far_beats == false) 
			{
				if (t.Rose_Innate == true && my.ID == 5 && my.cur_beats >= my.Beat) 
				{
					Execute(my,null,t);
					t.Rose_Innate = false;
				}else{
					my.cur_beats--;
					if (CloseRangeHit(my,t) == true) Execute(my,null,t);
					else HUD.info = "MISS!!!";
				}
			}
			if (my.cur_beats == 0) my.EndTurn();
		}

		public void Execute(PC_Main my, PC_Main a, NPC_Main t)
		{
			if (name == "Attack") Attack(my,t,0);
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
			if (name == "Ventus") Ventus(my,t);
		}

		public void Attack(PC_Main my, NPC_Main t, int karma)
		{
			dmg = my.damage + DoorManager.PhysicalDoor;
			if (dmg > 0) 
			{
				t.cur_hp -= ((dmg * (int) Random.Range(1,1.125f)) + karma);
				t.tier_count -= deTier(my);
			HUD.info = t.Name + " Remaining HP: "+t.cur_hp;
			}
			if (my.ID == 1 && CloseRangeHit(my,t) == true) Zen_Attack(my,t,karma);
			if (my.ID == 3 && my.cur_beats > 0) Sky_Attack(my,t,karma);
			else t.Sky_attacks = 0;
			if (my.ID == 4) Hena_Attack(my,t,karma);
			if (my.ID == 6) Ann_Attack(my,t,karma);

			if (t.cur_beats > 0 && t.cur_hp > 0)
			{
				int t_hit = Random.Range (0,100 + t.Hit * (int) Random.Range(1,1.125f));
				int my_dodge = Random.Range (0,100 + my.stats[0,1] * (int) Random.Range(1,1.125f));
				if (t_hit > my_dodge) my.cur_hp--;
			}
		}

		void Zen_Attack(PC_Main my, NPC_Main t, int karma)
		{
			if (dmg > 0) t.cur_hp -= ((dmg * (int) Random.Range(1,1.125f)) - karma);
			deTier(my);
			HUD.info = "DOUBLE HIT! "+t.Name + " Remaining HP: "+t.cur_hp;
		}

		void Sky_Attack(PC_Main my, NPC_Main t, int karma)
		{
			int divide = my.Beat - t.Sky_attacks - (karma/2);
			if (divide < 0) divide = 1;
			if (t.Sky_attacks > 0 && dmg > 0) 
				t.cur_hp -= ((dmg/divide) * (int) Random.Range(1,1.125f));
			t.Sky_attacks ++;
			HUD.info = "REPETITIVE STRIKE!!" + t.Name + " Remaining HP: "+t.cur_hp;
		}
		
		void Hena_Attack(PC_Main my, NPC_Main t, int karma)
		{
			int steal = Random.Range (0,100 + my.stats[0,2]);
			if (steal > 98) 
				for (int i = 0; i < t.items.Length;i++)
					if (Random.Range(1,4) == i && t.items[i] != null)
				{
					ItemList.items.Add(t.items[i]);
					
					if (t.items[i].type < 2 && karma == 0) 
					{
						for (int j = 0; j < my.items.Length;j++)
						{
							if (my.items[j] == null) my.items[j] = t.items[i];
							t.items[i] = null;
						}
					}else t.items[i] = null;
				}
		}

		void Ann_Attack(PC_Main my, NPC_Main t, int karma)
		{
			int stagger = (my.stats[1,0] + DoorManager.PhysicalDoor * 5);
			int t_resist = Random.Range (0,100 + t.stats[2]);
			if (stagger > t_resist && karma == 0) t.cur_beats = 0;
			else if (stagger > t_resist && karma != 0) t.cur_beats -= t.cur_beats/karma;
		}
		
		public void Anima(PC_Main my, NPC_Main t)
		{
			t.cur_hp -= my.stats[1,0] + (DoorManager.MagicalDoor + DoorManager.PhysicalDoor + 1)*2;
			HUD.info = "ANIMA casted! "+t.name+" remaining hp: "+t.cur_hp;
			Debug.Log("ANIMA casted!");
		}

		public void Barrage(PC_Main my)
		{
			if (my.target != null) my.far_beats = true;
		}

		public void Butterfly()
		{
			NPC_Main[] search = GameObject.FindObjectsOfType(typeof(NPC_Main)) as NPC_Main[];
			int[] power = new int[search.Length];
			int weakest = 40;
			if (search.Length > 2)
			{
				for(int i = 0; i < power.Length;i++) power[i] = search[i].Brawns + search[i].Tenacity + search[i].Courage;
				foreach (int p in power) if (p <= weakest) weakest = p;
				for(int i = 0; i < search.Length;i++)
					if (search[i].Brawns + search[i].Tenacity + search[i].Courage == weakest && search.Length == power.Length) 
				{
					GameObject.Destroy(search[i].gameObject);
					break;
				}
			}
		}

		public void Chaos(PC_Main my, NPC_Main t)
		{
			float distance;
			int tenacity = my.stats[1,1];
			while (tenacity > 0)
			{
				foreach (Transform strike in my.targets)
				{
					distance = Vector3.Distance(strike.position, GameInformer.check.transform.position);
					NPC_Main tar = strike.GetComponent<NPC_Main>();
					if (max_range >= distance)
					{
						int my_hit = Random.Range (0,100 + my.hit);
						int t_dodge = Random.Range (0,100 + tar.stats[1]);
						if (my_hit > t_dodge)
						{
							int dmg = my.damage + DoorManager.PhysicalDoor;
							if (dmg >= 1) tar.cur_hp -= dmg;
							Debug.Log(tar.name+" HP Remaining "+tar.cur_hp);
						}
					}
					tenacity--;
				}
			}
		}

		public void Eximo(PC_Main my, NPC_Main t)
		{
			t.cur_hp -= (my.damage + DoorManager.PhysicalDoor)*(2 + my.stats[1,3]/2);
			//int destroy_chance = Random.Range(0,100 + (my.wep[0].weight*10));
			HUD.info = "EXIMO casted! "+t.name+" remaining hp: "+t.cur_hp;
			//if (destroy_chance <= 50) my.wep[0] = null;
		}

		public void Feint(PC_Main my, NPC_Main t)
		{
			if (my.far_beats == false)
			{
				my.cur_beats--;
				Vector3 jump = new Vector3(my.transform.position.x,my.transform.position.y,my.transform.position.z + 2f);
				my.agent.SetDestination(jump);
				my.far_beats = true;
			}
		}

		public void Fidelity()
		{

		}

		public void Hush(PC_Main my, NPC_Main t)
		{
			
		}

		public void Inquisitio(PC_Main my, NPC_Main t)
		{
			float distance = Vector3.Distance(t.transform.position, my.transform.position);
			if (my.cur_hp > 1) my.cur_hp--;
			if (max_range >= distance)
			{
				dmg = my.stats[0,2] * (2 + DoorManager.MagicalDoor);
				if (dmg > 0) t.cur_hp -= dmg;
			}
		}

		public void Jinx(PC_Main my, NPC_Main t)
		{
			foreach (Transform strike in my.targets)
			{
				float distance = Vector3.Distance(strike.position, GameInformer.check.transform.position);
					if (max_range >= distance)
					{
						int spellcheck = my.stats[0,3]*10;
						int resist = (t.stats[3] + t.stats[2]) *5;
						if (spellcheck > resist)
						{
							t.tier_count = 0;
							t.tier = 1;
						}
					}
			}
		}

		public void Kadabra(PC_Main my, NPC_Main t)
		{
			NPC_Main[] search = GameObject.FindObjectsOfType(typeof(NPC_Main)) as NPC_Main[];
			foreach (NPC_Main e in search) 
			{
				e.kadabra = true;
				e.move_points = 0;
			}
			GameInformer.battle = false;
			GameInformer.stop = false;
		}

		public void Karma(PC_Main my, NPC_Main t)
		{
			Attack(my,t,t.Defense);
		}

		public void Libro(PC_Main my, NPC_Main t)
		{
			
		}

		public void Luck(PC_Main my, NPC_Main t)
		{
			dmg = (my.damage + DoorManager.PhysicalDoor + (my.stats[0,3]/5))*my.tier-1;
			if (dmg > 0) 
			{
				t.cur_hp -= (dmg * (int) Random.Range(1,1.125f));
				t.tier_count -= deTier(my);
			}
		}

		public void Oblivio(PC_Main my, NPC_Main t)
		{

		}

		public void Omni(PC_Main my, PC_Main t, bool apply)
		{
			my.omni = true;
			if (apply)
			{
				DoorManager.PhysicalDoor *= 2;
				DoorManager.MagicalDoor *= 2;
				DoorManager.DivineDoor *= 2;
				DoorManager.EarthDoor *= 2;
				DoorManager.FireDoor *= 2;
				DoorManager.NatureDoor *= 2;
				DoorManager.ThunderDoor *= 2;
				DoorManager.WindDoor *= 2;
				DoorManager.WaterDoor *= 2;
				DoorManager.WickedDoor *= 2;
			} else {
				DoorManager.PhysicalDoor /= 2;
				DoorManager.MagicalDoor /= 2;
				DoorManager.DivineDoor /= 2;
				DoorManager.EarthDoor /= 2;
				DoorManager.FireDoor /= 2;
				DoorManager.NatureDoor /= 2;
				DoorManager.ThunderDoor /= 2;
				DoorManager.WindDoor /= 2;
				DoorManager.WaterDoor /= 2;
				DoorManager.WickedDoor /= 2;
			}
		}

		public void Panacea(PC_Main t)
		{
			if (t.cur_beats <= t.Beat || t.cur_beats <= t.Beat+1 ) t.cur_beats++;
			HUD.info = t.name+" Beats equals "+t.cur_beats+" now!";
		}

		public void Provoke(PC_Main my, NPC_Main t)
		{
			int chance = Random.Range(0,(100 + my.stats[0,3]));
			int resist = Random.Range(0,(100 + t.stats[2]));
			if (chance > resist) t.target = my.transform;
		}

		public void Pulse(PC_Main my, NPC_Main t)
		{
			t.cur_hp -= my.stats[0,1] + (DoorManager.MagicalDoor);
		}

		public void Rapture(PC_Main my, NPC_Main t)
		{
			foreach (Transform strike in my.targets)
			{
				float distance = Vector3.Distance(strike.position, GameInformer.check.transform.position);
				NPC_Main tar = strike.GetComponent<NPC_Main>();
				if (max_range >= distance)
				{
					int my_hit = Random.Range (0,100 + my.hit);
					int t_dodge = Random.Range (0,100 + tar.stats[1]);
					if (my_hit > t_dodge)
					{
						int dmg = my.damage + DoorManager.PhysicalDoor;
						if (dmg > 0) tar.cur_hp -= dmg;
						Debug.Log(tar.name+" HP Remaining "+tar.cur_hp);
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
				my.AimCamera.GetComponent<MouseLook>().enabled = true;
			}else{
				my.AimCamera.enabled = false;
				my.AimCamera.GetComponent<Shoot>().enabled = false;
				my.AimCamera.GetComponent<MouseLook>().enabled = false;
			}
		}
		
		public void Verto(PC_Main my, NPC_Main t)
		{
			foreach (Transform strike in my.targets)
			{
				float distance = Vector3.Distance(strike.position, my.transform.position);
				if (max_range >= distance)
				{
					dmg = my.stats[0,3] + DoorManager.PhysicalDoor + DoorManager.MagicalDoor;
					if (dmg > 0) t.cur_hp -= dmg;
				}
			}
		}

		public void Day(PC_Main t)
		{
			if (t.cur_beats <= t.Beat || t.cur_beats <= t.Beat+1 ) t.cur_beats++;
		}

		public void Divine(PC_Main my, NPC_Main t)
		{
			int amount = 0;
			foreach (Transform touched in my.targets)
			{
				float distance = Vector3.Distance(touched.position, my.transform.position);
				if (max_range >= distance) amount++;
			}
			foreach (Transform player in my.targets)
			{
				float distance = Vector3.Distance(my.transform.position, player.position);
				if (max_range >= distance)
				{
					PC_Main tar = player.GetComponent<PC_Main>();
					int heal = (my.stats[0,3] + DoorManager.DivineDoor)/amount;
					if (heal + tar.cur_hp <= tar.HP) tar.cur_hp += heal;
					else tar.cur_hp = tar.HP;
				}
			}
		}

		public void Earth(PC_Main my, NPC_Main t)
		{
			int staggerchance = Random.Range(0,101 + my.stats[0,1]);
			if (staggerchance > 50) t.cur_beats = 0;
			dmg = my.stats[0,2] + DoorManager.EarthDoor;
			if (dmg > 0) t.cur_hp -= dmg;
		}

		public void Fire(PC_Main my, NPC_Main t)
		{
			dmg = (my.stats[0,2] + my.stats[0,2]/2) + DoorManager.FireDoor;
			if (dmg > 0) t.cur_hp -= dmg;
		}

		public void Nature(PC_Main my, NPC_Main t)
		{
			foreach (Transform target in my.targets)
			{
				float distance = Vector3.Distance(my.transform.position, target.position);
				if (max_range >= distance)
				{
					NPC_Main tar = target.GetComponent<NPC_Main>();
					int trapchance = Random.Range(0,101 + DoorManager.NatureDoor);
					if (trapchance > 50) t.move_points = 0;
					dmg = my.stats[0,2] + DoorManager.NatureDoor;
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
			dmg = my.stats[0,2]/2 + DoorManager.ThunderDoor;
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
						int chance = Random.Range (1,(101 + DoorManager.ThunderDoor));
						if (chance > 65) if (dmg > 0) t.cur_hp -= dmg;
						else amount --;
					}
				}
			}
		}

		public void Water(PC_Main my, NPC_Main t)
		{
			dmg = my.stats[0,2] + DoorManager.WaterDoor;
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
			dmg = my.stats[0,2] + DoorManager.WickedDoor;
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
				NPC_Main tar = enemy.GetComponent<NPC_Main>();
				float distance = Vector3.Distance(my.transform.position, enemy.position);
				if (max_range >= distance)
				{
					dmg = 5 + DoorManager.WaterDoor;
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
			
		}
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
		public string name,a_name;
		public int type;
		public int heal;
		public int amount;
		public bool equipped;
	
	public void CastItem(int j, Item i, PC_Main my, PC_Main t)
	{
		if ( i.type == 0)
		{
			if (i.heal + t.cur_hp <= t.HP) t.cur_hp += i.heal;
			else t.cur_hp = t.HP;
			i.amount--;
			for (int k = 0; k < my.items.Length;k++)
				if ( my.items[k] != null)
					if ( my.items[k].amount == 0) my.items[k] = null;
			Debug.Log(t.name+ " heals "+i.heal+" HP!");
			
		}else if (i.type == 1)
		{
			for (int k = 0; k < t.items.Length;k++)
			{
				if (my.target != my.transform && t.items[k] == null)
				{
					t.items[k] = i;
					my.items[j] = null;
					break;
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
	#region Weapon
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
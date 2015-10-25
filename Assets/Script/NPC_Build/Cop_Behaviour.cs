using UnityEngine;
using System.Collections;
using Database;

public class Cop_Behaviour : MonoBehaviour {

	NPC_Main my;
	public int Gun_Timer, Max_Heal_Items;
	public bool cover;
	public int state, t, a, gun;
	//State: 0 == null, 1 == Move, 2 == Cover, 3 == Attacking, 4 == Shooting 5 == Heal;
	bool CheckForHealItem()
	{
		bool t = false;
		for (int i = 0; i< my.items.Length;i++)
			if (my.items[i] != null && my.items[i].type == 0) t = true;
		return t;
	}
	bool CloseRangeHit(NPC_Main my, PC_Main t)
	{
		int my_hit = Random.Range (0,100);
		int t_dodge = Random.Range (0,100 + t.stats[0,1]);
		return my_hit > t_dodge;
	}
	bool FarRangeHit(float d,float max_range, NPC_Main my, PC_Main t)
	{
		int my_hit = Random.Range (0,100 + my.stats[2]);
		int t_dodge = Random.Range (0,100 + t.stats[0,1]);
		if (d < max_range) return my_hit > t_dodge;
		else return false;
	}

	void Start()
	{
		my = GetComponent<NPC_Main>();
		gun = Random.Range (0,Max_Heal_Items);
		for (int i = 0; i < gun;i++)
		{
		my.items[i] = new Item();
		my.items[i].name = "Tar Water";
		my.items[i].amount = Random.Range(0,2);
		my.items[i].type = 0;
		}
		my.items[3] = new Item();
		my.items[3].name = "Pistol";
		my.items[3].type = 1;
	}

	void OnTriggerStay(Collider col)
	{
		if (state == 3 && my.target == col.transform)
		{
			PC_Main p = col.GetComponent<PC_Main>();
			if (p.Combat_Turn == false && my.cur_beats > 0) 
			{
				print ("Continuing to Attack");
				Attack();
			}
		}
	}
	void Update()
	{
		if (cover == true && GameInformer.stop == true) t++;
		if (t >= Gun_Timer)
		{
			UseGun();
			t = 0;
		}
		if (my.target != null) 
			if (state == 1 || state == 2)
		{
			my.agent.SetDestination(my.target.position);
			float distance = Vector3.Distance(my.target.position, transform.position);
			if (distance < 3f && state == 1) Attack();
			else if (distance < 3f) 
			{
				cover = true;
				state = 0;
			}
		}
	}
	void FixedUpdate()
	{
		if (my.move_points > 0)
		{
			if (state == 0)
			{
				my.target = null;
				my.TargetType(1);
				float distance = Vector3.Distance(my.target.position, transform.position);
				if (distance >= 3.5f) 
				{
					my.target = null;
					if (my.cur_hp <= my.HP/2 && CheckForHealItem() == true) Heal();
					else if (cover == false && my.target == null) Cover();
					else UseGun();
				}
				else Move();
			} else if (state == 4)
			{
				my.target = null;
				my.TargetType(1);
				float distance = Vector3.Distance(my.target.position, transform.position);
				if (distance < 3.5f) 
				{
					state = 0;
				}
			}
		}
	}
	void Move()
	{
		cover = false;
		state = 1;
	}
	void UseGun()
	{
		my.target = null;
		state = 4;
		my.TargetType(1);
		print ("USING GUN");
		PC_Main tar = my.target.GetComponent<PC_Main>();
		float distance = Vector3.Distance(my.target.position, transform.position);
		if (FarRangeHit(distance, 20, my,tar) == true && my.items[3] != null)
		{
			tar.cur_hp--;
			print ("Shot HIM");
		}
		my.cur_beats = 0;
		my.move_points--;
	}
	void Attack()
	{
		state = 3;
		print ("ATTACKING");
		if (GameInformer.stop == false) GameInformer.stop = true;

			PC_Main tar = my.target.GetComponent<PC_Main>();
		tar.Combat_Turn = true;
		tar.target = this.transform;
		if (my.cur_beats > 0 && CloseRangeHit(my,tar) == true)
		{
			tar.cur_hp--;
		}
		my.cur_beats--;
		if (my.cur_beats == 0)
		{
			my.move_points--;
			state = 0;
			print ("TURN END");
		}
	}
	void Cover()
	{
		/*my.TargetType(0);
		Cover c = my.target.GetComponent<Cover>();
		if (c.selected == false) 
		{
			c.selected = true;
			c.taken = true;
		}
		state = 2;
		my.move_points--;
		print ("GOING FOR COVER");*/

	}
	void Heal()
	{
		state = 5;
		for (int i = 0;i < my.items.Length;i++)
			if (my.items[i] != null && my.items[i].type == 0)
		{
			my.items[i].amount--;
			int heal = 2;
			if (heal + my.cur_hp <= my.HP) my.cur_hp += heal;
			else my.cur_hp = my.HP;
			if (my.items[i].amount == 0) my.items[i] = null;
		}
		print ("USING HEAL");
		my.move_points--;
		state = 0;
	}
}

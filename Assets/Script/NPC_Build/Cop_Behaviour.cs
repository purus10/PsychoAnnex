using UnityEngine;
using System.Collections;
using Database;

public class Cop_Behaviour : MonoBehaviour {

	NPC_Main my;
	Vector3 destination;
	bool chase;
	PC_Main target;

	void Start()
	{
		my = GetComponent<NPC_Main>();
		for (int i = 0; i < my.items.Length;i++)
		{
		my.items[i] = new Item();
		my.items[i].name = "Tar Water";
		my.items[i].amount = Random.Range(0,2);
		my.items[i].type = 1;
		}
	}

	void Update()
	{
		if (my.target != null) 
			if (chase == true) my.agent.SetDestination(my.target.position);
	}

	void FixedUpdate()
	{

		if (my.move_points > 0 && my.myturn == true)
		{
			if (my.cur_hp <= my.HP/2) UseItem(my.items[0]);

			my.TargetType(1);
			my.Target();
			float distance = Vector3.Distance(my.target.position, transform.position);
			if (distance > 1.5f && my.agent.remainingDistance == 0) Move();
			else if (distance < 1.5f) InitiateCloseRange();
		}

	}

	void Move()
	{
		chase = true;
		my.move_points--;
	}

	public void InitiateCloseRange()
	{
		if (GameInformer.stop == false) GameInformer.stop = true;
		my.move_points--;
		target = my.target.GetComponent<PC_Main>();
		target.target = transform;
		GameInformer.target = my.target;
		int b = my.cur_beats;
		if (my.cur_beats == b)
		{
			print ("ENEMY Attacks");
			target.cur_hp--;
			my.cur_beats--;
			my.myturn = false;
		}
		if (target.cur_beats > 0) target.myturn = true;
		else if (my.cur_beats > 0) my.myturn = true;
	}

	void UseItem(Item i)
	{
		if (i.amount > 0)
		{
			print ("HEAL");
			i.amount--;
			my.cur_hp += 2;
		}
	}


}

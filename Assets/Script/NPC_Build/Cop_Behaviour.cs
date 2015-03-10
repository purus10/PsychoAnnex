using UnityEngine;
using System.Collections;

public class Cop_Behaviour : MonoBehaviour {

	NPC_Main my;
	NavMeshAgent agent;
	Vector3 destination;
	bool chase;
	PC_Main target;

	void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		my = GetComponent<NPC_Main>();
		my.items[0] = new item();
		my.items[0].name = "Tar Water";
		my.items[0].amount = Random.Range(0,2);
		my.items[0].type = 1;
	}

	void OnTriggerEnter()
	{
		if (my.myturn == false && GameInformer.stop == false) my.myturn = true;
	}

	void Update()
	{
		if (my.target != null) 
			if (chase == true) agent.SetDestination(my.target.position);
	}

	void FixedUpdate()
	{
		if (my.cur_hp == 0) Destroy(gameObject);

		if (my.move_points > 0 && my.myturn == true)
		{
			if (my.cur_hp <= my.HP/2) UseItem(my.items[0]);

			my.TargetType(1);
			my.Target();
			float distance = Vector3.Distance(my.target.position, transform.position);
			if (distance > 1f && agent.remainingDistance == 0) Move();
			else if (distance < 1f) Attack();
		}

	}

	void Move()
	{
		chase = true;
		my.move_points--;
	}

	public void Attack()
	{
		if (GameInformer.stop == false) GameInformer.stop = true;
		my.move_points--;
		target = my.target.GetComponent<PC_Main>();
		target.target = transform;
		GameInformer.target = my.target;
		int b = my.cur_beats;
		if (my.cur_beats == b)
		{
			print ("ATTACK");
			target.cur_hp--;
			my.cur_beats--;
			my.myturn = false;
		}
		if (target.cur_beats > 0) target.myturn = true;
		else if (my.cur_beats > 0) my.myturn = true;
	}

	void UseItem(item i)
	{
		if (i.amount > 0)
		{
			print ("HEAL");
			i.amount--;
			my.cur_hp += 2;
		}
	}


}

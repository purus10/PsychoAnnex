using UnityEngine;
using System.Collections;

public class NPC_Movement : MonoBehaviour {
	
	public bool moving;
	public NPC_Target check;
	NavMeshAgent agent;

	// Update is called once per frame
	public void MoveStart (NPC_Target target) 
	{
		if (target.target != null)
		{
			StartCoroutine(Move(target.target));
			
		}
	}
	
	public IEnumerator Move (Transform target) {
		
		float distance = Vector3.Distance(target.position, transform.position);

		Faceless attack = GetComponent<Faceless>();
		check = GetComponent<NPC_Target>();
		agent = GetComponent<NavMeshAgent>();
		
		while (distance > 1.5f)
		{

			distance = Vector3.Distance(target.position, transform.position);

			if (check.target == transform)
			{
			print ("yeah");
			agent.ResetPath();
			} else {
			agent.SetDestination(target.position);
			}

			
			if (check.target != target)
			{
				print ("yeh");
				break;
			}

			if (distance <= 1.5f && check.type == "Damage")
			{
				attack.foundplayer = true;
			}
			moving = false;
			yield return null;
		}
		
	}
}

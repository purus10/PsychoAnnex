using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPC_Target : MonoBehaviour {
	
	public string type = "";
	public List <Transform> targets;
	public Transform[] search;
	public Transform target;
	int index = 0;
	
	public void Start()
	{
		search = GameObject.FindObjectsOfType(typeof(Transform)) as Transform[];
	}
	
	public void choice()
	{
		target = null;
		targets.Clear();
		if (type == "Buff")
		{
			AllyTarget();
			AimTarget();
		} else if (type == "Damage")
		{
			EnemyTarget();
			AimTarget();
		} else if (type == "Cover"){
			CoverTarget();
			AimTarget();
		} else if (type == "Special"){
			EnemyTarget();
		} else {
			target = transform;
		}
	}
	
	public void AimTarget()
	{
		if (target == null)
		{
			sort ();
			target = targets[0];
		} else {
			index++;
			if (index == targets.Count)
			{
				index = 0;
			}
			target = targets[index];
		}
		if (target.name != name)
		{
			transform.LookAt(target);
		}
	}
	
	void sort()
	{
		targets.Sort(delegate(Transform t1, Transform t2) { 
			return (Vector3.Distance(t1.position, transform.position)).CompareTo(Vector3.Distance(t2.position,transform.position)); 
		});
	}
	
	#region Ally Target
	void AllyTarget()
	{
	/*	foreach (Transform tar in search)
		{
			NPC_Stat ally = tar.GetComponent<NPC_Stat>(); 
			if (ally != null)
			{
				if (ally.transform != transform)
				{
				targets.Add(tar);
				}
			}
		}*/
	}
	#endregion\
#region Enemy Target
void EnemyTarget()
	{
		foreach (Transform tar in search)
		{
		/*	PC_Stat enemy = tar.GetComponent<PC_Stat>(); 
			if (enemy != null)
			{
			targets.Add(tar);
			}*/
		}
	}
	#endregion
	#region Cover Target
	void CoverTarget()
	{
	/*	foreach (Transform tar in search)
		{
			NPC_Stat enemy = tar.GetComponent<NPC_Stat>(); 
			if (enemy != null)
			{
			targets.Add(tar);
			}

		}*/
	}
	#endregion
	
}

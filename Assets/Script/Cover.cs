using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cover : MonoBehaviour {

	public bool coverselect;

	public GameObject player;

	public List<Transform> covchilds;
	public Transform covchild;

	void Start()
	{
		covchild = null;
		player = null;
	}

	public void AddAllcov()
	{
		GameObject[] co = GameObject.FindGameObjectsWithTag("Trigger");
		
		
		foreach (GameObject covers in co)
		{

			AddTarget(covers.transform);
		}
	}
	
	public void AddTarget(Transform covers)
	{
		covchilds.Add(covers);
	}
	
	private void Aimcov()
	{
		if (covchild == null)
		{
			SortcovByDistance();
			covchild = covchilds[0];
		} else {
			int index = covchilds.IndexOf(covchild);
			
			if (index < covchilds.Count -1)
			{
				index++;
			} else {
				index = 0;
			}
			covchild = covchilds[index];
		}
		onthecov();
	}
	
	private void onthecov()
	{
		player.transform.LookAt (covchild);

		foreach(Transform covs in covchilds)
		{
			covs.GetComponent<CoverTrigger>().cov = covchild;
		}
	}
	
	private void SortcovByDistance()
	{
		covchilds.Sort (delegate(Transform t1, Transform t2) { 
			return (Vector3.Distance(t1.position, player.transform.position)).CompareTo(Vector3.Distance(t2.position,player.transform.position)); 
		});
	}

	public float TriggerDistance(Transform x)
	{
		return Vector3.Distance (x.position, covchild.transform.position);
	}

	void FixedUpdate ()
	{

		if (Input.GetKeyDown (KeyCode.E) && covchild != null)
		{
			coverselect = false;
		//	player.GetComponent<Character>().move = true;
		//	player.GetComponent<Character>().select = false;
		}

		if (Input.GetKeyDown (KeyCode.Space))
		{
			player = null;
			covchild = null;
			coverselect = false;
			covchilds.Clear();
		}

		if (Input.GetKeyDown (KeyCode.Tab) && coverselect == true)
		{
			if (covchilds != null)
			{
				covchilds = new List<Transform>();
				AddAllcov();
			}
			Aimcov ();

		}
	}

	void Update()
	{

		if (covchild != null)
		{
	//	player.GetComponent<Character>().distance = TriggerDistance(player.GetComponent<Character>().myplace);
		} 
	}
}

using UnityEngine;
using System.Collections;

public class CoverTrigger : MonoBehaviour {

	public bool cover;
	public Transform cov;

	void OnTriggerEnter(Collider col)
	{
		if (cover == false && cov == transform)
		{
			cover = true;
			col.transform.position = transform.position;
			col.transform.rotation = transform.rotation;
		}
	}
	
	void OnTriggerExit(Collider col)
	{
		if (Vector3.Distance(col.transform.position, transform.position) < 2f)
		{
	//		col.GetComponent<Character>().cover = false;
			cover = false;
			tag = "Trigger";
		}
	}	

	void FixedUpdate()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
		cov = null;
		}
	}

	void Update()
	{
		if (cover == true && cov == transform)
		{
			tag = "Occupied";
		}
	}
}

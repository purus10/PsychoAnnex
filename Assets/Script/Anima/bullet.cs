using UnityEngine;
using System.Collections;

public class bullet : MonoBehaviour {

	public void FaceTarget(Vector3 target)
	{
		transform.LookAt(target);
	}
	public void Shoot()
	{
		GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0,0,5000));
	}


}

using UnityEngine;
using System.Collections;

public class PC_Movement : MonoBehaviour {
//Any movement will be added here	

	public AudioClip[] hit;
	public AudioClip strike;
	public bool moving;
	NavMeshAgent agent;
	

	public void MoveStart(Transform target, float wait)
	{
		if (moving == false)
		{
		StartCoroutine(Move(target,wait));
		moving = true;
		}
	}

	public IEnumerator Move (Transform target, float speed) {

		float distance = Vector3.Distance(target.position, transform.position);
		agent = GetComponent<NavMeshAgent>();

		while (distance > 1.5f)
		{
			distance = Vector3.Distance(target.position, transform.position);
			agent.SetDestination(target.position);
			moving = false;
			yield return null;
			}
		agent.ResetPath();
		yield return null;

	}
		

	
	public void ButterflyStart(GameObject butterfly, Transform target, float speed)
	{
		StartCoroutine(Butterfly(butterfly,target,speed));
	}

	public IEnumerator Butterfly (GameObject butterfly , Transform target, float speed) {

		float journey = Vector3.Distance(butterfly.transform.position,target.position);
		butterfly.GetComponent<Collider>().enabled = false;


		while ( journey > 1.5f)
		{
		butterfly.transform.LookAt(target);
		butterfly.transform.Translate(transform.forward * speed * Time.deltaTime ,Space.Self);
		journey = Vector3.Distance(butterfly.transform.position,target.position);
		Debug.Log(journey);
		yield return null;
		}

		if (journey <= 1.5f)
		{
			GameObject.DestroyImmediate(butterfly);
			GameObject.Destroy(target.gameObject);
		}


	}

	public void ChaosStart(Transform[] target, float speed)
	{
		if (moving == false)
		{
		StartCoroutine(Chaos(target,speed));
		moving = true;
		}
	}


	public IEnumerator Chaos (Transform[] target, float wait) {

		Vector3 revert = transform.GetComponent<PC_Main>().transform.position;
		int amount = -1;

		while (amount + 1 < target.Length)
		{
		amount ++;
		GetComponent<Collider>().enabled = false;
		agent.enabled = false;
		transform.position = target[amount].position;
		if (amount == 0)
		{
		GetComponent<AudioSource>().PlayOneShot(hit[Random.Range(0,hit.Length)]);
		}
		GetComponent<AudioSource>().PlayOneShot(strike);
		yield return new WaitForSeconds(wait);
		transform.position = revert;
		transform.LookAt(target[amount].position);
		}
		moving = false;
		GetComponent<Collider>().enabled = true;
		agent.enabled = true;





	}

	public void FeintStart(Transform target, float speed)
	{
		StartCoroutine(Feint(target,speed));
	}

	public IEnumerator Feint (Transform target, float speed) {
		float journey = Vector3.Distance(transform.position,target.position);
		while (journey <= 3f)
		{
		transform.Translate (-transform.forward * speed * Time.deltaTime);
		journey = Vector3.Distance(transform.position,target.position);
			print (journey);
		yield return null;
		}
	}
}

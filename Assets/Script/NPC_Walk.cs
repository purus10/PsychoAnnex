using UnityEngine;
using System.Collections;

public class NPC_Walk : MonoBehaviour {

	public Animator animator;
	static int played = -1;
	public NavMeshAgent agent;
	public Transform[] Places;
	public AudioClip[] Barks;
	bool BarkIsDone;
	int t;

	void OnTriggerEnter(Collider col)
	{
		PC_Main p = col.GetComponent<PC_Main>();

		if (p != null && p.name == "Zen" && BarkIsDone == false)
		{
			int bark = Random.Range(0,Barks.Length-1);
			if (bark == played) bark++;
			if (bark > Barks.Length-1) bark = 0;
			GetComponent<AudioSource>().PlayOneShot(Barks[bark]);
			played = bark;
		}

	}

	void Awake()
	{
		agent.SetDestination(Places[Random.Range(0,Places.Length-1)].position);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (GameInformer.stop == false && agent.remainingDistance != 0)
		{
			animator.SetFloat("Speed", 5);
			
		}
	}
}

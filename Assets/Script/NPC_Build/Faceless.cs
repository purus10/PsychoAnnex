using UnityEngine;
using System.Collections;

public class Faceless : MonoBehaviour {

	public NPC_Target target;
	public float movepause;
	private float reset;
	public string[] targets;
	public string choice;
	public bool foundplayer;

	// Use this for initialization
	void Start () 
	{
		reset = movepause;
		target = GetComponent<NPC_Target>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (foundplayer == true)
		{
			print("Attack!");
		}
		if (movepause <= Time.time)
		{
			if (foundplayer == false)
			{
			target.type = targets[Random.Range(0, targets.Length)];
			target.choice();
			NPC_Movement start = GetComponent<NPC_Movement>();
			start.MoveStart(target);
			movepause += reset;
			}
		}
	}
}

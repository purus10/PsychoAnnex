using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cover : MonoBehaviour {

	NPC_Main NPC;
	PC_Main PC;
	public Color target_off;
	public bool taken = false, selected = false;

	void Start()
	{
		target_off = GetComponentInParent<Renderer>().material.color;
	}

	void Update()
	{
		if (Input.GetKeyDown(GameInformer.Fight) && GameInformer.battle == false)
			selected = false;
	}
	
	void OnTriggerEnter(Collider col)
	{
		NPC = col.GetComponent<NPC_Main>();
		PC = col.GetComponent<PC_Main>();
		if (NPC != null) 
		{
			if (NPC.target == transform) 
			{
				taken = true;
				NPC.cover = true;
			}
		}
		if (PC != null) 		
		{
			if (PC.target == transform) 
			{
				taken = true;
				PC.cover = true;
			}
		}
	}
	
	void OnTriggerExit(Collider col)
	{
		taken = false;
		if (NPC != null) NPC.cover = false;
		if (PC != null) PC.cover = false;
	}
}

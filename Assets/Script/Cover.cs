using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cover : MonoBehaviour {

	NPC_Base NPC;
	PC_Main PC;
	PC_Base Pc;
	public GameObject render;
	public Color target_off;
	public enum Spot {Empty,Selected,Taken}
	public Spot Status;

	void Start()
	{
		target_off = render.GetComponent<Renderer>().material.color;
	}
	
	void OnTriggerEnter(Collider col)
	{
		print ("yes");
		Status = Spot.Taken;
		NPC = col.GetComponent<NPC_Base>();
		Pc = col.GetComponent<PC_Base>();
		if (Pc != null && Status != Spot.Taken)
			Pc.State = Database.Get.State.Cover;
		else if (NPC != null && Status != Spot.Taken)
			NPC.State = Database.Get.State.Cover;

	}
	void OnTriggerStay(Collider col)
	{

		//if (Pc.State == Database.Get.State.Idle || Pc.State == Database.Get.State.Move) Pc.State = Database.Get.State.Cover;
	} 
	
	void OnTriggerExit(Collider col)
	{
		Status = Spot.Empty;
	}
}

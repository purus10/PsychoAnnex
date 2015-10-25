using UnityEngine;
using System.Collections;
using Database;

public class LightPost : MonoBehaviour {
	
	void OnTriggerStay(Collider col) 
	{
		if (Get.PhaseofDay == Get.Hour.Evening) 
		{
			GetComponent<Light>().enabled = true;
			PC_Base player = col.GetComponent<PC_Base>();
			if (player != null) GetComponent<Light>().shadows = LightShadows.Hard;
		}
	}

	void OnTriggerExit()
	{
		GetComponent<Light>().enabled = false;
	}
}

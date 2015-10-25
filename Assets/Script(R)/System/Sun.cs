using UnityEngine;
using System.Collections;
using Database;

public class Sun : MonoBehaviour {

	static Light SunObject;
	public float RotationSpeed, DayLength, MidnightLength, FogDensity;

	void Awake()
	{
		RenderSettings.fogDensity = FogDensity;
		SunObject = GetComponent<Light>();
	}

	void Update()
	{
		print (Get.PhaseofDay);
		if (SunObject.transform.rotation.x > 0.01 && SunObject.transform.rotation.x < 0.02 || 
		    SunObject.transform.rotation.x < -0.01 && SunObject.transform.rotation.x > -0.02)
		{
			Get.PhaseofDay = Get.Hour.Morning;
			RenderSettings.fogDensity = Mathf.Lerp(RenderSettings.fogDensity,FogDensity,RotationSpeed);
			print ("MORNING");
			SunObject.enabled = true;
		} else if (SunObject.transform.rotation.x > 0.3 && SunObject.transform.rotation.x < 0.31 && Get.PhaseofDay == Get.Hour.Morning|| 
		           SunObject.transform.rotation.x < -0.3 && SunObject.transform.rotation.x > -0.31 && Get.PhaseofDay == Get.Hour.Morning)
		{
			Get.PhaseofDay = Get.Hour.AfterNoon;

			print ("Afternoon");
		} else if (SunObject.transform.rotation.x > 0.9998 && SunObject.transform.rotation.x < 0.9999 || 
		           SunObject.transform.rotation.x < -0.9998 && SunObject.transform.rotation.x > -0.9999)
		{
			Get.PhaseofDay = Get.Hour.Evening;
			print ("Evening");
			RenderSettings.fogDensity = Mathf.Lerp(RenderSettings.fogDensity,0f,RotationSpeed);
			SunObject.intensity = Mathf.Lerp(SunObject.intensity,0f,RotationSpeed);
		}
		else if (SunObject.transform.rotation.x > 0.3 && SunObject.transform.rotation.x < 0.31 && Get.PhaseofDay == Get.Hour.Evening|| 
		         SunObject.transform.rotation.x < -0.3 && SunObject.transform.rotation.x > -0.31 && Get.PhaseofDay == Get.Hour.Evening)
		{
			if (Get.Midnight == true)
			{
			Get.PhaseofDay = Get.Hour.Midnight;
			print ("MIDNIGHT");
			}
		}
		
		/*	if (SunObject.transform.rotation.x < 0.5 || SunObject.transform.rotation.x > -0.5) 
		{

		} else*/

		if (Get.PhaseofDay != Get.Hour.Midnight) 
		{
			RotationSpeed = Time.deltaTime/DayLength;
		} else RotationSpeed = Time.deltaTime/MidnightLength;
		SunObject.transform.Rotate(RotationSpeed,0,0);
	}
}

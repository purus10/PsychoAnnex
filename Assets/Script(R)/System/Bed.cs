using UnityEngine;
using System.Collections;
using Database;

public class Bed : MonoBehaviour {
	
	public int SaveMenu;
	public Light SunObject;
	public Rect MenuBox, Save, Sleep, Morning, Evening;

	void OnTriggerEnter(Collider col)
	{
		PC_Base player = col.GetComponent<PC_Base>();
		if (player != null) SaveMenu = 1;
	}

	void OnGUI()
	{
		if (SaveMenu > 0) GUI.Box(MenuBox,"");
		if (SaveMenu == 1)
		{
			if (GUI.Button(Save,"Save")) SaveGame();
			if (GUI.Button(Sleep,"Rest")) SaveMenu = 2;
		}
		if (SaveMenu == 2) 
		{
			if (GUI.Button(Morning,"Til Morning")) Dream(true);
			if (GUI.Button(Evening,"Til Evening")) Dream(false);
		}
	}

	void SaveGame()
	{
		print ("Save");
		SaveMenu = 0;
	}

	void Dream(bool Morning)
	{
		if (Morning == true) 
		{
			Get.PhaseofDay = Get.Hour.Morning;
			SunObject.transform.rotation = new Quaternion (0.009f,0,0,0.01f);
		} else {
			SunObject.transform.rotation = new Quaternion (0.9998f,0,0,0f);
		}
		//Application.LoadLevel("Anima System");
		SaveMenu = 0;
	}
}

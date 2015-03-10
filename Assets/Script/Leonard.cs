using UnityEngine;
using System.Collections;

public class Leonard : MonoBehaviour {
	
	//when choice: 1 = dialog 2 = choice
	public Rect dialog_box, selection_box, acc_button, wep_button, quit_button;
	public string comment;
	public AudioClip greet, walkaway, bye;
	static int choice = 0;
	public GameObject menu;
	
	void LoadLevel(string i, int t)
	{
		Selected_Button.selected = i;
		Selected_Button.type = t;
		Menu m = menu.GetComponent<Menu>();
		foreach(GameObject c in m.chara)
		{
			c.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
			c.GetComponent<CharacterController>().enabled = false;
			GameInformer.stop = true;
		}
		choice = 3;
		Application.LoadLevel("Customization");
	}

	void PlaySound(AudioClip a, float vol)
	{
		GetComponent<AudioSource>().clip = a;
		GetComponent<AudioSource>().volume = vol;
		GetComponent<AudioSource>().Play();
	}

	void OnTriggerEnter (Collider col) 
	{
		if (choice == 0)
			if (GetComponent<AudioSource>().isPlaying == false)
			{
			PlaySound(greet,1);
			choice = 1;
			}
	}

	void OnTriggerStay()
	{
		if (choice == 0)
		if (Input.GetKeyDown(GameInformer.Select)) choice = 1;
	}
	
	void OnTriggerExit()
	{
		if (choice != 0)
		{
			PlaySound(walkaway,1);
			choice = 0;
		}
	}

	void OnGUI() 
	{
		if (choice == 1)
		{
			GUI.Box(dialog_box,comment);
			if (Input.GetKeyDown(GameInformer.Select)) choice = 2;
		}
		
		if (choice == 2)
		{
			GUI.Box(selection_box,"What will it be?");
			
			if (GUI.Button(acc_button,"an Accessory")) LoadLevel("Accessory",2);
			
			if (GUI.Button(wep_button,"a Weapon")) LoadLevel("Weapon",1);
			
			if (GUI.Button(quit_button,"Done for now"))
			{
				PlaySound(bye,1);
				choice = 0;
			}
		}
		
	}
}

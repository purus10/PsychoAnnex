using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Menu : MonoBehaviour {
	
	public Rect box, collum, character, item_button, anima_button, equip_button, goddess_gift, options, money, time, essence, info, stat;
	public List <GameObject> chara = new List<GameObject>();
	
	public bool open;
	public string desc;

	void Start()
	{
		chara.Add(GameObject.Find("Zen"));
		chara.Add(GameObject.Find("Serenity"));
		chara.Add(GameObject.Find("Sky"));

		foreach(GameObject c in chara)
		{
			c.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
			c.GetComponent<CharacterController>().enabled = true;
			GameInformer.stop = false;
		}
	}
	
	void Update()
	{
		if (Input.GetKeyDown(GameInformer.Deselect))
		{
			if(open) open = false;
			else open = true;
		}
	}

	void LoadLevel(string n)
	{
		foreach(GameObject c in chara)
		{
			c.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
			c.GetComponent<CharacterController>().enabled = false;
			GameInformer.stop = true;
		}
		Application.LoadLevel(n);
	}
	
	void OnGUI () 
	{
		if (open == true && GameInformer.stop != true)
		{
			GUI.Box(box,"");
			GUI.Box(collum,"");
			GUI.Box(info,"");
			GUI.Box (stat,"");
			GUI.Label(money, GameInformer.money+" ¢");
			GUI.Label(time, Time.time.ToString());
			GUI.Label(essence, "C: "+GameInformer.Cruelty + " E: "+GameInformer.Empathy + " T: "+GameInformer.Tranquality+ " L: "+GameInformer.Luck); 
			
			if (GUI.Button(item_button, "Items")) print ("item");
			
			if (GUI.Button(anima_button, "Anima")) LoadLevel("Anima System");
			
			if (GUI.Button(equip_button, "Equip")) LoadLevel("Equip");
			
			if (GUI.Button(goddess_gift, "Goddess Gifts")) print ("Goddess Gift");
			
			if (GUI.Button(options, "Options")) print ("Options");
		}
	}
}

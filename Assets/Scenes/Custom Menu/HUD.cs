using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {

	public static string info;
	PC_Main character;
	public Rect[] abilities, items, stat, helpfuls;
	public Rect box,info_box;
	public string[] stats;
	GUIStyle[] high_ability = new GUIStyle[4];
	GUIStyle[] high_item = new GUIStyle[4];
	
	
	// Use this for initialization
	void Start () {
		character = GetComponent<PC_Main>();
		
		for(int i =0;i<high_ability.Length;i++)
		{
			high_ability[i] = new GUIStyle();
			high_item[i] = new GUIStyle();
			high_ability[i].normal.textColor = Color.white;
			high_item[i].normal.textColor = Color.white;
		}
	}
	
	string A_name(int i)
	{
		if (character.ability[i] != null)
			return character.ability[i].name;
		else return "None";
	}
	
	string I_name(int i)
	{
		if (character.items[i] != null)
			return character.items[i].name;
		else return "None";
	}
	
	// Update is called once per frame
	void OnGUI () 
	{
		if (character.myturn)
		{
			GUI.Box(box,"");
			for(int i = 0;i< character.items.Length;i++)
			{
				GUI.Label (abilities[i], "A"+(i+1)+": "+A_name(i),high_ability[i]);
				GUI.Label(items[i], "S"+(i+1)+": "+I_name(i),high_item[i]);
				if (i != 3) GUI.Label(stat[i], stats[i]+": "+character.stats[0,i].ToString());
			}
			GUI.Label(stat[3], stats[3]+": "+character.cur_hp.ToString());
			GUI.Label(stat[4], stats[4]+": "+character.cur_beats.ToString());
			GUI.Label(helpfuls[0],"Undecover? "+character.cover.ToString());
			if (character.target == null) GUI.Label(helpfuls[1],"Current Target:");
			else GUI.Label(helpfuls[1],"Current Target: "+character.target.ToString());
			GUI.Label(helpfuls[2],"Current Tier: "+character.tier.ToString());
			GUI.Label(helpfuls[3],character.name);
			GUI.Label(info_box,info);
		}
	}
	
	void Update()
	{
		if (character.myturn)
		{
			for (int i=0;i<high_ability.Length;i++)
			{
				if (Input.GetKeyDown(GameInformer.A[i])) 
				{
					high_ability[i].normal.textColor = Color.yellow;
					for (int j=0;j<high_ability.Length;j++) if (j != i) high_ability[j].normal.textColor = Color.white;
					for (int j=0;j<high_item.Length;j++) if (j != i) high_item[j].normal.textColor = Color.white;
				}
				
				if (Input.GetKeyDown(GameInformer.A[i]) && Input.GetKey(GameInformer.ItemTog)) 
				{
					high_item[i].normal.textColor = Color.yellow;
					for (int j=0;j<high_item.Length;j++) if (j != i) high_item[j].normal.textColor = Color.white;
					for (int j=0;j<high_ability.Length;j++) if (j != i) high_ability[j].normal.textColor = Color.white;
				}
			}
		}
	}
}

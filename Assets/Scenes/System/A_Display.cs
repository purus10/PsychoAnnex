using UnityEngine;
using System.Collections;
using Database;

public class A_Display : MonoBehaviour {

	public Rect infobox,picbox,selbox,essencebox, costbox;
	public Rect title,cruelty,empathy,tranquilty,luck,info, pic, cost;
	public Rect yesbutton,nobutton;
	
	GUIStyle largefont,midfont;
	
	public Texture2D[] potrait;

	public int Id = 1;
	public GameObject player;

	public GameObject node;
	
	public PC_Main stat;
	
	public string price;
	public string selected;
	public string description;
	
	void Selected()
	{
		if (Id == 1)player = GameObject.Find("Zen");
		if (Id == 2) player = GameObject.Find("Serenity");
		if (Id == 3) player = GameObject.Find("Sky");
		if (Id == 4)player = GameObject.Find("Hena");
		if (Id == 5)player = GameObject.Find("Rose");
		if (Id == 6)player = GameObject.Find("Annihilator");
		if (Id == 7) player = GameObject.Find("Xeres");
	}

	void Awake()
	{
		stat = player.GetComponent<PC_Main>();
	}
	
	void Start () {
		
		largefont = new GUIStyle();
		largefont.fontSize = 20;
		largefont.normal.textColor = Color.white;
		
		midfont = new GUIStyle();
		midfont.fontSize = 15;
		midfont.normal.textColor = Color.white;
	}

	void Update()
	{
		if (Input.GetKeyDown(GameInformer.Deselect)) Application.LoadLevel(GameInformer.previous);
	}
	
	void FixedUpdate () 
	{
		Selected();
		stat = player.GetComponent<PC_Main>();
	}
	
	void Learn(A_Node n)
	{
		if (n.Node_State[Id-1] == 1)
		{
			if (n.Essence_cost[Id-1] <= GameInformer.Essence[n.Essence_type[Id-1]])
			{
				n.Node_State[Id-1] = 2;
				PC_Main a = player.GetComponent<PC_Main>();
				GameInformer.Essence[n.Essence_type[Id-1]] -= n.Essence_cost[Id-1];
				if (n.type[Id-1] > 0) a.abilities.Add(n.ability);
				else a.passives.Add(n.passive);
				n.UnlockPaths(Id);
				node = null;
			}
		}
	}
	
	void OnGUI()
	{
		
		GUI.Box(infobox,"");
		GUI.Box(costbox,selected);
		GUI.Box(picbox,player.name);
		GUI.Box(essencebox,"");

		GUI.Label(pic, potrait[stat.ID]);
		GUI.Label(info, description, largefont);
		GUI.Label(cost, price, midfont);
		
		GUI.Label(cruelty, "Cruelty: "+ GameInformer.Essence[0].ToString(),midfont);
		GUI.Label(empathy, "Emapthy: "+ GameInformer.Essence[1].ToString(),midfont);
		GUI.Label(tranquilty, "Tranquility: "+ GameInformer.Essence[2].ToString(), midfont);
		GUI.Label(luck, "Luck: "+ GameInformer.Essence[3].ToString(), midfont);
		
		
		if (node != null)
		{
			A_Node n = node.GetComponent<A_Node>();
			if (n != null)
			{
				if (n.Node_State[Id-1] == 1)
				{
					GUI.Box(selbox,"Learn " + selected +"?");
					if (GUI.Button(yesbutton, "Yes")) Learn(n);
					if (GUI.Button(nobutton, "No")) node = null;
				}
				else{
					GUI.Box(selbox,"Equip " + selected +"?");
					if (GUI.Button(yesbutton, "Yes"))print ("Equip");
					if (GUI.Button(nobutton, "No")) node = null;
				}
			} else {
				G_Node g = node.GetComponent<G_Node>();
				if (g != null)
				{
				if (g.Node_State[Id-1] == 1)
				{
					GUI.Box(selbox,"Use Essence ?");
					if (GUI.Button(yesbutton, "Yes")) g.SetNode(Id-1);
					if (GUI.Button(nobutton, "No")) node = null;

				}
				} else {
					C_Node c = node.GetComponent<C_Node>();
					if (c != null)
					{
						if (c.Node_State[Id-1] == 1)
						{
							GUI.Box(selbox,"Learn " + selected +"?");
							if (GUI.Button(yesbutton, "Yes")) c.SetNode(c.Type);
							if (GUI.Button(nobutton, "No")) node = null;
							
						}
					}
				}
			}
		}
		
	}
}

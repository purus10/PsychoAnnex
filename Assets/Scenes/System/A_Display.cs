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
	
	PC_Main stat;
	
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
		if (n.unlocked[Id-1] == false)
		{
			if (n.Ccost <= GameInformer.Cruelty && n.Ecost <= GameInformer.Empathy && n.Tcost <= GameInformer.Tranquality && n.Lcost <= GameInformer.Luck)
			{
				PC_Main a = player.GetComponent<PC_Main>();
				GameInformer.Cruelty -= n.Ccost;
				GameInformer.Empathy -= n.Ecost;
				GameInformer.Tranquality -= n.Tcost;
				GameInformer.Luck -= n.Lcost;
				a.abilities.Add(n.ability);
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
		
		GUI.Label(cruelty, "Cruelty: "+ GameInformer.Cruelty.ToString(),midfont);
		GUI.Label(empathy, "Emapthy: "+ GameInformer.Empathy.ToString(),midfont);
		GUI.Label(tranquilty, "Tranquility: "+ GameInformer.Tranquality.ToString(), midfont);
		GUI.Label(luck, "Luck: "+ GameInformer.Luck.ToString(), midfont);
		
		
		if (node != null)
		{
			A_Node n = node.GetComponent<A_Node>();
			GUI.Box(selbox,"Learn " + selected +"?");
			
			if (GUI.Button(yesbutton, "Yes"))Learn(n);
			if (GUI.Button(nobutton, "No")) node = null;
		}
		
	}
}

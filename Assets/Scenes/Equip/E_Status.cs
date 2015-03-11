using UnityEngine;
using System.Collections;

public class E_Status : MonoBehaviour {

	public int Id;
	GUIStyle largeFont;
	GUIStyle smallFont;

	public GUIStyle DMGFont;
	public GUIStyle HITFont;
	public GUIStyle BRWFont;
	public GUIStyle TENFont;
	public GUIStyle CRGFont;

	public PC_Main stat;

	public GameObject selected;

	public Rect Box;
	public Rect Name;

	public Rect Anima;
	public Rect ABRW;
	public Rect ATEN;
	public Rect ACRG;

	public Rect HP;
	public Rect BRW;
	public Rect TEN;
	public Rect CRG;
	public Rect DMG;
	public Rect HIT;

	void Start()
	{
		Selected ();

		DMGFont = new GUIStyle();
		DMGFont.fontSize = 20;
		DMGFont.normal.textColor = Color.white;

		HITFont = new GUIStyle();
		HITFont.fontSize = 20;
		HITFont.normal.textColor = Color.white;

		BRWFont = new GUIStyle();
		BRWFont.fontSize = 20;
		BRWFont.normal.textColor = Color.white;
		
		TENFont = new GUIStyle();
		TENFont.fontSize = 20;
		TENFont.normal.textColor = Color.white;

		CRGFont = new GUIStyle();
		CRGFont.fontSize = 20;
		CRGFont.normal.textColor = Color.white;

		largeFont = new GUIStyle();

		largeFont.fontSize = 40;
		largeFont.normal.textColor = Color.white;

		smallFont = new GUIStyle();
		
		smallFont.fontSize = 20;
		smallFont.normal.textColor = Color.white;

	}

	void Selected()
	{
		switch(Id)
		{
		case 1:
			selected = GameObject.Find("Zen");
			break;
		case 2:
			selected = GameObject.Find("Serenity");
			break;
		case 3:
			selected = GameObject.Find("Sky");
			break;
		case 4:
			selected = GameObject.Find("Hena");
			break;
		case 5:
			selected = GameObject.Find("Rose");
			break;
		case 6:
			selected = GameObject.Find("Annihilator");
			break;
		case 7:
			selected = GameObject.Find("Xeres");
			break;
		}
	}

	void FixedUpdate()
	{
		Selected();
		stat = selected.GetComponent<PC_Main>();
	}


	void OnGUI()
	{
		GUI.Box(Box,"");

		//Anima
		GUI.Label(Anima,"Remaining Anima",smallFont);

		if (stat != null)
		{
		//Anima Brawns;
		GUI.Label(ABRW, " BRW: " + stat.stats[1,0] ,smallFont);

		//Anima Tenacity
			GUI.Label(ATEN, "TEN:  " + stat.stats[1,1] ,smallFont);

		//Anima Courage
			GUI.Label(ACRG, "CRG: " + stat.stats[1,2],smallFont);

		//Name
		GUI.Label(Name, selected.name, largeFont);

		//HP-Brawns
		GUI.Label(HP, "HP    " + stat.HP, smallFont);
		GUI.Label(BRW, "BRW " + stat.stats[0,0], BRWFont);

		//Tenacity-Courage
			GUI.Label(TEN, "TEN  " + stat.stats[0,1], TENFont);
			GUI.Label(CRG, "CRG " + stat.stats[0,2], CRGFont);

		//Damage-Hit
		GUI.Label(DMG, "DMG " + stat.damage, DMGFont);
		GUI.Label(HIT, "HIT   " + stat.hit, HITFont);
		}
	}
}

using UnityEngine;
using System.Collections;
using Database;

public class controlscrap : MonoBehaviour {

	public PC_Main controlling;
	public PC_Main equip; 
	#region Movement
	public static bool stop = false;
	public float speed;
	static public KeyCode Up;
	static public KeyCode Down;
	static public KeyCode Left;
	static public KeyCode Right;
	#endregion
	#region Ability
	static public bool ability = false;
	static public KeyCode A1;
	public string g1name = "";
	static public bool g1burnt = false; 
	public string t1name = "";
	static public KeyCode A2;
	public string g2name = "";
	static public bool g2burnt = false; 
	public string t2name = "";
	static public KeyCode A3;
	public string g3name = "";
	static public bool g3burnt = false; 
	public string t3name = "";
	static public KeyCode A4;
	public string g4name = "";
	static public bool g4burnt = false; 
	public string t4name = "";
	#endregion
	#region Ability Toggle
	static public KeyCode GoddessTog;
	static public KeyCode ItemTog;
	#endregion
	#region Navigation
	static public KeyCode Cover;
	static public KeyCode Rotate;
	static public KeyCode Select;
	static public KeyCode Battle;
	static public KeyCode Special;
	static public KeyCode Deselect;
	#endregion



	// Use this for initialization
	void Start () {

		controlling = GetComponent<PC_Main>();
		equip = GetComponent<PC_Main>();
		GameInformer.player = 1;

		Up = KeyCode.W;
		Down = KeyCode.S;
		Left = KeyCode.A;
		Right = KeyCode.D;
		A1 = KeyCode.W;
		A2 = KeyCode.S;
		A3 = KeyCode.D;
		A4 = KeyCode.A;
		Select = KeyCode.Mouse0;
		GoddessTog = KeyCode.LeftShift;
		ItemTog = KeyCode.E;
		Deselect = KeyCode.Escape;
		Battle = KeyCode.F;
	}
	
	// Update is called once per frame
	void Update () {

		if (stop == false)
		{
		#region Movement Control

		if (ability == false)
		{
			if (controlling.ID == GameInformer.Idler)
			{
				
				if (Input.GetKey(Up))
				{
					checkUp();
				}

				if (Input.GetKey(Down))
				{
						checkDown();
				}

				if (Input.GetKey(Left))
				{
						checkLeft();
				}

				if (Input.GetKey(Right))
				{
						checkRight();
				}
			}
		}
		}
	}
		#endregion
		#region Ability Control
	void FixedUpdate() {
		if (stop == false)
		{
		if (ability == true)
		{
		if (controlling.ID == GameInformer.player)
		{	
		if (Input.GetKeyDown(A1) && !Input.GetKey(GoddessTog))
		{
			checkA1("Attack");
		}
		
		if (Input.GetKeyDown(A2) && !Input.GetKey(GoddessTog))
		{
		checkA2(equip.ability[3].name);
		}
		
		if (Input.GetKeyDown(A3) && !Input.GetKey(GoddessTog))
		{
						checkA3(equip.ability[4].name);
		}
		
		if (Input.GetKeyDown(A4) && !Input.GetKey(GoddessTog))
		{
						checkA4(equip.ability[5].name);
		}
		}
		#endregion
		#region Ability Toggle
		if (Input.GetKeyDown(GoddessTog))
		{
				Debug.Log("activated!");
		}
		if (Input.GetKey(GoddessTog) && Input.GetKeyDown(A1))
		{
			checkGodTog1(g1name);
		}
		
		if (Input.GetKey(GoddessTog) && Input.GetKeyDown(A2))
		{
			checkGodTog2();
		}
		
		if (Input.GetKey(GoddessTog) && Input.GetKeyDown(A3))
		{
			checkGodTog3();
		}
		
		if (Input.GetKey(GoddessTog) && Input.GetKeyDown(A4))
		{
			checkGodTog4();
		}

		if (Input.GetKey(ItemTog) && Input.GetKeyDown(A1))
		{
			checkItemTog1(t1name);
		}
		
		if (Input.GetKey(ItemTog) && Input.GetKeyDown(A2))
		{
			checkItemTog2();
		}
		
		if (Input.GetKey(ItemTog) && Input.GetKeyDown(A3))
		{
			checkItemTog3();
		}
		
		if (Input.GetKey(ItemTog) && Input.GetKeyDown(A4))
		{
			checkItemTog4();
		}
		#endregion
		#region Navigation
		if (Input.GetKeyDown(Cover))
		{
			checkCover();
		}
		}
		
		if (Input.GetKeyDown(Rotate))
		{
			checkRotate();
		}
		
		if (Input.GetKeyDown(Select))
		{
			checkSelect();
		}
		
		if (Input.GetKeyDown(Battle))
		{
			if (ability == false)
			{
			ability = true;
			Debug.Log ("BATTLE START!!");
			}
		}

		if (Input.GetKeyDown(Special))
		{
			checkSpecial();
		}
		if (Input.GetKeyDown(Deselect))
		{
			checkDeselect();
		}
		#endregion
	}
	}

	#region Movement Check

	void checkUp()
	{
		transform.Translate(Vector3.forward * speed * Time.deltaTime);
	}

	void checkDown()
	{
		transform.Translate(-Vector3.forward * speed * Time.deltaTime);
	}

	void checkLeft()
	{
		transform.Rotate(-Vector3.up * speed*34 * Time.deltaTime);
	}

	void checkRight()
	{
		transform.Rotate(-Vector3.down * speed*34 * Time.deltaTime);
	}
	#endregion
	#region Ability Check
	public void checkA1(string name)
	{
		//Abilities.AManage(name);
	}

	void checkA2(string name)
	{
		//Abilities.AManage(name);
	}

	void checkA3(string name)
	{
		//Abilities.AManage(name);
	}

	void checkA4(string name)
	{
		//Abilities.AManage(name);
	}
	#endregion
	#region Toggle Check
	void checkGodTog1(string name)
	{
	//	Goddess.GodManager(name);
	}

	void checkGodTog2()
	{
	}

	void checkGodTog3()
	{
	}

	void checkGodTog4()
	{
	}

	void checkItemTog1(string name)
	{

		//item.TManage(name);
	}

	void checkItemTog2()
	{
	}

	void checkItemTog3()
	{
	}

	void checkItemTog4()
	{
	}
	#endregion
	#region Navigation
	void checkCover()
	{
	}

	void checkRotate()
	{
	}

	void checkSelect()
	{
	}

	void checkBattle()
	{
	}

	void checkSpecial()
	{
	}

	void checkDeselect()
	{
	}
	#endregion

}

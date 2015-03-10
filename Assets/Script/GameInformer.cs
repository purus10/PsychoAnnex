using UnityEngine;
using System.Collections;

public class GameInformer : MonoBehaviour {

	static public int money;
	static public int battlestate;
	static public int turn = 0;
	static public int player;
	static public int Idler = 1;
	static public int selected;
	static public KeyCode Up,Down,Left,Right,A1,A2,A3,A4,GoddessTog,ItemTog,Select,Fight,Special,Deselect;
	static public bool stop;

	public GameObject[] Character;
	public Vector3 position;

	static public string previous;

	static public int Cruelty, Tranquality, Empathy, Luck;

	public int lighter;

	static public GameObject check;
	// The target we are following
	static public  Transform target;
	// The distance in the x-z plane to the target
	public float distance = 3.0f;
	// the height we want the camera to be above the target
	public float height = 1.0f;
	// How much we 
	public float heightDamping = 2.0f;
	public float rotationDamping = 0.6f;

	void Awake()
	{
		if (check == null) CreateCharacter();
		check = Character[Idler-1];
		previous = Application.loadedLevelName;
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
		Fight = KeyCode.F;
	}

	void CreateCharacter()
	{
		Instantiate (Character[Idler-1], position,Quaternion.identity);
	}


	private void Idlercamera()
	{
		switch(Idler)
		{
		case 1:
		target = GameObject.Find("Zen").transform;
		break;
		case 2:
		target = GameObject.Find("Serenity").transform;
		break;
		}
	}

	public static void Isee()
	{
		GameObject[] world = FindObjectsOfType(typeof(GameObject)) as GameObject[];

		foreach (GameObject seen in world)
		{
			if (seen.name == "Shadows")
			{
				if (Idler == 1 || Idler == 2)
				{
				seen.GetComponent<Renderer>().enabled = true;
				}else {
				seen.GetComponent<Renderer>().enabled = false;
				}
			}
		}
	}


	void Update ()
	{
		if (battlestate > 0)
		Battle ();
	}


	public void Battle ()
	{
		switch (battlestate)
		{
		//Start
		case 1: //Zen's turn
		if (turn == 0)
			{
				player = Random.Range (1,7);
				if (player == 1)
				{
					check =  GameObject.Find("Zen");
					if (check != null)
					{
						target = GameObject.Find("Zen").transform;

						lighter++;
						battlestate = 0;
					} else {
						player = Random.Range (1,7);
						Unburn();
				}
			}else if (player == 2)
			{
				check =  GameObject.Find("Serenity");
					if (check != null)
				{
					target = GameObject.Find("Serenity").transform;
						lighter++;
						battlestate = 0;
				} else {
					player = Random.Range (1,7);
					Unburn();
				}
				}else if (player == 3)
					{
						check =  GameObject.Find("Sky");
					if (check != null)
						{
							target = GameObject.Find("Sky").transform;
							battlestate = 0;
						} else {
							player = Random.Range (1,7);
							Unburn();
						}
				}else if (player == 4)
				{
					check =  GameObject.Find("Hena");
					if (check != null)
					{
						target = GameObject.Find("Hena").transform;
						lighter++;
						battlestate = 0;
					} else {
						player = Random.Range (1,7);
						Unburn();
					}
				}else if (player == 5)
				{
					check =  GameObject.Find("Rose");
					if (check != null)
					{
						target = GameObject.Find("Rose").transform;
						lighter++;
						battlestate = 0;
					} else {
						player = Random.Range (1,7);
						Unburn();
					}
				}else if (player == 6)
				{
					check = GameObject.Find("Annihilator");
					if (check != null)
					{
						target = GameObject.Find("Annihilator").transform;
						lighter++;
						battlestate = 0;
					} else {
						player = Random.Range (1,7);
						Unburn();
					}
				}else if (player == 7)
				{
					check =  GameObject.Find("Xeres");
					if (check != null)
					{
						target = GameObject.Find("Xeres").transform;
						lighter++;
						battlestate = 0;
					} else {
						player = Random.Range (1,7);
						Unburn();
					}
				}
			}
		break;
		//Battle over check?
		case 2:
			if (BattleEndCheck())
			{
				battlestate = 3;
			} else {
				battlestate = 1;
			}
		break;
		//Calculations
		case 3:
			print ("DUM DUM DUM DUUUM DUUUM DUM DUM DUM!!!");
		break;
		//Nothing
		default:
		break;
		}
	}

	public void Unburn()
	{
	GameObject [] unburns;
	unburns =  GameObject.FindGameObjectsWithTag("Player");
	turn = 0;
	Battle ();
	}

	public void Menu ()
	{
	}

	static public bool BattleEndCheck()
	{
		int goopies = GameObject.FindGameObjectsWithTag("Enemy").Length;

		print ( "this" + goopies);

		return goopies == 0;

	}

	void  LateUpdate (){

		if (target != null)
		{
			// Calculate the current rotation angles
			float wantedRotationAngle = target.eulerAngles.y;
			float wantedHeight = target.position.y + height;
			
			float currentRotationAngle = transform.eulerAngles.y;
			float currentHeight = transform.position.y;
			
			// Damp the rotation around the y-axis
			currentRotationAngle = Mathf.LerpAngle (currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
			
			// Damp the height
			currentHeight = Mathf.Lerp (currentHeight, wantedHeight, heightDamping * Time.deltaTime);

			// Set the position of the camera on the x-z plane to:
			// distance meters behind the target
			transform.position = target.position;
			transform.position -= Quaternion.Euler (0, currentRotationAngle, 0) * Vector3.forward * distance;
			transform.Translate(Vector3.up , Space.Self);
			// Always look at the target
			transform.LookAt (target);
		} else {
			Idlercamera();
		}
		}

}

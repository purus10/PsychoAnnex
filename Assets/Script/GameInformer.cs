using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameInformer : MonoBehaviour {

	static public GameInformer GI;
	static public int money;
	static public int battlestate;
	static public int Completed_Turns = 0;
	static public int player = 6;
	static public int Idler = 0;
	static public int selected;
	static public KeyCode Up,Down,Left,Right,GoddessTog,ItemTog,Select,Fight,Special,Deselect;
	static public KeyCode[] A = new KeyCode[4];
	static public bool stop, battle, ambushed;
	static public List <NPC_Main> Opponents = new List<NPC_Main>();

	public GameObject[] Character;
	static public PC_Main[] Characters;
	public Vector3[] position;
	
	static public string previous;

	//0 = Cruelty, 1 = Tranquality, 2 = Empathy, 3 = Luck
	static public int[] Essence = new int[4];

	public int lighter;

	static public GameObject check;
	// The target we are following
	static public  Transform target;
	// The distance in the x-z plane to the target
	public float distance;
	// the height we want the camera to be above the target
	public float height;
	// How much we 
	public float heightDamping;
	public float rotationDamping;
	public NPC_Main Highest()
	{
		NPC_Main highest = null;
		int h = 0;
		foreach(NPC_Main n in Opponents)
		{
			if (n.cur_beats != 0)
			{
				int p = n.Courage;
				if (highest != null) h = highest.Courage;
				if (p > h) highest = n;
			}
		}
		return highest;
	}
	public bool OpponentRefresh()
	{
		bool refresh = true;
		foreach(NPC_Main n in Opponents)
		{
			if (n.cur_beats != 0) refresh = false;
		}
		return refresh;
	}

	public bool CharacterRefresh()
	{
		bool refresh = true;
		foreach(PC_Main n in Characters)
		{
			if (n.cur_beats != 0) refresh = false;
		}
		return refresh;
	}


	void Awake()
	{
		GI = this;
		UnityEngine.Cursor.visible = false;
		if (check == null) CreateCharacter();
		check = Character[0];
		previous = Application.loadedLevelName;
		Up = KeyCode.W;
		Down = KeyCode.S;
		Left = KeyCode.A;
		Right = KeyCode.D;
		A[0] = KeyCode.W;
		A[1] = KeyCode.A;
		A[2] = KeyCode.S;
		A[3] = KeyCode.D;
		Select = KeyCode.Mouse0;
		GoddessTog = KeyCode.LeftShift;
		ItemTog = KeyCode.E;
		Deselect = KeyCode.Escape;
		Fight = KeyCode.F;
	}

	void Start()
	{
		Characters = GameObject.FindObjectsOfType(typeof(PC_Main)) as PC_Main[];
	}

	void Update()
	{
		if (Opponents.Count > 0)
		{
			if (Input.GetKeyDown(GameInformer.Fight))
			{
				stop = true;
				battle = true;
				foreach(PC_Main m in Characters) m.BattleSetup();
			}

		}
		/*if (target != null) 
			if (Character[Idler] != target.gameObject) Idlercamera();*/
	}
	
	public void Initation()
	{
		Completed_Turns++;
		print ("Completed turns "+Completed_Turns);
		if (OpponentRefresh() == true)
		{
			print ("Yes REFRESH");
			foreach(NPC_Main n in Opponents) n.cur_beats = n.Beat;
		}
		if (CharacterRefresh() == true)
		{
			print ("Yes CHARACTER REFRESH");
			for (int i = 0;i<Characters.Length;i++) Characters[i].cur_beats = Characters[i].Beat;
		}
		print ("TURN START OVER");
		Highest().move_points++;
	}

	void CreateCharacter()
	{
		for(int i = 0; i < Character.Length;i++)
		Instantiate (Character[i], position[i],Quaternion.identity);
	}


	private void Idlercamera()
	{
		switch(Idler)
		{
		case 0:
		target = GameObject.Find("Zen").transform;
		break;
		case 1:
		target = GameObject.Find("Serenity").transform;
		break;
		case 2:
			target = GameObject.Find("Sky").transform;
			break;
		case 3:
			target = GameObject.Find("Hena").transform;
			break;
		case 4:
			target = GameObject.Find("Rose").transform;
			break;
		case 5:
			target = GameObject.Find("Annihilator").transform;
			break;
		case 6:
			target = GameObject.Find("Xeres").transform;
			break;
		}
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
			transform.position = new Vector3(target.position.x,target.position.y,target.position.z);
			transform.position -= Quaternion.Euler (currentHeight, currentRotationAngle, 0) * Vector3.forward * distance;
			transform.Translate(Vector3.up , Space.Self);
			// Always look at the target
			transform.LookAt (target);
		} else {
			Idlercamera();
		}
		}

}

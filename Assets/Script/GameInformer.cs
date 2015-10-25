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
	static public KeyCode[] A;
	static public KeyCode Up,Down,Left,Right,GoddessTog,ItemTog,Select,Fight,Special,Deselect;
	static public bool stop, battle, ambushed;
	static public List <NPC_Main> Opponents = new List<NPC_Main>();

	public GameObject[] Character;
	static public PC_Main[] Characters;
	public Vector3[] position;
	
	static public string previous;

	//0 = Cruelty, 1 = Tranquality, 2 = Empathy, 3 = Luck
	static public int[] Essence = new int[4];

	public int lighter;
	public float smooth = 1.5f;         // The relative speed at which the camera will catch up.
	private Vector3 relCameraPos;       // The relative position of the camera from the player.
	private float relCameraPosMag;      // The distance of the camera from the player.
	private Vector3 newPos;             // The position the camera is trying to reach.
	static public GameObject check;
	// The target we are following
	static public  Transform target;

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
	/*	if (check == null) CreateCharacter();
		check = Character[0];*/
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
		relCameraPos = transform.position - target.position;
		relCameraPosMag = relCameraPos.magnitude - 0.5f;
	}

	bool ViewingPosCheck (Vector3 checkPos)
	{
		RaycastHit hit;
		
		// If a raycast from the check position to the player hits something...
		if(Physics.Raycast(checkPos, target.position - checkPos, out hit, relCameraPosMag))
			// ... if it is not the player...
			if(hit.transform != target)
				// This position isn't appropriate.
				return false;
		
		// If we haven't hit anything or we've hit the player, this is an appropriate position.
		newPos = checkPos;
		return true;
	}
	
	
	void SmoothLookAt ()
	{
		// Create a vector from the camera towards the player.
		Vector3 relPlayerPosition = target.position - transform.position;
		
		// Create a rotation based on the relative position of the player being the forward vector.
		Quaternion lookAtRotation = Quaternion.LookRotation(relPlayerPosition, Vector3.up);
		
		// Lerp the camera's rotation between it's current rotation and the rotation that looks at the player.
		transform.rotation = Quaternion.Lerp(transform.rotation, lookAtRotation, smooth * Time.deltaTime);
	}

	void FixedUpdate ()
	{
	/*	if (target != null)
		{
		// The standard position of the camera is the relative position of the camera from the player.
		Vector3 standardPos = target.position + relCameraPos;
		
		// The abovePos is directly above the player at the same distance as the standard position.
		Vector3 abovePos = target.position + Vector3.up * relCameraPosMag;
		
		// An array of 5 points to check if the camera can see the player.
		Vector3[] checkPoints = new Vector3[5];
		
		// The first is the standard position of the camera.
		checkPoints[0] = standardPos;
		
		// The next three are 25%, 50% and 75% of the distance between the standard position and abovePos.
		checkPoints[1] = Vector3.Lerp(standardPos, abovePos, 0.25f);
		checkPoints[2] = Vector3.Lerp(standardPos, abovePos, 0.5f);
		checkPoints[3] = Vector3.Lerp(standardPos, abovePos, 0.75f);
		
		// The last is the abovePos.
		checkPoints[4] = abovePos;
		
		// Run through the check points...
		for(int i = 0; i < checkPoints.Length; i++)
		{
			// ... if the camera can see the player...
			if(ViewingPosCheck(checkPoints[i]))
				// ... break from the loop.
				break;
		}
		
		// Lerp the camera's position between it's current position and it's new position.
		transform.position = Vector3.Lerp(transform.position, newPos, smooth * Time.deltaTime);
		
		// Make sure the camera is looking at the player.
		SmoothLookAt();
		} else Idlercamera();*/
	}
}

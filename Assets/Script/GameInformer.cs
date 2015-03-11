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
		case 3:
			target = GameObject.Find("Sky").transform;
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

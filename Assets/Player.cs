using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	public bool MoveUp;
	public bool MoveDown;

	public KeyCode UpBinding;
	public KeyCode DownBinding;

	void Update () 
	{
		MoveUp = Input.GetKey (UpBinding);
		MoveDown = Input.GetKey (DownBinding);
	}
}

using UnityEngine;
using System.Collections;

public class Paddle : MonoBehaviour 
{
	public Player Controls;
	public float Speed = 0.6f;

	// Update is called once per frame
	void FixedUpdate () 
	{
		if (Controls.MoveUp && transform.position.y <= 6) 
		{
			transform.Translate(new Vector3(Speed, 0, 0));
		}

		if (Controls.MoveDown && transform.position.y >= -2) 
		{
			transform.Translate(new Vector3(-Speed, 0, 0));
		}
	}
}

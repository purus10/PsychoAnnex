using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	public float InitialForce = 20f;
 	public int Score1;
	public int Score2;
	public GUIText ScoreBoard1;
	public GUIText ScoreBoard2;
	public GUIText Fin;
	public GUIText Draw;
	public GUIText onewins;
	public GUIText twowins;
	public bool pause;
	Vector3 initVelocity = new Vector3();

	// Use this for initialization

	// Update is called once per frame
	void Start () 
	{
		Launch ();
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.name == "Goal1") {
			Score2 ++;
			GetComponent<Rigidbody>().MovePosition (new Vector3(0,3.233599f,0));
			pause = true;
			ScoreBoard2.text = Score2.ToString();

		} else {
			if (col.gameObject.name == "Goal2") {
				GetComponent<Rigidbody>().MovePosition (new Vector3(0,3.233599f,0));
				Score1 ++;
				pause = true;
				ScoreBoard1.text = Score1.ToString();

				if (Score1 + Score2 >= 10) 
				{
					GameEnd ();
				}
			}
		}
		if (col.gameObject.name == "Wall") 
		{
		initVelocity.x = Random.Range (-1f, 1f);
		GetComponent<Rigidbody>().AddForce (initVelocity, ForceMode.Impulse);
		} 
		else 
		{
		initVelocity.y = Random.Range (-1f, 1f);
		GetComponent<Rigidbody>().AddForce (initVelocity, ForceMode.Impulse);
		}
		
	}

	public void Launch () 
	{
		initVelocity.x = Random.Range(-1f,1f);
		initVelocity.Normalize();
		GetComponent<Rigidbody>().AddForce( initVelocity* InitialForce, ForceMode.Impulse);
	}

	void GameEnd()
	{
		Fin.GetComponent<GUIText>().enabled = true;
		GetComponent<Rigidbody>().MovePosition (new Vector3(0,0,0));
		GetComponent<Rigidbody>().velocity = Vector3.zero;

		if (Score1 == Score2)
		{
			Draw.GetComponent<GUIText>().enabled = true;
		}
		
		if (Score1 > Score2)
		{
			onewins.GetComponent<GUIText>().enabled = true;
		}
		else
		{
			if (Score2 > Score1)
			{
				twowins.GetComponent<GUIText>().enabled = true;
			}
		}
	}


	void Update()
	{





	}
}

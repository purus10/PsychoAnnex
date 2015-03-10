using UnityEngine;
using System.Collections;

public class Cancel_Button : MonoBehaviour {

	Finish_Button fin;
	Metal_Button metal;
	public bool exit;
	public Rect Box;


	void Start()
	{
		fin = GetComponent<Finish_Button>();
	}

	void OnGUI()
	{

		if(GUI.Button(Box, "Exit"))
		{
			Application.LoadLevel(GameInformer.previous);
		}
	}
}

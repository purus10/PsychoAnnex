using UnityEngine;
using System.Collections;

public class Cancel_Button : MonoBehaviour {
	
	public bool exit;
	public Rect Box;

	void OnGUI()
	{

		if(GUI.Button(Box, "Exit"))
		{
			Application.LoadLevel(GameInformer.previous);
		}
	}
}

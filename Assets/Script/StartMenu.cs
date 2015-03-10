using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour {
	
	public Rect start_box, info_text;
	public Rect start_button, quit_button;
	public Texture2D info;

	void OnGUI()
	{
		GUI.Box(start_box,"");

		if (GUI.Button(start_button,"Start"))
		{
			Application.LoadLevel("Corridor");
		}

		if (GUI.Button(quit_button,"Quit"))
		{
			print ("Quit");
		}
	}
}

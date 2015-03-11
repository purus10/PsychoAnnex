using UnityEngine;
using System.Collections;

public class E_Info : MonoBehaviour {


	public Rect Box;
	public Rect Info;

	GUIStyle smallFont;
	GUIStyle center;
	public string info;
	
	void Start()
	{
		center = new GUIStyle();
		center.alignment = TextAnchor.MiddleCenter;
		center.fontSize = 24;
		center.normal.textColor = Color.white;

		smallFont = new GUIStyle();
		
		smallFont.fontSize = 20;
		smallFont.normal.textColor = Color.white;
	}
	
	// Update is called once per frame
	void OnGUI () {
		
		GUI.Box(Box,"");
		GUI.Label(Info,info, center);
		
	}
}

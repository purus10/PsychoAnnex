using UnityEngine;
using System.Collections;

public class E_Quirk : MonoBehaviour {

	public Rect Box, Title;
	public Rect Quirk1, Quirk2, Quirk3, Quirk4;
	GUIStyle q1center, q2center, q3center, q4center;

	GUIStyle largeFont;
	GUIStyle smallFont;
	
	E_Status selected;
	E_Info desc;

	void Start()
	{
		q1center = new GUIStyle();
		q1center.alignment = TextAnchor.MiddleCenter;
		q1center.fontSize = 20;
		q1center.normal.textColor = Color.white;

		q2center = new GUIStyle();
		q2center.alignment = TextAnchor.MiddleCenter;
		q2center.fontSize = 20;
		q2center.normal.textColor = Color.white;

		q3center = new GUIStyle();
		q3center.alignment = TextAnchor.MiddleCenter;
		q3center.fontSize = 20;
		q3center.normal.textColor = Color.white;

		q4center = new GUIStyle();
		q4center.alignment = TextAnchor.MiddleCenter;
		q4center.fontSize = 20;
		q4center.normal.textColor = Color.white;

		largeFont = new GUIStyle();
		
		largeFont.fontSize = 40;
		largeFont.normal.textColor = Color.white;

		smallFont = new GUIStyle();
		
		smallFont.fontSize = 30;
		smallFont.normal.textColor = Color.white;
	}

	void FixedUpdate()
	{
		selected = GetComponent<E_Status>();
		desc = GetComponent<E_Info>();
	}
	
	// Update is called once per frame
	void OnGUI () {

		GUI.Box(Box,"");

		//Quirks
		GUI.Label(Title, "Quirks", smallFont);
		if (selected != null)
		{
		GUI.Label (Quirk1, selected.stat.quirk[0,0], q1center);
		}
		if (Quirk1.Contains(new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y)))
		{
			q1center.normal.textColor = Color.yellow;
			desc.info = selected.stat.quirk[0,1];
			}else{
			if (desc.info == selected.stat.quirk[0,1]) desc.info = "";
			q1center.normal.textColor = Color.white;
		}

		/*GUI.Label(Quirk2, selected.stat.Quirk2, q2center);
		if (Quirk2.Contains(new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y)))
		{
			q2center.normal.textColor = Color.yellow;
			desc.info = selected.stat.Qinfo[1];
		}else{
			if (desc.info == selected.stat.Qinfo[1]) desc.info = "";
			q2center.normal.textColor = Color.white;
		}

		GUI.Label(Quirk3, selected.stat.Quirk3, q3center);
		if (Quirk3.Contains(new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y)))
		{
			q3center.normal.textColor = Color.yellow;
			desc.info = selected.stat.Qinfo[2];
		}else{
			if (desc.info == selected.stat.Qinfo[2]) desc.info = "";
			q3center.normal.textColor = Color.white;
		}

		GUI.Label(Quirk4, selected.stat.Quirk4, q4center);
		if (Quirk4.Contains(new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y)))
		{
			q4center.normal.textColor = Color.yellow;
			desc.info = selected.stat.Qinfo[4];
		}else{
			//if (desc.info == selected.stat.Qinfo[4]) desc.info = "";
			q4center.normal.textColor = Color.white;
		}*/


	
	}
}

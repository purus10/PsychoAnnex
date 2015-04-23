using UnityEngine;
using System.Collections;
using Database;

public class Cancel_Button : MonoBehaviour {
	
	public Rect Box;
	Metal_Button metals;
	Selected_Button selected;

	void Start()
	{
		metals = GetComponent<Metal_Button>();
		selected = GetComponent<Selected_Button>();
	}

	void OnGUI()
	{
		if(GUI.Button(Box, "Exit"))
		{
			Cancel();
			Application.LoadLevel(GameInformer.previous);
		}
	}

	void Cancel()
	{
		for(int i = 0; i < metals.metal.Length;i++)
		{
			if (metals.metal[i] != null && selected.wep != null || selected.acc != null &&  selected.wep.metal[i] != null)
			{
				if (selected.wep.metal[i].name != metals.metal[i].name /*|| selected.acc.metal[i].name != metals.metal[i].name*/ )
				{
					foreach (Item m in metals.metals)
					{
						if (m.name == metals.metal[i].name) m.amount++;
					}
				}
			}
		}
	}
}

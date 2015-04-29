using UnityEngine;
using System.Collections;

public class Passive_Menu : MonoBehaviour {

	public Rect[] Menu_Box;
	public Rect[] Stat_Labels = new Rect[10];
	public Rect Passive_Button;
	string[] labels = new string[3]{"BRW","TEN","COR"};
	int menu_show = 0;
	A_Display main;

	void Start() {
		main = GetComponent<A_Display>();
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.R)) menu_show = 1;
	}

	void OnGUI() {
		if (menu_show > 0)
		{
			foreach (Rect rect in Menu_Box) GUI.Box(rect,"");
			for (int i = 0; i < labels.Length;i++) GUI.Label(Stat_Labels[i],main.stat.stats[0,i].ToString()+" "+labels[i]);
			for (int i = 0; i < labels.Length;i++) GUI.Label(Stat_Labels[i+3],main.stat.stats[1,i].ToString()+" "+labels[i]);
			GUI.Label(Stat_Labels[6],"Anima");
			GUI.Label(Stat_Labels[7],"Beats: "+main.stat.Beat.ToString());
			GUI.Label(Stat_Labels[8],"Two Acc? :"+main.stat.second_acc);
			GUI.Label(Stat_Labels[9],"Fourth Item?: "+main.stat.fourth_item);
			for (int i = 0; i < main.stat.passives.Count;i++)
				if (GUI.Button(new Rect(Passive_Button.x,Passive_Button.y + (Passive_Button.width*i),Passive_Button.width,Passive_Button.height),main.stat.passives[i].Name))
			{
				print (main.stat.passives[i].Description);
			}

		}
	}
}

using UnityEngine;
using UnityEditor;
using System.Collections;

/*[CustomEditor (typeof(PC_Main))]
public class PC_Custom : Editor {
	PC_Main mypcmain;
	SerializeField BeatProp;
	SerializedProperty brawnsProp;
	
	void OnEnable()
	{
		mypcmain = (PC_Main) target;
	}
	void Tier()
	{
		for (int i = 0; i < mypcmain.stats.GetLength(1);i++)
			if (mypcmain.tier > 1) mypcmain.stats[0,i] = mypcmain.stats[0,i]*(mypcmain.tier-1);
		else mypcmain.stats[0,i] = mypcmain.stats[0,i]/(mypcmain.tier+1);
	}
	void Set(int type,int stat)
	{
		for (int j = 0 ; j < mypcmain.stats.GetLength(0);j++)
			mypcmain.stats[j,type] = stat;
	}
	string Type()
	{
		if (mypcmain.type == 0) return "Cover";
		else if (mypcmain.type == 1) return "Enemy";
		else if (mypcmain.type == 2) return "Ally";
		else return "Self";
	}
	string Target()
	{
		if (mypcmain.target == null) return "None";
		else return mypcmain.target.name;
	}
	string Cover()
	{
		if (mypcmain.cover == false) return "No";
		else return "Yes";
	}
	public override void OnInspectorGUI()
	{
		serializedObject.Update();
		EditorGUILayout.LabelField("Name", mypcmain.Name);
		mypcmain.HP = EditorGUILayout.IntField("HP", mypcmain.HP);
		EditorGUILayout.BeginHorizontal();
		mypcmain.Beat = EditorGUILayout.IntField("Beats", mypcmain.Beat);
		EditorGUILayout.EndHorizontal();
		//Brawns
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("Brawns",mypcmain.stats[0,0].ToString());
		mypcmain.Brawns = EditorGUILayout.IntField(mypcmain.Brawns);
		Set(0,mypcmain.Brawns);
		EditorGUILayout.EndHorizontal();
		//Tenacity
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("Tenacity",mypcmain.stats[0,1].ToString());
		mypcmain.Tenacity = EditorGUILayout.IntField(mypcmain.Tenacity);
		Set(1,mypcmain.Tenacity);
		EditorGUILayout.EndHorizontal();
		//Courage
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("Courage",mypcmain.stats[0,2].ToString());
		mypcmain.Courage = EditorGUILayout.IntField(mypcmain.Courage);
		Set(2,mypcmain.Courage);
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.LabelField("Tier", mypcmain.tier.ToString());
		mypcmain.tier_count = EditorGUILayout.Slider(mypcmain.tier_count,0,15);
		Tier();
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("Speed/Rotation");
		mypcmain.speed = EditorGUILayout.FloatField(mypcmain.speed);
		mypcmain.rotation = EditorGUILayout.FloatField(mypcmain.rotation);
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.LabelField("Target Type:", Type());
		EditorGUILayout.LabelField("Target", Target());

		EditorGUILayout.LabelField("UnderCover?", Cover());
		if (GUI.changed) EditorUtility.SetDirty(target);
		serializedObject.ApplyModifiedProperties();
	}
	
}*/

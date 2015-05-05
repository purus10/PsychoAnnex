using UnityEngine;
using System.Collections;

public class Imp_Behaviour : MonoBehaviour {

	NPC_Main my;
	public Color[] Element_Skin = new Color[10];
	public bool cover;
	public int Walk_Timer;
	public float Walk_Range;
	int t;
	Vector3 destination(float range, float x, float y, float z)
	{
		return new Vector3(Random.Range(-x-range,x+range),y,Random.Range(-z-range,z+range));
	}
	void OnTriggerEnter(Collider col)
	{
		PC_Main p = col.gameObject.GetComponent<PC_Main>();
		if (p != null) Destroy(gameObject);
	}
	void Awake () 
	{
		my = GetComponent<NPC_Main>();
		if (my.Element_Type == 0)
		{
			int element = Random.Range(1,10);
			my.Element_Type = element;
		}
		SetElementType(my.Element_Type);
	
	}
	void SetElementType(int element)
	{
		GetComponentInParent<Renderer>().material.color = Element_Skin[element-1];
	}
	void Update () 
	{
		t++;
		if (t >= Walk_Timer) 
		{
			t = 0;
			my.agent.SetDestination(destination(Walk_Range,my.transform.position.x,my.transform.position.y,my.transform.position.z));
		}
		if (my.move_points > 0) ChangeTargetElement();
	}

	void ChangeTargetElement()
	{
		NPC_Main[] search = GameObject.FindObjectsOfType(typeof(NPC_Main)) as NPC_Main[];
		foreach (NPC_Main n in search) 
			if (n.Element_Type != 0 && n.Name != "Imp") my.targets.Add(n.transform);
			my.targets.Sort(delegate(Transform t1, Transform t2) { 
				return (Vector3.Distance(t1.position, transform.position)).CompareTo(Vector3.Distance(t2.position,transform.position));});
			my.target = my.targets[0];
		NPC_Main t = my.target.GetComponent<NPC_Main>();
		t.Element_Type = my.Element_Type;
		print (t.Name+" "+t.Element_Type);
		my.target = null;
		my.targets.Clear();
		my.move_points--;
	}
}

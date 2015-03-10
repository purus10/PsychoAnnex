using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {
	public GameObject zen;

	// Use this for initialization
	void Start () {
		UnityEngineInternal.APIUpdaterRuntimeServices.AddComponent(zen, "Assets/Maps/Prototype/Scene/test.cs (9,3)", "Anima");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

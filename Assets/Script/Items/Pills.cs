using UnityEngine;
using System.Collections;

public class Pills : MonoBehaviour {

	public string Name = "Pills";
	public string Description;
	public string Type;
	public bool locked;
	public int amount;

	// Use this for initialization
	void Start () {

		amount = 1;
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
	}
}

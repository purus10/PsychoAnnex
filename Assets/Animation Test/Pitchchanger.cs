using UnityEngine;
using System.Collections;

public class Pitchchanger : MonoBehaviour {

	public AudioClip voice;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.anyKeyDown)
		{
			GetComponent<AudioSource>().PlayOneShot(voice);
		}
	}
}

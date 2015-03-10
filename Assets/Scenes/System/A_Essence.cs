using UnityEngine;
using System.Collections;

public class A_Essence : MonoBehaviour {

	public TextMesh Cruelty;
	public TextMesh Empathy;
	public TextMesh Tranquility;
	public TextMesh Luck;
	static GameObject player;

	
	// Update is called once per frame
	void FixedUpdate () {

		Cruelty.text = GameInformer.Cruelty.ToString();
		Empathy.text = GameInformer.Empathy.ToString();
		Tranquility.text = GameInformer.Tranquality.ToString();
		Luck.text = GameInformer.Luck.ToString();
	
	}
}

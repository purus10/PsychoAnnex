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

		Cruelty.text = GameInformer.Essence[0].ToString();
		Empathy.text = GameInformer.Essence[1].ToString();
		Tranquility.text = GameInformer.Essence[2].ToString();
		Luck.text = GameInformer.Essence[3].ToString();
	
	}
}

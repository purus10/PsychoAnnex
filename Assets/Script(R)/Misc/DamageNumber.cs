using UnityEngine;
using System.Collections;

public class DamageNumber : MonoBehaviour {

	public TextMesh number;
	public float YBonus, XBonus, Timer, Fade, OffWait, DoubleWait, TripleWait;
	public bool OffHand;
	public Transform Target;
	public int Hit;
	Vector3 endpoint;

	// Use this for initialization
	void Start () 
	{
		Fade = Time.time + Fade;
		Timer = Fade + Timer;
		OffWait = Time.time + OffWait;
		DoubleWait = Time.time + DoubleWait;
		TripleWait = Time.time + TripleWait;
		if (OffHand == false) endpoint = new Vector3(Target.position.x,Target.position.y+YBonus,Target.position.z);
		else { 
			endpoint = new Vector3(Target.position.x+XBonus,Target.position.y+YBonus,Target.position.z);
			transform.position = new Vector3(transform.position.x+XBonus,transform.position.y,transform.position.z);
		}
	}
	void BounceNumber()
	{
		number.transform.position =  Vector3.Lerp(transform.position,endpoint,Time.deltaTime * 2f);
		if (Time.time >= Fade) number.color = Color.Lerp(number.color,Color.clear,Time.deltaTime * 1.5f);
		if (Time.time >= Timer) Destroy(gameObject);
	}
	// Update is called once per frame
	void FixedUpdate () 
	{
		if (Hit == 0)
		{
			if (gameObject.name != "Number") BounceNumber();
		} else if (Hit == 1) {if (Time.time >= OffWait) BounceNumber();}
		else if (Hit == 2) {if (Time.time >= DoubleWait) BounceNumber();}
		else if (Hit == 3) {if (Time.time >= TripleWait) BounceNumber();}
	}
}

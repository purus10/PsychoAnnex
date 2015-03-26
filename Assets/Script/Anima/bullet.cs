using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public float speed;
	public float timer;
	public int damage;
	
	void Update()
	{
		timer++;
		transform.Translate(Vector3.forward * speed * Time.deltaTime);

		if (timer == 60) Destroy(gameObject);
	}

	void OnCollisionEnter(Collision col)
	{
		NPC_Main t = col.gameObject.GetComponentInParent<NPC_Main>();

		if (t != null) 
		{
			t.cur_hp -= damage;
			HUD.info = t.name+" Shot! "+t.Name + " Remaining HP: "+t.cur_hp;
		}
		Destroy(gameObject);
	}


}

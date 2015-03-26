using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {
	
	public GameObject nozzle, bullet, Character;
	public Texture Crosshair;
	PC_Main my;
	Bullet bul;


	void Start()
	{
		my = Character.GetComponent<PC_Main>();
		bul = bullet.GetComponent<Bullet>();
	}

	void FixedUpdate () 
	{
		if (this.gameObject.activeInHierarchy == true)
		{
		if(Input.GetMouseButtonDown(0)) 
			{
				Instantiate(bullet,nozzle.transform.position,nozzle.transform.rotation);
				bul.damage = my.hit + DoorManager.WindDoor;
				GameInformer.target = null;
				my.AimCamera.enabled = false;
				my.AimCamera.GetComponent<Shoot>().enabled = false;
				my.AimCamera.GetComponent<MouseLook>().enabled = false;
				if (my.far_beats == false) my.EndTurn();
				else my.cur_beats--;
			}
		}
		
	}
	void OnGUI()
	{
		GUI.DrawTexture(new Rect(Screen.width/2 - 50,Screen.height/2 - 50,100,100),Crosshair);
	}
}

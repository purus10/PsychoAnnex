using UnityEngine;
using System.Collections;

public class gunscript : MonoBehaviour {

	public Texture Crosshair;
	Ray ray;

	public GameObject Nozzle;
	public GameObject Bullet;
	
	// Use this for initialization
	void Start () {
		ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2,Screen.height/2,0));
		
	}

	// Update is called once per frame
	void FixedUpdate () {
		RaycastHit hit;
		ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2,Screen.height/2,0));
		if(Input.GetMouseButtonDown(0))
		{
			if(Physics.Raycast(ray,out hit,100000f))
			{
				Instantiate(Bullet,Nozzle.transform.position,Nozzle.transform.rotation);
			} else {
				Instantiate(Bullet,Nozzle.transform.position,Nozzle.transform.rotation);
			}
		}
		
	}
	void OnGUI()
	{
		GUI.DrawTexture(new Rect(Screen.width/2 - 50,Screen.height/2 - 50,100,100),Crosshair);
	}
}

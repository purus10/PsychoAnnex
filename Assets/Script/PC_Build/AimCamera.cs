using UnityEngine;
using System.Collections;

public class AimCamera : MonoBehaviour {

	public Camera Main;
	public Transform refpoint;
	public float Lerpspeed;

	// Use this for initialization
	public void Aiming () 
	{
		GameInformer player = Main.gameObject.GetComponent<GameInformer>();
		GetComponent<gunscript>().enabled = true;

		if (GameInformer.target != null)
		{
		Main.gameObject.SetActive(false);
		}

		transform.position = refpoint.position;

		gameObject.transform.parent = refpoint.gameObject.transform;
	}

	public void DisAiming()
	{
		GetComponent<gunscript>().enabled = false;

		Main.gameObject.SetActive(true);
		gameObject.SetActive(false);
	}

}

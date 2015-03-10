using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {

	public Camera AimCamera;

	// Use this for initialization
	void Start () {
	

	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetKeyDown(KeyCode.L))
		{
			AimCamera cam = AimCamera.gameObject.GetComponent<AimCamera>();
			if (AimCamera.gameObject.activeInHierarchy == false)
			{
			AimCamera.gameObject.SetActive(true);
			cam.Aiming();
			} else {
			cam.DisAiming();
			}
		}
	}
}

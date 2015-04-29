using UnityEngine;
using System.Collections;

public class Anima_Movement : MonoBehaviour {

	public Camera MainCamera;
	public int speed;

	void Update () {

		if(Input.GetKey(GameInformer.Up) && MainCamera.transform.position.y < 23f)
		{
			MainCamera.transform.Translate(transform.up * speed * Time.deltaTime);
		}

		if(Input.GetKey(GameInformer.Right) && MainCamera.transform.position.x < 24f)
		{
			MainCamera.transform.Translate(transform.right * speed * Time.deltaTime);
		}

		if(Input.GetKey(GameInformer.Left) && MainCamera.transform.position.x > -28f)
		{
			MainCamera.transform.Translate(-transform.right * speed * Time.deltaTime);
		}

		if(Input.GetKey(GameInformer.Down) && MainCamera.transform.position.y > -25f)
		{
			MainCamera.transform.Translate(-transform.up * speed * Time.deltaTime);
		}

		if(Input.GetKey(KeyCode.Q) && MainCamera.transform.position.y > -25f)
		{
			MainCamera.GetComponent<Camera>().orthographicSize = 7f;
		}

		if(Input.GetKey(KeyCode.E) && MainCamera.transform.position.y > -25f)
		{
			MainCamera.GetComponent<Camera>().orthographicSize = 12f;
		}
	
	}
}

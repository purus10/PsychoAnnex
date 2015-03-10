using UnityEngine;
using System.Collections;

public class objedct : MonoBehaviour {

	// Update is called once per frame
	void Update () {

		transform.Translate(transform.forward * 10f * Time.deltaTime, Space.World);
	
	}
}

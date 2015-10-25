using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Database;

public class Location : MonoBehaviour {
	
	public string Name;
	public Get.Element[] Domain = new Get.Element[3];
	public List<Shop> Shops = new List<Shop>();

	void Awake()
	{

	}
}

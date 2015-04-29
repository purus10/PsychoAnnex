using UnityEngine;
using System.Collections;

public class DoorManager : MonoBehaviour {

	// 0 = Earth 1 = Fire 2 = Wind 3 = Thunder 4 = Wicked
	// 5 = Divine 6 = Water 7 = Nature 8 = Day 9 = Night
	static public int[] Door = new int[10];
	static public int PhysicalDoor()
	{
		return Door[0] + Door[1] + Door[2] + Door[3] + Door[4];
	}
	static public int MagicalDoor()
	{
		return Door[5] + Door[6] + Door[7] + Door[8] + Door[9];
	}

	// Update is called once per frame
	void Update () {
	
	}
}

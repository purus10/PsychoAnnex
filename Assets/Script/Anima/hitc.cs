using UnityEngine;
using System.Collections;

/*public class hitc : MonoBehaviour
{

	public Transform target;


	void Update()
	{

		RaycastHit hit;
		Ray aim = new Ray(transform.position,Vector3.forward);

		Vector3 forward = (transform.TransformDirection(Vector3.forward));

		if (Physics.Raycast(transform.position, forward, 100))
		{
			if (Input.GetMouseButtonDown(0))
			{
				print ("ISHOT HIM!!!");
			}
			Debug.DrawRay(transform.position,transform.TransformDirection(Vector3.forward), Color.red);
		}
	}
*/
	public class hitc : MonoBehaviour 
	{
		// set variables for the range, the type of projectile, and where the projectile will spawn from.
		public GameObject projectile;
		public float range;
		Vector3 target;
		public Transform spawnpnt;
		// Use this for initialization
		void Start () 
		{
			
		}
		
		// Update is called once per frame
		void Update () 
		{
			
			if (Input.GetButtonDown("Fire1")|| Input.GetButton("Fire2"))
			{
				// declare the ray and have it cast from the center of the camera
				Ray finder = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit contact;
				
				if(Physics.Raycast(finder,out contact,range))
				{
					// save the contact point
					target = contact.transform.position;
				print (target);
				print (contact.transform);
					// spawn the projectile on the gun and rotate it in the direction of the contact point
					Instantiate(projectile,spawnpnt.transform.position,Quaternion.LookRotation(target));
					
				}
				else
				{
					target = finder.GetPoint(range);
					Instantiate(projectile,spawnpnt.transform.position, Quaternion.LookRotation(target));
					
				}
			}
			
			
			
		}
	}
//}

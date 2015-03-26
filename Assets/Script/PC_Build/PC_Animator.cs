using UnityEngine;
using System.Collections;

public class PC_Animator : MonoBehaviour {

	public Animator animator;

	void Update () 
	{
		if (GameInformer.stop == false)
		{
			if (Input.GetKey(GameInformer.Up)) animator.SetFloat("Speed", 5);
			else animator.SetFloat("Speed", 0);
			
			if (Input.GetKey(KeyCode.LeftShift)) animator.SetBool("Run", true);
			else animator.SetBool("Run", false);
			
			if (Input.GetKey(GameInformer.Left)) animator.SetBool("Left turn", true);
			else animator.SetBool("Left turn", false);
			
			if (Input.GetKey(GameInformer.Right)) animator.SetBool("Right turn", true);
			else animator.SetBool("Right turn", false);
			
		}else {
			animator.SetFloat("Speed", 0);
			animator.SetBool("Run", false);
			animator.SetBool("Left turn", false);
			animator.SetBool("Right turn", false);
			
		}
	}
}

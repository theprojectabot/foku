using UnityEngine;
using System.Collections;

public class Cat : MonoBehaviour
{
	public float MaxSpeed, Acceleration;
	private float speed;
	
	void Update ()
	{
		float acc = Input.GetAxis ("Horizontal") * Acceleration;
		if (acc == 0)
			acc = -Mathf.Sign (speed) * Acceleration;
		speed += acc * Time.deltaTime;
		speed = Mathf.Clamp (speed, -MaxSpeed, MaxSpeed);
		
		GetComponent<CharacterController> ().SimpleMove (Vector3.right * speed);
		animation ["Run"].speed = speed;
		animation ["Idle"].weight = 1f - Mathf.Abs (speed) / MaxSpeed;
		animation ["Run"].weight = Mathf.Abs (speed) / MaxSpeed;
		animation.Blend ("Run");
	}
}

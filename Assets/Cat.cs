using UnityEngine;
using System.Collections;

public class Cat : MonoBehaviour
{
	public float MaxSpeed, Acceleration, JumpSpeed;
	public Transform LandingDustPrefab, Body;
	private ParticleSystem FootDust;
	private float speed, verticalSpeed;
	private int direction = 1;
	private CharacterController character;
		
	void Start ()
	{
		animation ["Idle"].layer = 0;
		animation ["Run"].layer = 0;
		animation ["Jump"].layer = 1;
		animation ["Land"].layer = 1;
		
		Body.animation ["BodyIdle"].layer = 0;
		Body.animation ["BodyIdleFork"].layer = 0;
		Body.animation ["ForkTake"].layer = 2;
		Body.animation ["ForkHide"].layer = 2;
		Body.animation ["AttackSlash"].layer = 3;
		
		animation.Play ("Idle");
		Body.animation.Play ("BodyIdle");
		
		character = GetComponent<CharacterController> ();
		FootDust = transform.Find ("Foot Dust").GetComponent<ParticleSystem> ();
	}
	
	void Update ()
	{
		float acc = Input.GetAxis ("Horizontal") * Acceleration * direction;
		if (acc == 0 && speed > 0 && character.isGrounded) {
			speed -= Acceleration * 2 * Time.deltaTime;
			speed = Mathf.Clamp (speed, 0, MaxSpeed);
		} else {
			speed += acc * Time.deltaTime;
		}
		speed = Mathf.Clamp (speed, -MaxSpeed, MaxSpeed);
		
		if (speed < 0) {
			direction = -direction;
			speed = -speed;
			transform.localEulerAngles = new Vector3 (0, 180 * (direction / 2f - 0.5f), 0);
		}
		
		if (speed < 0.01f) {
			speed = 0;
		}
		
		if (Input.GetKeyDown (KeyCode.Space))
			StartCoroutine (Jump ());
		
		bool oldGrounded = character.isGrounded;
		
		Vector3 motion = transform.right * speed + transform.up * verticalSpeed;
		Vector3 oldPosition = transform.localPosition;
		CollisionFlags collFlags = character.Move (motion * Time.deltaTime);
		float dx = transform.localPosition.x - oldPosition.x;
		speed = Mathf.Abs (dx / Time.deltaTime);
		verticalSpeed = (character.isGrounded) ? -0.1f : (verticalSpeed + Physics.gravity.y * Time.deltaTime);
	
		if (character.isGrounded)
		if (speed > 0.1f)
			animation.CrossFade ("Run");
		else
			animation.CrossFade ("Idle");
		animation ["Run"].speed = speed;

		if (ForkReady)
			Body.animation.CrossFade ("BodyIdleFork");
		else
			Body.animation.CrossFade ("BodyIdle");

		
		FootDust.enableEmission = character.isGrounded && (speed > MaxSpeed * 0.75f);
		
		if (!oldGrounded && character.isGrounded) {
			animation.CrossFade ("Land");
			Instantiate (LandingDustPrefab, transform.position - Vector3.up * character.height / 2, Quaternion.identity);
		}
		
		if (Input.GetKeyDown (KeyCode.E)) 
			ToggleFork ();
		if (Input.GetKeyDown (KeyCode.RightControl)) 
			Attack ();
	}
	
	private bool ForkReady = false;

	public void ToggleFork ()
	{
		Body.animation.CrossFade (ForkReady ? "ForkHide" : "ForkTake");
		ForkReady = !ForkReady;
	}
	
	public void Attack ()
	{
		if (!ForkReady)
			ToggleFork ();
		else 
			Body.animation.CrossFade ("AttackSlash", 0.1f, PlayMode.StopSameLayer);
	}
	
	IEnumerator Jump ()
	{
		if (!character.isGrounded)
			yield break;
		animation.CrossFade ("Jump");
		yield return new WaitForSeconds(0.15f);
		verticalSpeed = JumpSpeed;
	}
}

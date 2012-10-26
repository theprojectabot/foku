using UnityEngine;
using System.Collections;

public class Cat : MonoBehaviour
{
	public float MaxSpeed, Acceleration, JumpSpeed;
	public Transform LandingDustPrefab;
	private ParticleSystem FootDust;
	private float speed, verticalSpeed;
	private int direction = 1;
	private CharacterController character;
		
	void Start ()
	{
		animation.Play ("Idle");
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
		
		
		if (Input.GetKeyDown (KeyCode.W))
			StartCoroutine (Jump ());
		
		bool oldGrounded = character.isGrounded;
		
		Vector3 motion = transform.right * speed + transform.up * verticalSpeed;
		Vector3 oldPosition = transform.localPosition;
		CollisionFlags collFlags = character.Move (motion * Time.deltaTime);
		float dx = transform.localPosition.x - oldPosition.x;
		speed = Mathf.Abs (dx / Time.deltaTime);
		verticalSpeed = (character.isGrounded) ? -0.1f : (verticalSpeed + Physics.gravity.y * Time.deltaTime);
	
		if (character.isGrounded)
		if (!animation ["Idle"].enabled)
			animation.Play ("Idle");
		animation ["Run"].speed = speed;
		animation ["Idle"].weight = (1f - Mathf.Abs (speed) / MaxSpeed) * 0.5f;
		animation ["Run"].weight = (Mathf.Abs (speed) / MaxSpeed) * 0.5f;
		animation.Blend ("Run");
		
		FootDust.enableEmission = character.isGrounded && (speed > MaxSpeed * 0.75f);
		
		Debug.Log (oldGrounded + " " + character.isGrounded);
		if (!oldGrounded && character.isGrounded) {
			animation ["Idle"].weight = 0.1f;
			animation ["Run"].weight = 0.1f;
			animation ["Land"].time = 0;
			animation ["Land"].weight = 0.5f;
			animation.Blend ("Land");
			Instantiate (LandingDustPrefab, transform.position - Vector3.up * character.height / 2, Quaternion.identity);
		}
	}
	
	IEnumerator Jump ()
	{
		if (!character.isGrounded)
			return;
		animation.Blend ("Jump");
		yield return new WaitForSeconds(0.15f);
		verticalSpeed = JumpSpeed;
	}
}

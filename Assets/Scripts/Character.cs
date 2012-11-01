using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
	public float Health, MaxHealth, HealthRegeneration;
	public Transform DeathFX;
	public float MaxSpeed, Acceleration, JumpSpeed;
	public Transform LandingDustPrefab;
	public bool ForkReady = false;
	public CatBody Body;
	public bool Confused = false;
	public float ConfusionTimeout = 0.5f;
	private ParticleSystem FootDust;
	private float speed, verticalSpeed, slideSpeed;
	public int Direction = 1;
	private CharacterController character;
	internal bool ForceBodyIdleForkAnimation = false;
	internal CollisionFlags LastCollision;
	
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
		Body.animation ["AttackSlash2"].layer = 3;
		Body.animation ["AttackPoke"].layer = 3;
		Body.animation ["AttackPoke2"].layer = 3;
		
		animation.Play ("Idle");
		Body.animation.Play ("BodyIdle");
		
		character = GetComponent<CharacterController> ();
		FootDust = transform.Find ("Foot Dust").GetComponent<ParticleSystem> ();
	}
	
	private bool movedThisFrame = false;
	
	public void Move (float acc)
	{
		if (Confused)
			return;
		movedThisFrame = acc != 0;
		acc *= Acceleration * Direction;
		
		speed += acc * Time.deltaTime;
		speed = Mathf.Clamp (speed, -1, MaxSpeed * (ForkReady ? 0.9f : 1f));
	}
	
	void LateUpdate ()
	{
		if (Health < MaxHealth)
			Health += HealthRegeneration * Time.deltaTime;
	
		if (!movedThisFrame && speed > 0 && character.isGrounded) {
			speed -= Acceleration * 2 * Time.deltaTime;
			speed = Mathf.Clamp (speed, 0, MaxSpeed);
		}
		movedThisFrame = false;
		
		if (speed < 0) {
			Direction = -Direction;
			speed = -speed;
		}
		transform.localEulerAngles = new Vector3 (0, 180 * (Direction / 2f - 0.5f), 0);
		
		if (speed < 0.01f) {
			speed = 0;
		}
		
		if (slideSpeed != 0) {
			slideSpeed -= Time.deltaTime * Acceleration * Mathf.Sign (slideSpeed);
			if (Mathf.Abs (slideSpeed) < 0.01f)
				slideSpeed = 0;
			character.Move (Vector3.right * slideSpeed * Time.deltaTime - Vector3.up);
		}
		
		bool oldGrounded = character.isGrounded;
		
		Vector3 motion = transform.right * speed + transform.up * verticalSpeed;
		Vector3 oldPosition = transform.localPosition;
		LastCollision = character.Move (motion * Time.deltaTime);
		float dx = transform.localPosition.x - oldPosition.x;
		speed = Mathf.Abs (dx / Time.deltaTime);
		verticalSpeed = (character.isGrounded) ? -0.1f : (verticalSpeed + Physics.gravity.y * Time.deltaTime);
	
		if (character.isGrounded)
		if (speed > 0.1f)
			animation.CrossFade ("Run");
		else
			animation.CrossFade ("Idle");
		animation ["Run"].speed = speed;

		if (ForkReady || ForceBodyIdleForkAnimation)
			Body.animation.CrossFade ("BodyIdleFork");
		else
			Body.animation.CrossFade ("BodyIdle");
		
		FootDust.enableEmission = character.isGrounded && (speed > MaxSpeed * 0.75f);
		
		if (!oldGrounded && character.isGrounded) {
			animation.CrossFade ("Land");
			//Instantiate (LandingDustPrefab, transform.position - Vector3.up * character.height / 2, Quaternion.identity);
		}
	}
	
	public void Backoff (float speed)
	{
		slideSpeed = -speed;
	}
	
	public void ToggleFork ()
	{
		Body.audio.clip = ForkReady ? Body.ForkTakeSound : Body.ForkHideSound;
		Body.audio.Play ();
		Body.animation.CrossFade (ForkReady ? "ForkHide" : "ForkTake");
		ForkReady = !ForkReady;
	}
	
	public void Attack (string attack)
	{
		if (Body.AttackInProgress || Confused)
			return;
		if (!ForkReady)
			ToggleFork ();
		else {
			Body.animation.CrossFade (attack, 0.1f);
		}
	}
	
	public void Jump ()
	{
		StartCoroutine (DoJump ());
	}
	
	IEnumerator DoJump ()
	{
		if (!character.isGrounded)
			yield break;
		animation.CrossFade ("Jump");
		yield return new WaitForSeconds(0.15f);
		verticalSpeed = JumpSpeed;
	}

	public void Confuse ()
	{
		StartCoroutine (DoConfuse ());
	}
	
	IEnumerator DoConfuse ()
	{
		Confused = true;
		Body.AttackInProgress = false;
		yield return new WaitForSeconds(ConfusionTimeout);
		Confused = false;
	}
	
	public void ReceiveHit (float damage)
	{
		Health -= damage;
		if (Health < 0) {
			Instantiate (DeathFX, transform.position, Quaternion.identity);
			Destroy (gameObject);
		}
		Confuse ();
		SendMessage ("OnHitReceived", SendMessageOptions.DontRequireReceiver);
	}
}

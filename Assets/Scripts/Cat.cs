using UnityEngine;
using System.Collections;

public class Cat : MonoSingleton<Cat>
{
	private string[] attacks = new string[]{
		"AttackSlash",
		"AttackPoke",
		"AttackSlash2",
		"AttackPoke2"
	};
	internal Character character;
	public Flashlight flashlight;
		
	public override void Start ()
	{
		base.Start();
		character = GetComponent<Character> ();
	}

	void Update ()
	{
		if (Input.GetKey (KeyCode.A))
			character.Move (-1);
		else if (Input.GetKey (KeyCode.D))
			character.Move (1);
		else
			character.Move (Input.GetAxis ("Horizontal"));
		
		if (Input.GetKeyDown (KeyCode.Space))
			character.Jump ();
		
		if (Input.GetKeyDown (KeyCode.B)) 
			character.Backoff (2);
		
		if (Input.GetKeyDown (KeyCode.E)) 
			character.ToggleFork ();
		
		if (Input.GetKeyDown (KeyCode.RightControl)) {
			string attack = attacks [Random.Range (0, attacks.Length)];
			character.Attack (attack);
		}
	}
	
	public void ToggleFlashlight() {
		flashlight.Toggle();
	}
	
	public void OnHitReceived ()
	{
		FX.Instance.Run ("HitReceive");
	}
	
	public void OnDidHit ()
	{
		FX.Instance.Run ("Hit");
	}
}

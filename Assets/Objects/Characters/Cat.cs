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
	private float forkHideTimeout = 0;
	public Flashlight flashlight;
	public float Rage = 0;
		
	public override void Start ()
	{
		base.Start ();
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
		
		if (Input.GetKeyDown (KeyCode.RightControl) || Input.GetKeyDown (KeyCode.LeftControl)) {
			forkHideTimeout = 5;
			string attack = attacks [Random.Range (0, attacks.Length)];
			character.Attack (attack);
		}
		
		forkHideTimeout -= Time.deltaTime;
		if (forkHideTimeout < 0 && character.ForkReady)
			character.ToggleFork ();
		
		if (Rage > 4) {
			Rage = 0;
			Catness.Instance.DoMagic ();
		}
	}
	
	public void ToggleFlashlight ()
	{
		flashlight.Toggle ();
		character.ForceBodyIdleForkAnimation = flashlight.On;
	}
	
	public void OnHitReceived ()
	{
		FX.Instance.Run ("HitReceive");
		Rage = Mathf.Max (Rage - 1, 0);
	}
	
	public void OnDidHit (MeleeWeapon weapon)
	{
		FX.Instance.Run ("Hit");
		if (weapon.GetComponent<Fireball> () == null)
			Rage += 1;
	}
	
	public void OnDied ()
	{
		enabled = false;
		StartCoroutine (DoDie ());
	}
	
	IEnumerator DoDie ()
	{
		ScreenFade.Instance.To (1);
		yield return new WaitForSeconds(2);
		GameProgress.RestartLevel ();
	}
}

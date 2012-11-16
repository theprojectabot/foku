using UnityEngine;
using System.Collections;

public class _BattleShadowMonk : MonoSingleton<_BattleShadowMonk>
{
	public Transform HitFX;
	
	public void OnTriggerEnter (Collider collider)
	{
		MeleeWeapon c = collider.GetComponent<MeleeWeapon> ();
		if (c != null && c.enabled) {
			Instantiate (HitFX, transform.position, transform.rotation);
			Cat.Instance.GetComponent<Character> ().Backoff (3);
			Cat.Instance.GetComponent<Character> ().Confuse ();
			audio.Play ();
			
			FX.Instance.Run("Hit");
			
			_BattleScript.Instance.MonkEnergy -= 0.3f;
		}
	}
	
	public override void Start ()
	{
		base.Start ();
		animation.CrossFade ("Shadow Monk Casting", 0.5f);
	}
	
	public void FlyAway ()
	{
		animation.CrossFade ("Fly Away", 0.5f);
		StartCoroutine (Do ());
	}
	
	private bool flyingAway = false;

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.M) && Application.isEditor)
			FlyAway ();
			
		if (flyingAway)
			transform.position += new Vector3 (5, 2) * Time.deltaTime * 2;
	}

	IEnumerator Do ()
	{
		yield return new WaitForSeconds(2);
		flyingAway = true;
	}
}

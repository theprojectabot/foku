using UnityEngine;
using System.Collections;

public class _BattleForkFX : MonoSingleton<_BattleForkFX>
{
	public float Amount;
	public ParticleSystem Wind;
	public Transform Portal;
	public AudioSource Sound0, Sound1;
	internal bool SuckPlayerIn = true;
	private float life0, speed0, alpha0;
	private Vector3 size0;
	private SmoothFloat amount = new SmoothFloat ();
	
	public override void Start ()
	{
		base.Start ();
		size0 = Portal.localScale;
		life0 = Wind.startLifetime;
		speed0 = Wind.startSpeed;
		alpha0 = Wind.startColor.a;
		amount.Damping = 4;
	}
	
	public void Collapse ()
	{
		amount.Damping = 0.5f;
		amount.Force (1);
		Amount = 0.1f;
	}
	
	public void Expand ()
	{
		amount.Damping = 0.5f;
		Amount = 16f;
	}

	void Update ()
	{
		amount.Update (Amount);
		
		Wind.startSpeed = speed0 * amount.Value;
		Wind.startLifetime = life0 / amount.Value;
		Wind.startColor = new Color (255, 255, 255, alpha0 * amount.Value);
		Portal.localScale = size0 * amount.Value;
		
		FX.Instance.Shaking = (amount.Value - 0.8f) * 5;
		ClothWind.SetWind (Vector3.right * 30 * amount.Value, Vector3.right * 50 * amount.Value);
		
		Sound0.volume = Mathf.Clamp01 (amount.Value - 1);
		Sound1.volume = Mathf.Clamp01 (amount.Value - 1) / 3;
		
		if (Mathf.Abs (_BattleShield.Instance.transform.position.x - Cat.Instance.transform.position.x) > 1.5f && SuckPlayerIn) {
			float d = transform.position.x - Cat.Instance.transform.position.x;
			Cat.Instance.GetComponent<CharacterController> ().Move (transform.right * Mathf.Sign (d) * Time.deltaTime * 3 * (Mathf.Clamp (amount.Value, 2, 3) - 1.8f));
		}
	}
	
	public void OnTriggerEnter (Collider collider)
	{
		Cat c = collider.GetComponent<Cat> ();
		if (c != null) {
			c.GetComponent<Character> ().ReceiveHit (110);
		}
	}
}

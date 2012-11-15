using UnityEngine;
using System.Collections;

public class _BattleForkFX : MonoSingleton<_BattleForkFX>
{
	public float Amount;
	public ParticleSystem Wind;
	public Transform Portal;
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
	
	void Update ()
	{
		amount.Update (Amount);
		
		Wind.startSpeed = speed0 * amount.Value;
		Wind.startLifetime = life0 / amount.Value;
		Wind.startColor = new Color (255, 255, 255, alpha0 * amount.Value);
		Portal.localScale = size0 * amount.Value;
		
		FX.Instance.Shaking = (amount.Value - 0.8f) * 5;
		ClothWind.SetWind (Vector3.right * 30 * amount.Value, Vector3.right * 50 * amount.Value);
		
		float d = transform.position.x - Cat.Instance.transform.position.x;
		Cat.Instance.GetComponent<CharacterController> ().Move (transform.right * Mathf.Sign (d) * Time.deltaTime * 1 * (2 * amount.Value - 0.95f));
	}
	
	public void OnTriggerEnter (Collider collider)
	{
		Cat c = collider.GetComponent<Cat> ();
		if (c != null) {
			c.GetComponent<Character> ().ReceiveHit (110);
		}
	}
}

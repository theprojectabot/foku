using UnityEngine;
using System.Collections;

public class MeleeWeapon : MonoBehaviour
{
	public string HitsTag;
	public Transform HitFX;
	public float Damage;
	public bool ReportHits = true;
	public float BackoffAmount = 3;
	internal Character owner;
		
	private Character getOwner (Transform t)
	{
		if (t == null)
			return null;
		Character cc = t.GetComponent<Character> ();
		if (cc == null)
			return getOwner (t.parent);
		return cc;
	}
	
	void Start ()
	{
		if (owner == null)
			owner = getOwner (transform);
	}
	
	public void OnTriggerEnter (Collider collider)
	{
		if (!enabled)
			return;
		Character c = collider.GetComponent<Character> ();
		if (c != null && collider.gameObject.tag == HitsTag && !collider.isTrigger) {
			c.ReceiveHit (Damage);
			c.Backoff (-BackoffAmount * Mathf.Sign (collider.transform.position.x - transform.position.x));
			Instantiate (HitFX, collider.transform.position, collider.transform.rotation);
			if (owner != null && ReportHits)
				owner.SendMessage ("OnDidHit", this, SendMessageOptions.DontRequireReceiver);
		}
	}
}

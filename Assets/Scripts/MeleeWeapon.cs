using UnityEngine;
using System.Collections;

public class MeleeWeapon : MonoBehaviour
{
	public string HitsTag;
	public Transform HitFX;
	public float Damage;
	private Character owner;
		
	private Character getOwner (Transform t)
	{
		Character cc = t.GetComponent<Character> ();
		if (cc == null)
			return getOwner (t.parent);
		return cc;
	}
	
	void Start ()
	{
		owner = getOwner (transform);
	}
	
	public void OnTriggerEnter (Collider collider)
	{
		if (!enabled)
			return;
		Character c = collider.GetComponent<Character> ();
		if (c != null && collider.gameObject.tag == HitsTag) {
			c.ReceiveHit (Damage);
			c.Backoff (-3 * Mathf.Sign (collider.transform.position.x - transform.position.x));
			Instantiate (HitFX, collider.transform.position, collider.transform.rotation);
			//Instantiate (HitFX, transform.position + ((BoxCollider)this.collider).center, collider.transform.rotation);
			owner.SendMessage ("OnDidHit", SendMessageOptions.DontRequireReceiver);
		}
	}
}

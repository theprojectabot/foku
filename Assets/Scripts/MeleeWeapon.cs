using UnityEngine;
using System.Collections;

public class MeleeWeapon : MonoBehaviour
{
	public string HitsTag;
	
	public void OnTriggerEnter (Collider collider)
	{
		Character c = collider.GetComponent<Character> ();
		if (c != null && collider.gameObject.tag == HitsTag) {
			c.ReceiveHit ();
			c.Backoff (-3 * Mathf.Sign (collider.transform.position.x - transform.position.x));
		}
	}
}

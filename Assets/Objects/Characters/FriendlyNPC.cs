using UnityEngine;
using System.Collections;

public class FriendlyNPC : MonoBehaviour
{
	public Transform Follow, Waypoint;
	public float AttackDistance = 1.2f, KeepDistance = 0.5f;
	//public string[] Attacks;
	private Transform target;
	private Character character;
	
	void OnTriggerEnter (Collider c)
	{
		//if (c.tag == "Player")
		//target = c.transform;
	}
	
	void Start ()
	{
		character = GetComponent<Character> ();
		StartCoroutine (AttackWorker ());
	}
	
	public void GoTowards (Vector3 position, float keepAway)
	{
		float dx = position.x - transform.position.x;
		if (Mathf.Abs (dx) > keepAway) {
			character.Move (Mathf.Sign (dx));
			if ((character.LastCollision & CollisionFlags.Sides) == CollisionFlags.Sides)
				character.Jump ();
		}
	}
	
	void Update ()
	{
		if (Waypoint != null) 
			GoTowards (Waypoint.position, 0.1f);
		else {
			if (target != null) {
				//if (!character.ForkReady)
				//character.ToggleFork ();
				GoTowards (target.position, KeepDistance);
			} else {
				if (Follow != null) {
					GoTowards (Follow.position, KeepDistance);
				}
			}
		}
	}

	IEnumerator AttackWorker ()
	{
		while (true) {
			while (character.Body.AttackInProgress)
				yield return new WaitForSeconds(0.1f);
			yield return new WaitForSeconds(0.8f);
		
			if (target != null) {
				//float dx = target.position.x - transform.position.x;
				//if (Mathf.Abs (dx) < AttackDistance)
				//character.Attack (Attacks [Random.Range (0, Attacks.Length)]);
			}
		}
	}
	
	public void OnHitReceived ()
	{
		target = Cat.Instance.transform;
	}
}

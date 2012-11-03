using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
	public float AttackDistance = 1.2f, KeepDistance = 0.5f;
	public string[] Attacks;
	public  Transform target;
	private Character character;
	
	void OnTriggerEnter (Collider c)
	{
		if (c.tag == "Player")
			target = c.transform;
	}
	
	void Start ()
	{
		character = GetComponent<Character> ();
		StartCoroutine (AttackWorker ());
	}
	
	void Update ()
	{
		if (target != null) {
			if (!character.ForkReady)
				character.ToggleFork ();
			float dx = target.position.x - transform.position.x;
			if (Mathf.Abs (dx) > KeepDistance)
				character.Move (Mathf.Sign (dx));
			
		}
	}
	
	IEnumerator AttackWorker ()
	{
		while (true) {
			while (character.Body.AttackInProgress)
				yield return new WaitForSeconds(0.1f);
			yield return new WaitForSeconds(0.8f);
		
			if (target != null) {
				float dx = target.position.x - transform.position.x;
				if (Mathf.Abs (dx) < AttackDistance)
					character.Attack (Attacks [Random.Range (0, Attacks.Length)]);
			}
		}
	}
	
	public void OnHitReceived ()
	{
		target = Cat.Instance.transform;
	}
}

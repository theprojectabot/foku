using UnityEngine;
using System.Collections;

public class _MorningForestScript : MonoSingleton<_MorningForestScript>
{
	public Cat Cat;
	public Transform Catness, CatnessWaypoint1, CatnessWaypoint2, CatnessWaypointTalk;
	public Transform[] SpawnPoints;
	public Transform EnemyPrefab;
	public Transform LeftLimit;
	public int EnemyCount = 10;
	
	public override	void Start ()
	{
		base.Start ();
		StartCoroutine (Do1 ());
	}
	
	IEnumerator Do1 ()
	{
		yield return new WaitForEndOfFrame();
		
		
		// Begin, cat sleeps
		Cat.enabled = false;
		Cat.GetComponent<Character> ().enabled = false;
		Cat.animation.Play ("Cat Awaking");
		yield return new WaitForEndOfFrame();
		Cat.animation.Stop ();
		
		// Catness walks by
		Catness.GetComponent<FriendlyNPC> ().Waypoint = CatnessWaypoint1;
		yield return new WaitForSeconds(2);
		
		Dialogue.Instance.Say (Catness, "They're preparing to assault the house!", 3);
		
		Catness.GetComponent<Character> ().ForceBodyIdleForkAnimation = true;
		yield return new WaitForSeconds(2);
		
		Cat.animation.Play ("Cat Awaking");
		yield return new WaitForSeconds(4);
		Cat.enabled = true;
		Cat.GetComponent<Character> ().enabled = true;

		Dialogue.Instance.Say (Catness, "Monks are approaching from both sides.\nGet them before they reach us!", 4);
		
		Catness.GetComponent<FriendlyNPC> ().Waypoint = null;
		Catness.GetComponent<FriendlyNPC> ().Follow = Cat.transform;
		
		// Spawning
		for (int i =0; i < EnemyCount; i++) {
			Transform e = Instantiate (EnemyPrefab, SpawnPoints [Random.Range (0, SpawnPoints.Length)].position, Quaternion.identity) as Transform;
			e.GetComponent<Enemy> ().target = Cat.Instance.transform;
			yield return new WaitForSeconds(Random.Range(5,10));
		}
		
		while (FindSceneObjectsOfType(typeof(Enemy)).Length > 0)
			yield return new WaitForSeconds(1);
		yield return new WaitForSeconds(3);
		
		Dialogue.Instance.Say (Catness, "That was the last one I guess.", 3);
		yield return new WaitForSeconds(4);
		Dialogue.Instance.Say (Catness, "We need to get to the mountain peak to the west.", 5);
		yield return new WaitForSeconds(6);
		Dialogue.Instance.Say (Catness, "From up there, I can use my powers\nto locate the Fork's energy.", 3);
		yield return new WaitForSeconds(4);
		Dialogue.Instance.Say (Catness, "But we better hurry up if you want\nto be there before the sunset.", 3);
		
		Destroy (LeftLimit.gameObject);
	}
}

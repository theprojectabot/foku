using UnityEngine;
using System.Collections;

public class _ForestScript : MonoSingleton<_ForestScript>
{
	public Cat Cat;
	public Transform Catness, CatnessWaypoint1, CatnessWaypoint2, Fire;
	
	public override	void Start ()
	{
		base.Start ();
		//StartCoroutine (Do1 ());
	}
	
	IEnumerator Do1 ()
	{
		yield return new WaitForEndOfFrame();
	}
	
	public IEnumerator Do2 ()
	{
		Catness.GetComponentInChildren<Flashlight> ().On = false;
		Catness.GetComponent<Character> ().MaxSpeed = 0.9f;
		Catness.GetComponent<FriendlyNPC> ().Waypoint = CatnessWaypoint1;
		
		// Cat waits
		Cat.enabled = false;
		Cat.GetComponent<Character> ().enabled = false;
		Cat.animation.Stop ();
		Cat.character.Body.animation.Stop ();
		
		// Wait for Catness
		yield return new WaitForSeconds(4);
		
		// Catness lights fire
		Catness.GetComponentInChildren<Flashlight> ().On = true;
		yield return new WaitForSeconds(1);
		Fire.gameObject.SetActiveRecursively (true);
		yield return new WaitForSeconds(1);
		Catness.GetComponentInChildren<Flashlight> ().On = false;
		
		Cat.GetComponentInChildren<Flashlight> ().On = false;
		
		// Catness speaks
		yield return new WaitForSeconds(2);
		
		// Catness walks away
		Catness.GetComponent<Character> ().ForceBodyIdleForkAnimation = false;
		Catness.GetComponent<FriendlyNPC> ().Waypoint = CatnessWaypoint2;
		
		// Cat sleeps
		Cat.animation ["Cat Awaking"].speed = -1;
		Cat.animation ["Cat Awaking"].time = Cat.animation ["Cat Awaking"].length;
		Cat.animation.Play ("Cat Awaking");
		yield return new WaitForSeconds(5);
		
		
	}
}

using UnityEngine;
using System.Collections;

public class _ForestScript : MonoSingleton<_ForestScript>
{
	public Cat Cat;
	public Transform Catness, CatnessWaypoint1, CatnessWaypoint2, CatnessWaypointTalk, Fire;
	
	public override	void Start ()
	{
		base.Start ();
		StartCoroutine (Do1 ());
	}
	
	IEnumerator Do1 ()
	{
		yield return new WaitForEndOfFrame();
		yield return new WaitForSeconds(1);
		Dialogue.Instance.Say (Catness, "Watch out", 2);
		yield return new WaitForSeconds(3);
		Dialogue.Instance.Say (Catness, "By now, Shadow monks probably\nknow where we are", 3.5f);
		yield return new WaitForSeconds(4);
		Dialogue.Instance.Say (Catness, "This shield should give us\nsome protection", 3f);
		yield return new WaitForSeconds(1);
		Catness.GetComponent<Catness> ().DoShield ();
		
		yield return new WaitForSeconds(3);
		Dialogue.Instance.Say (null, "Press Right Ctrl to draw the weapon and attack.", 5, 1.5f);
	}
	
	public IEnumerator Do2 ()
	{
		if (Cat.GetComponent<Character> ().ForkReady)
			Cat.GetComponent<Character> ().ToggleFork ();
		
		Catness.GetComponentInChildren<Flashlight> ().On = false;
		Catness.GetComponent<Character> ().MaxSpeed = 0.9f;
		Catness.GetComponent<FriendlyNPC> ().Waypoint = CatnessWaypoint1;
		
		Dialogue.Instance.Say (Catness, "That's the safe house? Meh...", 3);
		
		// Cat waits
		Cat.enabled = false;
		Cat.animation.Stop ();
		Cat.character.Body.animation.Stop ();
		
		// Wait for Catness
		yield return new WaitForSeconds(4);
		Dialogue.Instance.Say (Catness, "We need to get to the\ntop of the mountain.", 3);
		
		// Catness lights fire
		Catness.GetComponentInChildren<Flashlight> ().On = true;
		yield return new WaitForSeconds(1);
		Fire.gameObject.SetActive (true);
		yield return new WaitForSeconds(1);
		Catness.GetComponentInChildren<Flashlight> ().On = false;
		yield return new WaitForSeconds(2);
		
		Cat.GetComponentInChildren<Flashlight> ().On = false;
		
		Dialogue.Instance.Say (Catness, "From there, I can use my\npowers to locate the Fork.", 3);
		yield return new WaitForSeconds(4);
		
		
		// Catness walks
		Catness.GetComponent<FriendlyNPC> ().Waypoint = CatnessWaypointTalk;
		yield return new WaitForSeconds(2);

		// Catness speaks
		Dialogue.Instance.Say (Catness, "Get some sleep, I will\nguard the outside.", 3);
		yield return new WaitForSeconds(2);
		
		// Catness walks away
		Catness.GetComponent<Character> ().ForceBodyIdleForkAnimation = false;
		Catness.GetComponent<FriendlyNPC> ().Waypoint = CatnessWaypoint2;
		
		// Cat sleeps
		Cat.GetComponent<Character> ().ForceBodyIdleForkAnimation = false;
		Cat.animation ["Cat Awaking"].speed = -1;
		Cat.animation ["Cat Awaking"].time = Cat.animation ["Cat Awaking"].length;
		Cat.animation.Play ("Cat Awaking");
		yield return new WaitForSeconds(5);
		
		// Done here
		ScreenFade.Instance.To (1);
		yield return new WaitForSeconds(2);
		GameProgress.GoToLevel ("Forest Morning");
	}
}


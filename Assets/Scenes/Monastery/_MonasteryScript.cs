using UnityEngine;
using System.Collections;

public class _MonasteryScript : MonoSingleton<_MonasteryScript>
{
	public Cat Cat;
	public Transform Catness, MainMonk, CatnessWaypoint1, Guard, GuardWaypoint;
	
	public override	void Start ()
	{
		base.Start ();
		StartCoroutine (Do1 ());
	}
	
	IEnumerator Do1 ()
	{
		yield return new WaitForEndOfFrame();
		
		Catness.GetComponentInChildren<Flashlight> ().On = false;
		Catness.GetComponent<Character> ().MaxSpeed = 0.9f;
		
		// Begin, cat sleeps
		Cat.enabled = false;
		Cat.GetComponent<Character> ().enabled = false;
		Cat.GetComponent<Character> ().MaxSpeed = 0.9f;
		Cat.GetComponentInChildren<Flashlight> ().On = false;
		Cat.animation.Play ("Cat Awaking");
		yield return new WaitForEndOfFrame();
		Cat.animation.Stop ();
		
		// Catness walks by
		Catness.GetComponent<FriendlyNPC> ().Waypoint = CatnessWaypoint1;
		yield return new WaitForSeconds(2);
		Dialogue.Instance.Say (Catness, "Hey! Wake up!", 3);
		
		Catness.GetComponent<Character> ().ForceBodyIdleForkAnimation = true;
		Catness.GetComponentInChildren<Flashlight> ().On = true;
		yield return new WaitForSeconds(2);
		
		Cat.animation.Play ("Cat Awaking");
		yield return new WaitForSeconds(2);
		Dialogue.Instance.Say (Cat.Instance.transform, "Uh... What?", 1);
		yield return new WaitForSeconds(1.5f);
		Dialogue.Instance.Say (Catness, "Shh, dont wake the others!", 2);
		yield return new WaitForSeconds(3);
		Dialogue.Instance.Say (Catness, "Master wants to speak to you", 2);
		yield return new WaitForSeconds(3);
		Dialogue.Instance.Say (Catness, "...and it's urgent.", 2);
		
		Cat.enabled = true;
		Cat.GetComponent<Character> ().enabled = true;
		Cat.GetComponentInChildren<Flashlight> ().On = true;
		Catness.GetComponentInChildren<Flashlight> ().On = false;
		
		Catness.GetComponent<FriendlyNPC> ().Waypoint = null;
		Catness.GetComponent<FriendlyNPC> ().Follow = Cat.transform;
		
		// Going out
	}
		
	public IEnumerator Do2 ()
	{
		// Guard ahead
		Guard.GetComponent<FriendlyNPC> ().Waypoint = GuardWaypoint;
		Cat.GetComponent<Cat> ().enabled = false;
		
		yield return new WaitForSeconds(5);
		
		Cat.GetComponent<Cat> ().enabled = true;
		Catness.GetComponent<Character> ().MaxSpeed = 1.2f;
		Cat.GetComponent<Character> ().MaxSpeed = 1.2f;
	}
}

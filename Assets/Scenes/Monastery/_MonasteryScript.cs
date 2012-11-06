using UnityEngine;
using System.Collections;

public class _MonasteryScript : MonoSingleton<_MonasteryScript>
{
	public Cat Cat;
	public Transform Catness, MainMonk, CatnessWaypoint1, Guard, GuardWaypoint, StoryBarrier1;
	
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
	
	public IEnumerator DoMonkStory ()
	{
		Cat.GetComponent<Cat> ().enabled = false;
		yield return new WaitForSeconds(3);
		Dialogue.Instance.Say (MainMonk, "I'll have to keep it short.", 3);
		yield return new WaitForSeconds(4);
		Dialogue.Instance.Say (MainMonk, "The monks of Shadow Monastery\nhave stolen the Great Fork.", 3);
		yield return new WaitForSeconds(4);
		Dialogue.Instance.Say (MainMonk, "A friend has warned me. In a few minutes,\nguards will swarm in and accuse us of robbery.", 4);
		yield return new WaitForSeconds(5);
		Dialogue.Instance.Say (MainMonk, "Posession of the Fork enables Shadow monks\nto open portals into the Void", 3);
		yield return new WaitForSeconds(4);
		Dialogue.Instance.Say (MainMonk, "And this is not something I can let happen.", 2);
		yield return new WaitForSeconds(3);
		Dialogue.Instance.Say (MainMonk, "Now you must go and stop Shadow monks\nbefore guards catch you.", 3);
		yield return new WaitForSeconds(4);
		Dialogue.Instance.Say (MainMonk, "The back door is open. Go past the guard tower\nand reach the safe house in the forest.", 4);
		yield return new WaitForSeconds(4);
		Dialogue.Instance.Say (Catness, "But what about you? You can't stay!\nThey'll get you too.", 2);
		yield return new WaitForSeconds(2);
		Dialogue.Instance.Say (MainMonk, "Then I'll have to think of something...", 2);
		yield return new WaitForSeconds(3);
		Dialogue.Instance.Say (MainMonk, "Now go!", 2);
		Cat.GetComponent<Cat> ().enabled = true;
		Catness.GetComponent<Character> ().MaxSpeed = 1f;
		Cat.GetComponent<Character> ().MaxSpeed = 1f;
		Destroy (StoryBarrier1.gameObject);
	}
		
	public IEnumerator Do2 ()
	{
		// Guard ahead
		Guard.GetComponent<FriendlyNPC> ().Waypoint = GuardWaypoint;
		Cat.GetComponent<Cat> ().enabled = false;
		Dialogue.Instance.Say (Catness, "There's a guard ahead!", 2);
		
		yield return new WaitForSeconds(7);
		
		Dialogue.Instance.Say (Catness, "Run!", 2);
		Cat.GetComponent<Cat> ().enabled = true;
		Catness.GetComponent<Character> ().MaxSpeed = 1.4f;
		Cat.GetComponent<Character> ().MaxSpeed = 1.4f;
	}
}

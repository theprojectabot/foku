using UnityEngine;
using System.Collections;

public class _MountainSceneTrigger : MonoBehaviour
{
	public GameObject Spell;
	public Transform CatnessWaypoint;
	
	public void OnTriggerEnter (Collider collider)
	{
		Cat c = collider.GetComponent<Cat> ();
		if (c != null) {
			c.enabled = false;
			StartCoroutine (Do ());
		}
	}
	
	IEnumerator Do ()
	{
		Catness.Instance.GetComponent<Character> ().MaxSpeed = 0.8f;
		Catness.Instance.GetComponent<FriendlyNPC> ().Waypoint = CatnessWaypoint;
		Dialogue.Instance.Say (Catness.Instance.transform, "Ok, that's it.", 3);
		yield return new WaitForSeconds(3);
		Catness.Instance.GetComponent<Character> ().enabled = false;
		Catness.Instance.animation.CrossFade ("Sit Down");
		yield return new WaitForSeconds(3);
		Dialogue.Instance.Say (Catness.Instance.transform, "Now just concentrate like this...", 2);
		yield return new WaitForSeconds(2);
		Spell.SetActive (true);
		yield return new WaitForSeconds(1);
		Dialogue.Instance.Say (Catness.Instance.transform, "Yay!", 2);
		yield return new WaitForSeconds(3);
		Dialogue.Instance.Say (Catness.Instance.transform, "It's working!", 2);
		
		yield return new WaitForSeconds(3);
		Dialogue.Instance.Say (Cat.Instance.transform, "Is it going to show us the direction?", 3);
		yield return new WaitForSeconds(4);
		Dialogue.Instance.Say (Cat.Instance.transform, "Any giant glowing magic arrows maybe?", 3);
		yield return new WaitForSeconds(3);
		Dialogue.Instance.Say (Catness.Instance.transform, "It's difficult enough\neven without your whining", 3);
		yield return new WaitForSeconds(4);
		Dialogue.Instance.Say (Catness.Instance.transform, "Just sit down and wait", 3);
		yield return new WaitForSeconds(2);
		
		Cat.Instance.GetComponent<Character> ().Direction = -1;
		yield return new WaitForSeconds(2);
		Cat.Instance.GetComponent<Character> ().enabled = false;
		Cat.Instance.animation.CrossFade ("Sit Down");
		yield return new WaitForSeconds(4);
		
		
		ScreenFade.Instance.To (1);
		yield return new WaitForSeconds(2);
		GameProgress.GoToLevel ("Downhill");
	}
}

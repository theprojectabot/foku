using UnityEngine;
using System.Collections;

public class Level1Script : MonoBehaviour
{
	public Cat Cat;
	public Transform Catness, MainMonk;
	
	void Start ()
	{
		StartCoroutine (Do1 ());
	}
	
	IEnumerator Do1 ()
	{
		yield return new WaitForEndOfFrame();
		
		Catness.GetComponentInChildren<Flashlight> ().On = false;
		
		Cat.enabled = false;
		Cat.GetComponent<Character> ().enabled = false;
		Cat.GetComponentInChildren<Flashlight> ().On = false;
		Cat.animation.Play ("Cat Awaking");
		yield return new WaitForEndOfFrame();
		Cat.animation.Stop ();
		
		yield return new WaitForSeconds(2);
		Catness.GetComponent<Character> ().ForceBodyIdleForkAnimation = true;
		Catness.GetComponentInChildren<Flashlight> ().On = true;
		yield return new WaitForSeconds(2);
		
		Cat.animation.Play ("Cat Awaking");
		yield return new WaitForSeconds(5);
		Cat.enabled = true;
		Cat.GetComponent<Character> ().enabled = true;
		Cat.GetComponentInChildren<Flashlight> ().On = true;
		
	}
}

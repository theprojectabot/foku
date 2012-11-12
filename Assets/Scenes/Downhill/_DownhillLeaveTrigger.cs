using UnityEngine;
using System.Collections;

public class _DownhillLeaveTrigger : MonoBehaviour
{
	public void OnTriggerEnter (Collider collider)
	{
		Cat c = collider.GetComponent<Cat> ();
		if (c != null)
			StartCoroutine (Do ());
	}
	
	IEnumerator Do ()
	{
		Cat.Instance.enabled = false;
		yield return new WaitForSeconds(1);
		Cat.Instance.GetComponent<Character> ().enabled = false;
		Cat.Instance.GetComponent<Character> ().Body.animation.Stop ();
		Cat.Instance.animation.CrossFade ("Climb");
		yield return new WaitForSeconds(2);
		ScreenFade.Instance.To (1);
		yield return new WaitForSeconds(2);
		GameProgress.GoToLevel ("Evening");
	}
}

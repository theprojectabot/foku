using UnityEngine;
using System.Collections;

public class _MonasteryLeaveTrigger : MonoBehaviour
{
	public void OnTriggerEnter (Collider collider)
	{
		Cat c = collider.GetComponent<Cat> ();
		if (c != null) {
			StartCoroutine (Do ());
		}
	}
	
	IEnumerator Do ()
	{
		ScreenFade.Instance.To (1);
		yield return new WaitForSeconds(2);
		GameProgress.GoToLevel ("Forest");
	}
}

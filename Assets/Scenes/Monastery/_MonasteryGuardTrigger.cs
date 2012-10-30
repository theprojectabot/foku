using UnityEngine;
using System.Collections;

public class _MonasteryGuardTrigger : MonoBehaviour
{
	public void OnTriggerEnter (Collider collider)
	{
		Cat c = collider.GetComponent<Cat> ();
		if (c != null)
			_MonasteryScript.Instance.StartCoroutine (_MonasteryScript.Instance.Do2 ());
	}
}

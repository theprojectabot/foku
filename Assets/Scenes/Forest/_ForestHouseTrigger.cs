using UnityEngine;
using System.Collections;

public class _ForestHouseTrigger : MonoBehaviour
{
	public void OnTriggerEnter (Collider collider)
	{
		Cat c = collider.GetComponent<Cat> ();
		if (c != null)
			_ForestScript.Instance.StartCoroutine (_ForestScript.Instance.Do2 ());
	}
}

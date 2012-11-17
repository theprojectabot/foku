using UnityEngine;
using System.Collections;

public class NoRigidsForMobile : MonoBehaviour
{
	void Start ()
	{
		if (Application.platform == RuntimePlatform.Android) {
			foreach (Joint r in GetComponentsInChildren<Joint>())
				Destroy (r);
			foreach (Rigidbody r in GetComponentsInChildren<Rigidbody>())
				Destroy (r);
		}
	}
	
}

using UnityEngine;
using System.Collections;

public class NotForMobile : MonoBehaviour
{
	void Start ()
	{
		if (Application.platform == RuntimePlatform.Android)
			Destroy (gameObject);
	}
}

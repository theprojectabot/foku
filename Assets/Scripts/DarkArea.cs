using UnityEngine;
using System.Collections;

public class DarkArea : MonoBehaviour
{
	public void OnTriggerEnter (Collider collider)
	{
		Cat c = collider.GetComponent<Cat> ();
		if (c != null) 
			c.flashlight.On = true;
	}
	
	public void OnTriggerExit (Collider collider)
	{
		Cat c = collider.GetComponent<Cat> ();
		if (c != null) 
			c.flashlight.On = false;
	}
}

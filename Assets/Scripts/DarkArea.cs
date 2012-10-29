using UnityEngine;
using System.Collections;

public class DarkArea : MonoBehaviour
{
	public void OnTriggerStay (Collider collider)
	{
		Cat c = collider.GetComponent<Cat> ();
		if (c != null && !c.flashlight.On)  
			c.ToggleFlashlight ();
	}
	
	public void OnTriggerExit (Collider collider)
	{
		Cat c = collider.GetComponent<Cat> ();
		if (c != null && c.flashlight.On) 
			c.ToggleFlashlight ();
	}
}

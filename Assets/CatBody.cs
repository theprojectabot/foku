using UnityEngine;
using System.Collections;

public class CatBody : MonoBehaviour
{
	public Transform Fork, ForkHandle, ForkSlot;
	
	public void OnForkAttach ()
	{
		Fork.parent = ForkHandle;
	}

	public void OnForkDetach ()
	{
		Fork.parent = ForkSlot;
	}
}

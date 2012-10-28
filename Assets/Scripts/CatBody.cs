using UnityEngine;
using System.Collections;

public class CatBody : MonoBehaviour
{
	public Transform Fork, ForkHandle, ForkSlot;
	private MeleeWeapon weapon = null;

	void Start ()
	{
		weapon = Fork.GetComponent<MeleeWeapon> ();
	}
	
	public void OnForkAttach ()
	{
		Fork.parent = ForkHandle;
	}

	public void OnForkDetach ()
	{
		Fork.parent = ForkSlot;
	}
	
	public void AlignFork ()
	{
		Fork.transform.localPosition = Vector3.zero;
		Fork.transform.localRotation = Quaternion.identity;
	}
	
	public bool AttackInProgress = false;

	void Update ()
	{
		weapon.enabled = AttackInProgress;
	}
}

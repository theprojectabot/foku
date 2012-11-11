using UnityEngine;
using System.Collections;

public class Catness : MonoSingleton<Catness>
{
	public Transform Fireball;
	public Transform Shield;
	public Transform Push;
	
	public override void Start ()
	{
		base.Start ();
		_<Character> ().ForceBodyIdleForkAnimation = true;
		_<FriendlyNPC> ().Follow = Cat.Instance.transform;
	}
	
	void Update ()
	{
		if (Application.isEditor && Input.GetKeyDown (KeyCode.F))
			DoFireball ();
		if (Application.isEditor && Input.GetKeyDown (KeyCode.G))
			DoPush ();
	}
	
	public void DoMagic ()
	{
		int r = Random.Range (0, 3);
		if (r == 0)
			DoFireball ();
		if (r == 1)
			DoShield ();
		if (r == 2)
			DoPush ();
	}
	
	public void DoFireball ()
	{
		Spawn (Fireball).GetComponent<MeleeWeapon> ().owner = Cat.Instance._<Character> ();
	}

	public void DoShield ()
	{
		Spawn (Shield).parent = transform;
	}

	public void DoPush ()
	{
		Spawn (Push).parent = transform;
	}

	public Transform Spawn (Transform prefab)
	{ 
		return ((Transform)Instantiate (prefab, transform.position, transform.rotation));
	}
}

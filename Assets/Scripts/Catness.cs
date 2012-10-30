using UnityEngine;
using System.Collections;

public class Catness : MonoSingleton<Catness>
{
	public Transform Fireball;
	public Transform Shield;
	
	public override void Start ()
	{
		base.Start ();
		_<FriendlyNPC> ().Follow = Cat.Instance.transform;
	}
	
	public void DoMagic ()
	{
		int r = Random.Range (0, 2);
		if (r == 0)
			DoFireball ();
		if (r == 1)
			DoShield ();
	}
	
	public void DoFireball ()
	{
		Spawn (Fireball).GetComponent<MeleeWeapon> ().owner = Cat.Instance._<Character> ();
	}

	public void DoShield ()
	{
		Spawn (Shield).parent = transform;
	}
	
	public Transform Spawn (Transform prefab)
	{ 
		return ((Transform)Instantiate (prefab, transform.position, transform.rotation));
	}
}

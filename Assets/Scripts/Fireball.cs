using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour
{
	public float Speed;
	
	void Update ()
	{
		transform.position += transform.right * Speed * Time.deltaTime;
	}
	
	void Start ()
	{
		Invoke ("End", 2);
	}
	
	public void End ()
	{
		GetComponent<MeleeWeapon> ().enabled = false;
	}
}

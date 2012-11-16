using UnityEngine;
using System.Collections;

public class Cam : MonoSingleton<Cam>
{
	public float FollowCoeff = 1;
	public Transform Target;
	
	void Update ()
	{
		Vector3 d = Target.position - transform.position;
		d.z = 0;
		transform.position += d * FollowCoeff;
	}
}

using UnityEngine;
using System.Collections;

public class RelativeFollow : MonoBehaviour
{
	public Transform Target;
	private Vector3 diff;
	
	void Start ()
	{
		diff = Target.position - transform.position;
	}
	
	void Update ()
	{
		transform.position = Target.position - diff;
	}
}

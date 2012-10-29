using UnityEngine;
using System.Collections;

public class ForkLight : MonoBase
{
	private Vector3 lastPosition;
	private SmoothFloat emission = new SmoothFloat ();
	
	public override void Start ()
	{
		base.Start ();
		emission.Damping = 0.1f;
	}
	
	void Update ()
	{
		float d = (transform.position - lastPosition).magnitude / Time.deltaTime;
		emission.Update (Mathf.Clamp (d, 0, 8) * 2);
		light.intensity = emission.Value;
		lastPosition = transform.position;
	}
}

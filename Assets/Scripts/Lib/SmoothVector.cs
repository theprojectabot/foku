using System;
using UnityEngine;

public class SmoothVector
{
	internal SmoothFloat x, y, z;

	public SmoothVector () : this(false)
	{
	}

	public SmoothVector (bool rt)
	{
		x = new SmoothFloat (rt);
		y = new SmoothFloat (rt);
		z = new SmoothFloat (rt);
	}

	public void Update (Vector3 v)
	{
		x.Update (v.x);
		y.Update (v.y);
		z.Update (v.z);
	}

	public void Force (Vector3 v)
	{
		x.Force (v.x);
		y.Force (v.y);
		z.Force (v.z);
	}

	public Vector3 Value {
		get { return new Vector3 (x.Value, y.Value, z.Value); }
	}

	public float Damping {
		set {
			x.Damping = value;
			y.Damping = value;
			z.Damping = value;
		}
	}
	
	public bool AngularMode {
		set {
			x.AngularMode = value;
			y.AngularMode = value;
			z.AngularMode = value;
		}
	}
}


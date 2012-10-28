using System;
using UnityEngine;

public class SmoothFloat
{
	public float Value {
		get { return current; }
	}

	public float Damping = 0.5f;
	public bool IsRealtime = false;
	public bool AngularMode = false;
	public bool IsFixedTime = false;
	public float FixationTreshold = 0;
	protected float speed;
	protected float current;

	public SmoothFloat ()
	{
	}

	public SmoothFloat (bool rt)
	{
		IsRealtime = rt;
	}

	public void Force (float target)
	{
		speed = 0;
		current = target;
	}

	public virtual float Update (float target)
	{
		float t = IsFixedTime ? Time.fixedDeltaTime : (IsRealtime ? Realtime.deltaTime : Time.deltaTime);
		if (AngularMode)
			current = Mathf.SmoothDampAngle (current, target, ref speed, Damping, 9000, t);
		else
			current = Mathf.SmoothDamp (current, target, ref speed, Damping, 9000, t);
		if (Mathf.Abs (current - target) < FixationTreshold)
			current = target;
		return current;
	}
}


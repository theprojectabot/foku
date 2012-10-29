using UnityEngine;
using System.Collections;

public class Flashlight : MonoBase
{
	public Light Light, Flare;
	public ParticleSystem Particles;
	public bool On = false;
	private SmoothFloat emission = new SmoothFloat ();
	
	public override void Start ()
	{
		base.Start ();
		emission.Damping = 0.1f;
	}
	
	public void Toggle ()
	{
		On = !On;
	}
	
	void Update ()
	{
		emission.Update (On ? 1 : 0);
		Light.intensity = 8 * emission.Value;
		Flare.intensity = 0.5f * emission.Value;
		Particles.enableEmission = On;
	}
}

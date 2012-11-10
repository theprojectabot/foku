using UnityEngine;
using System.Collections;

public class ScreenFade : MonoSingleton<ScreenFade>
{
	private SmoothFloat alpha = new SmoothFloat ();
	public float target = 0;
	
	public override void Start ()
	{
		base.Start ();
		alpha.Damping = 1.5f;
		alpha.Force (1);
	}
	
	void Update ()
	{
		alpha.Update (target);
		renderer.material.SetColor ("_TintColor", new Color (0, 0, 0, alpha.Value));
		renderer.enabled = (alpha.Value > 0.01f);
	}
	
	public void To (float t)
	{
		target = t;		
		foreach (Sound s in FindSceneObjectsOfType(typeof(Sound)))
			s.FadeTo = 1 - t;
	}
}

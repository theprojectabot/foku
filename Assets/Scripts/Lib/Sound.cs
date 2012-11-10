using UnityEngine;
using System.Collections;

public class Sound : MonoBase
{
	public AudioClip[] Clips;
	public bool Autostart;
	public bool Fade = true;
	public bool Loop = false;
	private SmoothFloat volume = new SmoothFloat ();
	private float volume0;
	internal float FadeTo = 1;
	
	public override void Start ()
	{
		base.Start ();
		volume.Damping = 1f;
		if (audio == null)
			gameObject.AddComponent<AudioSource> ();
		audio.clip = Clips [Random.Range (0, Clips.Length)];
		audio.loop = Loop;
		volume0 = audio.volume;
		if (Fade)
			audio.volume = 0;
		if (Autostart)
			audio.Play ();
	}
	
	void Update ()
	{
		volume.Update (FadeTo);
		if (Fade)
			audio.volume = volume.Value * volume0; 	
	}
}

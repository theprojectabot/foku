using UnityEngine;
using System.Collections;

public class Sound : MonoBase
{
	public AudioClip[] Clips;
	public bool Autostart;
	public bool Fade = true;
	public bool Loop = false;
	public float Volume = 1, Pitch = 1;
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
		audio.pitch = Pitch;
		volume0 = audio.volume;
		if (Fade)
			audio.volume = 0;
		else
			volume.Force (1);
		if (Autostart)
			audio.Play ();
	}
	
	void Update ()
	{
		if (Fade)
			volume.Update (FadeTo);
		audio.volume = volume.Value * volume0 * Volume; 	
	}
}

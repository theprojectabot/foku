using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Audiobox : MonoBehaviour
{
	public AudioClip[] Clips;
	
	public void Play (string name)
	{
		for (int i =0; i < 100; i++) {
			AudioClip clip = Clips [Random.Range (0, Clips.Length)];
			if (clip.name.StartsWith (name)) {
				audio.clip = clip;
				audio.Play ();
				return;
			}
		}
	}
}

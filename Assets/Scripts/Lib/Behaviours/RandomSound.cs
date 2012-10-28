using UnityEngine;
using System.Collections;

[AddComponentMenu("Audio/Random Sound")]
public class RandomSound : MonoBehaviour
{
	public AudioClip[] Sounds;

	void Awake ()
	{
		audio.playOnAwake = false;
		audio.PlayOneShot (Sounds[Random.Range (0, Sounds.Length)]);
	}
}

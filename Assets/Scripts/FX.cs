using UnityEngine;
using System.Collections.Generic;

public class FX : MonoSingleton<FX>
{
	public float TimeScale = 1;
	private List<string> clips;
	
	void Start ()
	{
		clips = new List<string> ();
		foreach (AnimationState s in animation)
			clips.Add (s.name);
	}
	
	public void Run (string fx)
	{
		//if (!animation.isPlaying) {
		while (true) {
			int idx = Random.Range (0, clips.Count);
			if (clips [idx].StartsWith (fx)) {
				animation.CrossFade (clips[idx], 0.5f);
				return;
			}
		}
		//}
	}
	
	void Update ()
	{
		Realtime.SetTimeScale (TimeScale, 0);
	}
}

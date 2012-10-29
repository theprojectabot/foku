using UnityEngine;
using System.Collections.Generic;

public class FX : MonoSingleton<FX>
{
	public float TimeScale = 1;
	public float DamageFX = 0;
	public Vignetting VignetteFX;
	private List<string> clips;
	
	public override void Start ()
	{
		base.Start();
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
				animation.CrossFade (clips [idx], 0.5f);
				return;
			}
		}
		//}
	}
	
	void Update ()
	{
		Realtime.SetTimeScale (TimeScale, 0);
		
		float health = 1 - Cat.Instance.character.Health / Cat.Instance.character.MaxHealth;
		VignetteFX.blur = 4 * health;
		VignetteFX.intensity = 8 * health;
	}
}

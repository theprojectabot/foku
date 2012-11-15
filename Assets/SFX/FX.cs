using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FX : MonoSingleton<FX>
{
	public float TimeScale = 1;
	public float DamageFX = 0;
	public float Shaking = 0;
	public Vignetting VignetteFX;
	private List<string> clips;
	private Vector3 targetShake;
	private SmoothVector shakeOffset = new SmoothVector ();
	
	public override void Start ()
	{
		base.Start ();
		clips = new List<string> ();
		foreach (AnimationState s in animation)
			clips.Add (s.name);
		shakeOffset.Damping = 0.1f;
		StartCoroutine (Shaker ());
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
		
		if (Cat.Instance != null) {
			float health = 1 - Cat.Instance.character.Health / Cat.Instance.character.MaxHealth;
			VignetteFX.blur = 4 * health;
			VignetteFX.intensity = 8 * health;
		}
		
		shakeOffset.Update (targetShake);
		transform.position += shakeOffset.Value * Time.deltaTime;
	}
	
	IEnumerator Shaker ()
	{
		while (true) {
			yield return new WaitForSeconds(Random.Range(0.1f,0.2f));
			targetShake = Random.onUnitSphere * Shaking;
			targetShake.z = 0;
		}
	}
}

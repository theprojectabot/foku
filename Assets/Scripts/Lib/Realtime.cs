using UnityEngine;

public class Realtime : MonoSingleton<Realtime>
{
	public static float deltaTime;
	public bool AffectSoundPitch = true;
	private static float lastTime;
	// Scale
	private SmoothFloat timeScale = new SmoothFloat (true);
	private float targetTimeScale = 1;
	private Object[] audioSources = new Object[0];
	private static float OldFixedDeltaTime;
	private static bool Saved = false;

	void LateUpdate ()
	{
		if (!Saved) {
			OldFixedDeltaTime = Time.fixedDeltaTime;
			Saved = true;
		}
		
		deltaTime = Time.realtimeSinceStartup - lastTime;
		lastTime = Time.realtimeSinceStartup;
		
		// Scale
		timeScale.Update (targetTimeScale);
		Time.fixedDeltaTime = OldFixedDeltaTime * timeScale.Value;
		Time.timeScale = timeScale.Value;
		if (AffectSoundPitch)
		if (Mathf.Abs (timeScale.Value - targetTimeScale) > 0.0000001f)
			foreach (AudioSource a in audioSources)
				if (a != null)
					a.pitch = timeScale.Value;
	}
	
	internal void RescanAudioSources ()
	{
		audioSources = FindObjectsOfType (typeof(AudioSource));
	}
	
	public static void SetTimeScale (float scale)
	{
		SetTimeScale (scale, 0.5f);
	}

	public static void SetTimeScale (float scale, float damping)
	{
		Instance.timeScale.Damping = damping;
		Instance.targetTimeScale = scale;
		Instance.RescanAudioSources ();
	}

	public static float TimeScale{ get { return Instance.timeScale.Value; } }
}


using UnityEngine;
using System.Collections;

public class MenuWeather : MonoBehaviour
{
	public float DayProgress, DaySpeed;
	public Transform Sun;
	public Animation WindAnimation;
	public Renderer Background;
	public Gradient LightColor;
	public ParticleSystem Leaves;
	private Object[] grass;
	
	void Start ()
	{
		grass = FindSceneObjectsOfType (typeof(Grass));
		WindAnimation ["Wind"].time = Random.Range (0, 1f);
		DayProgress = Random.Range (0, 1f);
		WindAnimation ["Wind"].speed = DaySpeed / 1.7f;
	}
	
	void Update ()
	{
		Sun.localEulerAngles = new Vector3 (0, 0, 360 * (DayProgress + 0.25f));
		DayProgress += Time.deltaTime * DaySpeed;
		Background.material.color = LightColor.Evaluate (DayProgress);
		if (DayProgress > 1)
			DayProgress = 0;
		
		Debug.Log (WindAnimation ["Wind"].time);
		float wind = Mathf.Max (0, -Mathf.Sin (WindAnimation ["Wind"].time * 6.28f));
		foreach (Grass g in grass) {
			g.Amplitude = 0.05f + 0.25f * wind;
		}
		
		Leaves.gravityModifier = 0.05f + 0.35f * wind;
		Leaves.startSpeed = 0.015f + 5 * wind;
	}
}

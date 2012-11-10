using UnityEngine;
using System.Collections;

public class _DownhillDarknessTrigger : MonoBase
{
	public Renderer Background;
	
	void Update ()
	{
		float a = transform.position.x - Cat.Instance.transform.position.x;
		a = 1 - Mathf.Clamp01 (a / 15);
		ClothWind.SetWind (Vector3.left * a * 50, Vector3.one * 30 * a);
		
		float c = 0.3f + (1 - a) * 0.7f;
		Background.material.color = new Color (c, c, c, 1);
		
		_<Sound>().Volume = a;
	}
}

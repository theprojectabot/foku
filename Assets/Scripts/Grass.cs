using UnityEngine;
using System.Collections;

public class Grass : MonoBehaviour
{
	public float Period, Amplitude;
	private Matrix4x4 matrix0;
	private float rnd;
	
	void Start ()
	{
		matrix0 = transform.localToWorldMatrix;
		rnd = Random.Range (0f, 10f);
	}
	
	void Update ()
	{
		Matrix4x4 skew = Matrix4x4.identity;
		float d = Amplitude * Mathf.Sin ((rnd + Time.time) / Period * 6.28f);
		skew [0, 2] = d;
		skew [0, 3] = d / 2;// * transform.localScale.y;
		renderer.material.SetMatrix ("Transform", skew);
	}
}

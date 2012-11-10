using UnityEngine;
using System.Collections;

public class WarpSpawn : MonoBehaviour
{
	public float Size = 2;
	public ParticleSystem Particles;
	public Renderer Renderer;
	public Transform Spawn;
	private float targetSize = 0;
	private SmoothFloat smoothSize = new SmoothFloat ();

	void Start ()
	{
		smoothSize.Damping = 1f;
		StartCoroutine (Do ());
	}
	
	void Update ()
	{
		smoothSize.Update (targetSize);
		Renderer.material.SetFloat ("strength", smoothSize.Value / Size);
		Particles.startSize = smoothSize.Value;
		transform.localScale = Vector3.one * smoothSize.Value;
	}
	
	IEnumerator Do ()
	{
		targetSize = Size;
		yield return new WaitForSeconds(1f);
		targetSize = 0;
		Instantiate (Spawn, transform.position, Quaternion.identity);
		yield return new WaitForSeconds(5f);
		Destroy (gameObject);
	}		
}

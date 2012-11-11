using UnityEngine;
using System.Collections;

public class PushMagic : MonoBehaviour
{
	public Renderer Renderer;
	private float targetSize = 0;
	private SmoothFloat smoothSize = new SmoothFloat ();

	void Start ()
	{
		smoothSize.Damping = 0.3f;
		StartCoroutine (Do ());
		transform.localScale = Vector3.one / 1000;
	}
	
	void Update ()
	{
		smoothSize.Update (targetSize);
		Renderer.material.SetFloat ("strength", smoothSize.Value / 6f);
		transform.localScale = Vector3.one * smoothSize.Value;
	}
	
	IEnumerator Do ()
	{
		targetSize = 3f;
		yield return new WaitForSeconds(0.3f);
		targetSize = 0;
		
		foreach (Enemy e in FindSceneObjectsOfType(typeof(Enemy))) {
			if ((e.transform.position - transform.position).magnitude < 3) {
				e.GetComponent<Character> ().Backoff (-4 * Mathf.Sign (e.transform.position.x - transform.position.x));
				e.GetComponent<Character> ().Confuse ();
			}
		}
		
		yield return new WaitForSeconds(5f);
		Destroy (gameObject);
	}	
}
 
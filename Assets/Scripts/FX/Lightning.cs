using UnityEngine;
using System.Collections;

public class Lightning : MonoBehaviour
{

	void Start ()
	{
		StartCoroutine (Do ());
	}
	
	IEnumerator Do ()
	{
		while (true) {
			yield return new WaitForSeconds(Random.Range(5,35f));
			
			audio.Play ();
			
			renderer.enabled = true;
			yield return new WaitForSeconds(Random.Range(0.01f,0.02f));
			renderer.enabled = false;
			yield return new WaitForSeconds(Random.Range(0.01f,0.02f));
			renderer.enabled = true;
			yield return new WaitForSeconds(Random.Range(0.01f,0.02f));
			renderer.enabled = false;
			yield return new WaitForSeconds(Random.Range(0.01f,0.02f));
			renderer.enabled = true;
			yield return new WaitForSeconds(Random.Range(0.01f,0.02f));
			renderer.enabled = false;
			yield return new WaitForSeconds(Random.Range(0.01f,0.02f));
		}
	}
}

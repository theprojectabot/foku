using UnityEngine;
using System.Collections;

public class LoadingScreen : MonoBehaviour
{
	public float Progress;
	public Transform Fire;
	public string URL;
	private SmoothFloat smoothProgress = new SmoothFloat ();
	
	void Start ()
	{
		StartCoroutine (Loader ());
	}
	
	public IEnumerator Loader ()
	{
		yield return new WaitForSeconds(2);
		WWW stream = new WWW (URL);
		while (!stream.isDone) {
			Progress = stream.progress;
			yield return new WaitForSeconds(0.1f);
		}
		stream.LoadUnityWeb ();
	}
	
	void Update ()
	{
		smoothProgress.Update (Progress);
		Fire.transform.position = new Vector3 (0, -40 + 28 * smoothProgress.Value, 1);
	}
}

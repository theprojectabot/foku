using UnityEngine;
using System.Collections;

public class ForkTrail : MonoBehaviour
{
	private Vector3 lastPosition;
	private TrailRenderer trail;
	private float w0, w1;

	void Start ()
	{
		trail = GetComponent<TrailRenderer> ();
		w0 = trail.startWidth;
		w1 = trail.endWidth;
	}
	
	void Update ()
	{
		float d = (transform.position - lastPosition).magnitude / Time.deltaTime;
		lastPosition = transform.position;
		
		float c = Mathf.Clamp (d, 1.6f, 8) / 8 - 0.2f;
		trail.startWidth = w0 * c;
		trail.endWidth = w1 * c;
	}
}

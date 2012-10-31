using UnityEngine;
using System.Collections;

public class ClothWind : MonoBehaviour
{
	public float Force;
	public Vector3 RandomForce;
	
	void Start ()
	{
		Vector3 dir = transform.forward;
		foreach (InteractiveCloth cloth in FindSceneObjectsOfType(typeof(InteractiveCloth))) {
			cloth.externalAcceleration += dir * Force;
			cloth.randomAcceleration += transform.TransformDirection (RandomForce);
		}
	}
}

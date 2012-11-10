using UnityEngine;
using System.Collections;

public class ClothWind : MonoBehaviour
{
	public float Force;
	public Vector3 RandomForce;
	
	void Start ()
	{
		SetWind (transform.forward * Force, RandomForce);
	}
	
	public static void SetWind (Vector3 wind, Vector3 random)
	{
		foreach (InteractiveCloth cloth in FindSceneObjectsOfType(typeof(InteractiveCloth))) {
			cloth.externalAcceleration = wind;
			cloth.randomAcceleration = random;
		}
	}
}

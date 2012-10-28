using UnityEngine;
using System.Collections;

public class ConstantRotation : MonoBehaviour
{
	public Vector3 Rotation;

	void Update ()
	{
		transform.Rotate(Rotation * Time.deltaTime);
	}
}

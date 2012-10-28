using UnityEngine;
using System.Collections;

public class SelfDestruct : MonoBehaviour
{
	public float Delay;
	
	void Start ()
	{
		Invoke ("Destruct", Delay);
	}
	
	public void Destruct ()
	{
		Destroy (gameObject);
	}
}

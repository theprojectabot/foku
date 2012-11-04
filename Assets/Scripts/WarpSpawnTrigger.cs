using UnityEngine;
using System.Collections;

public class WarpSpawnTrigger : MonoBehaviour
{
	public Transform Enemy;
	public float Size;
	public WarpSpawn WarpPrefab;
	
	public void OnTriggerEnter (Collider collider)
	{
		Cat c = collider.GetComponent<Cat> ();
		if (c != null && c.enabled) {
			WarpSpawn s = Instantiate (WarpPrefab, transform.position, transform.rotation) as WarpSpawn;
			s.Spawn = Enemy;
			s.Size = Size;
			FX.Instance.Run ("Slowdown");
			Destroy (gameObject);
		}
	}
}

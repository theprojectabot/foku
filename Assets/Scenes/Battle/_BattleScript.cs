using UnityEngine;
using System.Collections;

public class _BattleScript : MonoBehaviour
{
	void Start ()
	{
		StartCoroutine (Do ());
	}
	
	IEnumerator Do ()
	{
		while (true) {
			yield return new WaitForSeconds(Random.Range(10,15));
			_BattleForkFX.Instance.Amount = 3;
			yield return new WaitForSeconds(Random.Range(8,12));
			_BattleForkFX.Instance.Amount = 1;
		}
	}
}

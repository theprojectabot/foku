using UnityEngine;
using System.Collections;

public class LoadingScreen : MonoBehaviour
{
	public static string Level = "Monastery 1";
	
	void Start ()
	{
		StartCoroutine (Loader ());
	}
	
	public IEnumerator Loader ()
	{
		yield return new WaitForSeconds(1);
		Application.LoadLevelAsync (Level);
	}
}

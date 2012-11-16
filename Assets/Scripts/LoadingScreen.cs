using UnityEngine;
using System.Collections;

public class LoadingScreen : MonoBehaviour
{
	public static string Level = "Menu";
	
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

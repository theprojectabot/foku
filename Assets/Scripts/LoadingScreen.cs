using UnityEngine;
using System.Collections;

public class LoadingScreen : MonoBehaviour
{
	public static string Level = "Menu";
	
	void Start ()
	{
//#if UNITY_DESKTOP
		Screen.SetResolution(800,450,false);
//#endif	
		StartCoroutine (Loader ());
	}
	
	public IEnumerator Loader ()
	{
		yield return new WaitForSeconds(1);
		Application.LoadLevelAsync (Level);
	}
}

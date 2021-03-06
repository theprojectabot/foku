using UnityEngine;
using System.Collections;

public class LoadingScreen : MonoBehaviour
{
	public static string Level = "Menu";
	
	void Start ()
	{
		if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.LinuxPlayer)
			Screen.SetResolution (800, 450, false);
		StartCoroutine (Loader ());
	}
	
	public IEnumerator Loader ()
	{
		yield return new WaitForSeconds(1);
		if (!Application.isWebPlayer)
			Application.LoadLevelAsync (Level);
	}
	
	public UISlider Progress;
	private bool started = false;
	
	void Update ()
	{
		Progress.sliderValue = Application.GetStreamProgressForLevel (Level);
		Progress.gameObject.SetActive (Progress.sliderValue < 1);
		if (Application.isWebPlayer && !started && Application.CanStreamedLevelBeLoaded (Level)) {
			started = true;
			Application.LoadLevelAsync (Level);
		}
	}
}

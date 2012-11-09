using UnityEngine;
using System.Collections;

public class GameProgress
{
	public static void ContinueGame ()
	{
		GoToLevel (PlayerPrefs.GetString ("level"));
	}
	
	public static bool HasSave ()
	{
		return PlayerPrefs.HasKey ("level");
	}
	
	public static void RestartGame ()
	{
		GoToLevel ("Monastery");
	}
	
	public static void RestartLevel ()
	{
		GoToLevel (Application.loadedLevelName);
	}
	
	public static void GoToLevel (string level)
	{
		LoadingScreen.Level = level;
		PlayerPrefs.SetString ("level", level);
		Application.LoadLevel ("Loading Screen");
	}
}

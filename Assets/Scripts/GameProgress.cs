using UnityEngine;
using System.Collections;

public class GameProgress
{
	public static void GoToLevel (string level)
	{
		LoadingScreen.Level = level;
		Application.LoadLevel ("Loading Screen");
	}
}

using UnityEngine;
using System.Collections;

public class StartGameButton : MonoBehaviour
{
	public void OnClick ()
	{
		StartCoroutine (Do ());
	}
	
	IEnumerator Do ()
	{
		ScreenFade.Instance.To (1);
		yield return new WaitForSeconds(2);
		GameProgress.RestartGame ();
	}
}

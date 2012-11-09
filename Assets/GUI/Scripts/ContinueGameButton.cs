using UnityEngine;
using System.Collections;

public class ContinueGameButton : MonoBehaviour
{
	void Start ()
	{
		if (!GameProgress.HasSave ())
			gameObject.SetActive (false);
	}
	
	public void OnClick ()
	{
		StartCoroutine (Do ());
	}
	
	IEnumerator Do ()
	{
		ScreenFade.Instance.To (1);
		yield return new WaitForSeconds(2);
		GameProgress.ContinueGame ();
	}
}

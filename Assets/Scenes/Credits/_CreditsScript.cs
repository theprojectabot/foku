using UnityEngine;
using System.Collections;

public class _CreditsScript : MonoBehaviour
{
	public UILabel Label1, Label2, Label3;
	
	void Start ()
	{
		StartCoroutine (Do ());
	}
	
	void SetTitles (string a, string b)
	{
		Label1.text = b;
		Label2.text = a;
	}
	
	IEnumerator Do ()
	{
		Label3.enabled = false;
		SetTitles ("Art & programming", "Eugene Pankov");
		yield return new WaitForSeconds(4);
		SetTitles ("Music", "Marc Teichert");
		yield return new WaitForSeconds(4);
		SetTitles ("Powered by", "Unity Engine");
		yield return new WaitForSeconds(4);
		SetTitles ("Download soundtrack for free at", "Jamendo");
		Label3.enabled = true;
		yield return new WaitForSeconds(5);
		Label3.enabled = false;
		SetTitles ("Click X to return to main menu", "");
	}
}

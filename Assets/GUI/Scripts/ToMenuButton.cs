using UnityEngine;
using System.Collections;

public class ToMenuButton : MonoBehaviour
{
	public void OnClick ()
	{
		Application.LoadLevel ("Menu");
	}
}

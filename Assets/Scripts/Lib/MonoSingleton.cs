using UnityEngine;
using System.Collections;

public class MonoSingleton<T> : MonoBase where T : class
{
	public static T Instance = null;

	public override void Awake ()
	{
		base.Awake();
		Instance = this as T;
	}
}

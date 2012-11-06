using UnityEngine;
using System.Collections;

public class Dialogue : MonoSingleton<Dialogue>
{
	public Phrase PhrasePrefab;

	public void Say (string text, float time)
	{
		Say (null, text, time, 1);
	}

	public void Say (Transform attach, string text, float time)
	{
		Say (attach, text, time, 1);
	}
	
	public void Say (Transform attach, string text, float time, float size)
	{
		Phrase p = Instantiate (PhrasePrefab) as Phrase;
		p.transform.parent = transform;
		p.transform.localPosition = Vector3.zero;
		p.transform.rotation = Quaternion.identity;
		p.Text = text;
		p.Lifetime = time;
		p.Size = size;
		p.AttachTo = attach;
	}
}

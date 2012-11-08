using UnityEngine;
using System.Collections;

public class Phrase : MonoBase
{
	public float Lifetime = 5, Size = 1;
	public string Text;
	public Transform AttachTo;
	private SmoothFloat alpha = new SmoothFloat ();
	
	public override void Start ()
	{
		base.Start ();
		_<UILabel> ().text = Text;
	}
	
	void LateUpdate ()
	{
		alpha.Update ((Lifetime > 0) ? 1 : 0);
		_<UILabel> ().color = new Color (1, 1, 1, alpha.Value);
		Vector3 newPos = (AttachTo == null) ? transform.parent.position : (AttachTo.position + new Vector3 (0, 1, 0));
		//newPos.z = transform.localPosition.z;
		transform.position = newPos - Vector3.right * 0.15f * Lifetime;
		transform.localScale = Vector3.one * Size * 64;
		Lifetime -= Time.deltaTime;
		if (Lifetime < -3)
			Destroy (gameObject);
	}
}

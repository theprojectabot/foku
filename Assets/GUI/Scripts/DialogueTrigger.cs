using UnityEngine;
using System.Collections;

public class DialogueTrigger : MonoBehaviour
{
	public bool Once = true;
	public DialogueTriggerPhrase[] Phrases;
	
	public void OnTriggerEnter (Collider collider)
	{
		Cat c = collider.GetComponent<Cat> ();
		if (c != null) {
			StartCoroutine (Do ());
			if (Once)
				this.collider.enabled = false;
		}
	}
	
	IEnumerator Do ()
	{
		foreach (DialogueTriggerPhrase p in Phrases) {
			Dialogue.Instance.Say (p.Origin, p.Text, p.Duration, 1);
			yield return new WaitForSeconds(p.Duration + 1);
		}
	}
	
	[System.Serializable]
	public class DialogueTriggerPhrase
	{
		public Transform Origin;
		public string Text;
		public float Duration = 3;
	}
}

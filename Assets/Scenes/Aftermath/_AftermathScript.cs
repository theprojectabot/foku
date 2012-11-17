using UnityEngine;
using System.Collections;

public class _AftermathScript : MonoBehaviour
{
	public Transform CamTarget, CatWaypoint, Fork, CatHand;
	public GameObject Flame;
	
	void Start ()
	{
		StartCoroutine (Do ());
	}
	
	IEnumerator Do ()
	{
		yield return new WaitForEndOfFrame();
		Cat.Instance.enabled = false;
		Cat.Instance.GetComponent<FriendlyNPC> ().Waypoint = CatWaypoint;
		// Ascend
		yield return new WaitForSeconds(23);
		Cat.Instance.GetComponent<Character> ().Body.animation.enabled = false;
		Cat.Instance.GetComponent<Character> ().enabled = false;
		Cat.Instance.animation ["Grab Fork"].normalizedSpeed = 0.3f;
		Cat.Instance.animation.CrossFade ("Idle", 0.5f);
		yield return new WaitForSeconds(2);
		Cat.Instance.animation.CrossFade ("Grab Fork", 0.5f);
		yield return new WaitForSeconds(1);
		Fork.parent = CatHand;
		yield return new WaitForSeconds(0.3f);
		Flame.SetActive (true);
		
		yield return new WaitForSeconds(5);
		ScreenFade.Instance.To (1);
		yield return new WaitForSeconds(6);
		GameProgress.GoToLevel ("Credits");
	}
	
	void Update ()
	{
		CamTarget.position += new Vector3 (3, 0.7f, 0) * Time.deltaTime * 0.2f;
	}
}

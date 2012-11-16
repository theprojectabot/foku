using UnityEngine;
using System.Collections;

public class _BattleScript : MonoSingleton<_BattleScript>
{
	public Transform CatnessWaypoint, ShadowMonk, ShieldPrefab;
	public float MonkEnergy = 0.1f;
	public UISlider Health;
	public Transform PortalLookAt;
	public AudioClip BassClip, ThunderClip;
	
	void Start ()
	{
		StartCoroutine (Do ());
	}
	
	void Update ()
	{
		Health.sliderValue = MonkEnergy / 10;
	}
	
	IEnumerator Do ()
	{
		yield return new WaitForSeconds(0);
		_BattleShield.Instance.gameObject.SetActive (false);
		Catness.Instance.enabled = false;
		//Cat.Instance.enabled = false;
		
		yield return new WaitForSeconds(2);
		
		Catness.Instance.GetComponent<FriendlyNPC> ().Waypoint = CatnessWaypoint;
		yield return new WaitForSeconds(3);
		Catness.Instance.GetComponent<Character> ().Body.animation.enabled = false;
		Catness.Instance.GetComponent<Character> ().enabled = false;
		Catness.Instance.animation.Play ("Shadow Monk Casting");
		_BattleShield.Instance.gameObject.SetActive (true);
		
		
		Dialogue.Instance.Say (null, "Go back behind the shield to avoid\nbeing absorbed by the portal", 5, 1.5f);
		
		yield return new WaitForSeconds(4);
		while (MonkEnergy > 0) {
			_BattleForkFX.Instance.Amount = 3;
			Instantiate (ShieldPrefab, ShadowMonk.position, Quaternion.identity);
			yield return new WaitForSeconds(Random.Range(8,9));
			_BattleForkFX.Instance.Amount = 1;
			
			for (int i =0; i < 6; i++) {
				yield return new WaitForSeconds(1);
				if (MonkEnergy <= 0)
					break;
			}
		}
		
		yield return new WaitForSeconds(2);
		_BattleForkFX.Instance.SuckPlayerIn = false;
		_BattleForkFX.Instance.Amount = 3;
		yield return new WaitForSeconds(1);
		_BattleShadowMonk.Instance.FlyAway ();
		yield return new WaitForSeconds(2);
		_BattleForkFX.Instance.Amount = 1;
		yield return new WaitForSeconds(2);
		
		Cam.Instance.Target = PortalLookAt;
		FX.Instance.Run ("Slowdown");
		_BattleForkFX.Instance.Collapse ();
		audio.clip = BassClip;
		audio.Play ();
		yield return new WaitForSeconds(1);
		_BattleForkFX.Instance.Expand ();
		//yield return new WaitForSeconds(1);
		//_BattleForkFX.Instance.Collapse2 ();
		//yield return new WaitForSeconds(2);
		//_BattleForkFX.Instance.Disable ();
		audio.clip = ThunderClip;
		audio.Play ();
		
		ScreenFade.Instance.To (1);
		yield return new WaitForSeconds(2);
		GameProgress.GoToLevel ("Aftermath");
	}
}

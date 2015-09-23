using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Ability : MonoBehaviour
{
	public float cooldown;
	float cooldownTimer;
	public GameObject placementGhost;
	public KeyCode hotkey;
	public GameObject player;
	public static Ability placingAbility;

	void Awake ()
	{
		player = transform.root.gameObject;
		placementGhost = transform.Find("PlacementGhost").gameObject;
	}

	void Update ()
	{
		cooldownTimer -= Time.deltaTime;
		transform.Find("CooldownImage").GetComponent<Image>().fillAmount = cooldownTimer / cooldown;
		if (cooldownTimer <= 0)
		{
			transform.Find("Title").GetComponent<Text>().enabled = true;
			transform.Find("CooldownImage").Find("CooldownText").GetComponent<Text>().enabled = false;
		}
		else
		{
			if (cooldownTimer >= 1)
				transform.Find("CooldownImage").Find("CooldownText").GetComponent<Text>().text = "" + Mathf.RoundToInt(cooldownTimer);
			else
				transform.Find("CooldownImage").Find("CooldownText").GetComponent<Text>().text = "" + Mathf.Round(cooldownTimer / .1f) * .1f;
		}
		if (Input.GetKeyDown(hotkey))
			Place ();
	}

	public void Place ()
	{
		if (cooldownTimer > 0 || player.GetComponent<Player_Death>().isDead)
			return;
		if (placementGhost == null)
			SendMessage("Cast");
		else
		{
			Vector3 localScale;
			if (placingAbility != null)
			{
				localScale = placingAbility.placementGhost.transform.localScale;
				placingAbility.placementGhost.transform.SetParent(placingAbility.transform);
				placingAbility.placementGhost.transform.localScale = localScale;
				foreach (Renderer r in placingAbility.placementGhost.transform.GetComponentsInChildren<Renderer>())
					r.enabled = false;
				placingAbility.placementGhost.GetComponentInChildren<PlacementGhost>().enabled = false;
			}
			placementGhost.GetComponentInChildren<PlacementGhost>().enabled = true;
			placementGhost.GetComponentInChildren<PlacementGhost>().ability = this;
			localScale = placementGhost.transform.localScale;
			placementGhost.transform.SetParent(player.transform.Find("PlacementGhostParent"));
			placementGhost.transform.position = player.transform.Find("PlacementGhostParent").position + (Vector3.up * placementGhost.transform.localPosition.y);
			placementGhost.transform.localScale = localScale;
			placementGhost.transform.localEulerAngles = Vector3.zero;
			foreach (Renderer r in placementGhost.GetComponentsInChildren<Renderer>())
				r.enabled = true;
			placingAbility = this;
		}
	}

	public void Use ()
	{
		cooldownTimer = cooldown;
		transform.Find("Title").GetComponent<Text>().enabled = false;
		transform.Find("CooldownImage").Find("CooldownText").GetComponent<Text>().enabled = true;
	}
}

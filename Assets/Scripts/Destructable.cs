using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Destructable : TeamMember
{
	public int maxHp;
	public float extraHp;
	public float hp;
	public float decayLength;
	public bool dead;
	public Transform blood;
	RectTransform info;

	// Use this for initialization
	public void Awake ()
	{
		hp = maxHp;
		//info = (RectTransform) transform.Find("Info"));
		//info.SetParent(GameObject.Find("InfoGroup").transform);
		//UpdateInfo ();
	}

	void LateUpdate ()
	{
		if (dead && (GetComponent<Animation>() == null || !GetComponent<Animation>().isPlaying))
			StartCoroutine(DelayDecay ());
	}

	public void TakeDamage (float amount)
	{
		if (dead)
			return;
		if (amount > 0)
		{
			float reduceDamageAmount = 0;
			if (extraHp - amount <= 0)
				reduceDamageAmount = extraHp;
			extraHp -= amount;
			extraHp = Mathf.Clamp(extraHp, 0, Mathf.Infinity);
			amount -= reduceDamageAmount;
		}
		hp -= amount;
		hp = Mathf.Clamp(hp, 0, maxHp) + extraHp;
		//UpdateInfo ();
		if (hp == 0)
			Death ();
	}

	public void Death ()
	{
		gameObject.layer = LayerMask.NameToLayer("Dead");
		if (GetComponentInChildren<Rigidbody>() != null)
			Destroy(GetComponent<Rigidbody>());
		foreach (Collider c in GetComponentsInChildren<Collider>())
			Destroy(c);
		if (GetComponentInChildren<Animator>() != null)
			GetComponentInChildren<Animator>().enabled = false;
		if (GetComponentInChildren<Animation>() != null)
			GetComponentInChildren<Animation>().Play ();
		dead = true;
		//Destroy(info.gameObject);
		//info.gameObject.SetActive(false);
	}

	public IEnumerator DelayDecay ()
	{
		yield return new WaitForSeconds(decayLength);
		Decay ();
	}

	public void Decay ()
	{
		GameObject.FindWithTag("Player").GetComponent<Player_DestroyObj>().DestroyObj (name);
	}

	public void UpdateInfo ()
	{
		Vector2 maxHpBarSize = info.Find("MaxHealth").GetComponent<RectTransform>().sizeDelta;
		info.Find("Health").GetComponent<RectTransform>().sizeDelta = new Vector2(maxHpBarSize.x / (maxHp / (hp + extraHp)), maxHpBarSize.y);
	}
}

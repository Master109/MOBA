using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Player_Health : NetworkBehaviour
{
	public int maxHp = 250;
	[SyncVar (hook = "OnHealthChanged")] private int hp;

	public void Start ()
	{
		hp = maxHp;
		SetHealthText();
	}

	void SetHealthText()
	{
		if(isLocalPlayer)
			GameObject.Find("Health Text").GetComponent<Text>().text = "Health: " + hp;
	}

	public void TakeDamage (int damage)
	{
		hp -= damage;
	}

	void OnHealthChanged (int newHP)
	{
		hp = newHP;
		SetHealthText();
		if (hp <= 0)
			GetComponent<Player_Death>().CmdDeath ();
	}
}

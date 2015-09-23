using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Player_Respawn : NetworkBehaviour
{
	public float respawnTimer;

	void Update ()
	{
		if (!isLocalPlayer || !GetComponent<Player_Death>().isDead)
			return;
		respawnTimer -= Time.deltaTime;
		if (respawnTimer >= 1)
			GameObject.Find("RespawnTimer").GetComponent<Text>().text = "Respawn in: " + Mathf.RoundToInt(respawnTimer);
		else
		{
			GameObject.Find("RespawnTimer").GetComponent<Text>().text = "Respawn in: " + Mathf.Round(respawnTimer / .1f) * .1f;
			if (respawnTimer <= 0)
				CmdRespawn ();
		}
	}

	[Command]
	void CmdRespawn ()
	{
		RpcRespawn ();
	}

	[ClientRpc]
	void RpcRespawn ()
	{
		GameObject.Find("RespawnTimer").GetComponent<Text>().enabled = false;
		foreach (Renderer r in GetComponentsInChildren<Renderer>())
			r.enabled = true;
		GetComponent<Player_Death>().isDead = false;
		GetComponent<Player_Health>().Start();
		NetworkStartPosition[] teamStartPositions = GetComponent<Player_NetworkSetup>().team.GetComponentsInChildren<NetworkStartPosition>();
		transform.position = teamStartPositions[Random.Range(0, teamStartPositions.Length)].transform.position;
		GetComponent<NavMeshAgent>().destination = transform.position;
	}
}

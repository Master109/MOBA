using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player_NetworkSetup : TeamMember
{
	[SerializeField] GameObject[] activateObjs;
	GameObject lastSpawned;

	// Use this for initialization
	public override void OnStartLocalPlayer ()
	{
		GetComponent<Player_ID>().Setup ();
		foreach (GameObject g in activateObjs)
			g.SetActive(true);
		transform.Find("Canvas").SetParent(null);
		DestroyImmediate(GetComponent<NavMeshObstacle>());
		foreach (GameObject g in GameObject.FindGameObjectsWithTag("Player"))
			if (g != gameObject)
			{
				Destroy(g.GetComponent<NavMeshAgent>());
				//g.GetComponent<NavMeshObstacle>().enabled = true;
			}

		foreach (Revealable r in GetComponentsInChildren<Revealable>())
			r.GetComponent<Renderer>().enabled = true;
		if (GameObject.FindGameObjectsWithTag("Player").Length == FindObjectOfType<NetworkManager_Custom>().maxConnections && Global.paused)
			StartGame ();
		else if (GameObject.FindGameObjectsWithTag("Player").Length > FindObjectOfType<NetworkManager_Custom>().maxConnections)
		{
			GetComponent<Player_DestroyObj>().DestroyObj (name);
			Application.LoadLevel(0);
		}
	}

	public void StartGame ()
	{
		CmdStartGame ();
	}

	[Command]
	void CmdStartGame ()
	{
		RpcStartGame ();
	}

	[ClientRpc]
	void RpcStartGame ()
	{
		gameObject.AddComponent<SpawnMinions>();
		Global.paused = false;
	}

	public void TestDebug ()
	{
		CmdDebug ();
	}

	[Command]
	void CmdDebug ()
	{
		RpcDebug ();
	}
	
	[ClientRpc]
	void RpcDebug ()
	{
		Debug.Log(1);
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetworkManager_Custom : NetworkManager
{
	public void StartupHost ()
	{
		NetworkManager.singleton.StartHost();
	}

	public void JoinGame ()
	{
		NetworkManager.singleton.StartClient();
	}

	public override void OnServerDisconnect (NetworkConnection connection)
	{
		SpawnMinions sm = FindObjectOfType<SpawnMinions>();
		sm.ContinueSpawningMinions ();
		sm.GetComponent<Player_DestroyObj>().CmdDestroy(sm.name);
	}

	public override void OnClientDisconnect (NetworkConnection connection)
	{
		SpawnMinions sm = FindObjectOfType<SpawnMinions>();
		sm.ContinueSpawningMinions ();
		sm.GetComponent<Player_DestroyObj>().CmdDestroy(sm.name);
	}
}

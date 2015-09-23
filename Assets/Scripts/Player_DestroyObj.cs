using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player_DestroyObj : NetworkBehaviour
{
	public void DestroyObj (string goName)
	{
		CmdDestroy (goName);
	}

	[Command]
	public void CmdDestroy (string goName)
	{
		RpcDestroy (goName);
	}
	
	[ClientRpc]
	void RpcDestroy (string goName)
	{
		Destroy(GameObject.Find(goName));
	}
}

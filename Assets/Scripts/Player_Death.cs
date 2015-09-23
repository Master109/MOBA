using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Player_Death : NetworkBehaviour
{
	public bool isDead;

	[Command]
	public void CmdDeath ()
	{
		RpcDeath ();
	}

	[ClientRpc]
	void RpcDeath ()
	{
		isDead = true;
		if (isLocalPlayer)
			GameObject.Find("RespawnTimer").GetComponent<Text>().enabled = true;
		foreach (Renderer r in GetComponentsInChildren<Renderer>())
			r.enabled = false;
		gameObject.layer = LayerMask.NameToLayer("Dead");
	}
}

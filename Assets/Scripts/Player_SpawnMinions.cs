using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player_SpawnMinions : NetworkBehaviour
{
	public Minion minion;

	[Command]
	public void CmdSpawnMinions ()
	{
		RpcSpawnMinions ();
	}

	[ClientRpc]
	void RpcSpawnMinions ()
	{
		foreach (MinionSpawnPos msp in FindObjectsOfType<MinionSpawnPos>())
		{
			Minion m = (Minion) Lean.LeanPool.Spawn(minion, msp.transform.position, msp.transform.rotation);
			foreach (Transform target in msp.targets)
			m.targets.Add(target);
			m.name = "Minion " + Random.Range(float.MinValue, float.MaxValue);
		}
	}

	[Command]
	public void CmdContinueSpawningMinions (float newWaveTimer)
	{
		RpcContinueSpawningMinions (newWaveTimer);
	}
	
	[ClientRpc]
	void RpcContinueSpawningMinions (float newWaveTimer)
	{
		SpawnMinions sm = gameObject.AddComponent<SpawnMinions>();
		sm.waveTimer = newWaveTimer;
	}
}

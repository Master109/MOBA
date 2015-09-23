using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Spawner : NetworkBehaviour
{
	public GameObject[] spawns;
	public Transform[] spawnTransforms;
	public float[] spawnTimes;
	float[] spawnTimers;
	int currentSpawn;
	public Vector3[] scaleMultipliers;
	public bool[] destroyAfterDone;

	void Awake ()
	{
		currentSpawn = 0;
		if (spawns.Length == 1)
		{
			GameObject spawn = spawns[0];
			spawns = new GameObject[spawnTimes.Length];
			for (int i = 0; i < spawnTimes.Length; i ++)
				spawns[i] = spawn;
		}
		if (spawnTransforms.Length == 0)
		{
			Transform spawnTransform = transform;
			spawnTransforms = new Transform[spawnTimes.Length];
			for (int i = 0; i < spawnTimes.Length; i ++)
				spawnTransforms[i] = spawnTransform;
		}
		else if (spawnTransforms.Length == 1)
		{
			Transform spawnTransform = spawnTransforms[0];
			spawnTransforms = new Transform[spawnTimes.Length];
			for (int i = 0; i < spawnTimes.Length; i ++)
				spawnTransforms[i] = spawnTransform;
		}
		if (scaleMultipliers.Length == 0)
		{
			Vector3 scaleMultiplier = Vector3.one;
			scaleMultipliers = new Vector3[spawnTimes.Length];
			for (int i = 0; i < spawnTimes.Length; i ++)
				scaleMultipliers[i] = scaleMultiplier;
		}
		else if (scaleMultipliers.Length == 1)
		{
			Vector3 scaleMultiplier = scaleMultipliers[0];
			scaleMultipliers = new Vector3[spawnTimes.Length];
			for (int i = 0; i < spawnTimes.Length; i ++)
				scaleMultipliers[i] = scaleMultiplier;
		}
		if (destroyAfterDone.Length == 0)
		{
			destroyAfterDone = new bool[spawnTimes.Length];
			for (int i = 0; i < spawnTimes.Length; i ++)
				destroyAfterDone[i] = false;
		}
		spawnTimers = new float[spawnTimes.Length];
		for (int i = 0; i < spawnTimes.Length; i ++)
			spawnTimers[i] = spawnTimes[i];
	}

	// Update is called once per frame
	void Update ()
	{
		spawnTimers[currentSpawn] -= Time.deltaTime;
		if (spawnTimers[currentSpawn] <= 0)
		{
			for (int i = 0; i < GetComponents<Spawner>().Length; i ++)
				if (GetComponents<Spawner>()[i] == this)
				{
				CmdSpawn (i, currentSpawn, Global.TopDownVector(spawnTransforms[currentSpawn].position) + (Vector3.up * 0), spawnTransforms[currentSpawn].rotation, scaleMultipliers[currentSpawn]);
					break;
				}
			currentSpawn ++;
			if (currentSpawn == spawns.Length)
			{
				Awake ();
				enabled = false;
			}
		}
	}

	[Command]
	void CmdSpawn (int componentId, int spawnId, Vector3 pos, Quaternion rota, Vector3 scale)
	{
		RpcSpawn (componentId, spawnId, pos, rota, scale);
	}

	[ClientRpc]
	void RpcSpawn (int componentId, int spawnId, Vector3 pos, Quaternion rota, Vector3 scale)
	{
		Spawner thisSpawner = GetComponents<Spawner>()[componentId];
		GameObject g = (GameObject) Lean.LeanPool.Spawn(thisSpawner.spawns[spawnId], pos, rota);
		g.transform.localScale = Global.MultiplyVectors(g.transform.localScale, scale);
		if (g.GetComponent<TeamMember>() != null)
			g.GetComponent<TeamMember>().team = GetComponent<TeamMember>().team;
		if (destroyAfterDone[spawnId])
		{
			float destroyTime = 0;
			for (int i = spawnId + 1; i < thisSpawner.spawnTimes.Length; i ++)
				destroyTime += thisSpawner.spawnTimes[i];
			Destroy(g, destroyTime);
		}
	}
}

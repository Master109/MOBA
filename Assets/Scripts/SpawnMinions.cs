using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SpawnMinions : NetworkBehaviour
{
	int initWaveTime = 0;
	int waveRate = 3;
	public float waveTimer;
	
	// Use this for initialization
	void Start ()
	{
		if (waveTimer == 0)
			waveTimer = initWaveTime;
	}
	
	public void ContinueSpawningMinions ()
	{
		GameObject player = GameObject.FindGameObjectsWithTag("Player")[Random.Range(0, GameObject.FindGameObjectsWithTag("Player").Length)];
		while (player == gameObject)
			player = GameObject.FindGameObjectsWithTag("Player")[Random.Range(0, GameObject.FindGameObjectsWithTag("Player").Length)];
		player.GetComponent<Player_SpawnMinions>().CmdContinueSpawningMinions (waveTimer);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!isLocalPlayer)
			return;
		waveTimer -= Time.deltaTime;
		if (waveTimer <= 0)
		{
			GetComponent<Player_SpawnMinions>().CmdSpawnMinions ();
			waveTimer = waveRate;
		}
	}
}

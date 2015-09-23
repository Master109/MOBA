using UnityEngine;
using System.Collections;

public class Ability_Spawn : MonoBehaviour
{
	public Spawner[] spawners;
	public Spawner aoeSpawner;

	public void Cast ()
	{
		foreach (Spawner spawner in spawners)
		{
			if (spawner == aoeSpawner)
				aoeSpawner.scaleMultipliers[0] = transform.lossyScale;
			spawner.enabled = true;
		}
	}
}

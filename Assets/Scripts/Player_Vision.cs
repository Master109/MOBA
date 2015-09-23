using UnityEngine;
using System.Collections;

public class Player_Vision : MonoBehaviour
{
	public bool enemyVision;
	ArrayList visibleEnemies = new ArrayList();

	void OnTriggerEnter (Collider other)
	{
		if (other.GetComponent<Revealable>() != null && (other.GetComponent<TeamMember>().team != transform.root.GetComponent<TeamMember>().team) == enemyVision)
		{
			visibleEnemies.Add(other);
			RevealObj(other, true);
			if (!enemyVision)
				Physics.IgnoreCollision(other, GetComponent<Collider>());
		}
	}

	void OnTriggerExit (Collider other)
	{
		if (enemyVision && other.GetComponent<Revealable>() != null && (other.GetComponent<TeamMember>().team != transform.root.GetComponent<TeamMember>().team) == enemyVision)
		{
			visibleEnemies.Remove(other);
			RevealObj(other, false);
		}
	}

	public void RevealObj (Collider collider, bool reveal)
	{
		if (!reveal && (visibleEnemies.Contains(collider) || ((collider.GetComponent<TeamMember>().team.name == "GreenTeam" && NonPlayerVision.visibleGreenUnits.Contains(collider)) || (collider.GetComponent<TeamMember>().team.name == "RedTeam" && NonPlayerVision.visibleRedUnits.Contains(collider)))))
			return;
		foreach (Renderer r in collider.GetComponentsInChildren<Renderer>())
			r.enabled = reveal;

	}
}

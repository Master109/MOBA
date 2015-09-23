using UnityEngine;
using System.Collections;

public class NonPlayerVision : MonoBehaviour
{
	public static ArrayList visibleGreenUnits = new ArrayList();
	public static ArrayList visibleRedUnits = new ArrayList();
	
	void OnTriggerEnter (Collider other)
	{
		if (other.GetComponent<Revealable>() != null && other.GetComponent<TeamMember>().team != transform.root.GetComponent<TeamMember>().team)
		{
			if (other.GetComponent<TeamMember>().team.name == "GreenTeam" && !visibleGreenUnits.Contains(other))
				visibleGreenUnits.Add(other);
			else if (!visibleRedUnits.Contains(other))
				visibleRedUnits.Add(other);
			foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
				if (player.GetComponent<TeamMember>().team == transform.root.GetComponent<TeamMember>().team)
					player.GetComponentInChildren<Player_Vision>().RevealObj(other, true);
		}
	}
	
	void OnTriggerExit (Collider other)
	{
		if (other.GetComponent<Revealable>() != null && other.GetComponent<TeamMember>().team != transform.root.GetComponent<TeamMember>().team)
		{
			if (other.GetComponent<TeamMember>().team.name == "GreenTeam" && visibleGreenUnits.Contains(other))
				visibleGreenUnits.Remove(other);
			else if (visibleRedUnits.Contains(other))
				visibleRedUnits.Remove(other);
			foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
				if (player.GetComponent<TeamMember>().team == transform.root.GetComponent<TeamMember>().team)
					player.GetComponentInChildren<Player_Vision>().RevealObj(other, false);
		}
	}
}

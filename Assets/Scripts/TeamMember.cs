using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class TeamMember : NetworkBehaviour
{
	public Transform team;
	public Material redTeamMat;
	public bool autoSetTeam;

	void Awake ()
	{
		if (autoSetTeam)
		{
			if (transform.position.x < 0)
				team = GameObject.Find("GreenTeam").transform;
			else
				team = GameObject.Find("RedTeam").transform;
		}
	}

	void Start ()
	{
		Awake ();
	}

	void FixedUpdate ()
	{
		if (team.name == "RedTeam")
			GetComponent<Renderer>().material = redTeamMat;
	}
}

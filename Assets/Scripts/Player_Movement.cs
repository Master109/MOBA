using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player_Movement : NetworkBehaviour
{
	NavMeshAgent agent;
	RaycastHit hit;
	public LayerMask whatAffectsMyMovement;

	// Use this for initialization
	void Start ()
	{
		agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void LateUpdate ()
	{
		if (!isLocalPlayer || Global.paused || GetComponent<Player_Death>().isDead)
			return;
		if (Input.GetMouseButtonDown(1))
		{
			if ((GameObject.Find("Minimap").GetComponent<Camera>().pixelRect.Contains(Input.mousePosition) && Physics.Raycast(GameObject.Find("Minimap").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition), out hit)) || Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, whatAffectsMyMovement))
				agent.destination = hit.point;
		}
		if (Input.GetKeyDown(KeyCode.S))
			agent.destination = transform.position;
	}
}

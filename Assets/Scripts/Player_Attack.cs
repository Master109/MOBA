using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player_Attack : NetworkBehaviour
{
	RaycastHit hit;
	NavMeshAgent agent;
	public Bullet bullet;
	public float attackRate;
	float attackTimer;
	Vector3 shootDest;
	public float stopTime;
	float stopTimeRemaining;

	// Use this for initialization
	void Start ()
	{
		agent = GetComponent<NavMeshAgent>();
		stopTime = attackRate;
	}
	
	// Update is called once per frame
	void LateUpdate ()
	{
		if (!isLocalPlayer || Global.paused || GetComponent<Player_Death>().isDead)
			return;
		attackTimer += Time.deltaTime;
		stopTimeRemaining -= Time.deltaTime;
		if (stopTimeRemaining <= 0)
			agent.Resume();
		if (Input.GetKey(KeyCode.LeftShift))
			return;
		if (attackTimer >= attackRate && Input.GetMouseButton(0) && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit) && !(GameObject.Find("Minimap").GetComponent<Camera>().pixelRect.Contains(Input.mousePosition)))
		{
			agent.Stop();
			stopTimeRemaining = stopTime;
			attackTimer = 0;
			shootDest = new Vector3(hit.point.x, 0, hit.point.z);
			CmdCreateBullet(shootDest);
		}
	}

	[Command]
	void CmdCreateBullet (Vector3 shootDest)
	{
		RpcCreateBullet(shootDest);
	}

	[ClientRpc]
	void RpcCreateBullet (Vector3 shootDest)
	{
		Bullet b = (Bullet) Lean.LeanPool.Spawn(GetComponent<Player_Attack>().bullet, Global.TopDownVector(transform.position), Quaternion.LookRotation(shootDest - Global.TopDownVector(transform.position), Vector3.up));
		b.team = GetComponent<TeamMember>().team;
	}
}

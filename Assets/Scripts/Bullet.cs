using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Bullet : TeamMember
{
	public float speed;
	public Vector3 vel;
	public Vector3 shootLoc;
	public float range;
	public float damage;
	public int penetrations;

	// Use this for initialization
	void Start ()
	{
		shootLoc = transform.position;
		vel = transform.forward;
	}
	
	// Update is called once per frame
	void Update ()
	{
		GetComponent<Rigidbody>().velocity = vel.normalized * speed;
		if (Vector3.Distance(Global.TopDownVector(shootLoc), Global.TopDownVector(transform.position)) > range)
			Destroy(gameObject);
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.isTrigger || (other.GetComponent<TeamMember>() != null && other.GetComponent<TeamMember>().team == team))
			return;
		other.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
		other.SendMessage("DamageSplat", new ArrayList () {transform.position, vel}, SendMessageOptions.DontRequireReceiver);
		if (penetrations <= 0)
			Destroy(gameObject);
		penetrations --;
	}
}

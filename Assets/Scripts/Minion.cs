using UnityEngine;
using System.Collections;

public class Minion : MonoBehaviour
{
	public ArrayList targets = new ArrayList();
	AIPath agent;
	int currentTarget;

	// Use this for initialization
	void Start ()
	{
		agent = GetComponent<AIPath>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		Transform target = (Transform) targets[0];
		agent.target = target;
	}
}

using UnityEngine;
using System.Collections;

public class Global : MonoBehaviour
{
	public static Global instance;
	public static bool paused = true;

	void Start ()
	{
		instance = this;
	}

	public static Vector3 TopDownVector (Vector3 v)
	{
		v.y = 0;
		return v;
	}

	public static float GetCapsuleColliderRadius (CapsuleCollider cc)
	{
		return cc.radius * Mathf.Max(cc.transform.lossyScale.x, cc.transform.lossyScale.z);
	}

	public static Vector3 MultiplyVectors (Vector3 v1, Vector3 v2)
	{
		return new Vector3(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z);
	}
}

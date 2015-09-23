using UnityEngine;
using System.Collections;

public class AdjustCameraSize : MonoBehaviour
{
	public float size;

	// Use this for initialization
	void Start ()
	{
		GetComponent<Camera>().orthographicSize = size / Screen.width * Screen.height / 2;
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetUILayer : MonoBehaviour
{
	public int layerOffset;
	
	// Update is called once per frame
	void Start ()
	{
		gameObject.AddComponent<Canvas>();
		GetComponent<Canvas>().overrideSorting = true;
		GetComponent<Canvas>().sortingOrder = layerOffset;
	}
}

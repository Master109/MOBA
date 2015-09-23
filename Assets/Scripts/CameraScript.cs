using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{
	Transform player;
	public float distance;
	public Vector3 offset;
	public int screenBorder;
	public float scrollRate;
	RaycastHit hit;
	bool clickedOnMinimap;

	// Use this for initialization
	void Start ()
	{
		player = transform.parent;
		transform.SetParent(null);
		GetComponent<BoxCollider>().size = new Vector3(GetComponent<AdjustCameraSize>().size, GetComponent<BoxCollider>().size.y, GetComponent<AdjustCameraSize>().size / GetComponent<Camera>().aspect);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.mousePosition.x <= screenBorder)
			transform.position += Vector3.left * scrollRate;
		else if (Input.mousePosition.x >= Screen.width - screenBorder)
			transform.position += Vector3.right * scrollRate;
		if (Input.mousePosition.y <= screenBorder)
			transform.position += Vector3.back * scrollRate;
		else if (Input.mousePosition.y >= Screen.height - screenBorder)
			transform.position += Vector3.forward * scrollRate;
		if (Input.GetMouseButtonDown(0))
			clickedOnMinimap = GameObject.Find("Minimap").GetComponent<Camera>().pixelRect.Contains(Input.mousePosition);
		else if (Input.GetMouseButtonUp(0))
			clickedOnMinimap = false;
		if (clickedOnMinimap && GameObject.Find("Minimap").GetComponent<Camera>().pixelRect.Contains(Input.mousePosition) && Input.GetMouseButton(0) && Physics.Raycast(GameObject.Find("Minimap").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition), out hit))
			transform.position = new Vector3(hit.point.x, player.position.y, hit.point.z) + (-transform.forward * distance) + offset;
		if (Input.GetKey(KeyCode.Space))
			transform.position = player.position + (-transform.forward * distance) + offset;
	}
}

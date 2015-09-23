using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlacementGhost : MonoBehaviour
{
	public Ability ability;
	RaycastHit hit;
	
	// Update is called once per frame
	void Update ()
	{
		transform.eulerAngles = Vector3.zero;
		if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
		{
			if (transform.parent.GetComponent<Renderer>() != null)
			{
				Vector3 idealPos = new Vector3(hit.point.x, 0, hit.point.z);
				float angToIdealPos = Mathf.Atan2(idealPos.z - transform.parent.position.z, idealPos.x - transform.parent.position.x);
				Vector3 pos = new Vector3(Mathf.Cos(angToIdealPos), 0, Mathf.Sin(angToIdealPos)) * Vector3.Distance(Global.TopDownVector(idealPos), Global.TopDownVector(transform.parent.position));
				pos = Vector3.ClampMagnitude(pos, transform.parent.lossyScale.x / 2);
				pos.y = transform.localPosition.y;
				transform.position = transform.parent.position + pos;
				if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
				{
					if (Input.GetMouseButtonDown(0))
					{
						ability.Use ();
						SendMessage("Cast");
					}
					pos = transform.position;
					Vector3 parentLocalScale = transform.parent.localScale;
					transform.parent.SetParent(ability.transform);
					transform.parent.localScale = parentLocalScale;
					transform.position = pos;
					foreach (Renderer r in transform.parent.GetComponentsInChildren<Renderer>())
						r.enabled = false;
					enabled = false;
					Ability.placingAbility = null;
				}
			}
		}
	}
}

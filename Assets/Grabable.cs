using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabable : Clickable
{
	public GameObject top;
	bool isGrabbed = false, justGrabbed=false;
	public override bool clicked()
	{
		if (base.clicked())
		{
			if (!isGrabbed)
			{
				if (top == null) top = gameObject;
				foreach (Rigidbody rb in top.GetComponentsInChildren<Rigidbody>(true))
				{
					rb.isKinematic = true;
				}
				ClickController cc = FindObjectOfType<ClickController>();
				top.transform.position = cc.holdPos.transform.position;
				top.transform.rotation = cc.holdPos.transform.rotation;
				cc.onClick.AddListener(drop);
				top.transform.parent = FindObjectOfType<UnityTemplateProjects.SimplePlayerController>().transform;
				isGrabbed = true;
				justGrabbed = true;
			}
		}
		return false;
	}

	void drop()
	{
		if (justGrabbed) { justGrabbed = false;return; }
		ClickController cc = FindObjectOfType<ClickController>();
		cc.onClick.RemoveListener(drop);

		if (top == null) top = gameObject;
		foreach (Rigidbody rb in top.GetComponentsInChildren<Rigidbody>(true))
		{
			rb.isKinematic = false;
		}
		top.transform.parent = null;
		isGrabbed = false;
	}

}

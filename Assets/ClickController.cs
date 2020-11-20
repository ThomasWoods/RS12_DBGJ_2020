using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ClickController : MonoBehaviour
{
	public float clickableDistance = 5;
	public GameObject holdPos;
	public UnityEvent onClick = new UnityEvent();

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

		if (Mouse.current.leftButton.wasPressedThisFrame)
		{
			RaycastHit hitInfo = default;
			bool hit = Physics.Raycast(new Ray(transform.position, transform.forward), out hitInfo, clickableDistance);
			if (hit)
			{
				Clickable clicked = hitInfo.collider.GetComponent<Clickable>();
				if (clicked != null)
				{
					clicked.clicked();
				}
			}
			onClick.Invoke();
		}
	}
}

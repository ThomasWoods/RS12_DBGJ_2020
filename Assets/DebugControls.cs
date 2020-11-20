using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugControls : MonoBehaviour
{
	public GameObject Spawner;
    // Start is called before the first frame update
    void Start()
    {

	}

    // Update is called once per frame
    void Update()
	{
		if (Keyboard.current.slashKey.wasPressedThisFrame && Keyboard.current.shiftKey.isPressed && Keyboard.current.leftAltKey.isPressed)
		{
			collectall();
		}
		if (Keyboard.current.spaceKey.wasPressedThisFrame && Keyboard.current.shiftKey.isPressed && Keyboard.current.leftAltKey.isPressed)
		{
			spawn();
		}
	}

	void collectall()
	{
		foreach (Prize p in FindObjectsOfType<Prize>())
		{
			p.won();
		}
	}

	void spawn()
	{
		Spawner.SetActive(true);
	}
}

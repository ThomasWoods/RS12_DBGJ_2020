using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Clickable : MonoBehaviour
{
	public UnityEvent OnClick = new UnityEvent();
	virtual public bool clicked()
	{
		if (isActiveAndEnabled)
		{
			OnClick.Invoke();
			return true;
		}
		return false;
	}
}

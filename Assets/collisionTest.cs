using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class collisionTest : MonoBehaviour
{
	public int triggers = 0, collisions=0;

	private void OnTriggerEnter(Collider other)
	{
		triggers++;
		Debug.Log(this+" OnTriggerEnter: "+other);
	}
	private void OnTriggerExit(Collider other)
	{
		triggers--;
		Debug.Log(this + " OnTriggerExit " + other);
	}
	private void OnCollisionEnter(Collision collision)
	{
		collisions++;
		Debug.Log(this + " OnCollisionEnter " + collision);
	}
	private void OnCollisionExit(Collision collision)
	{
		collisions--;
		Debug.Log(this + " OnCollisionExit " + collision);
	}
}

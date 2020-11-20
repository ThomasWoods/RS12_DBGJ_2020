using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrizeCatch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	private void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<Prize>() != null)
		{
			other.GetComponent<Prize>().won();
		}
	}
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.GetComponent<Prize>() != null)
		{
			collision.gameObject.GetComponent<Prize>().won();
		}
	}
}

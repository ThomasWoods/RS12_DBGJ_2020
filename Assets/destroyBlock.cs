using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class destroyBlock : Clickable
{
	public Vector3 trashcan = new Vector3(100, 100, 100);
	public float attacksPerSec = 1;
	public float lastAttack = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

	public override bool clicked()
	{
		if(base.clicked()){
			if (Time.time > lastAttack + 1 / attacksPerSec)
			{
				lastAttack = Time.time;
				StartCoroutine(DestroyBlock());
				return true;
			}
		}
		return false;
	}

	IEnumerator DestroyBlock()
	{
		RaycastHit hitInfo = default;
		bool hit = Physics.Raycast(new Ray(transform.position, transform.forward), out hitInfo, 5,LayerMask.GetMask("World"));
		if (hit && hitInfo.collider.tag.Contains("Destructable"))
		{
			//hitInfo.collider.attachedRigidbody.transform.position = new Vector3(100, 100, 100);
			hitInfo.collider.transform.position = trashcan;
			yield return null;
			hitInfo.collider.gameObject.SetActive(false);
			Destroy(hitInfo.collider.gameObject);
		}
	}
}

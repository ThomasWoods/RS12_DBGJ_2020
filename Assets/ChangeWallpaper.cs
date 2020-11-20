using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWallpaper : MonoBehaviour
{
	public GameObject target;
	public Material material;
	private void OnEnable()
	{
		MeshRenderer mr = target.GetComponent<MeshRenderer>();
		mr.material = material;
	}
}

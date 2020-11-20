using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flicker : MonoBehaviour
{
	public GameObject o;
	public float[] pattern = new float[]{1};
	public int i=0;
    // Start is called before the first frame update
    void Start()
    {
		StartCoroutine(logic());
    }

	IEnumerator logic()
	{
		o.SetActive(!o.activeSelf);
		i++;
		if (i >= pattern.Length) i = 0;
		yield return new WaitForSeconds(pattern[i]);
		StartCoroutine(logic());
	}
}

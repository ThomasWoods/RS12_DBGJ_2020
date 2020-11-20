using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycleObjects : MonoBehaviour
{
	public int current = 0;
	public float delay = 1;
	public int step = 1;

    // Start is called before the first frame update
    void Start()
    {
		if (current >= transform.childCount) current = 0;
		if (current < 0) current = transform.childCount - 1;
		StartCoroutine(next());
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	IEnumerator next()
	{
		try { transform.GetChild(current).GetComponentInChildren<Light>(true).gameObject.SetActive(true); }
		catch (System.Exception e) { }
		current += step;
		if (current >= transform.childCount) current = 0;
		if (current <0 ) current = transform.childCount-1;
		try { transform.GetChild(current).GetComponentInChildren<Light>(true).gameObject.SetActive(false); }
		catch (System.Exception e) { }
		yield return new WaitForSeconds(delay);
		StartCoroutine(next());
	}
}

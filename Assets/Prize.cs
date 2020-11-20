using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prize : MonoBehaviour
{
	public GameObject top, prize;
	public float speed = 0.5f;
	GameObject emitter;
	Vector3 end;

	public void won()
	{
		if (prize == null) { Debug.Log(this+" has no prize"); return; }
		if (top == null) top = gameObject;
		StartCoroutine(wonProcedure());
	}

	IEnumerator wonProcedure()
	{
		Vector3 current = top.transform.position;
		end = prize.transform.position;
		emitter = Instantiate(Resources.Load("ParticleTrail") as GameObject);
		ParticleSystem emitterP = emitter.GetComponent<ParticleSystem>();
		GameObject audioA = emitter.transform.GetChild(0).gameObject;
		GameObject audioB = emitter.transform.GetChild(1).gameObject;

		foreach (MeshRenderer mr in top.GetComponentsInChildren<MeshRenderer>())
		{
			mr.enabled = false;
		}
		while (current != end) {
			current = Vector3.MoveTowards(current, end, speed);
			var shape = emitterP.shape;
			shape.position = current;
			audioA.transform.position = current;
			audioB.transform.position = current;
			yield return null;
		}
		yield return new WaitForSeconds(0.5f);
		if (prize != null) prize.SetActive(true);
		emitter.transform.GetChild(0).gameObject.SetActive(false);
		emitter.transform.GetChild(1).gameObject.SetActive(true);
		yield return new WaitForSeconds(1f);
		Destroy(emitter);
		Destroy(top.gameObject);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CraneController : MonoBehaviour
{
	public HingeJoint[] FingerJoints;
	public SpringJoint Line;
	public enum CraneState { Locked, MoveRight, MoveBack, Dropping, Returning }
	public CraneState state = CraneState.Locked;
	public float FingerOpenSpeed=10, FingerOpenMin = -30, FingerOpenMax = 5;
	public float LineSpeed=10, LineMin = 0, LineMax = 15;
	public GameObject DropPoint;
	public Vector3 DropperZero, DropperMax;
	public float maxDropperMoveSpeed = 1;
	public GameObject thumb;
	Vector3 thumbStart, thumbEnd;
	public float thumbdistance = 0, thumbSpeed=1;
	
	bool armIsMoving, armIsReturning, lineIsMoving;

	public AudioSource whir;

	float fingerRange, lineRange;

	private void Awake()
	{
		fingerRange = FingerOpenMax - FingerOpenMin + 1;
		lineRange = LineMax - LineMin + 1;
		DropperZero = DropPoint.transform.position;
		DropperMax = DropPoint.transform.position+new Vector3(18,0,-18);
		thumbStart = thumb.transform.position;
		thumbEnd = thumbStart -new Vector3(0, thumbdistance, 0);
	}

	public void Drop()
	{
		//Debug.Log("Drop!");
		if (state == CraneState.MoveBack)
		{
			state = CraneState.Dropping;
			StartCoroutine(DropSequence());
		}
	}

	IEnumerator DropSequence()
	{
		yield return StartCoroutine(ThumbPress());
		yield return StartCoroutine(OpenClaws());
		yield return StartCoroutine(LowerClaw());
		yield return StartCoroutine(CloseClaws());
		yield return StartCoroutine(RaiseClaw());
		yield return StartCoroutine(ReturnToZero());
		yield return new WaitForSeconds(1);
		yield return StartCoroutine(OpenClaws());
	}

	IEnumerator ThumbPress()
	{

		while (thumb.transform.position != thumbEnd)
		{
			thumb.transform.position = Vector3.MoveTowards(thumb.transform.position, thumbEnd, thumbSpeed);
			yield return null;
		}
		while (thumb.transform.position != thumbStart)
		{
			thumb.transform.position = Vector3.MoveTowards(thumb.transform.position, thumbStart, thumbSpeed);
			yield return null;
		}
	}

	IEnumerator OpenClaws()
	{
		float t = Time.time;
		while (Time.time < t + FingerOpenSpeed)
		{
			foreach (HingeJoint joint in FingerJoints)
			{
				float nextVal = joint.limits.max - (fingerRange * (1 / FingerOpenSpeed) * Time.deltaTime);
				//Debug.Log("NextVal:"+nextVal);
				if (nextVal < FingerOpenMin) nextVal = FingerOpenMin;
				if (nextVal > FingerOpenMax) nextVal = FingerOpenMax;
				JointLimits limits = joint.limits;
				limits.max = nextVal;
				joint.limits = limits;
			}
			yield return null;
		}
	}

	IEnumerator LowerClaw()
	{
		lineIsMoving = true;
		float t = Time.time;
		while (Time.time < t + LineSpeed)
		{
			float nextVal = Line.minDistance + (lineRange * (1 / LineSpeed) * Time.deltaTime);
			if (nextVal < LineMin) nextVal = LineMin;
			if (nextVal > LineMax) nextVal = LineMax;
			Line.minDistance = nextVal;
			yield return null;
		}
		lineIsMoving = false;
	}
	IEnumerator RaiseClaw()
	{
		lineIsMoving = true;
		float t = Time.time;
		while (Time.time < t + LineSpeed)
		{
			float nextVal = Line.minDistance - (lineRange * (1 / LineSpeed) * Time.deltaTime);
			if (nextVal < LineMin) nextVal = LineMin;
			if (nextVal > LineMax) nextVal = LineMax;
			Line.minDistance = nextVal;
			yield return null;
		}
		lineIsMoving = false;
	}

	IEnumerator CloseClaws()
	{
		float t = Time.time;
		while (Time.time < t + FingerOpenSpeed)
		{
			foreach (HingeJoint joint in FingerJoints)
			{
				float nextVal = joint.limits.max + (fingerRange * (1 / FingerOpenSpeed) * Time.deltaTime);
				if (nextVal < FingerOpenMin) nextVal = FingerOpenMin;
				if (nextVal > FingerOpenMax) nextVal = FingerOpenMax;
				JointLimits limits = joint.limits;
				limits.max = nextVal;
				joint.limits = limits;
			}
			yield return null;
		}
	}

	IEnumerator ReturnToZero()
	{
		armIsReturning = true;
		while (DropPoint.transform.position != DropperZero)
		{
			DropPoint.transform.position = Vector3.MoveTowards(DropPoint.transform.position, DropperZero, maxDropperMoveSpeed);
			yield return null;
		}
		state = CraneState.MoveBack;
		armIsReturning = false;
	}

	public void MoveBack()
	{

	}

	public void MoveRight()
	{

	}

	private void Update()
	{
		Vector3 moveVector = new Vector3(
			Keyboard.current.upArrowKey.isPressed ? 1 : 0, 0,
			Keyboard.current.rightArrowKey.isPressed ? -1 : 0);
		if (moveVector != Vector3.zero)
		{
			armIsMoving = true;
			Vector3 newPos = new Vector3(Mathf.Min(DropPoint.transform.position.x + moveVector.x, DropperMax.x),
				DropperMax.y,
				Mathf.Max(DropPoint.transform.position.z + moveVector.z, DropperMax.z));
			//Debug.Log("moveVector: " + moveVector);
			//Debug.Log("Positon, Target: " + DropPoint.transform.position + " + " + newPos);
			DropPoint.transform.position = Vector3.MoveTowards(DropPoint.transform.position, newPos, maxDropperMoveSpeed);
		}
		else armIsMoving = false;
		if (armIsMoving || armIsReturning || lineIsMoving) { if(!whir.isPlaying) whir.Play(); }
		else { whir.Stop(); }
		//if (Keyboard.current.rightArrowKey.isPressed) MoveRight();
		//if (Keyboard.current.upArrowKey.isPressed) MoveBack();
	}
}

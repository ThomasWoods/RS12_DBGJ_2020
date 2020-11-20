using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class createWorld : MonoBehaviour
{
	public GameObject block;
	public int x = 5, y = 5, z = 5;
	Vector3 OffsetC = new Vector3(0, 0.01f, 0.5f), 
			OffsetD = new Vector3(0.2887f, 0, 0);
	Vector3 StepR = new Vector3(0, 0, 1f), 
			StepC = new Vector3(0.2887f, 0, 0),
			StepD = new Vector3(0, 0.5f, 0);
	public Transform worldParent;
	public Vector3 startLoc;

	public UnityEvent OnFinish = new UnityEvent();

    // Start is called before the first frame update
    void OnEnable()
    {
		StartCoroutine(createBlock(new Vector3(0, 0), x, y, z));
		
    }

    // Update is called once per frame
    void Update()
    {
	}

	IEnumerator createBlock(Vector3 start, int columns, int rows, int depths)
	{
		for (int depth = 0; depth < depths; depth++)
		{
			for (int col = 0; col < columns; col++)
			{
				for (int row = 0; row < rows; row++)
				{
					Vector3 position = (StepR * row) + (StepC * col) + (StepD * depth);
					if (col % 2 != 0) position += OffsetC;
					if (depth % 2 != 0) position += OffsetD;
					Instantiate(block, transform.TransformPoint(startLoc)+position, block.transform.rotation,worldParent).transform.localScale = block.transform.localScale;

				}
				yield return null;
			}
		}
		yield return new WaitForSeconds(1);
		OnFinish.Invoke();
		/*
		{
			blockNum++;
			GameObject.Instantiate(block, new Vector3(blockNum, 0, 0), Quaternion.identity);
			yield return null;
		}
		*/
	}
}

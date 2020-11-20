using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayMusic : Clickable
{
	StereoController stereo;
	public int songnum=0;
    // Start is called before the first frame update
    void Start()
    {
		stereo = FindObjectOfType<StereoController>();
    }

	public override bool clicked()
	{
		if (base.clicked())
		{
			stereo.Play(songnum);
			return true;
		}
		return false;
	}
}

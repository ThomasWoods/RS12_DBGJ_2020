using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StereoController : MonoBehaviour
{
	public AudioSource audioSource;
	public Text nowPlayingText;
	public Image nowPlayingPanel;
	RectTransform nowPlayingRect;
	Vector3 nowPlayingStart, nowPlayingEnd;
	public float nowPlayingSpeed = 1;
	Coroutine running = null;

	[System.Serializable]
	public struct Song 
	{ 
		public AudioClip clip;
		public string band, album, title;
		public Material cover; 
	}

	public Song[] songs;

	// Start is called before the first frame update
	void Start()
    {
		nowPlayingRect = nowPlayingPanel.GetComponent<RectTransform>();
		nowPlayingStart = nowPlayingPanel.transform.position;
		nowPlayingEnd = nowPlayingStart + new Vector3(0, nowPlayingPanel.GetComponent<RectTransform>().sizeDelta.y, 0);

		//if (songs.Length > 0) Play(0);
	}

    // Update is called once per frame
    void Update()
	{
		

	}

	public void Replay()
	{
		audioSource.Play();
		StartCoroutine(ShowNowPlay());
	}
	public void Play(int clipNum)
	{
		audioSource.clip= songs[clipNum].clip;
		audioSource.Play();
		updateNowPlaying(clipNum);
		if (running != null) StopCoroutine(running);
		running=StartCoroutine(ShowNowPlay());
	}

	public void updateNowPlaying(int i)
	{
		nowPlayingText.text = songs[i].title+"\n"+ songs[i].band + "\n"+ songs[i].album;
	}

	IEnumerator ShowNowPlay()
	{
		while (nowPlayingRect.position != nowPlayingEnd)
		{
			nowPlayingRect.position = Vector3.MoveTowards(nowPlayingRect.position, nowPlayingEnd, nowPlayingSpeed);
			yield return null;
		}
		yield return new WaitForSeconds(6);
		while (nowPlayingRect.position != nowPlayingStart)
		{
			nowPlayingRect.position = Vector3.MoveTowards(nowPlayingRect.position, nowPlayingStart, nowPlayingSpeed);
			yield return null;
		}
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayManager : MonoBehaviour {

	public static PlayManager instance = null;
	Text txtMessage;
	GameObject panel;
	[SerializeField]
	int LoadingTimeOut = 3;
	public string play_ID, play_DisplayName, play_Car;
	public int play_Score, play_Coins;
	// Use this for initialization
	void Awake()
	{
		if(instance == null)
		{
			instance = this;
		} else if (instance != this)
		{
			Destroy(gameObject);
		}

		DontDestroyOnLoad(gameObject);

		panel = transform.Find("CanvasLoading").Find("Panel").gameObject;
		txtMessage = panel.GetComponentInChildren<Text>();
	}
	
	// Update is called once per frame
	public void LoadingShow () {
		if (!panel.activeInHierarchy)
		{
			panel.SetActive(true);
		}
	}

	public void LoadingHide()
	{
		StartCoroutine(Timer());
	}

	IEnumerator Timer()
	{
		yield return new WaitForSeconds(LoadingTimeOut);
		panel.SetActive(false);
	}

	public void LoadingMessage(string msg)
	{
		LoadingShow();
		txtMessage.text = msg;
	}
}

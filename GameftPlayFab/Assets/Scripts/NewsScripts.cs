using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using UnityEngine.UI;
using PlayFab.ClientModels;
using System;

public class NewsScripts : MonoBehaviour {

	// Use this for initialization
	void Awake()
	{
		var request = new GetTitleNewsRequest();
		PlayFabClientAPI.GetTitleNews(request, GetTitleSuccess, GetTitleFail);
	}

    private void GetTitleFail(PlayFabError err)
    {
        GetComponent<Text>().text = "Get title fail !";
    }

    private void GetTitleSuccess(GetTitleNewsResult result)
    {
		foreach ( var item in result.News)
		{
			GetComponent<Text>().text = item.Timestamp + " " + item.Title + " : " + item.Body;
			Debug.Log(item.Title + " : " + item.Body);
		}
    }
}

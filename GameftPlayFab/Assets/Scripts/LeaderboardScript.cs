using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using System;
using UnityEngine.SceneManagement;

public class LeaderboardScript : MonoBehaviour {

	[SerializeField]
	GameObject Rank, Name, Score;
	PlayManager playManager;
	
	void Awake()
	{
		playManager = GameObject.Find("PlayFabManager").GetComponent<PlayManager>();
		playManager.LoadingMessage("Loading LeaderBoard...");
		ReadLeaderBoard();
	}

	void ReadLeaderBoard()
	{
		var request = new GetLeaderboardRequest()
		{
			StatisticName = "score",
			StartPosition = 0,
			MaxResultsCount = 10			
		};

		PlayFabClientAPI.GetLeaderboard(request, LeaderboardSuccess, LeaderboardFail);
	}

    private void LeaderboardFail(PlayFabError err)
    {
        playManager.LoadingMessage(err.ErrorMessage);
		playManager.LoadingHide();
    }

    private void LeaderboardSuccess(GetLeaderboardResult result)
    {
        foreach(var item in result.Leaderboard)
		{
			GameObject GoRank = Instantiate(Rank, this.transform);
			GoRank.GetComponent<Text>().text = (item.Position + 1).ToString();

			GameObject GoName = Instantiate(Name, this.transform);
			GoName.GetComponent<Text>().text = item.DisplayName;

			GameObject GoScore = Instantiate(Score, this.transform);
			GoScore.GetComponent<Text>().text = item.StatValue.ToString();
		}

		playManager.LoadingHide();
    }

	public void Home()
	{
		SceneManager.LoadScene("Menu");
	}
}

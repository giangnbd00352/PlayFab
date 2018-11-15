using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;
using System;

public class UIGameScripts : MonoBehaviour {

	[SerializeField] Text txtCoin, txtScore;
	PlayManager playManager;

	private int InitCoins;
	private int coins;
	private int score;
	public int Coins
	{
		get { return coins; }
		set 
		{ 
			coins = value;
			txtCoin.text = coins.ToString();
		}
	}

	public int Score
	{
		get { return score; }
		set 
		{ 
			score = value;
			txtScore.text = score.ToString();
		}
	} 

	void Awake()
	{
		playManager = GameObject.Find("PlayFabManager").GetComponent<PlayManager>();
		Score = playManager.play_Score;
		Coins = playManager.play_Coins;
		InitCoins = Coins;
	}

	void UpdatePlayFabManagerInfo()
	{
		playManager.play_Score = Score;
		playManager.play_Coins = Coins;
	}
	
	void FixedUpdate()
	{
		if(Input.anyKey)
		{
			Score++;
		}
	}

	public void Home()
	{
		//Update Score
		playManager.LoadingMessage("Updating data...");
		UpdatePlayFabManagerInfo();

		var request = new UpdatePlayerStatisticsRequest()
		{
			Statistics = new List<StatisticUpdate>
			{
				new StatisticUpdate { StatisticName = "score", Value = Score}
			}
		};

		PlayFabClientAPI.UpdatePlayerStatistics(request, SuccessUpdate, FailUpdate);

		
	}

    private void FailUpdate(PlayFabError err)
    {
        playManager.LoadingMessage(err.ErrorMessage);
		playManager.LoadingHide();
    }

    private void SuccessUpdate(UpdatePlayerStatisticsResult success)
    {
		UpdateCoins();		
    }

	void UpdateCoins()
	{
		AddUserVirtualCurrencyRequest request = new AddUserVirtualCurrencyRequest();
		request.VirtualCurrency = "CO";
		request.Amount = Coins - InitCoins;

		PlayFabClientAPI.AddUserVirtualCurrency(request, UpdateCoinsSuccess, UpdateCoinsFail);
	}

    private void UpdateCoinsFail(PlayFabError err)
    {
        playManager.LoadingMessage(err.ErrorMessage);
		playManager.LoadingHide();
    }

    private void UpdateCoinsSuccess(ModifyUserVirtualCurrencyResult obj)
    {
		playManager.LoadingHide();
		SceneManager.LoadScene("Menu");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab.ClientModels;
using PlayFab;
using System;
using UnityEngine.SceneManagement;

public class AuthScript : MonoBehaviour {

	[SerializeField]
	InputField ifEmail, ifPassword, ifDisplayName;
	[SerializeField]
	PlayManager playManager;
	public void RegisterPlayer()
	{
		playManager.LoadingMessage("Connecting server...");
		var request = new RegisterPlayFabUserRequest()
		{
			Email = ifEmail.text,
			Password = ifPassword.text,
			DisplayName = ifDisplayName.text,
			Username = ifDisplayName.text
		};

		PlayFabClientAPI.RegisterPlayFabUser(request, Success, Failed);
	}

    private void Failed(PlayFabError err)
    {
        playManager.LoadingMessage(err.ErrorMessage);
		playManager.LoadingHide();
    }

    private void Success(RegisterPlayFabUserResult success)
    {
		playManager.LoadingMessage("Initialize Statistics...");
		InitStat();
    }

	void InitStat()
	{
		var request = new UpdatePlayerStatisticsRequest()
		{
			Statistics = new List<StatisticUpdate>
			{
				new StatisticUpdate { StatisticName="score", Value=0 }
			}
		};

		PlayFabClientAPI.UpdatePlayerStatistics(request, InitStatSuccess, InitStatFail);
	}

    private void InitStatFail(PlayFabError err)
    {
        playManager.LoadingMessage(err.ErrorMessage);
		playManager.LoadingHide();
    }

    private void InitStatSuccess(UpdatePlayerStatisticsResult result)
    {
        playManager.LoadingMessage("Register Success");
		playManager.LoadingHide();
    }

    public void LoginPlayer()
	{
		playManager.LoadingMessage("Connecting server...");

		var request = new LoginWithEmailAddressRequest()
		{
			Password = ifPassword.text,
			Email = ifEmail.text
		};

		PlayFabClientAPI.LoginWithEmailAddress(request, LoginSuccess, LoginFailed);
	}

    private void LoginFailed(PlayFabError err)
    {
        playManager.LoadingMessage(err.ErrorMessage);
		playManager.LoadingHide();
    }

    private void LoginSuccess(LoginResult success)
    {
       playManager.LoadingMessage("Login Successfull");
	   playManager.play_ID = success.PlayFabId;
	   playManager.LoadingHide();
	   //SceneManager.LoadScene("Menu");
	   //Get DisplayName
		GetDisplayName();
    }

	void GetDisplayName()
	{
		playManager.LoadingMessage("Loading DisplayName... ");
		var request = new GetAccountInfoRequest();
		PlayFabClientAPI.GetAccountInfo(request, InfoSuccess, InfoFail);
	}

    private void InfoFail(PlayFabError err)
    {
        playManager.LoadingMessage(err.ErrorMessage);
		playManager.LoadingHide();
    }

    private void InfoSuccess(GetAccountInfoResult result)
    {
        playManager.play_DisplayName = result.AccountInfo.Username;
		//read stat
		ReadStatScore();
    }

	void ReadStatScore()
	{
		playManager.LoadingMessage("Loading Statistics...");

		var request = new GetPlayerStatisticsRequest();
		PlayFabClientAPI.GetPlayerStatistics(request, StatSuccess, StatFail);
	}

    private void StatFail(PlayFabError err)
    {
        playManager.LoadingMessage(err.ErrorMessage);
		playManager.LoadingHide();
    }

    private void StatSuccess(GetPlayerStatisticsResult result)
    {
		playManager.play_Score = result.Statistics[0].Value;
		playManager.LoadingMessage("Login Successfull !");
		playManager.LoadingHide();
		GetBalance();
    }

	void GetBalance()
	{
		var request = new GetUserInventoryRequest();
		PlayFabClientAPI.GetUserInventory(request, InventorySuccess,InventoryFail);
	}

    private void InventoryFail(PlayFabError err)
    {
        playManager.LoadingMessage(err.ErrorMessage);
		playManager.LoadingHide();
    }

    private void InventorySuccess(GetUserInventoryResult result)
    {
        foreach(var item in result.VirtualCurrency)
		{
			if(item.Key=="CO")
			{
				playManager.play_Coins = item.Value;
			}
		}

		playManager.LoadingMessage("Loading Currency Successfull");
		GetCar();	
    }

	void GetCar()
	{
		var request = new GetUserDataRequest();
		PlayFabClientAPI.GetUserData(request, SuccessCar, FailedCar);
	}

    private void SuccessCar(GetUserDataResult result)
    {
		if (result.Data == null || !result.Data.ContainsKey("Car"))
		{
			playManager.LoadingMessage("Login Successfull...");
			playManager.LoadingHide();
			playManager.play_Car = "orange";
			LoadMenu();
		}
		else if (result.Data.ContainsKey("Car"))
		{
			playManager.play_Car = result.Data["Car"].Value;
			playManager.LoadingMessage("Login Successfull...");
			playManager.LoadingHide();
			LoadMenu();
		}
	}

    private void FailedCar(PlayFabError err)
    {
        playManager.LoadingMessage(err.ErrorMessage);
		playManager.LoadingHide();
    }

	void LoadMenu()
	{
		playManager.LoadingHide();
		SceneManager.LoadScene("Menu");
	}
}

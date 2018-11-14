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
		playManager.LoadingMessage("Register Success ! ");
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
	   SceneManager.LoadScene("Menu");
    }
}

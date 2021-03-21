using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using System;

public class PlayFabRegister : MonoBehaviour
{

    [SerializeField] Text newUsername, newUserEmail, newUserPassword;
    [SerializeField] Text signInUserEmail, singInUserPassword;
    [SerializeField] MainMenuScript mms;
    string enctyptedPassword;

    public void Register()
    {
        var registerRequest = new RegisterPlayFabUserRequest { Username = newUsername.text, Email = newUserEmail.text, Password = Encrypt(newUserPassword.text) };
        PlayFabClientAPI.RegisterPlayFabUser(registerRequest, OnRegisterRequestSuccess, OnRegisterFailure);
    }
    public void Login()
    {
        var request = new LoginWithEmailAddressRequest { Email = signInUserEmail.text, Password = Encrypt(singInUserPassword.text) };
        PlayFabClientAPI.LoginWithEmailAddress(request, LoginSuccess, LoginFailure);
    }

    private void LoginFailure(PlayFabError obj)
    {
        Debug.LogError(obj.GenerateErrorReport());
    }

    private void LoginSuccess(LoginResult obj)
    {
        Debug.Log("Success");
        mms.StartGame(obj.PlayFabId);
    }

    private void OnRegisterFailure(PlayFabError obj)
    {
        Debug.LogError(obj.GenerateErrorReport());

    }

    void OnRegisterRequestSuccess(RegisterPlayFabUserResult obj)
    {
        Debug.Log("Success");
        mms.StartGame(obj.PlayFabId);
    }

    
    string Encrypt(string pw)
    {
        System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] epw = System.Text.Encoding.UTF8.GetBytes(pw);
        epw = x.ComputeHash(epw);
        System.Text.StringBuilder s = new System.Text.StringBuilder();
        foreach(byte b in epw)
        {
            s.Append(b.ToString("x2").ToLower());
        }
        return pw.ToString();
    }

}

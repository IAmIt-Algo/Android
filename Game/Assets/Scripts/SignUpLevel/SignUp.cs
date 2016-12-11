using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Mindblower.Gui;
using Mindblower.Core;
using System;
using UnityEngine.UI;
using System.Threading;

public class SignUp : MonoBehaviour, IAmItRequestListener {

    public Text inputEmail;
    public Text inputPassword;
    private bool _visible = false;
    private string _code = "";
    private bool _isShowed = false;

    public void ButtonClick()
    {
        UserRegistrationModel model = new UserRegistrationModel();
        model.Email = inputEmail.text;
        model.Password = inputPassword.text;
        IAmItHttpRequest.Registration(model, this);
    }

    public void OnFail(string code)
    {
        _isShowed = false;
        _visible = true;
        _code = code;
    }

    void OnGUI()
    {
        if (_visible)
        {
            GUI.Box(new Rect(Screen.width / 2 - Screen.width * 4 / 10, Screen.height * 23 / 32, Screen.width * 4 / 5, Screen.height / 4), "");
            GUI.Label(new Rect(Screen.width / 2 - Screen.width * 4 / 10, Screen.height * 23 / 32, Screen.width*4/5, Screen.height / 4), _code);
            Thread myThread = new Thread(new ThreadStart(Pause));
            myThread.Start();
        }
    }

    public void Pause()
    {
        if (!_isShowed)
        {
            _isShowed = true;
            Debug.Log("Count");
            Thread.Sleep(3000);
            _visible = false;
        }
    }

    public void OnGet<T>(T responseModel)
    {
        throw new NotImplementedException();
    }

    public void OnLogin()
    {
        throw new NotImplementedException();
    }

    public void OnLogOut()
    {
        throw new NotImplementedException();
    }

    public void OnPost(string s)
    {
        SceneManager.LoadScene("SignInLevel");
    }
}

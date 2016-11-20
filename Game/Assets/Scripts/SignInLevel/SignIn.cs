using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Mindblower.Gui;
using Mindblower.Core;
using System;
using UnityEngine.UI;

public class SignIn : MonoBehaviour, IAmItRequestListener {

    public Text inputEmail;
    public Text inputPassword;

    public void ButtonClick()
    {
        UserLoginModel model = new UserLoginModel();
        model.Email = inputEmail.text;
        model.Password = inputPassword.text;
        IAmItHttpRequest.Login(model, this);
        //SceneManager.LoadScene("MapLevel");
        //Запустить какой-нибудь progress bar до завершения запроса
    }

    public void OnFail(string code)
    {
        Debug.Log("Request failed, code = " + code);
        //Неверно введен логин или пароль, обработать.
    }

    public void OnGet<T>(T responseModel)
    {
        throw new NotImplementedException();
    }

    public void OnLogin()
    {
        Debug.Log("Request succeed, Login");
        //Все правильно, погрузка карты
        SceneManager.LoadScene("MapLevel");
        
    }

    public void OnLogOut()
    {
        throw new NotImplementedException();
    }

    public void OnPost(string s)
    {
        throw new NotImplementedException();
    }
}

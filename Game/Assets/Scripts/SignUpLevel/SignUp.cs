using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Mindblower.Gui;
using Mindblower.Core;
using System;
using UnityEngine.UI;

public class SignUp : MonoBehaviour, IAmItRequestListener {

    private bool canRegistrate = true;
    private bool isRegistrated = false;

    public Text inputEmail;
    public Text inputPassword;
    //public Text inputDublicatePassword;

    public void ButtonClick()
    {
        Debug.Log("Registration");
        //if (inputPassword.text.Equals(inputDublicatePassword.text))
        //{
        if (canRegistrate && isRegistrated == false)
            {
                UserRegistrationModel model = new UserRegistrationModel();
                model.Email = inputEmail.text;
                model.Password = inputPassword.text;

                Debug.Log(IAmItServerMethods.REGISTRATION);

                IAmItHttpRequest.Registration(model, this);

                canRegistrate = false;

                //Запустить какой-нибудь progress bar до завершения запроса
            }
        //}
        //else
        //{
            //Неправильно введены пароли

            //Debug.Log("Passwords inputs dont contain the same text");
        //}
        

        //SceneManager.LoadScene("MapLevel");
    }

    public void OnFail(string code)
    {
        Debug.Log("Request failed, code = " + code);
        if (isRegistrated)
        {
            //не получился логин, вывести на экран
        }
        else
        {
            //Не получилась регистрация, вывести на экран, показать код ошибки
            canRegistrate = true;
        }
        
    }

    public void OnGet(string response)
    {
        throw new NotImplementedException();
    }

    public void OnLogin()
    {
        if (isRegistrated)
        {
            Debug.Log("Request succeed, Login");
            SceneManager.LoadScene("MapLevel");
        }
        //throw new NotImplementedException();
    }

    public void OnLogOut()
    {
        throw new NotImplementedException();
    }

    public void OnPost(string s)
    {
        Debug.Log("Request succeed, Registration");
        isRegistrated = true;

        UserLoginModel model = new UserLoginModel();
        model.Password = inputPassword.text;
        model.Email = inputEmail.text;

        IAmItHttpRequest.Login(model, this);
    }
}

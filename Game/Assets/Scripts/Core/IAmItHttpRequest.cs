using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;
namespace Mindblower.Core
{

    public class IAmItHttpRequest
    {

        private const string SERVER_ADDRESS = "http://ec2-184-72-112-237.compute-1.amazonaws.com/";
        private static string token;

        public static void Registration(UserRegistrationModel model, IAmItRequestListener listener)
        {

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(SERVER_ADDRESS + IAmItServerMethods.REGISTRATION);

            Debug.Log("In Registration: " + JsonConvert.SerializeObject(model));

            var data = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(model));

            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode.Equals(HttpStatusCode.OK))
            {
                listener.OnPost(new StreamReader(response.GetResponseStream()).ReadToEnd());
                JsonConvert.DeserializeObject(new StreamReader(response.GetResponseStream()).ReadToEnd());
            }
            else
            {
                listener.OnFail(response.StatusCode.ToString());
            }

        }

        public static void Login(UserLoginModel model, IAmItRequestListener listener)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(SERVER_ADDRESS + IAmItServerMethods.LOGIN);

            Debug.Log("In Login: " + JsonConvert.SerializeObject(model));

            var data = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(model));

            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode.Equals(HttpStatusCode.OK))
            {
                string cookies = response.Headers.Get("Set-Cookie");
                cookies.Trim();
                for (int i = 40; i < cookies.Length; i++)
                {
                    if (cookies.ElementAt(i) == ';')
                    {
                        token = cookies.Substring(26, i);
                        Debug.Log("In Login: token is " + token);
                        break;
                    }

                }

                listener.OnLogin();
                JsonConvert.DeserializeObject(new StreamReader(response.GetResponseStream()).ReadToEnd());
            }
            else
            {
                listener.OnFail(response.StatusCode.ToString());
            }
        }

        public static void Post <T> (T model, string method, IAmItRequestListener listener)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(SERVER_ADDRESS + method);
            
            var data = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(model));

            request.Method = "POST";
            request.Headers.Add(HttpRequestHeader.Cookie, ".AspNet.ApplicationCookie=" + token);
            request.ContentType = "application/json";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode.Equals(HttpStatusCode.OK))
            {
                listener.OnPost(new StreamReader(response.GetResponseStream()).ReadToEnd());
                //JsonConvert.DeserializeObject(new StreamReader(response.GetResponseStream()).ReadToEnd());
            }
            else
            {
                listener.OnFail(response.StatusCode.ToString());
            }
        }

        public static void Get <T>(string method, IAmItRequestListener listener, T inputModel)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(SERVER_ADDRESS + method);
            request.Method = "GET";
            request.Headers.Add(HttpRequestHeader.Cookie, ".AspNet.ApplicationCookie=" + token);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode.Equals(HttpStatusCode.OK))
            {
                inputModel = JsonConvert.DeserializeObject<T>(new StreamReader(response.GetResponseStream()).ReadToEnd());
                listener.OnGet(new StreamReader(response.GetResponseStream()).ReadToEnd());

            }
            else
            {
                listener.OnFail(response.StatusCode.ToString());
            }

        }

        /*public static void update(IAmItRequestListener listener)
        {
            WebRequest request = WebRequest.Create(SERVER_ADDRESS);
            request.Method = "POST";

            WebResponse response = request.GetResponse();


        }*/
    }


    
    public interface IAmItRequestListener
    {
        void OnLogin();

        void OnFail(string code);

        void OnGet<T>(T responseModel);

        //void OnPost(JSONObject response);

        void OnPost(string s);

        //void onUpdate(JSONObject answer);

        void OnLogOut();
    }

    public static class IAmItServerMethods
    {
        public static string LOGIN = "login";
        public static string REGISTRATION = "registration";
        public static string ADD_ATTEMPT = "addAttempt";
        public static string CHANGE_CREDENTIALS = "changeCredentials";
        public static string GET_RATING_POSITION = "getRatingPosition ";
        public static string LOGOUT = "logOff";
    }

}

﻿using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;
using System.Runtime.InteropServices;

namespace Mindblower.Core
{

    public class IAmItHttpRequest
    {

        private const string SERVER_ADDRESS = "http://ec2-184-72-112-237.compute-1.amazonaws.com/";
        private static string Token
        {
            get{
                return PlayerPrefs.GetString("Token");
            }
            set{
                PlayerPrefs.SetString("Token", value);
            }
        }
            

        public static void Registration(UserRegistrationModel model, IAmItRequestListener listener)
        {
            bool isConnected = true;
            WebClient Client = new WebClient();
            String Response;
            try
            {
                Response = Client.DownloadString("http://www.google.com");
            } catch (WebException e)
            {
                isConnected = false;
                listener.OnFail("Error. Check up you internet connection");
            }
            if (isConnected)
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(SERVER_ADDRESS + IAmItServerMethods.REGISTRATION);

                var data = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(model));

                request.Method = "POST";
                request.ContentType = "application/json";
                request.ContentLength = data.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                try
                {
                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        if (response.StatusCode.Equals(HttpStatusCode.OK))
                        {
                            PlayerPrefs.DeleteKey("Token");
                            Synchronizer.IsUsed = false;
                            listener.OnPost(new StreamReader(response.GetResponseStream()).ReadToEnd());
                        }
                    }
                }
                catch (WebException e)
                {
                    using (HttpWebResponse response = (HttpWebResponse)e.Response)
                    {
                        HttpWebResponse httpResponse = (HttpWebResponse)response;
                        using (Stream data1 = response.GetResponseStream())
                        using (var reader = new StreamReader(data1))
                        {
                            string text = reader.ReadToEnd();
                            listener.OnFail(text);
                        }
                    }
                }
            }
        }

        public static void Login(UserLoginModel model, IAmItRequestListener listener)
        {
            bool isConnected = true;
            WebClient Client = new WebClient();
            String Response;
            try
            {
                Response = Client.DownloadString("http://www.google.com");
            }
            catch (WebException e)
            {
                isConnected = false;
                listener.OnFail("Error. Check up you internet connection");
            }
            if (isConnected)
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(SERVER_ADDRESS + IAmItServerMethods.LOGIN);

                var data = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(model));

                request.Method = "POST";
                request.ContentType = "application/json";
                request.ContentLength = data.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                try
                {
                    using (HttpWebResponse response1 = (HttpWebResponse)request.GetResponse())
                    {
                        if (response1.StatusCode.Equals(HttpStatusCode.OK))
                        {
                            string cookies = response1.Headers.Get("Set-Cookie");
                            cookies.Trim();
                            for (int i = 40; i < cookies.Length; i++)
                            {
                                if (cookies.ElementAt(i) == ';')
                                {
                                    Token = cookies.Substring(26, i);
                                    break;
                                }

                            }

                            listener.OnLogin();
                        }
                    }
                }
                catch (WebException e)
                {
                    using (HttpWebResponse response1 = (HttpWebResponse)e.Response)
                    {
                        HttpWebResponse httpResponse = (HttpWebResponse)response1;
                        using (Stream data1 = response1.GetResponseStream())
                        using (var reader = new StreamReader(data1))
                        {
                            string text = reader.ReadToEnd();
                            listener.OnFail(text);
                        }
                    }
                }
            }
        }

        public static void Post <T> (T model, string method, IAmItRequestListener listener)
        {
            bool isConnected = true;
            WebClient Client = new WebClient();
            String Response;
            try
            {
                Response = Client.DownloadString("http://www.google.com");
            }
            catch (WebException e)
            {
                isConnected = false;
                listener.OnFail("Error. Check up you internet connection");
            }
            if (isConnected)
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(SERVER_ADDRESS + method);

                var data = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(model));

                request.Method = "POST";
                request.Headers.Add(HttpRequestHeader.Cookie, ".AspNet.ApplicationCookie=" + Token);
                request.ContentType = "application/json";
                request.ContentLength = data.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                HttpWebResponse response = null;
                try
                {
                    response = (HttpWebResponse)request.GetResponse();
                } catch(WebException ex)
                {
                    response = (HttpWebResponse)ex.Response;
                    Debug.Log(ex.Message);
                }
                if (response.StatusCode.Equals(HttpStatusCode.OK))
                {
                    listener.OnPost(new StreamReader(response.GetResponseStream()).ReadToEnd());
                }
                else
                {
                    listener.OnFail(response.StatusCode.ToString());
                }
            }
        }

        public static void Get<T>(string method, IAmItRequestListener listener)
        {
            bool isConnected = true;
            WebClient Client = new WebClient();
            String Response;
            try
            {
                Response = Client.DownloadString("http://www.google.com");
            }
            catch (WebException e)
            {
                isConnected = false;
                listener.OnFail("Error. Check up you internet connection");
            }
            if (isConnected)
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(SERVER_ADDRESS + method);
                request.Method = "GET";
                request.Headers.Add(HttpRequestHeader.Cookie, ".AspNet.ApplicationCookie=" + Token);
                HttpWebResponse response = null;
                try
                {
                    response = (HttpWebResponse)request.GetResponse();
                }
                catch (WebException ex)
                {
                    response = (HttpWebResponse)ex.Response;
                    Debug.Log(ex.Message);
                }
                string s2 = new StreamReader(response.GetResponseStream()).ReadToEnd();
            Debug.Log(response.StatusCode + ": " + s2);
                if (response.StatusCode.Equals(HttpStatusCode.OK))
                {
                    string s = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    Debug.Log(s);
                    T outputModel = JsonConvert.DeserializeObject<T>(s);
                    Debug.Log(JsonConvert.SerializeObject(outputModel));
                    listener.OnGet(outputModel);

                }
                else
                {
                    listener.OnFail(response.StatusCode.ToString());
                }
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
        public readonly static string LOGIN = "login";
        public readonly static string REGISTRATION = "registration";
        public readonly static string ADD_ATTEMPT = "addAttempt";
        public readonly static string CHANGE_CREDENTIALS = "changeCredentials";
        public readonly static string GET_RATING_POSITION = "getRating";
        public readonly static string LOGOUT = "logOff";
        public readonly static string GET_INFO = "getInfo";
    }

}

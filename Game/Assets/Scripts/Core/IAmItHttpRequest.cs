using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Helpers;

namespace Assets.Scripts.Core
{

    public class IAmItHttpRequest
    {

        private const string SERVER_ADDRESS = "http://ec2-184-72-112-237.compute-1.amazonaws.com/";
        private static string token;

        public static void registration(Models.UserRegistrationModel model, IAmItRequestListener listener)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(SERVER_ADDRESS + IAmItServerMethods.REGISTRATION);

            var data = Encoding.ASCII.GetBytes(Json.Encode(model));

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            HttpListener httpListener = new HttpListener();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode.ToString().Equals("200"))
            {
                listener.onPost(new StreamReader(response.GetResponseStream()).ReadToEnd());
                Json.Decode(new StreamReader(response.GetResponseStream()).ReadToEnd());
            }
            else
            {
                listener.onFail(response.StatusCode.ToString());
            }

        }

        public static void login(Models.UserLoginModel model, IAmItRequestListener listener)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(SERVER_ADDRESS + IAmItServerMethods.LOGIN);

            var data = Encoding.ASCII.GetBytes(Json.Encode(model));

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            HttpListener httpListener = new HttpListener();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode.ToString().Equals("200"))
            {
                string cookies = response.Headers.Get("Set-Cookie");
                cookies.Trim();
                for (int i = 40; i < cookies.Length; i++)
                {
                    if (cookies.ElementAt(i) == ';')
                    {
                        token = cookies.Substring(26, i);
                        break;
                    }

                }

                listener.onLogin();
                Json.Decode(new StreamReader(response.GetResponseStream()).ReadToEnd());
            }
            else
            {
                listener.onFail(response.StatusCode.ToString());
            }
        }

        public static void post(String paramData, string method, IAmItRequestListener listener)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(SERVER_ADDRESS + method);

            var data = Encoding.ASCII.GetBytes(paramData);

            request.Method = "POST";
            request.Headers.Add(HttpRequestHeader.Cookie, ".AspNet.ApplicationCookie=" + token);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            HttpListener httpListener = new HttpListener();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode.ToString().Equals("200"))
            {
                listener.onPost(new StreamReader(response.GetResponseStream()).ReadToEnd());
                Json.Decode(new StreamReader(response.GetResponseStream()).ReadToEnd());
            }
            else
            {
                listener.onFail(response.StatusCode.ToString());
            }
        }

        public static void get(string method, IAmItRequestListener listener)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(SERVER_ADDRESS + method);
            request.Method = "GET";
            request.Headers.Add(HttpRequestHeader.Cookie, ".AspNet.ApplicationCookie=" + token);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode.ToString().Equals("200"))
            {
                listener.onPost(new StreamReader(response.GetResponseStream()).ReadToEnd());
                Json.Decode(new StreamReader(response.GetResponseStream()).ReadToEnd());

            }
            else
            {
                listener.onFail(response.StatusCode.ToString());
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
        void onLogin();

        void onFail(string code);

        void onGet(String response);

        //void onPost(JSONObject response);

        void onPost(String s);

        //void onUpdate(JSONObject answer);

        void onLogOut();
    }

    public static class IAmItServerMethods
    {
        public static string LOGIN = "login";
        public static string REGISTRATION = "registration";
        public static string ADD_ATTEPT = "addAttempt";
        public static string CHANGE_CREDENTIALS = "changeCredentials";
        public static string GET_RATING_POSITION = "getRatingPosition ";
        public static string LOGOUT = "logOff";
    }

    namespace Models
    {

        public partial class UserRegistrationModel : IEquatable<UserRegistrationModel>
        {

            public string Email { get; set; }
            public string Password { get; set; }

            public UserRegistrationModel(string Email = null, string Password = null)
            {
                this.Email = Email;
                this.Password = Password;
            }


            public override string ToString()
            {
                var sb = new StringBuilder();
                sb.Append("class UserRegistrationModel {\n");
                sb.Append("  Email: ").Append(Email).Append("\n");
                sb.Append("  Password: ").Append(Password).Append("\n");
                sb.Append("}\n");
                return sb.ToString();
            }

            /*public string ToJson()
            {
                return JsonConvert.SerializeObject(this, Formatting.Indented);
            }*/

            public override bool Equals(object obj)
            {
                return this.Equals(obj as UserRegistrationModel);
            }

            public bool Equals(UserRegistrationModel other)
            {
                if (other == null)
                    return false;

                return
                    (
                        this.Email == other.Email ||
                        this.Email != null &&
                        this.Email.Equals(other.Email)
                    ) &&
                    (
                        this.Password == other.Password ||
                        this.Password != null &&
                        this.Password.Equals(other.Password)
                    );
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    int hash = 41;
                    // Suitable nullity checks etc, of course :)
                    if (this.Email != null)
                        hash = hash * 59 + this.Email.GetHashCode();
                    if (this.Password != null)
                        hash = hash * 59 + this.Password.GetHashCode();
                    return hash;
                }
            }
        }


        public partial class UserLoginModel : IEquatable<UserLoginModel>
        {
            public UserLoginModel(string Password = null, string Email = null)
            {
                this.Password = Password;
                this.Email = Email;
            }

            public string Password { get; set; }
            public string Email { get; set; }
            
            public override string ToString()
            {
                var sb = new StringBuilder();
                sb.Append("class UserLoginModel {\n");
                sb.Append("  Password: ").Append(Password).Append("\n");
                sb.Append("  Email: ").Append(Email).Append("\n");
                sb.Append("}\n");
                return sb.ToString();
            }

           
            /*public string ToJson()
            {
                return JsonConvert.SerializeObject(this, Formatting.Indented);
            }*/

            
            public override bool Equals(object obj)
            {
                return this.Equals(obj as UserLoginModel);
            }

            
            public bool Equals(UserLoginModel other)
            {
                if (other == null)
                    return false;

                return
                    (
                        this.Password == other.Password ||
                        this.Password != null &&
                        this.Password.Equals(other.Password)
                    ) &&
                    (
                        this.Email == other.Email ||
                        this.Email != null &&
                        this.Email.Equals(other.Email)
                    );
            }

            public override int GetHashCode()
            {
                unchecked // Overflow is fine, just wrap
                {
                    int hash = 41;
                    // Suitable nullity checks etc, of course :)
                    if (this.Password != null)
                        hash = hash * 59 + this.Password.GetHashCode();
                    if (this.Email != null)
                        hash = hash * 59 + this.Email.GetHashCode();
                    return hash;
                }
            }
        }


        public partial class UserChangeCredentialsModel : IEquatable<UserChangeCredentialsModel>
        {
            public UserChangeCredentialsModel(string NewEmail = null, string OldPassword = null, string NewPassword = null)
            {
                this.NewEmail = NewEmail;
                this.OldPassword = OldPassword;
                this.NewPassword = NewPassword;
            }

            public string NewEmail { get; set; }
            public string OldPassword { get; set; }
           
            public string NewPassword { get; set; }
            
            public override string ToString()
            {
                var sb = new StringBuilder();
                sb.Append("class UserChangeCredentialsModel {\n");
                sb.Append("  NewEmail: ").Append(NewEmail).Append("\n");
                sb.Append("  OldPassword: ").Append(OldPassword).Append("\n");
                sb.Append("  NewPassword: ").Append(NewPassword).Append("\n");
                sb.Append("}\n");
                return sb.ToString();
            }

            /*public string ToJson()
            {
                return JsonConvert.SerializeObject(this, Formatting.Indented);
            }*/

            public override bool Equals(object obj)
            {
                return this.Equals(obj as UserChangeCredentialsModel);
            }

            public bool Equals(UserChangeCredentialsModel other)
            {
                if (other == null)
                    return false;

                return
                    (
                        this.NewEmail == other.NewEmail ||
                        this.NewEmail != null &&
                        this.NewEmail.Equals(other.NewEmail)
                    ) &&
                    (
                        this.OldPassword == other.OldPassword ||
                        this.OldPassword != null &&
                        this.OldPassword.Equals(other.OldPassword)
                    ) &&
                    (
                        this.NewPassword == other.NewPassword ||
                        this.NewPassword != null &&
                        this.NewPassword.Equals(other.NewPassword)
                    );
            }
            
            public override int GetHashCode()
            {
                unchecked // Overflow is fine, just wrap
                {
                    int hash = 41;
                    // Suitable nullity checks etc, of course :)
                    if (this.NewEmail != null)
                        hash = hash * 59 + this.NewEmail.GetHashCode();
                    if (this.OldPassword != null)
                        hash = hash * 59 + this.OldPassword.GetHashCode();
                    if (this.NewPassword != null)
                        hash = hash * 59 + this.NewPassword.GetHashCode();
                    return hash;
                }
            }
        }


        public partial class GetRatingPositionModel : IEquatable<GetRatingPositionModel>
        {
           
            public GetRatingPositionModel(int? HighestPosition = null, int? LowestPosition = null)
            {
                this.HighestPosition = HighestPosition;
                this.LowestPosition = LowestPosition;
            }

            public int? HighestPosition { get; set; }
            public int? LowestPosition { get; set; }
            
            public override string ToString()
            {
                var sb = new StringBuilder();
                sb.Append("class GetRatingPositionModel {\n");
                sb.Append("  HighestPosition: ").Append(HighestPosition).Append("\n");
                sb.Append("  LowestPosition: ").Append(LowestPosition).Append("\n");
                sb.Append("}\n");
                return sb.ToString();
            }

            /*public string ToJson()
            {
                return JsonConvert.SerializeObject(this, Formatting.Indented);
            }*/

            public override bool Equals(object obj)
            {
                return this.Equals(obj as GetRatingPositionModel);
            }

            public bool Equals(GetRatingPositionModel other)
            {
                if (other == null)
                    return false;

                return
                    (
                        this.HighestPosition == other.HighestPosition ||
                        this.HighestPosition != null &&
                        this.HighestPosition.Equals(other.HighestPosition)
                    ) &&
                    (
                        this.LowestPosition == other.LowestPosition ||
                        this.LowestPosition != null &&
                        this.LowestPosition.Equals(other.LowestPosition)
                    );
            }

            public override int GetHashCode()
            {
                unchecked // Overflow is fine, just wrap
                {
                    int hash = 41;
                    // Suitable nullity checks etc, of course :)
                    if (this.HighestPosition != null)
                        hash = hash * 59 + this.HighestPosition.GetHashCode();
                    if (this.LowestPosition != null)
                        hash = hash * 59 + this.LowestPosition.GetHashCode();
                    return hash;
                }
            }
        }


        public partial class AddAttemptModel : IEquatable<AddAttemptModel>
        {

            public AddAttemptModel(string LevelName = null, int? Time = null, int? Stars = null)
            {
                this.LevelName = LevelName;
                this.Time = Time;
                this.Stars = Stars;
            }

            public string LevelName { get; set; }
            public int? Time { get; set; }
            public int? Stars { get; set; }
            
            public override string ToString()
            {
                var sb = new StringBuilder();
                sb.Append("class AddAttemptModel {\n");
                sb.Append("  LevelName: ").Append(LevelName).Append("\n");
                sb.Append("  Time: ").Append(Time).Append("\n");
                sb.Append("  Stars: ").Append(Stars).Append("\n");
                sb.Append("}\n");
                return sb.ToString();
            }

            /*public string ToJson()
            {
                return JsonConvert.SerializeObject(this, Formatting.Indented);
            }*/
            
            public override bool Equals(object obj)
            {
                return this.Equals(obj as AddAttemptModel);
            }
            
            public bool Equals(AddAttemptModel other)
            {
                if (other == null)
                    return false;

                return
                    (
                        this.LevelName == other.LevelName ||
                        this.LevelName != null &&
                        this.LevelName.Equals(other.LevelName)
                    ) &&
                    (
                        this.Time == other.Time ||
                        this.Time != null &&
                        this.Time.Equals(other.Time)
                    ) &&
                    (
                        this.Stars == other.Stars ||
                        this.Stars != null &&
                        this.Stars.Equals(other.Stars)
                    );
            }

            public override int GetHashCode()
            {
                unchecked // Overflow is fine, just wrap
                {
                    int hash = 41;
                    // Suitable nullity checks etc, of course :)
                    if (this.LevelName != null)
                        hash = hash * 59 + this.LevelName.GetHashCode();
                    if (this.Time != null)
                        hash = hash * 59 + this.Time.GetHashCode();
                    if (this.Stars != null)
                        hash = hash * 59 + this.Stars.GetHashCode();
                    return hash;
                }
            }
        }
    }
}

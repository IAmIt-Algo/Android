using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mindblower.Core
{
    public class UserRegistrationModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }


    public class UserLoginModel
    {
        public string Password { get; set; }
        public string Email { get; set; }
    }


    public class UserChangeCredentialsModel
    {
        public string NewEmail { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }


    public class GetRatingPositionModel
    {
        public int HighestPosition { get; set; }
        public int LowestPosition { get; set; }
        public int StarsCount { get; set; }
        public RatingModel[] Rating { get; set; }
    }


    public class AddAttemptModel
    {
        public string LevelName { get; set; }
        public int Time { get; set; }
        public int Stars { get; set; }
    }


    public class RatingModel
    {
        public string UserName { get; set; }
        public int? StarsCount { get; set; }
    }
    public class GetUserInformationModel
    {
        public List<Level> Levels { get; set; }
    }
    public class Level
    {
        public string Name { get; set; }
        public int StarsCount { get; set; }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Tests_application.Connect
{
    internal class DataHelper
    {
        public static Users CurrentUser = null;
        public static Groups CurrentUserGroup = null;
        public static List<Results> CurrentUserResults = null;
        public static List<Tests> CurrentUserTests = null;
        public DataHelper(string password, string login)
        {
            CurrentUser = Helper.connect.Users.FirstOrDefault(x => x.Login == login && x.Password == password);
            CurrentUserGroup = Helper.connect.Groups.FirstOrDefault(x => x.ID == CurrentUser.ID_Group);
            CurrentUserResults = Helper.connect.Results.Where(x => x.ID_Test == CurrentUser.ID).ToList();
            List<Groups_Tests> GroupsTests = Helper.connect.Groups_Tests.Where(x => x.ID_Group == CurrentUserGroup.ID).ToList();
            CurrentUserTests = new List<Tests>();
            foreach (Groups_Tests i in GroupsTests)
            {
                CurrentUserTests.Add(i.Tests);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests_application.Connect
{
    internal class SingletoneStudent
    {
        private static SingletoneStudent instance;


        public Users CurrentUser { get; private set; }
        public Groups CurrentUserGroup { get; private set; }
        public List<Tests> CurrentUserTests { get; private set; }

        private SingletoneStudent(string password, string login)
        {
            CurrentUserTests = new List<Tests>();

            CurrentUser = Helper.connect.Users.FirstOrDefault(x => x.Login == login && x.Password == password);
            foreach (var item in CurrentUser.Users_Groups)
            {
                CurrentUserGroup = item.Groups;
            }
            if (CurrentUserGroup != null)
            {
                List<Groups_Tests> GroupsTests = Helper.connect.Groups_Tests.Where(x => x.ID_Group == CurrentUserGroup.ID).ToList();
                foreach (Groups_Tests i in GroupsTests)
                {
                    CurrentUserTests.Add(i.Tests);
                }
            }

        }

        public static SingletoneStudent getInstance(string password, string login)
        {
            if (instance == null)
                instance = new SingletoneStudent(password, login);
            return instance;
        }
    }
}

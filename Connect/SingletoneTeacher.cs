using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests_application.Connect
{
    internal class SingletoneTeacher
    {
        private static SingletoneTeacher instance;


        public Users CurrentUser { get; private set; }
        public List<Groups> CurrentUserGroups { get; private set; } = new List<Groups>();
        public List<Users> Students { get; set; } = new List<Users>();
        public Groups SelectedGroup { get; set; }
        private SingletoneTeacher(string password, string login)
        {
            CurrentUser = Helper.connect.Users.FirstOrDefault(x => x.Login == login && x.Password == password);
            foreach (Users_Groups item in CurrentUser.Users_Groups)
            {
                CurrentUserGroups.Add(item.Groups);
            }
            if (CurrentUserGroups != null)
            {
                
            }

        }

        public static SingletoneTeacher getInstance(string password, string login)
        {
            if (instance == null)
                instance = new SingletoneTeacher(password, login);
            return instance;
        }
    }
}

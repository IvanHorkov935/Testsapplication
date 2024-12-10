using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tests_application.Connect;
using Tests_application.Models;

namespace Tests_application
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        private User selectedUser;
        public Users Teacher { get; set; }
        public int CurrentGroup { get; set; }
        public ObservableCollection<User> UsersCollection { get; set; }
        public ObservableCollection<Results> UserResultsCollection { get; set; }
        public ObservableCollection<Tests> UserTestsCollection { get; set; }
        public User SelectedUser
        {
            get { return selectedUser; }
            set
            {
                selectedUser = value;
                if (UserResultsCollection != null) { UserResultsCollection.Clear(); }

                foreach(Tests test in UserTestsCollection)
                {
                    if (Helper.connect.Results.Where(x => x.ID_User == selectedUser.ID && x.ID_Test == test.ID).FirstOrDefault() == null)
                    {
                        UserResultsCollection.Add(new Results() { ID_Test = test.ID, ID_User = selectedUser.ID, Per_Complete = 0 });
                    }
                    else
                    {
                        UserResultsCollection.Add(test.Results.First(x => x.Per_Complete == test.Results.Max(z => z.Per_Complete)));
                    }
                }

                OnPropertyChanged("SelectedUser");
            }
        }

        public ApplicationViewModel()
        {
            Teacher = DataHelper.CurrentUser;
            CurrentGroup = DataHelper.CurrentUserGroup.ID;

            UserTestsCollection = new ObservableCollection<Tests>();
            UserResultsCollection = new ObservableCollection<Results>();
            UsersCollection = new ObservableCollection<User>();
            List<Users> col = (List<Users>)Helper.connect.Users.Where(x => x.ID_Type == 3 && x.ID_Group == DataHelper.CurrentUserGroup.ID).ToList().AsEnumerable();
            foreach (var user in col)
            {
                UsersCollection.Add(new User
                {
                    ID = user.ID,
                    Login = user.Login,
                    Password = user.Password,
                    ID_Group = (int)user.ID_Group,
                    ID_Type = user.ID_Type,
                    Full_Name = user.Full_Name,
                });
            }

            foreach (var item in Helper.connect.Groups_Tests.Where(x => x.ID_Group == CurrentGroup))
            {
                UserTestsCollection.Add(item.Tests);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}

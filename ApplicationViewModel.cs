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
        public Groups CurrentGroup { get; set; }
        public string TeacherName { get; set; }
        public ObservableCollection<User> UsersCollection { get; set; }
        public ObservableCollection<Results> UserResultsCollection { get; set; }
        public List<Tests> UserTestsCollection { get; set; }
        public User SelectedUser
        {
            get { return selectedUser; }
            set
            {
                selectedUser = value;
                SelectedUser_Changed();
            }
        }

        public ApplicationViewModel()
        {
            SingletoneTeacher Teacher = SingletoneTeacher.getInstance("", "");
            CurrentGroup = Teacher.SelectedGroup;
            TeacherName = Teacher.CurrentUser.Full_Name;

            UserTestsCollection = new List<Tests>();
            UserResultsCollection = new ObservableCollection<Results>();
            if(Teacher.Students.Count() == 0)
            {
                foreach (var usergroups in Helper.connect.Users_Groups.Where(x => x.ID_Group == CurrentGroup.ID))
                {
                    if (Helper.connect.Users.First(x => x.ID == usergroups.ID).ID_Type == 3) 
                    { Teacher.Students.Add(Helper.connect.Users.First(x => x.ID == usergroups.ID)); }
                }
            }
            
            UsersCollection = new ObservableCollection<User>();
            foreach(Users user in Teacher.Students) { UsersCollection.Add(new User
            {
                ID = user.ID,
                Login = user.Login,
                Password = user.Password,
                ID_Type = user.ID_Type,
                ID_Group = Teacher.SelectedGroup.ID,
                Full_Name = user.Full_Name,
            }); }

            foreach (var item in Helper.connect.Groups_Tests.Where(x => x.ID_Group == CurrentGroup.ID))
            {
                UserTestsCollection.Add(item.Tests);
            }
        }

        public void SelectedUser_Changed()
        {
            if (UserResultsCollection != null) { UserResultsCollection.Clear(); }

            foreach (Tests test in UserTestsCollection)
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

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}

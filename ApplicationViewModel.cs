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
        public ObservableCollection<User> UsersCollection { get; set; }
        public ObservableCollection<Result> UserResultsCollection { get; set; }
        public User SelectedUser
        {
            get { return selectedUser; }
            set
            {
                selectedUser = value;
                if(UserResultsCollection != null) { UserResultsCollection.Clear(); }
                List<Results> col2 = (List<Results>)Helper.connect.Results.Where(x => x.ID_User == value.ID).ToList().AsEnumerable();
                List<int> con = new List<int>();
                foreach (var result in col2)
                {
                    if (con.Contains((int)result.ID_Test))
                    {
                        Result a = UserResultsCollection.Where(x => x.ID_User == result.ID_User && x.ID_Test == result.ID_Test).First();
                        if(result.Per_Complete > a.Per_Complete)
                        {
                            UserResultsCollection[UserResultsCollection.IndexOf(a)] = new Result
                            {
                                ID = result.ID,
                                ID_User = result.ID_User,
                                ID_Test = (int)result.ID_Test,
                                Per_Complete = (double)result.Per_Complete,
                            };
                        }
                        continue;
                    }
                    con.Add((int)result.ID_Test);
                    UserResultsCollection.Add(new Result
                    {
                        ID = result.ID,
                        ID_User = result.ID_User,
                        ID_Test = (int)result.ID_Test,
                        Per_Complete = (double)result.Per_Complete,
                    });
                }
                OnPropertyChanged("SelectedUser");
            }
        }
        //private Result selectedResult;
        //private ObservableCollection<Result> resultsCollection;
        //public ObservableCollection<Result> ResultsCollection
        //{
        //    get { return resultsCollection; }
        //    set
        //    {
        //        resultsCollection = value;
        //        OnPropertyChanged("ResultsCollection");
        //    }
        //}
        public ApplicationViewModel()
        {
            UsersCollection = new ObservableCollection<User>();
            List<Users> col = (List<Users>)Helper.connect.Users.Where(x => x.ID_Type == 3).ToList().AsEnumerable();
            foreach (var user in col)
            {
                UsersCollection.Add(new User
                {
                    ID = user.ID,
                    Login = user.Login,
                    Password = user.Password,
                    ID_Group = user.ID_Group,
                    ID_Type = user.ID_Type,
                    Full_Name = user.Full_Name,
                });
            }

            UserResultsCollection = new ObservableCollection<Result>();
            //List<Results> col2 = (List<Results>)Helper.connect.Results.ToList().AsEnumerable();
            //foreach (var result in col2)
            //{
            //    ResultsCollection.Add(new Result
            //    {
            //        ID = result.ID,
            //        ID_User = result.ID_User,
            //        ID_Test = (int)result.ID_Test,
            //        Per_Complete = (double)result.Per_Complete,
            //    });
            //}
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}

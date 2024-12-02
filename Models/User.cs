using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Tests_application.Models
{
    public class User : INotifyPropertyChanged
    {
        private int id;
        private string login;
        private string password;
        private int id_type;
        private int id_group;
        private string full_name;

        public int ID
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("ID");
            }
        }
        public string Login
        {
            get { return login; }
            set
            {
                login = value;
                OnPropertyChanged("Login");
            }
        }
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }
        public int ID_Type
        {
            get { return id_type; }
            set
            {
                id_type = value;
                OnPropertyChanged("ID_Type");
            }
        }
        public int ID_Group
        {
            get { return id_group; }
            set
            {
                id_group = value;
                OnPropertyChanged("ID_Group");
            }
        }
        public string Full_Name
        {
            get { return full_name; }
            set
            {
                full_name = value;
                OnPropertyChanged("Full_Name");
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

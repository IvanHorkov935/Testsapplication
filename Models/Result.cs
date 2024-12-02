using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Tests_application.Models
{
    public class Result : INotifyPropertyChanged
    {
        private int id;
        private int id_test;
        private int id_user;
        private double per_complete;


        public int ID
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("ID");
            }
        }
        public int ID_Test
        {
            get { return id_test; }
            set
            {
                id_test = value;
                OnPropertyChanged("ID_Test");
            }
        }
        public int ID_User
        {
            get { return id_user; }
            set
            {
                id_user = value;
                OnPropertyChanged("ID_User");
            }
        }
        public double Per_Complete
        {
            get { return per_complete; }
            set
            {
                per_complete = value;
                OnPropertyChanged("Per_Complete");
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

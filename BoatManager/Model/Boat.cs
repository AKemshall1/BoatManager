using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace BoatManager.Model
{
    class Boat : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged("Id"); }
        }

        private int level;
        public int Level
        {
            get { return level; }
            set { level = value; OnPropertyChanged("Level"); }
        }

        private string hull;
        public string Hull
        {
            get { return hull; }
            set { hull = value; OnPropertyChanged("Hull"); }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged("Name"); }
        }

        private string faction;
        public string Faction
        {
            get { return faction; }
            set { faction = value; OnPropertyChanged("Faction"); }
        }

        //private DateTime date;
        //public DateTime Date
        //{
        //    get { return date; }
        //    set { date = value; OnPropertyChanged("Date"); }
        //}


    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using BoatManager.Model;
using BoatManager.Commands;

namespace BoatManager.ViewModel
{
    class BoatViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        BoatService boatService;
        public BoatViewModel()
        {
            boatService = new BoatService();
            addCommand = new RelayCommand(Add);
            deleteCommand = new RelayCommand(Delete);
            updateCommand = new RelayCommand(Update);
            inputBoat = new Boat();
            LoadBoatList();
        }

        private ObservableCollection<Boat> boatObservableList;
        public ObservableCollection<Boat> BoatObservableList
        {
            get { return boatObservableList; }
            set { boatObservableList = value; OnPropertyChanged("BoatObservableList"); }
        }

        private void LoadBoatList()
        {
            BoatObservableList = new ObservableCollection<Boat>(boatService.GetAll());
        }

        private string message;
        public string Message
        {
            get { return message; }
            set { message = value; OnPropertyChanged("Message"); }
        }

        private string deleteMessage;
        public string DeleteMessage
        {
            get { return deleteMessage; }
            set { deleteMessage = value; OnPropertyChanged("DeleteMessage"); }
        }

        private Boat inputBoat;
        public Boat InputBoat
        {
            get { return inputBoat; }
            set { inputBoat = value; OnPropertyChanged("InputBoat"); }
        }

        private int deleteID;
        public int DeleteID
        {
            get { return deleteID; }
            set { deleteID = value; OnPropertyChanged("DeleteName"); }
        }

        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get { return addCommand; }
        }

        public void Add()
        {
            try
            {
                bool IsAdded = boatService.Add(InputBoat);
                LoadBoatList();
                if(IsAdded == true)
                {
                    Message = "Added";
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private RelayCommand deleteCommand;
        public RelayCommand DeleteCommand
        {
            get { return deleteCommand; }
        }

        public void Delete()
        {
            try
            {
                bool IsDeleted = boatService.Delete(DeleteID);
                LoadBoatList();
                DeleteMessage = IsDeleted.ToString();
          
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private RelayCommand updateCommand;

        public RelayCommand UpdateCommand
        {
            get { return updateCommand; }
        }

        public void Update()
        {
            try
            {
                bool isUpdated = boatService.Update(InputBoat);
                LoadBoatList();

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


    }


}

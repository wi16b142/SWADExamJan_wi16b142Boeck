using GalaSoft.MvvmLight;
using SWADExamJan_wi16b142Boeck.Communication;
using System;
using System.Collections.ObjectModel;

namespace SWADExamJan_wi16b142Boeck.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        Server server;
        private ObservableCollection<ShipVM> ships;
        Action<string> GuiUpdate;


        public ObservableCollection<ShipVM> Ships
        {
            get { return ships; }
            set { ships = value; RaisePropertyChanged(); }
        }


        public MainViewModel()
        {
            GuiUpdate = UpdateGui;
            Ships = new ObservableCollection<ShipVM>();

            if (IsInDesignMode)
            {
                //design mode
            }
            else
            {
                //real life
                server = new Server(GuiUpdate);
            }
        }

        private void UpdateGui(string msg)
        {
            App.Current.Dispatcher.Invoke(()=> 
            {
                string[] raw = msg.Split(':');

                //split string according to pattern given by the clients string - her ewe use ':' as delimiter
                string id = raw[0];
                string typ = raw[1];
                string heading = raw[2];
                double longitude = double.Parse(raw[3]); //watch out: non double inputs will make the app crash
                double altitude = double.Parse(raw[4]);
                string message = "";
                if (raw.Length == 6) //is a message added to the standard string??
                {
                    message = raw[5]; //if yes, then we are going to save this message to the shipVM
                }

                bool shipIsNew = true; //indicator if a ship is new or not

                foreach (var ship in ships) //check each ship
                {
                    if (ship.ID.Equals(id)) //if the current id does already exist
                    {
                        //if yes, store data to this shipVM
                        ship.Heading = heading;
                        ship.Longitude = longitude;
                        ship.Altitude = altitude;
                        if (!message.Equals("")) //if there was an additional message sent
                        {
                            ship.Message = message; //save this message also to the shipVM - note: in the class ShipVM, a display-timer starts when you save a message.
                        }
                        shipIsNew = false; //since this id already existed, we set our isNew indicator to false
                    }
                }

                if (shipIsNew) //but if the ship was new (= we found no such id in our ships)
                {
                    Ships.Add(new ShipVM(id, typ, heading, longitude, altitude)); //we create a new ship with the sent data
                }

            });
        }
    }
}
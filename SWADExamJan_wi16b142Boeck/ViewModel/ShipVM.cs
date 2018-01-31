using GalaSoft.MvvmLight;
using System;
using System.Windows.Threading;

namespace SWADExamJan_wi16b142Boeck.ViewModel
{
    public class ShipVM : ViewModelBase
    {
        private string iD;
        private string typ;
        private string heading;
        private double longitude;
        private double altitude;
        private string message = "";
        private DispatcherTimer timer;

        public ShipVM(string iD, string typ, string heading,  double longitude, double altitude)
        {
            Altitude = altitude;
            Longitude = longitude;
            Heading = heading;
            Typ = typ;
            ID = iD;
        }

        public void StartTimer()
        {
            //setup display timer for message
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 10); //timer for 10 seconds (...Timespan(h,m,s)...)
            timer.Tick += Delete; //define what happens when the timer has elapsed - here we call the Delete method which removes the message
            timer.Start(); //start the timer
        }

        private void Delete(object sender, EventArgs e)
        {
            Message = ""; //delete the message
            timer.Stop(); //stop timer
        }

        #region props
        public string Message
        {
            get { return message; }
            set { message = value; RaisePropertyChanged(); StartTimer(); } //when message changes, start display timer
        }


        public double Altitude
        {
            get { return altitude; }
            set { altitude = value; RaisePropertyChanged(); }
        }


        public double Longitude
        {
            get { return longitude; }
            set { longitude = value; RaisePropertyChanged(); }
        }


        public string Heading
        {
            get { return heading; }
            set { heading = value; RaisePropertyChanged(); }
        }


        public string Typ
        {
            get { return typ; }
            set { typ = value; RaisePropertyChanged(); }
        }


        public string ID
        {
            get { return iD; }
            set { iD = value; RaisePropertyChanged(); }
        }
        #endregion

    }
}

using ClientApp.Communication;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;
using System.Threading;

namespace ClientApp.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        Client client;
        bool isConnected = false;
        private string iD = "";
        private string typ = "";
        private string interval = "";
        private string heading = "";
        private string longitude = "0";
        private string altitude = "0";
        private string message = "";
        Thread sendingThread;

        private ObservableCollection<string> types;
        private ObservableCollection<string> headings;

        public RelayCommand ConnectBtnClick { get; set; }

        public RelayCommand SendBtnClick { get; set; }

        #region props

        public ObservableCollection<string> Headings
        {
            get { return headings; }
            set { headings = value; }

        }


        public ObservableCollection<string> Types
        {
            get { return types; }
            set { types = value; }
        }

        public string Message
        {
            get { return message; }
            set { message = value; RaisePropertyChanged(); }
            
        }

        public string Altitude
        {
            get { return altitude; }
            set { altitude = value; RaisePropertyChanged(); }
        }


        public string  Longitude
        {
            get { return longitude; }
            set { longitude = value; RaisePropertyChanged(); }
        }


        public string  Heading
        {
            get { return heading; }
            set { heading = value; RaisePropertyChanged(); }
        }


        public string Interval
        {
            get { return interval; }
            set { interval = value; RaisePropertyChanged(); }
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

        public bool IsConnected
        {
            get => isConnected; set
            {
                isConnected = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        public MainViewModel()
        {
            Types = new ObservableCollection<string>();
            Types.Add("Container");
            Types.Add("Ferry");
            Types.Add("Cruiser");

            Headings = new ObservableCollection<string>();
            Headings.Add("N");
            Headings.Add("E");
            Headings.Add("S");
            Headings.Add("W");
            Headings.Add("NE");
            Headings.Add("SE");
            Headings.Add("SW");
            Headings.Add("NW");

            Heading = Headings[0];

            if (IsInDesignMode)
            {
                //design mode
            }
            else
            {
                //real life
                ConnectBtnClick = new RelayCommand(()=> 
                {
                    client = new Client();
                    StartSending();
                    IsConnected = true;
                    
                }, ()=> 
                {
                    if(!IsConnected && !ID.Equals("") && !Typ.Equals("") && !Interval.Equals(""))
                    {
                        return true;
                    }
                    return false;
                });

                SendBtnClick = new RelayCommand(() =>
                {
                    string msg = ID + ":" + Typ + ":" + Heading + ":" + Longitude + ":" + Altitude + ":" + Message; //standard string with additional message sent only when "send" button was clicked
                    client.Send(msg);
                    Message = "";
                }, () =>
                {
                    if(IsConnected && Message.Length > 0)
                    {
                        return true;
                    }else return false;
                });
            }
        }

        private void StartSending()
        {
            sendingThread = new Thread(Send);
            sendingThread.IsBackground = true;
            sendingThread.Start();
        }

        private void Send()
        {
            while(sendingThread.IsAlive)
            {
                string msg = ID + ":" + Typ + ":" + Heading + ":" + Longitude + ":" + Altitude; //standard string which is sent every x seconds
                client.Send(msg);
                Thread.Sleep(int.Parse(Interval) * 1000); // wait for defined interval before sending the data again
            }
     
        }

    }
}
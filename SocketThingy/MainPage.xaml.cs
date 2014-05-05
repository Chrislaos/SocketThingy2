using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Networking.Sockets;
using Windows.Networking;
using Newtonsoft.Json.Linq;
using Demo.Protocol;
using Windows.Storage.Streams;
using System.Collections.ObjectModel;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SocketThingy
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        StreamSocket socket = new StreamSocket();
        static HostName localHost = new HostName("158.37.233.124");
        static HostName remoteHost = new HostName("158.37.233.122");
        static string socketString = "1337";
        DataReader dr2;
        EndpointPair connection = new EndpointPair(localHost, socketString, remoteHost, socketString);
        // ip 158.37.225.197
        // ip 158.37.225.206
        PDU pdu = new PDU()
        {
            MessageID = (int)CommandMessageID.SendAllProcedures,
            MessageDescription = "Server Please, load the sequencefile I specified as part of this message.",
            MessageType = "Command",
            Source = "Demo.Client",
            Data = new JObject()
        };
        public static bool DEBUG = true;

        // StreamSocketListener tcpListener = new StreamSocketListener();
        // private List<StreamSocket> _connections = new List<StreamSocket>();
        private bool connecting = false;
        private String recievedText;
        public String RecievedText
        {
            get
            {
                return recievedText;
            }
        }

        public MainPage()
        {
            this.InitializeComponent();

            

        }
        static public ObservableCollection<kakeclass> _kakecollection = new ObservableCollection<kakeclass>();
        // message fikk -> PDU object, 
        public ObservableCollection<kakeclass> kakecollection
        {
            get { return _kakecollection; }
        }
        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

       

        async private void recieveData()
        {
            StreamSocketListener listener = new StreamSocketListener();
            //dr2.InputStreamOptions = InputStreamOptions.Partial;
            //string msg = null;
            //var count = await dr2.LoadAsync(512);

            //if (count > 0)
            //    msg = dr2.ReadString(count);

            //PDU pdu2 = new PDU(msg);
            //Procedure temp = pdu2.Data.ToObject(typeof(Procedure));
            //_kakecollection.Add(new kakeclass { Name = "123", Description = temp.Description, Date = temp.Date });


            try
            {
                listener.ConnectionReceived += listener_ConnectionReceived;
                await listener.BindServiceNameAsync(socketString);

            }
            catch (Exception e) { recievedMessage.Text = e.ToString(); }

        }

        void listener_ConnectionReceived(StreamSocketListener sender, StreamSocketListenerConnectionReceivedEventArgs args)
        {
            throw new NotImplementedException();
            dr2 = new DataReader(socket.InputStream);
            PDU pdu2 = new PDU(dr2.ToString());
            Procedure temp = pdu2.Data.ToObject(typeof(Procedure));
            _kakecollection.Add(new kakeclass { Name = "123", Description = temp.Description, Date = temp.Date });
        }
        private async void sendData()
        {
            var dr = new DataWriter(socket.OutputStream);
            
            //var len = dr.MeasureString(pdu.ToJson());
            String message = pdu.ToJson();
            //dr.WriteInt32((int)len);
            dr.WriteString(pdu.ToJson());
            var ret = await dr.StoreAsync();
            
            dr.DetachStream();
            recieveData();
            
        }


        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            if (DEBUG)
            {
                socket.Dispose();
                
                Application.Current.Exit();
            }
            else
            {
                // do something legal
            }
        }



        private void RunTest_Click(object sender, RoutedEventArgs e)
        {
            SequencePage Seq = new SequencePage(gridMainPage);
            gridMainPage.Children.Add(Seq);
        }



        private void ListViewItem_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            SequencePage Seq = new SequencePage(gridMainPage);
            gridMainPage.Children.Add(Seq);
        }

        private void collectkake_Click(object sender, RoutedEventArgs e)
        {
            _kakecollection.Add(new kakeclass { Name = "entotre", Description = "send", Date = DateTime.Now });
            //_kakecollection.Add(new kakeclass("navn", DateTime.Now, "to"));
        }

        private void sendText_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private async void Coonnect3_Click(object sender, RoutedEventArgs e)
        {
            try { await socket.ConnectAsync(connection); }
            catch {
                
                recievedMessage.Text = "Could not connect"; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            sendData();
        }

        private void SendData_Click(object sender, RoutedEventArgs e)
        {
            sendData();
        }
        }
    }


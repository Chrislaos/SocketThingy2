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
        public static bool DEBUG = true;
        private static string SERVER_IP = "10.0.0.8";
        public static HostName SERVER_HOST = new HostName(SERVER_IP);
        public static string SERVER_PORT = "1337";

        PDU pdu2 = new PDU()
        {
            MessageID = (int)CommandMessageID.LoadSequenceFile,
            MessageDescription = "Server Please, load the sequencefile I specified as part of this message.",
            MessageType = "Command",
            Source = "Demo.Client",
            Data = new JObject()
        };
        StreamSocket tcpSocket = new StreamSocket();

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

            PDU pdu = new PDU()
            {
                MessageID = (int)CommandMessageID.LoadSequenceFile,
                MessageDescription = "Server Please, load the sequencefile I specified as part of this message.",
                MessageType = "Command",
                Source = "Demo.Client",
                Data = new JObject()
            };
            pdu.Data.SequenceFileName = "loltest.seqfile";
            
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }


        public void ReadFromSocket() 
        {
            recievedText = tcpSocket.InputStream.ToString();
            recievedTextBox.Text = recievedText;
        }

        async public void Connect() 
        {
            try
            {
                connecting = true;
                await tcpSocket.ConnectAsync(SERVER_HOST, SERVER_PORT);
                connecting = false;
                recievedTextBox.Text = tcpSocket.InputStream.ToString();
            }
            catch 
            {
                connecting = false;
            }
        }
        
        private void Read_Click(object sender, RoutedEventArgs e)
        {
            ReadFromSocket();
        }

        async private void Connect1_Click(object sender, RoutedEventArgs e)
        {
            Connect();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            if (DEBUG)
            {
                
                Application.Current.Exit();
            }
            else 
            {
                // do something legal
            }
        }
        
        async private void loltest_Click(object sender, RoutedEventArgs e)
        {
            PDU pdu2 = new PDU()
            {
                MessageID = (int)CommandMessageID.StartExecution,
                MessageDescription = "Server Please, load the sequencefile I specified as part of this message.",
                MessageType = "Command",
                Source = "Demo.Client",
                Data = new JObject()
            };
            pdu2.Data.SequenceFileName = "loltest.seq";
            StreamWriter datawr = new StreamWriter(tcpSocket.OutputStream.AsStreamForWrite(), System.Text.Encoding.UTF8);
            char [] trimmer = new char[1];
            trimmer[0] = '.';
            datawr.Flush();
            datawr.Write(pdu2.ToJson());
            recievedTextBox.Text = pdu2.ToJson();
            datawr.Flush();
        }
        
        private void MoveToList_click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ListView1.Items.Add(MoveToListBox.Text);
        }

       
        //public class SongDetails
        //{
        //    public string ArtistName { get; set; }
        //    public string SongName { get; set; }
        //    public string Artist { get; set; }
        //}

        //private void TestList_Click(object sender, RoutedEventArgs e)
        //{
        //    var songDetails = new[] {
        //        new SongDetails {Artist = "a1", ArtistName = "a2", SongName = "a3"},
        //        new SongDetails {Artist = "b1", ArtistName = "b2", SongName = "b3"}
        //    };
        //}
        
    }
}

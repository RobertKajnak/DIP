using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DIP
{
    class NetLogic
    {
        private TcpListener tcpListener;
        FormMain caller;
        FileLogic fileLogic;

        string hostIP;
        IPAddress targetIP;

        private TcpClient target;

        public NetLogic(FormMain caller, FileLogic fileLogic)
        {
            this.caller = caller;
            this.fileLogic = fileLogic;

            //TODO: check for IPv6 - http://ipv6.whatismyv6.com/
            tcpListener = new TcpListener(IPAddress.Any, 3252);
            Thread serverThread = new Thread(new ThreadStart(ServerThread));
            serverThread.IsBackground = true;
            serverThread.Start();
        }

        public bool isValidIP(string IP)
        {
            if ((IP.Count(c => c=='.')==3 || IP.Count(c => c==':')>=2) 
                && IPAddress.TryParse(IP,out targetIP))
                return true;
            return false;
        }

        public string GetHostIPAddress()
        {
            ///Credit goes to 'ezgar' @ stackoverflow.com; the answer was also featured in
            ///Question 13 Twenty C# Questions Explained of the Microsoft Academy

            Logger.Start("Fetching ip");
            String direction = "";
            WebRequest request = WebRequest.Create("http://checkip.dyndns.org/");
            Logger.Lap("Fetching ip", "request created");
            using (WebResponse response = request.GetResponse())
            {
                Logger.Lap("Fetching ip", "request.response got");
                using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                {
                    Logger.Lap("Fetching ip", "responseStream got");
                    direction = stream.ReadToEnd();
                }
            }
            Logger.Lap("Fetching ip", "page read");
            //Search for the ip in the html
            int first = direction.IndexOf("Address: ") + 9;
            int last = direction.LastIndexOf("</body>");
            hostIP = direction.Substring(first, last - first);
            Logger.Stop("Fetching ip");
            return hostIP;
            
        }

        public void AddTargetIPAddress(string ipString)
        {
            //targetIP = ipString;
            targetIP = IPAddress.Parse(ipString);
        }

        private void ServerThread()
        {
            tcpListener.Start();
            TcpClient client = tcpListener.AcceptTcpClient();

            //TODO: is this needed?: 'create a thread to handle communication with connected client'
            RecieveFile(client);
        }

        public bool SendFileHeader()
        {
            target = new TcpClient();
            IPEndPoint serverEndPoint = new IPEndPoint(targetIP, 3252);
            
            target.Connect(serverEndPoint);

            string msg = fileLogic.GetNameAndSize();

            NetworkStream targetStream = target.GetStream();

            ASCIIEncoding encoder = new ASCIIEncoding();
            byte[] buffer = encoder.GetBytes(msg);

            targetStream.Write(buffer, 0, buffer.Length);
            targetStream.Flush();

            // Receive the TcpServer.response.

            // Buffer to store the response bytes.
            Byte[] data = new Byte[1];

            // String to store the response ASCII representation.
            String responseData = String.Empty;
            Int32 bytes = targetStream.Read(data, 0, data.Length);
            target.Close();
            if (data[0] == 1)
            {
                return true;
            }

            return false;
            // Read the first batch of the TcpServer response bytes.
            /*Int32 bytes = clientStream.Read(data, 0, data.Length);
            responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);

            caller.PostMessage(responseData, "From Server: ");*/
        }

        public void SendFile()
        {
            target = new TcpClient();
            IPEndPoint serverEndPoint = new IPEndPoint(targetIP, 3252);
            //target = new TcpClient();
            target.Connect(serverEndPoint);

            string msg = " Testing ;;!";

            NetworkStream clientStream = target.GetStream();

            ASCIIEncoding encoder = new ASCIIEncoding();
            byte[] buffer = encoder.GetBytes(msg);

            clientStream.Write(buffer, 0, buffer.Length);
            clientStream.Flush();

            // Receive the TcpServer.response.

            // Buffer to store the response bytes.
            Byte[] data = new Byte[256];

            // String to store the response ASCII representation.
            String responseData = String.Empty;

            // Read the first batch of the TcpServer response bytes.
            /*Int32 bytes = clientStream.Read(data, 0, data.Length);
            responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);

            caller.PostMessage(responseData, "From Server: ");*/
            target.Close();
        }

        private void RecieveFile(object client)
        {
            TcpClient tcpClient = (TcpClient)client;
            NetworkStream clientStream = tcpClient.GetStream();

            byte[] message = new byte[16];
            int bytesRead;

            while (true)
            {
                bytesRead = 0;

                try
                {
                    //blocks until a client sends a message
                    bytesRead = clientStream.Read(message, 0, 16);
                }
                catch
                {
                    //a socket error has occured
                    break;
                }

                if (bytesRead == 0)
                {
                    //the client has disconnected from the server
                    message[0] = 63;
                    break;
                }

                //message has successfully been received
                ASCIIEncoding encoder = new ASCIIEncoding();

                // Convert the Bytes received to a string and display it on the Server Screen
                string msg = encoder.GetString(message, 0, bytesRead);
                SavePacket(msg);

                // Confirm recieved packet
                //Since its TCP i don't think it is needed
                //Confirm(msg, encoder, clientStream);
            }

            tcpClient.Close();
        }

        private void SavePacket(string msg)
        {
            caller.PostMessage(msg, "");
        }

        /// <summary>
        /// Echo the message back to the sending client
        /// </summary>
        /// <param name="msg">
        /// String: The Message to send back
        /// </param>
        /// <param name="encoder">
        /// Our ASCIIEncoder
        /// </param>
        /// <param name="clientStream">
        /// The Client to communicate to
        /// </param>
        private void Confirm(string msg, ASCIIEncoding encoder, NetworkStream clientStream)
        {
            // Now Echo the message back
            byte[] buffer = encoder.GetBytes(msg);

            clientStream.Write(buffer, 0, buffer.Length);
            clientStream.Flush();
        }
        /// <summary>
        /// Pings the target at TargetIP, and returns the statistic string
        /// </summary>
        /// <returns> null for failed connection</returns>
        public string Ping()
        {
            if (targetIP == null)
            {
                return null;
            }

            Ping pingSender = new Ping();
            PingReply reply = pingSender.Send(targetIP);

            if (reply.Status == IPStatus.Success)
            {
                string pingResult = "Address: " + reply.Address.ToString() +
                   "\nRoundTrip time: " + reply.RoundtripTime + " ms" +
                   "\nBuffer size: " + reply.Buffer.Length + " bytes";
                if (reply.Options != null)
                {
                    pingResult += "\nTime to live: " + reply.Options.Ttl +
                         "\nDon't fragment: " + reply.Options.DontFragment;
                }

                return pingResult;
            }
            else
            {
                return null;
            }
        }


    }
}

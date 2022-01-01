using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace clientForm
{
    class testUdpClient
    {
        private int clientPort = 8050;
        private int serveurPort = 8020;
        private string serveurHost = "127.0.0.1"; //adresse serveur
        private UdpClient Client = null; //le client de type UdpClient

        //constructeur
        public testUdpClient(int clientPort, int serveurPort, string serveurHost)
        {
            this.clientPort = clientPort;
            this.serveurPort = serveurPort;
            this.serveurHost = serveurHost;
            this.Client = new UdpClient(clientPort);

        }

        public void Close() { this.Client.Close(); }
        public bool Execute(string command, ref string resultat)
        {
            bool ok = true;
            //commande a envoyer
            string req = "127.0.0.1:" + this.clientPort + ":" + command;
            //conversion de la commande en byte
            byte[] rq = System.Text.Encoding.ASCII.GetBytes(req.ToCharArray());
            //envoi de la commande
            Client.Send(rq, rq.Length, this.serveurHost, this.serveurPort);
            //variable de reception du resultat
            IPEndPoint ip = null;
            //reception
            byte[] r = Client.Receive(ref ip);
            //conversion
            resultat = System.Text.Encoding.ASCII.GetString(r);
            ok = !resultat.Equals(0);
            return ok;
        }
    }
}

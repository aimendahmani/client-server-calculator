using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Serveur
{
    class serveur
    {
        static void Main(string[] args)
        {
            
            int p = 8020;
            IPEndPoint ip = null;

            Console.WriteLine("Démarage du serveur ...");
            Console.WriteLine(); Console.WriteLine();

            UdpClient serveur = new UdpClient(p);
            bool loop = true;

            while(loop)
            {
                //Attente de cnx
                Console.WriteLine("attente de connexion ...");
                byte[] tmp = serveur.Receive(ref ip);
                //conversion des bytes en string (chaine de caractere)
                string data = new System.Text.ASCIIEncoding().GetString(tmp);
                //Dechifrage de la commande -->ip:port:command
                string[] cmd = data.Split(new char[] { ':' });
                //adresse ip
                string host = cmd[0];
                //port de cnx
                int port = Int32.Parse(cmd[1]);
                //recuperation de nb1 du signe et du nb2
                Double nb1 = Double.Parse(cmd[2]);
                string signe = cmd[3];
                Double nb2 = Double.Parse(cmd[4]);

                Double resultat = 0;
                Console.WriteLine("Operation --> " + nb1 + " " + signe + " " + nb2);
                Console.WriteLine();
                //execution de la commande
                switch(signe)
                {
                    case "+": resultat = nb1 + nb2; break;
                    case "-": resultat = nb1 - nb2; break;
                    case "*": resultat = nb1 * nb2; break;
                    case "/": resultat = nb1 / nb2; break;
                    case "<>": resultat = 0; loop = false; break;
                    default: resultat = 0; loop = false; break;

                }//fin switch
                string reponse = nb1 + " " + signe + " " + nb2 + " = " + resultat;

                //conversion du resultat en bytes
                byte[] rep = System.Text.Encoding.ASCII.GetBytes(reponse.ToCharArray());
                serveur.Send(rep, rep.Length, host, port);
            }//fin while
            //arreter serveur
            Console.WriteLine("Arret du serveur...");
            Console.WriteLine("veuillez taper entree pour terminer");
            Console.ReadLine();

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Net.NetworkInformation; 

namespace Servidor
{
    class Comm
    {
        int porta;
        IPAddress IP;
        Random randomizador = new Random();

        int AbreSoquete() 
        {
            try 
            {
                porta = randomizador.Next(12400, 12422);
                determinaIP(); 
                Socket ouvidor = new TcpListener(IP, porta).Server;

                //FAZER A LÓGICA
                return porta;
            }
            catch (Exception ex) 
            {
                throw ex; 
            }
        }


        private void determinaIP()
        {
            //Usa o IP da LAN, definindo o a variável IP da classe
            IPHostEntry host;
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    IP = ip; 
                }
            }
        }


    }
}

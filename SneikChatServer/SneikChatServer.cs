using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;

namespace SneikChatServer
{
    public class SneikChatServer
    {
        static void Main(string[] args)
        {
            List<ManejadorCliente> clientes = new List<ManejadorCliente>();
            GestionFichero fichero = new GestionFichero(Environment.CurrentDirectory + @"\SneikChatserver.ini");
            int puerto = 25566;
            fichero.LeerConfig();
            List<String> config = fichero.GetConfig();
            puerto = int.Parse(config[0]);
            Console.WriteLine("Puerto en uso: " + puerto.ToString());
            TcpListener server = new TcpListener(puerto);
            server.Start();
            while (true)
            {
                Console.WriteLine("Esperando clientes");
                Socket cliente = server.AcceptSocket();
                ManejadorCliente cli = new ManejadorCliente(cliente);
                Thread manCli = new Thread(delegate ()
                {
                    cli.Principal();
                });
                manCli.Start();
                clientes.Add(cli);
                Console.WriteLine("Cliente conectado!");
                for(int i = 0;i < clientes.Count; i++)
                {
                    if (clientes[i].GetCerrar())
                    {
                        clientes.RemoveAt(i);
                    }
                }
            }
        }
    }
}

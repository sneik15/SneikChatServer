using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;
using System.IO;

namespace SneikChatServer
{
    public class ManejadorCliente
    {
        public static List<String> chat = new List<string>();
        private Socket cliente;
        private StreamReader entrada;
        private StreamWriter salida;
        private ManejadorEntrada entradaM;
        private ManejadorSalida salidaM;
        private bool cerrar;


        public ManejadorCliente(Socket maneCliente)
        {
            cliente = maneCliente;
            cerrar = false;
        }

        public void Principal()
        {
            try
            {
                NetworkStream ns = new NetworkStream(cliente);
                entrada = new StreamReader(ns);
                salida = new StreamWriter(ns);
                entradaM = new ManejadorEntrada(entrada);
                salidaM = new ManejadorSalida(salida);
                //Iniciamos las clases
                Thread manEnt = new Thread(delegate ()
                {
                    entradaM.Principal();
                });
                manEnt.Start();
                Thread manSal = new Thread(delegate ()
                {
                    salidaM.Principal();
                });
                manSal.Start();
                while (!cerrar)
                {
                    Thread.Sleep(100);
                    if(entradaM.GetSalir() || salidaM.GetSalir())
                    {
                        cerrar = true;
                    }
                }
                Console.WriteLine("Cliente desconectado!");
                entrada.Close();
                salida.Close();
                ns.Close();
                entradaM.Parar();
                salidaM.Parar();
                cliente.Close();
            }
            catch
            {
                Console.WriteLine("Error con un cliente, cerrando la conexion...");
                cliente.Close();
                cerrar = true;
            }
        }

        public bool GetCerrar()
        {
            return cerrar;
        }
    }
}

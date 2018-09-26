using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SneikChatServer
{
    public class ManejadorEntrada
    {
        private String ultEnvio;
        private StreamReader entrada;
        private bool salir;

        public ManejadorEntrada(StreamReader entradaP)
        {
            entrada = entradaP;
            ultEnvio = "Ninguno";
            salir = false;
        }

        public void Principal()
        {
            try
            {
                while (!salir)
                {
                    String cache = entrada.ReadLine();
                    if (!ultEnvio.Equals(cache))
                    {
                        if (cache.Equals("<<cmd>>!salir"))
                        {
                            Thread.Sleep(100);
                            salir = true;
                        }else if (cache.StartsWith("<<cmd>>!Ucnt") || cache.StartsWith("<<cmd>>!UDcnt"))
                        {
                            ultEnvio = cache;
                            ManejadorCliente.chat.Add(cache);
                        }else if (cache.StartsWith("<<cmd>>!"))
                        {
                            Console.WriteLine("Comando desconocido recibido: " + cache);
                        }
                        else
                        {
                            ultEnvio = cache;
                            ManejadorCliente.chat.Add(cache);
                        }
                    }
                }
            }
            catch
            {
                Console.WriteLine("Error en la entrada del cliente, desconectando...");
                salir = true;
            }
        }

        public void Parar()
        {
            salir = true;
        }

        public bool GetSalir()
        {
            return salir;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SneikChatServer
{
    public class ManejadorSalida
    {
        private StreamWriter salida;
        private String ultEnvio;
        private int intUltEnvio;
        private bool salir;

        public ManejadorSalida(StreamWriter salidaP)
        {
            ultEnvio = "Ninguno";
            salir = false;
            intUltEnvio = 0;
            salida = salidaP;
        }

        public void Principal()
        {
            try
            {
                while (!salir)
                {
                    Thread.Sleep(50);
                    while(intUltEnvio < ManejadorCliente.chat.Count)
                    {
                        String lin = ManejadorCliente.chat[intUltEnvio];
                        if (!ultEnvio.Equals(lin))
                        {
                            ultEnvio = lin;
                            salida.WriteLine(lin);
                            salida.Flush();
                        }
                        intUltEnvio++;
                    }
                    
                }
            }
            catch
            {
                Console.WriteLine("Error al enviar datos al cliente, desconectando...");
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

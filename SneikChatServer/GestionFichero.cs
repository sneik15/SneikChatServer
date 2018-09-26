using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneikChatServer
{
    class GestionFichero
    {
        private List<String> configuracion;
        private String archivo;

        public GestionFichero(String archi)
        {
            configuracion = new List<string>();
            archivo = archi;
        }

        public void LeerConfig()
        {
            if (File.Exists(archivo))
            {
                configuracion.Clear();
                StreamReader archi = new StreamReader(archivo);
                String linea = "";
                while(linea != null)
                {
                    linea = archi.ReadLine();
                    if(linea != null)
                    {
                        configuracion.Add(linea);
                    }
                }
                archi.Close();
            }
            else
            {
                GrabarConfig("25566");
                configuracion.Clear();
                StreamReader archi = new StreamReader(archivo);
                String linea = "";
                while (linea != null)
                {
                    linea = archi.ReadLine();
                    if (linea != null)
                    {
                        configuracion.Add(linea);
                    }
                }
                archi.Close();
            }
        }

        public List<String> GetConfig()
        {
            return configuracion;
        }

        public void GrabarConfig(String puerto)
        {
            StreamWriter archi = new StreamWriter(archivo);
            archi.WriteLine(puerto);
            archi.Close();
        }
    }
}

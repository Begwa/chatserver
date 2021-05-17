using ChatServerApp.Comunicacion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            int puerto = Int32.Parse(ConfigurationManager.AppSettings["puerto"]);
            Console.WriteLine("Iniciando Servidor en puerto {0}",puerto);
            ServerSocket servidor = new ServerSocket(puerto);
            if (servidor.Iniciar())
            {
                while (true)
                {
                    //Obtiene clientes
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Esperando CLientes...");
                    if (servidor.ObtenerCliente())
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("Conexion Establecida!");
                        //protocolo de comunicacion
                        string mensaje = "";
                        while(mensaje.ToLower() != "chao")
                        {
                            //leer el mensaje del cliente
                            mensaje = servidor.Leer();
                            Console.WriteLine("C:{0}", mensaje);
                            if(mensaje.ToLower() != "chao")
                            {
                                //se espera una respuesta del cliente
                                Console.WriteLine("Digame lo que quiere decir");
                                mensaje = Console.ReadLine().Trim();
                                Console.WriteLine("S:{0}", mensaje);
                                servidor.Escribir(mensaje);
                            }
                        }
                        servidor.CerrarConexion();
                    }
                }
            }else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No es posible iniciar el servidor");
                Console.ReadKey();
            }
        }
    }
}

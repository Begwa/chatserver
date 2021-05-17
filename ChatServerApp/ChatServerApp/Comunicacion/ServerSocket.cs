using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatServerApp.Comunicacion
{
    public class ServerSocket
    {
        //atributos
        private int puerto;
        private Socket servidor;
        private Socket comunicacionCliente;
        private StreamReader reader;
        private StreamWriter writer;

        //constructor
        public ServerSocket(int puerto)
        {
            this.puerto = puerto;
        }

        //metodo iniciar
        public bool Iniciar()
        {
            try
            {
                //crear socket
                this.servidor = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //tomar control del puerto
                this.servidor.Bind(new IPEndPoint(IPAddress.Any, this.puerto));
                //definir clientes que se atenderan
                this.servidor.Listen(10);
                //return
                return true;
            }catch(Exception ex)
            {
                return false;
            }
        }
        //metodo obtener cliente
        public bool ObtenerCliente()
        {
            try
            {
                this.comunicacionCliente = this.servidor.Accept();
                Stream stream = new NetworkStream(this.comunicacionCliente);
                this.writer = new StreamWriter(stream);
                this.reader = new StreamReader(stream);
                //return
                return true;
            }catch(Exception ex)
            {
                return false;
            }
        }
        //metodo escribir
        public bool Escribir(string mensaje)
        {
            try
            {
                this.writer.WriteLine(mensaje);
                this.writer.Flush();
                //return
                return true;
            }
            catch(IOException ex)
            {
                return false;
            }
        }
        //metodo leer
        public string Leer()
        {
            try
            {
                return this.reader.ReadLine().Trim();
            }
            catch(IOException ex)
            {
                return null;
            }
        }
        //metodo cerrar conexion
        public void CerrarConexion()
        {
            this.comunicacionCliente.Close();
        }
    }
}

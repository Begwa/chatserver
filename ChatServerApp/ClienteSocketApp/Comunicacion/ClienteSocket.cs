using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClienteSocketApp.Comunicacion
{
    public class ClienteSocket
    {
        //atributos
        private string ip;
        private int puerto;
        private Socket comunicacionServidor;
        private StreamReader reader;
        private StreamWriter writer;
        //constructor
        public ClienteSocket(string ip, int puerto)
        {
            this.puerto = puerto;
            this.ip = ip;
        }
        //metodo conectar
        public bool Conectar()
        {
            try
            {
                this.comunicacionServidor = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse(ip), puerto);
                this.comunicacionServidor.Connect(endpoint);
                Stream stream = new NetworkStream(this.comunicacionServidor);
                this.reader = new StreamReader(stream);
                this.writer = new StreamWriter(stream);
                //return true 
                return true;
            }
            catch(IOException ex)
            {
                return false;
            }
        }
        //metodo Escribir
        public bool Escribir(string mensaje)
        {
            try
            {
                this.writer.WriteLine(mensaje);
                this.writer.Flush();
                return true;
            }
            catch(IOException ex)
            {
                return false;
            }
        }
        //metodo Leer
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
        //metodo desconectar
        public void Desconectar()
        {
            this.comunicacionServidor.Close();
        }
    }
}

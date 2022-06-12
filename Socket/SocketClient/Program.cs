using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("SartTime={0}", DateTime.Now);
            //for (int i = 0; i < 1000; i++)
            //{
            //    SyncSocketClient.StartClient();
            //}
            //Console.WriteLine("EndTime={0}", DateTime.Now);

            SyncSocketClient.StartClient();
            Console.Read();
        }
    }
}

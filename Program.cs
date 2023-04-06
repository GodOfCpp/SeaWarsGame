using SeaWarsGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeaWars
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        /// 

        public 
        static void Main()
        {

            //var getFunc = FileGetter.UdpFileClient.Get;
            //var sendFunc = FileSender.UdpFileServer.Send;


            //Thread getThread = new Thread(() => getFunc());
            //Thread sendThread = new Thread(() => sendFunc("127.0.0.1", "test.txt"));

            //getThread.Start();
            //sendThread.Start();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}

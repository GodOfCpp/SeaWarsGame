using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaWarsGame
{
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Web;

    public class Chat
    {
        public string username;
        private IPAddress localAddress;
        private int localPort;
        private int remotePort;
        public string lastMsg;
        public Chat(string? username, string? localAddress, string? localPort, string? remotePort)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentNullException("Invalid username");
            }
            if (string.IsNullOrWhiteSpace(localAddress))
            {
                throw new ArgumentNullException("Invalid local address");
            }
            if (string.IsNullOrWhiteSpace(localPort))
            {
                throw new ArgumentNullException("Invalid local port");
            }
            if (string.IsNullOrWhiteSpace(remotePort))
            {
                throw new ArgumentNullException("Invalid remote port");
            }

            this.username = username;
            this.localAddress = IPAddress.Parse(localAddress);
            this.localPort = int.Parse(localPort);
            this.remotePort = int.Parse(remotePort);
        }
        public async Task SendMsgAsync()
        {
            var sender = new UdpClient();
            Console.WriteLine("Для отправки сообщений введите сообщение и нажмите Enter");
            await Task.Run(async () =>
            {
                while (true)
                {
                    var msg = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(msg)) break;
                    msg = $"{username}: {msg}";
                    byte[] data = Encoding.UTF8.GetBytes(msg);
                    await sender.SendAsync(data, new IPEndPoint(localAddress, remotePort));
                }
            });
        }

        public void SendMsg(string msg)
        {
            var sender = new UdpClient();
           
            msg = $"{username}: {msg}";
            byte[] data = Encoding.UTF8.GetBytes(msg);
            sender.Send(data, new IPEndPoint(localAddress, remotePort));
        }

        public async Task ReceiveMsgAsync()
        {
            var receiver = new UdpClient(localPort);
            while (true)
            {
                var result = await receiver.ReceiveAsync();
                var msg = Encoding.UTF8.GetString(result.Buffer);
                lastMsg = msg.Substring(msg.IndexOf(':')+2);
                Print(msg);
            }
        }

        void Print(string msg)
        {
            if (OperatingSystem.IsWindows())
            {
                var position = Console.GetCursorPosition();
                int left = position.Left;
                int top = position.Top;
                Console.MoveBufferArea(0, top, left, 1, 0, top + 1);
                Console.SetCursorPosition(0, top);
                Console.WriteLine(msg);
                Console.SetCursorPosition(left, top + 1);
            }
            else Console.WriteLine(msg);
        }

    }
}

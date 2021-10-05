using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerClientConnection
{
    public class Client : IDisposable
    {
        private TcpClient client;
        private Stream stream;

        public Client(TcpClient client)
        {
            this.client = client;
            this.stream = client.GetStream();
        }

        private static byte[] WrapMessage(byte[] message)
        {
            byte[] lengthPrefix = BitConverter.GetBytes(message.Length);
            byte[] ret = new byte[lengthPrefix.Length + message.Length];
            lengthPrefix.CopyTo(ret, 0);
            message.CopyTo(ret, lengthPrefix.Length);
            return ret;
        }

        public void Write(byte[] message)
        {
            try
            {
                stream.Write(WrapMessage(message));
                stream.Flush();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public async Task<string> Read()
        {
            byte[] length = new byte[4];
            this.stream.Read(length, 0, 4);

            int size = BitConverter.ToInt32(length);

            byte[] received = new byte[size];

            int bytesRead = 0;
            while (bytesRead < size)
            {
                int read = await this.stream.ReadAsync(received, bytesRead, received.Length - bytesRead);
                bytesRead += read;
            }

            return Encoding.ASCII.GetString(received);
        }

        public void Dispose()
        {
            this.stream.Close();
            this.stream.Dispose();
            this.client.Close();
            this.client.Dispose();
        }
    }
}

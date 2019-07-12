//-------------User.cs----------------//
using System.Net.Sockets;
using System.IO;
namespace ChatServer
{
    class User
    {
        public TcpClient client;
        public BinaryReader br;
        public BinaryWriter bw;
        public User(TcpClient client)
        {
            this.client = client;
            NetworkStream networkStream = client.GetStream();
            br = new BinaryReader(networkStream);
            bw = new BinaryWriter(networkStream);
        }
    }
}

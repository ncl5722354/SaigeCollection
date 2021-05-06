using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Communication
{
    public class ServerClient
    {
        public TcpClient client;
        public byte[] receive_byte = new byte[8192];
        public IPAddress ip;
        public int port;
        public bool connect_is = false;


        public event EventHandler disconnect;
        public event EventHandler receive;
        public int RECEIVE_NUM;
        
        public ServerClient()
        {
            client = new TcpClient();
            Thread receive = new Thread(Receive);
            receive.Start();
            
        }

        // 连接
        public void Reconnect()
        {
            try
            {
                client.Client.Connect(ip, port);
                connect_is = true;
            }
            catch {
                if (disconnect != null)
                {
                    this.disconnect(this, new EventArgs());
                    connect_is = false;
                }
            }
        }

        // 返回连接状态
        public bool Return_status()
        {
            return client.Client.Connected;
        }

        // 发送数据

        public byte[] Send_Message(string str)
        {
            byte[] receive_byte = new byte[str.Length / 2];
            try
            {
                for (int i = 0; i < str.Length / 2; i++)
                {
                    receive_byte[i] = Convert.ToByte(str.Substring(i * 2, 2), 16);
                }

                client.Client.Send(receive_byte);
            }
            catch
            {
                if (disconnect != null)
                {
                    this.disconnect(this, new EventArgs());
                    connect_is = false;
                }
            }

            return receive_byte;
        }

        


        void Receive()
        {
            while(true)
            {
                try
                {

                    for(int i=0;i<receive_byte.Length;i++)
                    {
                        receive_byte[i] = 0;
                    }

                    int receive_byte_num = client.Client.Receive(receive_byte);
                    RECEIVE_NUM = receive_byte_num;

                    if(receive_byte_num>0)
                    {
                        if(receive!=null)
                        {
                            receive(this, new EventArgs());
                        }
                    }


                }
                catch { }
            }
        }

        
    }
}

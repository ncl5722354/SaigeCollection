using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace Communication
{
    /// <summary>
    /// 2016 12 18 开发一个客户端程序
    /// Developed by Tom Ni 
    /// 通过构造函数设定客户端的远程ip与端口号port
    /// 不断的连接远程ip与端口号
    /// 连接上的时候发生连接上事件
    /// 连接不上或者接收错误，或者发送数据错误发生连接断开事件
    /// 
    /// </summary>
    public class TcpServerClient
    {
        // 成员变量
        TcpClient client = new TcpClient();
        public IPAddress ServerIp;
        int ServerPort;
        public byte[] receive_byte = new byte[8192*10];
        public int receive_num;

        // 事件
        public event EventHandler Connected_Server;                            // 连接到服务器事件
        public event EventHandler Data_Arrival_Event;                          // 数据到达事件
        public event EventHandler Disconned_Server_Event;                      // 与服务器断开事件


        public TcpServerClient(string serverip,int serverport)
        {
            try
            {
                ServerIp = IPAddress.Parse(serverip);
            }
            catch 
            { 
                throw new Exception("ip地址格式不正确！"); 
            }
            ServerPort = serverport;
            Thread connect = new Thread(Connect_To_Server);
            connect.Start();
            Thread data_arrival_thread = new Thread(Data_Arrival);
            data_arrival_thread.Start();
        }



        private void Connect_To_Server()
        {
            try
            {
                client = null;
                client = new TcpClient();
                client.Client.SendTimeout=1000;
                
                //IPEndPoint iep = new IPEndPoint(ServerIp, 8880);
                
                //client.Client.BeginConnect(ServerIp,8880, new AsyncCallback(Connect),client.Client);
                //Thread invokeThread = new Thread(new ThreadStart(StartMethod));
                //invokeThread.Start();
                Console.WriteLine("连接中……"+ServerIp.ToString()+" "+ServerPort.ToString());
                client.Client.Connect(ServerIp, ServerPort);

                
            }
            catch {
                Console.WriteLine("连接失败！");
                //Thread connect_thread = new Thread(Connect_To_Server);
                //connect_thread.Start();
            }
        }
        private void StartMethod()
        {
            try
            {
                client.Client.Connect(ServerIp, ServerPort);
            }
            catch { }
        }

        

        // 接收数据的函数
        private void Data_Arrival()
        {
            while(true)
            {
                try{
                    receive_num = client.Client.Receive(receive_byte);
                    if (receive_num > 0)
                    {
                        Data_Arrival_Trigger(new EventArgs());
                    }
                    else
                    {
                        DisConnected_Server_Trigger(new EventArgs());
                        //Connect_To_Server();                                         //重连
                    }
                }
                catch (Exception ex)
                {
                    DisConnected_Server_Trigger(new EventArgs());
                    Console.WriteLine(ex.ToString());
                    //Connect_To_Server(); 
                }
            }
        }

        
        // 发送数据的函数
        public void Send_Data(byte[] send_byte)
        {
            try
            {
                if(client.Client.Connected==true)
                {
                    client.Client.Send(send_byte);
                    
                }
            }
            catch
            {
                DisConnected_Server_Trigger(new EventArgs());
                //Connect_To_Server();
            }
        }


        // 事件触发
        private void Connected_Server_Trigger(EventArgs e)
        {
            if(Connected_Server!=null)
            {
                Connected_Server(this, e);
            }
        }      // 连接到服务器的触发

        private void Data_Arrival_Trigger(EventArgs e)
        {
            if(Data_Arrival_Event!=null)
            {
                Data_Arrival_Event(this, e);
            }
        }          // 数据到达事件的触发

        private void DisConnected_Server_Trigger(EventArgs e)
        {
            if(Disconned_Server_Event!=null)
            {
                Disconned_Server_Event(this, e);
            }
        }   // 与服务器断开的触发
    }
}

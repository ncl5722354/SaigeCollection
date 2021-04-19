using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using String_Caozuo;
using System.Runtime.InteropServices;

namespace Communication
{
    /// <summary>
    /// 2017 12 16 开始开发一个服务端 
    /// Developed By Tom Ni
    /// 主要功能
    /// 服务端拥有一个监听的ip地址与端口号
    /// 监听过程中如果有信息传进来，就进行信息的中断，并可以将数据读取出来，同时将远程的IP地址，端口号读取进来
    /// </summary>
    /// 

    [ComVisible(true)]
    [Guid("D41810E4-35C0-4948-A755-16154B3B44B7")]
    [ProgId("Communication.TcpServer")]
    public class TcpServer:ITcpServer
    {
        private IPAddress ServerIp;        // 服务IP地址
        private int ServerPort;            // 服务端口号
        private TcpListener tcplistener;   // TCP监听器
        private ArrayList TcpClient_ArrayList = new ArrayList();            // TCP客户端的队列
        private string_caozuo mystring_caozuo = new string_caozuo();

        // 连接时的消息
        public string connect_info = "";                                    // 客户端连接上的时候的信息

        
        // 事件注册
        public event EventHandler Remote_Clent_Connect;                     // 客户端连接时发生的事件
        public event EventHandler Data_Arrival;                             // 数据到达时发生的事件
        public event EventHandler Client_DisConnected;                      // 客户端连接断开事件


        
        // 构造函数
       
        public TcpServer(string ip,int port)
        {
            try
            {
                ServerIp = IPAddress.Parse(ip);               
                ServerPort = port;
            }
            catch 
            {
                throw new Exception("Ip地址字符串格式错误！");
            }
        }

        // 开始服务函数
        public void Start_Server()
        {
            tcplistener = new TcpListener(ServerIp,ServerPort);
            try
            {
                tcplistener.Start();                  // 开始以IP套接字(ServerIp,ServerPort)来侦听相应的IP地址与端口号
            }
            catch
            {
                throw new Exception("侦听ip地址 "+ServerIp.ToString()+" 端口号 "+ServerPort.ToString()+" 出现错误！");
            }
            // 开启侦听线程
            Thread listener_thread = new Thread(Listener);
            listener_thread.Start();
            //Begin_AsyncEvent();

        }

        private void Listener()
        {
            while(true)
            {
                //TcpClient client = tcplistener.AcceptTcpClient();
                //ServerClient serverclient = new ServerClient(client);
                //client.ReceiveBufferSize = 8192;
                //TcpClient_ArrayList.Add(serverclient);                        //连接到一个client，就将其放到队列当中
                //connect_info = client.Client.RemoteEndPoint.ToString();             // 显示远程端口信息
                //Remote_Clent_Connect_Trigger(serverclient,new EventArgs());                      // 引发此事件
                //Thread receive_thread = new Thread(new ParameterizedThreadStart(OnReceiveCompleted));
                //receive_thread.Start(serverclient);
            }
        }

        public void Send_Data(byte[] send_data,string remote_ip)
        {
             foreach(ServerClient serverclient in TcpClient_ArrayList)
             {
                 try
                 {
                     if(string_caozuo.Get_Maohao_String(serverclient.client.Client.RemoteEndPoint.ToString(),1)==remote_ip)
                     {
                         serverclient.client.Client.Send(send_data);
                     }
                 }
                 catch { }
             }
        }

        public ArrayList Get_Client_ArrayList()
        {
            return TcpClient_ArrayList;
        }


        // 事件触发
        private void Remote_Clent_Connect_Trigger(ServerClient serverclient,EventArgs e)
        {
            if(Remote_Clent_Connect!=null)
            {
                Remote_Clent_Connect(serverclient, e);
            }
        }    // 远程客户端连接到服务器端的事件

        private void Data_Arrival_Trigger(ServerClient serverclient,EventArgs e)
        {
            if(Data_Arrival!=null)
            {
                Data_Arrival(serverclient, e);
            }
        }     // 数据到达的事件

        private void Client_DisConnect_Trigger(ServerClient serverclient,EventArgs e)
        {
            if(Client_DisConnected!=null)
            {
                Client_DisConnected(serverclient, e);
            }
        }                          // 客户端断开事件


        // 事件处理
        private void OnReceiveCompleted(object sender)
        {
            ServerClient serverclient = (ServerClient)sender;
            while(true)
            {
                byte[] receive_buffer = new byte[8192];
                int receive_num = serverclient.client.Client.Receive(receive_buffer);
                
                if (receive_num == 0)
                {
                    TcpClient_ArrayList.Remove(serverclient);
                    Client_DisConnect_Trigger(serverclient,new EventArgs());
                    break;
                }
                else
                {
                    // 处理数据
                    serverclient.receive_byte = receive_buffer;
                    Data_Arrival_Trigger(serverclient, new EventArgs());
                }
            }
            //int a = 0;
        }   // 数据传输完成 
    }
}

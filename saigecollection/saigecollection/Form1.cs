using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using Communication;
using System.Net;

namespace saigecollection
{
    public partial class Form1 : Form
    {
        public static IniFile inifile = new IniFile("C://databaseconfig.ini");
        
        
        public static bool connnection_is = false;
        public static bool isscaning = false;
        public static bool collector_connect_is = false;

        // 通信相关的变量

        //
        public static string connect_device_id = "";      // 现在通讯的设备ID
        public static int connect_value_num = 0;          // 现在通讯的变量号
        public static string connect_value_name = "";     // 现在通讯的变量名称
        public static int time_count = 0;                 // 过时计时
        public static int time_out = 20;                  // 过时计时计数量
        public static bool send_is = false;               // 是否发送了数据
        public static bool receive_is = false;            // 是否接收了数据
        public static string show_text = "";
        public static int device_num = 0;

        public static ServerClient client1 = new ServerClient();
        

        public Form1()
        {
            InitializeComponent();
            client1.receive += new EventHandler(Receive_Data);
        }


        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                WindowState = FormWindowState.Normal;
                this.Activate();

                this.ShowInTaskbar = true;
                notifyIcon1.Visible = false;
            }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if(WindowState==FormWindowState.Minimized)
            {
                this.ShowInTaskbar = false;
                notifyIcon1.Visible=true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mysqlconfig view = new mysqlconfig();
            DialogResult result = view.ShowDialog();
        }

        private void button_reconnect_Click(object sender, EventArgs e)
        {
            // 读取所有的项目信息

            connnection_is = false;

            Mysql.remote_ip = inifile.IniReadValue("databaseset", "database_ip");
            Mysql.database_name = inifile.IniReadValue("databaseset", "database_name");
            Mysql.user = inifile.IniReadValue("databaseset","username");
            Mysql.password = inifile.IniReadValue("databaseset", "password");

            ArrayList allproject_list = Mysql.Get_Sql_Select_Return("select * from xiangmuguanlitable");

            comboBox_project.Items.Clear();

            if (allproject_list.Count > 0) connnection_is = true;

            for (int i = 0; i < allproject_list.Count; i++)
            {
                ArrayList list = (ArrayList) allproject_list[i];

                comboBox_project.Items.Add(list[1].ToString());
            
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(connnection_is==true)
            {
                label_database_status.Text = "连接上";
            }
            if(connnection_is==false)
            {
                label_database_status.Text = "未连接上";
            }

            if (isscaning == true)
            {
                dataGridView1.Enabled = false;
                button_stop_collect.Enabled = true;
                //button_begin_collect.Enabled = false;
            }
            if(isscaning==false)
            {
                dataGridView1.Enabled = true;
                button_stop_collect.Enabled = false;
                //button_begin_collect.Enabled = true;

            }

            if(client1.connect_is==false)
            {
                label_device_connect.Text = "未连接上";
            }

            if(client1.connect_is==true)
            {
                label_device_connect.Text = "已连接上";
            }

            if(client1.connect_is==true && connnection_is==true && isscaning==false)
            {
                button_begin_collect.Enabled = true;
            }
            else
            {
                button_begin_collect.Enabled = false;
            }
        }




        private void Reflush_Device_Table()
        {
            dataGridView1.RowCount = 1;
            dataGridView1.ColumnCount = 13;

            for (int i = 0; i < dataGridView1.ColumnCount;i++)
            {
                dataGridView1[i, 0].Value = "";
            }
            try
            {
                string project_name = comboBox_project.Items[comboBox_project.SelectedIndex].ToString();

                ArrayList device_list = Mysql.Get_Sql_Select_Return("select shebeiID, shebeiname,(select shebeizhongleiname from shebeizhongleitable where shebeizhongleitable.shebeizhongleiID=shebeizhongleiID),shebeidizhi,value1,value2,value3,value4,value5,value6,value7,value8,value9 from shebeitable where shebeixiangmuID=(select distinct xiangmuID from xiangmuguanlitable where xiangmuname=\"" + project_name + "\")");


                if (device_list.Count >= 1)
                {
                    dataGridView1.RowCount = device_list.Count;

                    for (int i = 0; i < device_list.Count; i++)
                    {

                        ArrayList list=(ArrayList)device_list[i];
                        for (int j = 0; j < list.Count; j++)
                        {
                            dataGridView1[j,i].Value = list[j].ToString();
                        }


                    }
                }
            }
            catch { }
        }

        private void comboBox_project_TextChanged(object sender, EventArgs e)
        {
            Reflush_Device_Table();
        }

        private void button_collect_config_Click(object sender, EventArgs e)
        {
            collector_config view = new collector_config();
            DialogResult result = view.ShowDialog();


        }

        private void button_connect_Click(object sender, EventArgs e)
        {
            try
            {
                client1.connect_is = false;
                client1.ip = IPAddress.Parse(inifile.IniReadValue("collector", "ip"));
                client1.port =int.Parse(inifile.IniReadValue("collector", "port"));
                client1.Reconnect();
            }
            catch { }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount == 0) return;
            if (dataGridView1[0,0].Value==null) return;
            if (dataGridView1[0, 0].Value.ToString() == "") return;

            if (send_is == true)
            {
                time_count++;

                if(time_count>=time_out)
                {
                    // 超时
                    time_count = 0;
                    send_is = false;
                }
            }



            if (isscaning == true && send_is == false)
            {
                if (device_num >= dataGridView1.RowCount)
                {
                    device_num = 0;
                }


                string device_address = dataGridView1[3, device_num].Value.ToString();   // 设备地址
                string device_id = dataGridView1[0, device_num].Value.ToString();        // 设备ID
                string device_type = dataGridView1[2, device_num].Value.ToString();      // 设备类型



                ArrayList canshuzhongleilist = Mysql.Get_Sql_Select_Return("SELECT (select canshutype from saigedatabase.canshutable where canshutypeid=canshu1zhongleiID),(select canshutype from saigedatabase.canshutable where canshutypeid=canshu2zhongleiID),(select canshutype from saigedatabase.canshutable where canshutypeid=canshu3zhongleiID),(select canshutype from saigedatabase.canshutable where canshutypeid=canshu4zhongleiID),(select canshutype from saigedatabase.canshutable where canshutypeid=canshu5zhongleiID),(select canshutype from saigedatabase.canshutable where canshutypeid=canshu6zhongleiID) ,(select canshutype from saigedatabase.canshutable where canshutypeid=canshu7zhongleiID),(select canshutype from saigedatabase.canshutable where canshutypeid=canshu8zhongleiID),(select canshutype from saigedatabase.canshutable where canshutypeid=canshu9zhongleiID)  FROM saigedatabase.shebeizhongleitable");

                if (canshuzhongleilist.Count == 0) return;

                ArrayList list = (ArrayList)canshuzhongleilist[0];


                send_is = true;
                time_count = 0;
                Send_Device_Message(device_address, device_id, device_type, connect_value_num,list[connect_value_num].ToString());
                connect_value_num++;


                if (connect_value_num >= 9)
                {
                    connect_value_num = 0;
                    device_num++;
                    
                }

            }
        }

        void Send_Device_Message(string address,string id,string type,int value_num,string value_type)
        {
            // 处理发送消息
            client1.Send_Message(address);

            show_text = "发送消息：设备ID：" + id + " 设备类型：" + type + " 数据类型：" + value_type;

        }

        void Receive_Data(object sender,EventArgs e)
        {
            string str = "";

            for(int i=0;i<8;i++)
            {
               str = str + client1.receive_byte[i].ToString("X")+" ";

            }

            show_text = "接收数据：" + str;


        }

        private void button_begin_collect_Click(object sender, EventArgs e)
        {
            isscaning = true;
        }

        private void button_stop_collect_Click(object sender, EventArgs e)
        {
            isscaning = false;
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            label_connect_info.Text = show_text;
        }

       
    }
}

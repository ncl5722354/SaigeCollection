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
        public static string connect_device_address = ""; // 现在通讯的设备地址
        public static int connect_value_num = 0;          // 现在通讯的变量号
        public static string connect_value_name = "";     // 现在通讯的变量名称
        public static int time_count = 0;                 // 过时计时
        public static int time_out = 10;                  // 过时计时计数量
        public static bool send_is = false;               // 是否发送了数据
        public static bool receive_is = false;            // 是否接收了数据
        public static string show_text = "";
        public static int device_num = 0;

        public static ServerClient client1 = new ServerClient();
        

        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;                           // 关闭跨线程访问
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


            if (isscaning == false)
            {
                button_reflush.Enabled = true;
            }
            if (isscaning == true)
            {
                button_reflush.Enabled = false;
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
                    connect_value_num++;


                    if (connect_value_num >= 9)
                    {
                        connect_value_num = 0;
                        device_num++;

                    }

                    time_count = 0;
                    send_is = false;
                }
            }



            if (isscaning == true && send_is == false)
            {
                if (device_num >= dataGridView1.RowCount)
                {
                    device_num = 0;
                    connect_value_num = 0;
                }


                string device_address = dataGridView1[3, device_num].Value.ToString();   // 设备地址
                string device_id = dataGridView1[0, device_num].Value.ToString();        // 设备ID
                string device_type = dataGridView1[2, device_num].Value.ToString();      // 设备类型



                ArrayList canshuzhongleilist = Mysql.Get_Sql_Select_Return("SELECT (select canshutype from saigedatabase.canshutable where canshutypeid=canshu1zhongleiID),(select canshutype from saigedatabase.canshutable where canshutypeid=canshu2zhongleiID),(select canshutype from saigedatabase.canshutable where canshutypeid=canshu3zhongleiID),(select canshutype from saigedatabase.canshutable where canshutypeid=canshu4zhongleiID),(select canshutype from saigedatabase.canshutable where canshutypeid=canshu5zhongleiID),(select canshutype from saigedatabase.canshutable where canshutypeid=canshu6zhongleiID) ,(select canshutype from saigedatabase.canshutable where canshutypeid=canshu7zhongleiID),(select canshutype from saigedatabase.canshutable where canshutypeid=canshu8zhongleiID),(select canshutype from saigedatabase.canshutable where canshutypeid=canshu9zhongleiID)  FROM saigedatabase.shebeizhongleitable");

                if (canshuzhongleilist.Count == 0) return;

                ArrayList list = (ArrayList)canshuzhongleilist[0];


                send_is = true;
                time_count = 0;

                connect_device_address = device_address;
                connect_device_id = device_id;
                connect_value_name = list[connect_value_num].ToString();


                Send_Device_Message(device_address, device_id, device_type, connect_value_num,list[connect_value_num].ToString());
                

            }
        }

        void Send_Device_Message(string address,string id,string type,int value_num,string value_type)
        {
            // 处理发送消息
            string send_cmd= address ;

            show_text = "发送消息：设备ID：" + id + " 设备类型：" + type + " 数据类型：" + value_type;


            if (value_type == "正泰电表电压")
            {
                send_cmd = send_cmd + "08010320000006CE08";
               
            }


            if (value_type == "合相有功功率")
            {
                send_cmd = send_cmd + "080103201200026FCE";
                
            }

            if (value_type == "合相功率因数")
            {
                send_cmd = send_cmd + "080103202A0002EE03";
                
            }

            if(value_type=="正向有功总电能")
            {
                send_cmd = send_cmd + "080103401E0002B1CD";
              
            }

            if(value_type=="正泰电表电流")
            {
                send_cmd = send_cmd + "080103200C00060E0B";
                
            }

            if (value_type == "电流互感器倍率")
            {
                send_cmd = send_cmd + "08010300060001640B";
            }

            if (value_type == "电压互感器倍率")
            {
                send_cmd = send_cmd + "0801030007000135CB";
            }

            client1.Send_Message(send_cmd);


        }

        void Receive_Data(object sender,EventArgs e)
        {
            string str = "";

            for (int i = 0; i < 300; i++)
            {

                str = str + client1.receive_byte[i].ToString("X").PadLeft(2, '0');

            }



            // 检查数据的地址是否正确
            if (str.Substring(4, 16) == connect_device_address)
            {

                string reslut_value = "";     // 写入某个数值 
                if (connect_value_name == "正泰电表电压")
                {
                    byte[] Ua_bytes = new byte[4];
                    Ua_bytes[0] = client1.receive_byte[31];
                    Ua_bytes[1] = client1.receive_byte[32];
                    Ua_bytes[2] = client1.receive_byte[29];
                    Ua_bytes[3] = client1.receive_byte[30];


                    byte[] Ub_bytes = new byte[4];
                    Ub_bytes[0] = client1.receive_byte[35];
                    Ub_bytes[1] = client1.receive_byte[36];
                    Ub_bytes[2] = client1.receive_byte[33];
                    Ub_bytes[3] = client1.receive_byte[34];

                    byte[] Uc_bytes = new byte[4];
                    Uc_bytes[0] = client1.receive_byte[39];
                    Uc_bytes[1] = client1.receive_byte[40];
                    Uc_bytes[2] = client1.receive_byte[37];
                    Uc_bytes[3] = client1.receive_byte[38];


                    float Ua = BitConverter.ToSingle(Ua_bytes, 0) / 10;
                    float Ub = BitConverter.ToSingle(Ub_bytes, 0) / 10;
                    float Uc = BitConverter.ToSingle(Uc_bytes, 0) / 10;

                    reslut_value = Ua.ToString() + " " + Ub.ToString() + " " + Uc.ToString();

                    show_text = "电压a：" + Ua.ToString()+" 电压b："+Ub.ToString()+" 电压c:"+Uc.ToString();
                }

                if (connect_value_name == "正泰电表电流")
                {
                    byte[] Ia_bytes = new byte[4];
                    Ia_bytes[0] = client1.receive_byte[32];
                    Ia_bytes[1] = client1.receive_byte[33];
                    Ia_bytes[2] = client1.receive_byte[30];
                    Ia_bytes[3] = client1.receive_byte[31];


                    byte[] Ib_bytes = new byte[4];
                    Ib_bytes[0] = client1.receive_byte[36];
                    Ib_bytes[1] = client1.receive_byte[37];
                    Ib_bytes[2] = client1.receive_byte[34];
                    Ib_bytes[3] = client1.receive_byte[35];

                    byte[] Ic_bytes = new byte[4];
                    Ic_bytes[0] = client1.receive_byte[40];
                    Ic_bytes[1] = client1.receive_byte[41];
                    Ic_bytes[2] = client1.receive_byte[38];
                    Ic_bytes[3] = client1.receive_byte[39];


                    float Ia = BitConverter.ToSingle(Ia_bytes, 0) / 10;
                    float Ib = BitConverter.ToSingle(Ib_bytes, 0) / 10;
                    float Ic = BitConverter.ToSingle(Ic_bytes, 0) / 10;

                    reslut_value = Ia.ToString() + " " + Ib.ToString() + " " + Ic.ToString();

                    show_text = "电流a：" + Ia.ToString() + " 电流b：" + Ib.ToString() + " 电流c:" + Ic.ToString();
                }


                if (connect_value_name == "合相有功功率")
                {
                    byte[] power_bytes = new byte[4];
                    power_bytes[0] = client1.receive_byte[32];
                    power_bytes[1] = client1.receive_byte[33];
                    power_bytes[2] = client1.receive_byte[30];
                    power_bytes[3] = client1.receive_byte[31];

                    float power = BitConverter.ToSingle(power_bytes, 0);

                    reslut_value = power.ToString();

                    show_text = "合相有功功率：" + reslut_value;
                }

                if (connect_value_name == "合相功率因数")
                {
                    byte[] power_factor_bytes = new byte[4];
                    power_factor_bytes[0] = client1.receive_byte[33];
                    power_factor_bytes[1] = client1.receive_byte[32];
                    power_factor_bytes[2] = client1.receive_byte[31];
                    power_factor_bytes[3] = client1.receive_byte[30];

                    float power_factor = BitConverter.ToSingle(power_factor_bytes, 0);
                    reslut_value = power_factor.ToString();

                    show_text = "合相功率因数:" + reslut_value;
                }

                if (connect_value_name == "正向有功总电能")
                {
                    byte[] Total_positive_active_energy_bytes = new byte[4];
                    Total_positive_active_energy_bytes[0] = client1.receive_byte[32];
                    Total_positive_active_energy_bytes[1] = client1.receive_byte[33];
                    Total_positive_active_energy_bytes[2] = client1.receive_byte[30];
                    Total_positive_active_energy_bytes[3] = client1.receive_byte[31];

                    float Total_positive_active_energy = BitConverter.ToSingle(Total_positive_active_energy_bytes, 0);
                    reslut_value = Total_positive_active_energy.ToString();

                    show_text = "正向有功总电能：" + reslut_value;
                }

                if (connect_value_name == "电流互感器倍率")
                {
                    float Current_ratio = client1.receive_byte[30] * 256 + client1.receive_byte[31];
                    reslut_value = Current_ratio.ToString();

                    show_text = "电流互感器倍数：" + reslut_value;
                }

                if (connect_value_name == "电压互感器倍率")
                {
                    float voltage_ratio = client1.receive_byte[30] * 256 + client1.receive_byte[31];

                    reslut_value = voltage_ratio.ToString();

                    show_text = "电压互感器倍数：" + reslut_value;
                }

                
                // 写入数据库并更新datagridview

                // 写入数据库
                try
                {
                    Mysql.Ex_Sql("update shebeitable set value" + (connect_value_num + 1).ToString() + "=\"" + reslut_value + "\" where shebeiID=\"" + connect_device_id + "\"");
                    
                }
                catch { }


                // 写入datagridview
               try
               {
                   for(int i=0;i<dataGridView1.RowCount;i++)
                   {
                       string id = dataGridView1[0, i].Value.ToString();

                       if(id==connect_device_id)
                       {
                           dataGridView1[connect_value_num + 4, i].Value = reslut_value;
                           break;
                       }
                   }
               }
               catch { }
               
                //connect_value_num++;


                //if (connect_value_num >= 9)
                //{
                //    connect_value_num = 0;
                //    device_num++;
                //    send_is = false;
                //    time_count = 0;
                    
                //}

            }

            


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

        private void button_reflush_Click(object sender, EventArgs e)
        {
            // 刷新表格


            Reflush_Device_Table();

        }

       
    }
}

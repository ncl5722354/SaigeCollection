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
        //    CheckForIllegalCrossThreadCalls = false;                           // 关闭跨线程访问
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
                ArrayList device_list = Mysql.Get_Sql_Select_Return("select shebeiID, shebeiname,shebeizhongleiname,shebeidizhi,value1,value2,value3,value4,value5,value6,value7,value8,value9 from shebeitable join shebeizhongleitable on shebeizhongleitable.shebeizhongleiID=shebeitable.shebeizhongleiID where shebeixiangmuID=(select distinct xiangmuID from xiangmuguanlitable where xiangmuname=\"" + project_name + "\")");

                

                //ArrayList device_list = Mysql.Get_Sql_Select_Return("select shebeiID, shebeiname,(select shebeizhongleiname from shebeizhongleitable where shebeizhongleitable.shebeizhongleiID=shebeizhongleiID),shebeidizhi,value1,value2,value3,value4,value5,value6,value7,value8,value9 from shebeitable where shebeixiangmuID=(select distinct xiangmuID from xiangmuguanlitable where xiangmuname=\"" + project_name + "\")");


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
                    send_is = false;
                    time_count = 0;
                    connect_value_num++;


                    if (connect_value_num >= 9)
                    {
                        connect_value_num = 0;
                        device_num++;
                        
                        if (device_num >= dataGridView1.RowCount)
                        {
                            device_num = 0;
                            connect_value_num = 0;
                        }

                    }

                   
                  
                    return;
                }
            }



            if (isscaning == true && send_is == false)
            {
                send_is = true;
               

                string device_address = dataGridView1[3, device_num].Value.ToString();   // 设备地址
                string device_id = dataGridView1[0, device_num].Value.ToString();        // 设备ID
                string device_type = dataGridView1[2, device_num].Value.ToString();      // 设备类型


                ArrayList canshuzhongleilist = Mysql.Get_Sql_Select_Return("SELECT (select canshutype from saigedatabase.canshutable where canshutypeid=canshu1zhongleiID),(select canshutype from saigedatabase.canshutable where canshutypeid=canshu2zhongleiID),(select canshutype from saigedatabase.canshutable where canshutypeid=canshu3zhongleiID),(select canshutype from saigedatabase.canshutable where canshutypeid=canshu4zhongleiID),(select canshutype from saigedatabase.canshutable where canshutypeid=canshu5zhongleiID),(select canshutype from saigedatabase.canshutable where canshutypeid=canshu6zhongleiID) ,(select canshutype from saigedatabase.canshutable where canshutypeid=canshu7zhongleiID),(select canshutype from saigedatabase.canshutable where canshutypeid=canshu8zhongleiID),(select canshutype from saigedatabase.canshutable where canshutypeid=canshu9zhongleiID)  FROM saigedatabase.shebeizhongleitable where shebeizhongleitable.shebeizhongleiname=\"" + device_type + "\"");
                //ArrayList canshuzhongleilist = Mysql.Get_Sql_Select_Return("SELECT (select canshutype from saigedatabase.canshutable where canshutypeid=canshu1zhongleiID),(select canshutype from saigedatabase.canshutable where canshutypeid=canshu2zhongleiID),(select canshutype from saigedatabase.canshutable where canshutypeid=canshu3zhongleiID),(select canshutype from saigedatabase.canshutable where canshutypeid=canshu4zhongleiID),(select canshutype from saigedatabase.canshutable where canshutypeid=canshu5zhongleiID),(select canshutype from saigedatabase.canshutable where canshutypeid=canshu6zhongleiID) ,(select canshutype from saigedatabase.canshutable where canshutypeid=canshu7zhongleiID),(select canshutype from saigedatabase.canshutable where canshutypeid=canshu8zhongleiID),(select canshutype from saigedatabase.canshutable where canshutypeid=canshu9zhongleiID)  FROM saigedatabase.shebeizhongleitable");

                if (canshuzhongleilist.Count == 0) return;

                ArrayList list = (ArrayList)canshuzhongleilist[0];


                
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

            if (value_type == "正泰三相电力温度")
            {
                send_cmd = send_cmd + "080103000D00039408";
            }

            if (value_type == "8位开关量")
            {
                send_cmd = send_cmd + "080102001000087809";
            }

            if (value_type == "4路模拟量采集")
            {
                send_cmd = send_cmd + "080103000C0004840A";
            }

            client1.Send_Message(send_cmd);


        }

        void Receive_Data(object sender,EventArgs e)
        {
            string str = "";


            if (client1.RECEIVE_NUM <= 30) return;

            for (int i = 0; i < 300; i++)
            {

                str = str + client1.receive_byte[i].ToString("X").PadLeft(2, '0');

            }



            // 检查数据的地址是否正确


            if (str.Substring(4, 16) == connect_device_address)
            {

                string reslut_value = "";     // 写入某个数值 

                // 三相电压采集
                #region
                if (connect_value_name == "正泰电表电压")
                {
                    byte[] Ua_bytes = new byte[4];
                    Ua_bytes[0] = client1.receive_byte[33];
                    Ua_bytes[1] = client1.receive_byte[32];
                    Ua_bytes[2] = client1.receive_byte[31];
                    Ua_bytes[3] = client1.receive_byte[30];


                    byte[] Ub_bytes = new byte[4];
                    Ub_bytes[0] = client1.receive_byte[37];
                    Ub_bytes[1] = client1.receive_byte[36];
                    Ub_bytes[2] = client1.receive_byte[35];
                    Ub_bytes[3] = client1.receive_byte[34];

                    byte[] Uc_bytes = new byte[4];
                    Uc_bytes[0] = client1.receive_byte[41];
                    Uc_bytes[1] = client1.receive_byte[40];
                    Uc_bytes[2] = client1.receive_byte[39];
                    Uc_bytes[3] = client1.receive_byte[38];


                    float Ua = BitConverter.ToSingle(Ua_bytes, 0) / 10;
                    float Ub = BitConverter.ToSingle(Ub_bytes, 0) / 10;
                    float Uc = BitConverter.ToSingle(Uc_bytes, 0) / 10;

                    reslut_value = Ua.ToString() + " " + Ub.ToString() + " " + Uc.ToString();

                    show_text = "电压a：" + Ua.ToString()+" 电压b："+Ub.ToString()+" 电压c:"+Uc.ToString();
                }

                #endregion


                // 三相电流采集
                #region
                if (connect_value_name == "正泰电表电流")
                {
                    byte[] Ia_bytes = new byte[4];
                    Ia_bytes[0] = client1.receive_byte[33];
                    Ia_bytes[1] = client1.receive_byte[32];
                    Ia_bytes[2] = client1.receive_byte[31];
                    Ia_bytes[3] = client1.receive_byte[30];


                    byte[] Ib_bytes = new byte[4];
                    Ib_bytes[0] = client1.receive_byte[37];
                    Ib_bytes[1] = client1.receive_byte[36];
                    Ib_bytes[2] = client1.receive_byte[35];
                    Ib_bytes[3] = client1.receive_byte[34];

                    byte[] Ic_bytes = new byte[4];
                    Ic_bytes[0] = client1.receive_byte[41];
                    Ic_bytes[1] = client1.receive_byte[40];
                    Ic_bytes[2] = client1.receive_byte[39];
                    Ic_bytes[3] = client1.receive_byte[38];


                    float Ia = BitConverter.ToSingle(Ia_bytes, 0)*50f*0.001f ;
                    float Ib = BitConverter.ToSingle(Ib_bytes, 0) *50f*0.001f;
                    float Ic = BitConverter.ToSingle(Ic_bytes, 0) *50f*0.001f;

                    reslut_value = Ia.ToString() + " " + Ib.ToString() + " " + Ic.ToString();

                    show_text = "电流a：" + Ia.ToString() + " 电流b：" + Ib.ToString() + " 电流c:" + Ic.ToString();
                }

                #endregion


                // 合相有功功率
                #region
                if (connect_value_name == "合相有功功率")
                {
                    byte[] power_bytes = new byte[4];
                    power_bytes[0] = client1.receive_byte[33];
                    power_bytes[1] = client1.receive_byte[32];
                    power_bytes[2] = client1.receive_byte[31];
                    power_bytes[3] = client1.receive_byte[30];

                    float power = BitConverter.ToSingle(power_bytes, 0);

                    reslut_value = power.ToString();

                    show_text = "合相有功功率：" + reslut_value;
                }
                #endregion


                // 合相功率因数
                #region
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

                #endregion


                

                if (connect_value_name == "正向有功总电能")
                {
                    byte[] Total_positive_active_energy_bytes = new byte[4];
                    Total_positive_active_energy_bytes[0] = client1.receive_byte[33];
                    Total_positive_active_energy_bytes[1] = client1.receive_byte[32];
                    Total_positive_active_energy_bytes[2] = client1.receive_byte[31];
                    Total_positive_active_energy_bytes[3] = client1.receive_byte[30];

                    float Total_positive_active_energy = BitConverter.ToSingle(Total_positive_active_energy_bytes, 0) * 50f;
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

                if (connect_value_name == "正泰三相电力温度")
                {
                    float temp1 = client1.receive_byte[30] * 256 + client1.receive_byte[31];
                    float temp2 = client1.receive_byte[32] * 256 + client1.receive_byte[33];
                    float temp3 = client1.receive_byte[34] * 256 + client1.receive_byte[35];

                    reslut_value = (temp1 / 10).ToString() + " " + (temp2 / 10).ToString() + " " + (temp3 / 10).ToString();

                    show_text = "正泰三相电力温度：" + reslut_value;
                }

                if (connect_value_name == "8位开关量")
                {

                    byte temp = client1.receive_byte[30];


                    // 第一位
                    //int bit1 = temp % 2;
                    //int bit2 = (temp - bit1) % 4;

                    ArrayList bit_list = new ArrayList();
                    ArrayList bit_result=new ArrayList();

                    for (int i = 1; i <= 8;i++)
                    {
                        int bit = (int)(temp % Math.Pow(2, i));
                        temp = (byte)(temp - (int)(temp % Math.Pow(2, i)));

                        bit_list.Add(bit);
                    }


                    for (int i = 0; i <= 7;i++)
                    {
                        if((int)bit_list[i]!=0)
                        {
                            bit_result.Add(1);
                        }
                        else
                        {
                            bit_result.Add(0);
                        }
                    }


                        //reslut_value = bit1.ToString() + " " + bit2.ToString() + " " + bit3.ToString() + " " + bit4.ToString() + " " + bit5.ToString() + " " + bit6.ToString() + " " + bit7.ToString() + " " + bit8.ToString();
                    for (int i = 0; i <= 7; i++)
                    {
                        if (i != 7)
                        {
                            reslut_value = reslut_value + bit_result[i].ToString() + " ";
                        }
                        if(i==7)
                        {
                            reslut_value = reslut_value + bit_result[i].ToString();
                        }
                    }

                        show_text = "开关量为：" + reslut_value;
                }

                // 4路模拟量采集
                if (connect_value_name == "4路模拟量采集")
                {
                    int a = 0;
                    int value1 = client1.receive_byte[36] * 256 + client1.receive_byte[37];
                    int value2 = client1.receive_byte[34] * 256 + client1.receive_byte[35];
                    int value3 = client1.receive_byte[32] * 256 + client1.receive_byte[33];
                    int value4 = client1.receive_byte[30] * 256 + client1.receive_byte[31];

                    reslut_value = value1.ToString() + " " + value2.ToString() + " " + value3.ToString() + " " + value4.ToString();
                    show_text = "模拟量为：" + reslut_value;
                }

                
                // 写入数据库并更新datagridview

                // 写入数据库
                try
                {
                    Mysql.Ex_Sql("update shebeitable set value" + (connect_value_num + 1).ToString() + "=\"" + reslut_value + "\" where shebeiID=\"" + connect_device_id + "\"");
                    

                    // 在value10里写入消息来到的时间

                    Mysql.Ex_Sql("update shebeitable set value10=\"" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\" where shebeiID=\"" + connect_device_id + "\"");

                    // 写入历史库
                    Mysql.Ex_Sql("insert into history_save values(\"" + connect_device_id + "\",(select canshutypeid from canshutable where canshutype=\"" + connect_value_name + "\"),\"" + reslut_value + "\",\"" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\")");
                }
                catch { }


                // 写入datagridview
               try
               {
                   //for(int i=0;i<dataGridView1.RowCount;i++)
                   //{
                   //    string id = dataGridView1[0, i].Value.ToString();

                   //    if(id==connect_device_id)
                   //    {
                   //        dataGridView1[connect_value_num + 4, i].Value = reslut_value;
                   //        break;
                   //    }
                   //}
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

        private void timer_project_online_Tick(object sender, EventArgs e)
        {
            if (comboBox_project.Text == "") return;


            if (isscaning == false) return;
            string project_name=comboBox_project.Text;

            Mysql.Ex_Sql("insert into project_online values(( select xiangmuID from xiangmuguanlitable where xiangmuname=\"" + project_name + "\"),\"" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\")");


            Mysql.Ex_Sql("update project_online set onlinetime=\"" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\" where ProjectID= ( select xiangmuID from xiangmuguanlitable where xiangmuname=\"" + project_name + "\")");
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            // 更新能源消耗情况  正泰电表能源消耗 value5
           
            string now_day=DateTime.Now.ToString("yyyy-MM-dd");
            string now_day_start=now_day+" 00:00:00";
            string now_day_end=now_day+" 23:59:59";

            try
            {

                if (isscaning == true)
                {
                    ArrayList all_elect_devices_id = Mysql.Get_Sql_Select_Return("select shebeiID from shebeitable where shebeizhongleiID=(select shebeizhongleiID from shebeizhongleitable where shebeizhongleiname=\"正泰电表\")");
                    
                     foreach(ArrayList device_id_object in all_elect_devices_id)
                     {
                         string device_id = device_id_object[0].ToString();
                         Mysql.Ex_Sql("insert into elect_device_energy_cost values(\"" + device_id + now_day + "\",\"" + device_id + "\",\"" + now_day + "\",(select max(value)-min(value) from history_save where value_id=(select canshutypeid from canshutable where canshutype=\"正向有功总电能\") and device_id=\"" + device_id + "\" and savetime>=\"" + now_day_start + "\" and savetime<=\"" + now_day_end + "\" ))");

                         Mysql.Ex_Sql("update  elect_device_energy_cost set cost=(select max(value)-min(value) from history_save where value_id=(select canshutypeid from canshutable where canshutype=\"正向有功总电能\") and device_id=\"" + device_id + "\" and savetime>=\"" + now_day_start + "\" and savetime<=\"" + now_day_end + "\" ) where device_id=\"" + device_id + "\" and datetime=\"" + now_day + "\"");

                     }
                }

                

            }
            catch { }
        }




       
    }
}

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

namespace saigecollection
{
    public partial class Form1 : Form
    {
        public static IniFile inifile = new IniFile("C://databaseconfig.ini");
        public static bool connnection_is = false;

        public Form1()
        {
            InitializeComponent();
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
        }


        private void Reflush_Device_Table()
        {
            dataGridView1.RowCount = 1;
            dataGridView1.ColumnCount = 12;

            for (int i = 0; i < dataGridView1.ColumnCount;i++)
            {
                dataGridView1[i, 0].Value = "";
            }
            try
            {
                string project_name = comboBox_project.Items[comboBox_project.SelectedIndex].ToString();

                ArrayList device_list = Mysql.Get_Sql_Select_Return("select shebeiname,(select shebeizhongleiname from shebeizhongleitable where shebeizhongleitable.shebeizhongleiID=shebeizhongleiID),shebeidizhi,value1,value2,value3,value4,value5,value6,value7,value8,value9 from shebeitable where shebeixiangmuID=(select distinct xiangmuID from xiangmuguanlitable where xiangmuname=\"" + project_name + "\")");


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

       
    }
}

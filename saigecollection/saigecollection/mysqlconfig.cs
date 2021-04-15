using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace saigecollection
{
    public partial class mysqlconfig : Form
    {

        public static string database_ip;
        public static string database_name;
        public static string username;
        public static string password;
        public mysqlconfig()
        {
            InitializeComponent();
            Fill_In_Info();
        }

        public void Fill_In_Info()
        {
            textBox_database_ip.Text = Form1.inifile.IniReadValue("databaseset", "database_ip");
            textBox_database_name.Text = Form1.inifile.IniReadValue("databaseset", "database_name");
            textBox_username.Text = Form1.inifile.IniReadValue("databaseset", "username");
            textBox_password.Text = Form1.inifile.IniReadValue("databaseset", "password");

        }


        private void button1_Click(object sender, EventArgs e)
        {
            Form1.inifile.IniWriteValue("databaseset", "database_ip", textBox_database_ip.Text);
            Form1.inifile.IniWriteValue("databaseset", "database_name", textBox_database_name.Text);
            Form1.inifile.IniWriteValue("databaseset", "username", textBox_username.Text);
            Form1.inifile.IniWriteValue("databaseset", "password", textBox_password.Text);

            this.DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}

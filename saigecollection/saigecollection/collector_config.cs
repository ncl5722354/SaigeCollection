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
    public partial class collector_config : Form
    {
        public static string remoteip;

        public static string port;


        public collector_config()
        {
            InitializeComponent();
            textBox_collector_ip.Text = Form1.inifile.IniReadValue("collector", "ip");
            textBox_port.Text = Form1.inifile.IniReadValue("collector", "port");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1.inifile.IniWriteValue("collector", "ip", textBox_collector_ip.Text);
            Form1.inifile.IniWriteValue("collector", "port", textBox_port.Text);
        }

        

    }
}

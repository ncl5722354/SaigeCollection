namespace saigecollection
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label_database_status = new System.Windows.Forms.Label();
            this.button_reconnect = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox_project = new System.Windows.Forms.ComboBox();
            this.timer_status = new System.Windows.Forms.Timer(this.components);
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button_begin_collect = new System.Windows.Forms.Button();
            this.button_collect_config = new System.Windows.Forms.Button();
            this.button_stop_collect = new System.Windows.Forms.Button();
            this.button_reflush = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label_device_connect = new System.Windows.Forms.Label();
            this.button_connect = new System.Windows.Forms.Button();
            this.timer_connect = new System.Windows.Forms.Timer(this.components);
            this.label_connect_info = new System.Windows.Forms.Label();
            this.timer_info_update = new System.Windows.Forms.Timer(this.components);
            this.timer_project_online = new System.Windows.Forms.Timer(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "赛格物业机电管控采集";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "连接数据库状态";
            // 
            // label_database_status
            // 
            this.label_database_status.AutoSize = true;
            this.label_database_status.Location = new System.Drawing.Point(147, 36);
            this.label_database_status.Name = "label_database_status";
            this.label_database_status.Size = new System.Drawing.Size(53, 12);
            this.label_database_status.TabIndex = 1;
            this.label_database_status.Text = "未连接上";
            // 
            // button_reconnect
            // 
            this.button_reconnect.Location = new System.Drawing.Point(330, 25);
            this.button_reconnect.Name = "button_reconnect";
            this.button_reconnect.Size = new System.Drawing.Size(75, 23);
            this.button_reconnect.TabIndex = 2;
            this.button_reconnect.Text = "连接";
            this.button_reconnect.UseVisualStyleBackColor = true;
            this.button_reconnect.Click += new System.EventHandler(this.button_reconnect_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(249, 25);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "设置";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(41, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "项目选择";
            // 
            // comboBox_project
            // 
            this.comboBox_project.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_project.FormattingEnabled = true;
            this.comboBox_project.Location = new System.Drawing.Point(116, 85);
            this.comboBox_project.Name = "comboBox_project";
            this.comboBox_project.Size = new System.Drawing.Size(146, 20);
            this.comboBox_project.TabIndex = 5;
            this.comboBox_project.TextChanged += new System.EventHandler(this.comboBox_project_TextChanged);
            // 
            // timer_status
            // 
            this.timer_status.Enabled = true;
            this.timer_status.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column13,
            this.Column1,
            this.Column2,
            this.Column12,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10,
            this.Column11});
            this.dataGridView1.Location = new System.Drawing.Point(12, 149);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(1145, 376);
            this.dataGridView1.TabIndex = 6;
            // 
            // Column13
            // 
            this.Column13.HeaderText = "设备ID";
            this.Column13.Name = "Column13";
            // 
            // Column1
            // 
            this.Column1.HeaderText = "设备名称";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "设备种类";
            this.Column2.Name = "Column2";
            // 
            // Column12
            // 
            this.Column12.HeaderText = "地址";
            this.Column12.Name = "Column12";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "值1";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "值2";
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.HeaderText = "值3";
            this.Column5.Name = "Column5";
            // 
            // Column6
            // 
            this.Column6.HeaderText = "值4";
            this.Column6.Name = "Column6";
            // 
            // Column7
            // 
            this.Column7.HeaderText = "值5";
            this.Column7.Name = "Column7";
            // 
            // Column8
            // 
            this.Column8.HeaderText = "值6";
            this.Column8.Name = "Column8";
            // 
            // Column9
            // 
            this.Column9.HeaderText = "值7";
            this.Column9.Name = "Column9";
            // 
            // Column10
            // 
            this.Column10.HeaderText = "值8";
            this.Column10.Name = "Column10";
            // 
            // Column11
            // 
            this.Column11.HeaderText = "值9";
            this.Column11.Name = "Column11";
            // 
            // button_begin_collect
            // 
            this.button_begin_collect.Location = new System.Drawing.Point(941, 53);
            this.button_begin_collect.Name = "button_begin_collect";
            this.button_begin_collect.Size = new System.Drawing.Size(75, 23);
            this.button_begin_collect.TabIndex = 7;
            this.button_begin_collect.Text = "开始采集";
            this.button_begin_collect.UseVisualStyleBackColor = true;
            this.button_begin_collect.Click += new System.EventHandler(this.button_begin_collect_Click);
            // 
            // button_collect_config
            // 
            this.button_collect_config.Location = new System.Drawing.Point(677, 25);
            this.button_collect_config.Name = "button_collect_config";
            this.button_collect_config.Size = new System.Drawing.Size(92, 23);
            this.button_collect_config.TabIndex = 8;
            this.button_collect_config.Text = "采集设备设定";
            this.button_collect_config.UseVisualStyleBackColor = true;
            this.button_collect_config.Click += new System.EventHandler(this.button_collect_config_Click);
            // 
            // button_stop_collect
            // 
            this.button_stop_collect.Location = new System.Drawing.Point(941, 99);
            this.button_stop_collect.Name = "button_stop_collect";
            this.button_stop_collect.Size = new System.Drawing.Size(75, 23);
            this.button_stop_collect.TabIndex = 9;
            this.button_stop_collect.Text = "停止采集";
            this.button_stop_collect.UseVisualStyleBackColor = true;
            this.button_stop_collect.Click += new System.EventHandler(this.button_stop_collect_Click);
            // 
            // button_reflush
            // 
            this.button_reflush.Location = new System.Drawing.Point(1031, 99);
            this.button_reflush.Name = "button_reflush";
            this.button_reflush.Size = new System.Drawing.Size(75, 23);
            this.button_reflush.TabIndex = 10;
            this.button_reflush.Text = "刷新";
            this.button_reflush.UseVisualStyleBackColor = true;
            this.button_reflush.Click += new System.EventHandler(this.button_reflush_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(508, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "连接采集设备";
            // 
            // label_device_connect
            // 
            this.label_device_connect.AutoSize = true;
            this.label_device_connect.Location = new System.Drawing.Point(606, 36);
            this.label_device_connect.Name = "label_device_connect";
            this.label_device_connect.Size = new System.Drawing.Size(53, 12);
            this.label_device_connect.TabIndex = 12;
            this.label_device_connect.Text = "未连接上";
            // 
            // button_connect
            // 
            this.button_connect.Location = new System.Drawing.Point(775, 25);
            this.button_connect.Name = "button_connect";
            this.button_connect.Size = new System.Drawing.Size(75, 23);
            this.button_connect.TabIndex = 13;
            this.button_connect.Text = "连接";
            this.button_connect.UseVisualStyleBackColor = true;
            this.button_connect.Click += new System.EventHandler(this.button_connect_Click);
            // 
            // timer_connect
            // 
            this.timer_connect.Enabled = true;
            this.timer_connect.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // label_connect_info
            // 
            this.label_connect_info.AutoSize = true;
            this.label_connect_info.Location = new System.Drawing.Point(12, 599);
            this.label_connect_info.Name = "label_connect_info";
            this.label_connect_info.Size = new System.Drawing.Size(41, 12);
            this.label_connect_info.TabIndex = 14;
            this.label_connect_info.Text = "label4";
            // 
            // timer_info_update
            // 
            this.timer_info_update.Enabled = true;
            this.timer_info_update.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // timer_project_online
            // 
            this.timer_project_online.Enabled = true;
            this.timer_project_online.Interval = 20000;
            this.timer_project_online.Tick += new System.EventHandler(this.timer_project_online_Tick);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 60000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1168, 678);
            this.Controls.Add(this.label_connect_info);
            this.Controls.Add(this.button_connect);
            this.Controls.Add(this.label_device_connect);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button_reflush);
            this.Controls.Add(this.button_stop_collect);
            this.Controls.Add(this.button_collect_config);
            this.Controls.Add(this.button_begin_collect);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.comboBox_project);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button_reconnect);
            this.Controls.Add(this.label_database_status);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "赛格物业机电管控采集器";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_database_status;
        private System.Windows.Forms.Button button_reconnect;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox_project;
        private System.Windows.Forms.Timer timer_status;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button_begin_collect;
        private System.Windows.Forms.Button button_collect_config;
        private System.Windows.Forms.Button button_stop_collect;
        private System.Windows.Forms.Button button_reflush;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label_device_connect;
        private System.Windows.Forms.Button button_connect;
        private System.Windows.Forms.Timer timer_connect;
        private System.Windows.Forms.Label label_connect_info;
        private System.Windows.Forms.Timer timer_info_update;
        private System.Windows.Forms.Timer timer_project_online;
        private System.Windows.Forms.Timer timer1;
    }
}


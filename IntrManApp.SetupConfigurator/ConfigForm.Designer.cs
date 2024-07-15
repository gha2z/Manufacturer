namespace IntrManApp.SetupConfigurator
{
    partial class ConfigForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigForm));
            ServerCk = new CheckBox();
            ClientCk = new CheckBox();
            label1 = new Label();
            ServerPanel = new Panel();
            label2 = new Label();
            panel1 = new Panel();
            ServerPortNum = new NumericUpDown();
            label6 = new Label();
            pictureBox1 = new PictureBox();
            PasswordTxt = new TextBox();
            label5 = new Label();
            UserIdTxt = new TextBox();
            label4 = new Label();
            SqlServerTxt = new TextBox();
            label3 = new Label();
            UseIntSecCk = new CheckBox();
            ClientPanel = new Panel();
            pictureBox2 = new PictureBox();
            ClientServerPortNum = new NumericUpDown();
            label7 = new Label();
            ServerTxt = new TextBox();
            label8 = new Label();
            TestServerBtn = new Button();
            ContinueBtn = new Button();
            LogLabel = new Label();
            ContentPanel = new Panel();
            FooterPanel = new Panel();
            pictureBox3 = new PictureBox();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            ServerPanel.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ServerPortNum).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ClientPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ClientServerPortNum).BeginInit();
            ContentPanel.SuspendLayout();
            FooterPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            SuspendLayout();
            // 
            // ServerCk
            // 
            ServerCk.AutoSize = true;
            ServerCk.Location = new Point(25, 43);
            ServerCk.Margin = new Padding(4);
            ServerCk.Name = "ServerCk";
            ServerCk.Size = new Size(79, 27);
            ServerCk.TabIndex = 1;
            ServerCk.Text = "Server";
            ServerCk.UseVisualStyleBackColor = true;
            ServerCk.CheckedChanged += ServerCk_CheckedChanged;
            // 
            // ClientCk
            // 
            ClientCk.AutoSize = true;
            ClientCk.Checked = true;
            ClientCk.CheckState = CheckState.Checked;
            ClientCk.Enabled = false;
            ClientCk.Location = new Point(25, 348);
            ClientCk.Margin = new Padding(4);
            ClientCk.Name = "ClientCk";
            ClientCk.Size = new Size(76, 27);
            ClientCk.TabIndex = 3;
            ClientCk.Text = "Client";
            ClientCk.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(25, 13);
            label1.Name = "label1";
            label1.Size = new Size(194, 23);
            label1.TabIndex = 3;
            label1.Text = "Installation components";
            // 
            // ServerPanel
            // 
            ServerPanel.BorderStyle = BorderStyle.FixedSingle;
            ServerPanel.Controls.Add(label2);
            ServerPanel.Controls.Add(panel1);
            ServerPanel.Controls.Add(pictureBox1);
            ServerPanel.Controls.Add(PasswordTxt);
            ServerPanel.Controls.Add(label5);
            ServerPanel.Controls.Add(UserIdTxt);
            ServerPanel.Controls.Add(label4);
            ServerPanel.Controls.Add(SqlServerTxt);
            ServerPanel.Controls.Add(label3);
            ServerPanel.Controls.Add(UseIntSecCk);
            ServerPanel.Location = new Point(25, 77);
            ServerPanel.Name = "ServerPanel";
            ServerPanel.Size = new Size(575, 249);
            ServerPanel.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(349, 79);
            label2.Name = "label2";
            label2.Size = new Size(211, 34);
            label2.TabIndex = 8;
            label2.Text = "e.g.: SqlExpress, Bartender, etc\r\nLeave blank to use default instance";
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(ServerPortNum);
            panel1.Controls.Add(label6);
            panel1.Location = new Point(-1, -3);
            panel1.Name = "panel1";
            panel1.Size = new Size(575, 69);
            panel1.TabIndex = 6;
            // 
            // ServerPortNum
            // 
            ServerPortNum.Location = new Point(186, 20);
            ServerPortNum.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
            ServerPortNum.Minimum = new decimal(new int[] { 50001, 0, 0, 0 });
            ServerPortNum.Name = "ServerPortNum";
            ServerPortNum.Size = new Size(141, 30);
            ServerPortNum.TabIndex = 1;
            ServerPortNum.Value = new decimal(new int[] { 50001, 0, 0, 0 });
            ServerPortNum.ValueChanged += ServerPortNum_ValueChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(87, 22);
            label6.Name = "label6";
            label6.Size = new Size(93, 23);
            label6.TabIndex = 0;
            label6.Text = "Server Port";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.button_cancel;
            pictureBox1.Location = new Point(38, 176);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(37, 32);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 7;
            pictureBox1.TabStop = false;
            pictureBox1.Visible = false;
            // 
            // PasswordTxt
            // 
            PasswordTxt.BorderStyle = BorderStyle.FixedSingle;
            PasswordTxt.Location = new Point(186, 199);
            PasswordTxt.Name = "PasswordTxt";
            PasswordTxt.PasswordChar = '*';
            PasswordTxt.Size = new Size(354, 30);
            PasswordTxt.TabIndex = 6;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(100, 201);
            label5.Name = "label5";
            label5.Size = new Size(80, 23);
            label5.TabIndex = 5;
            label5.Text = "Password";
            // 
            // UserIdTxt
            // 
            UserIdTxt.BorderStyle = BorderStyle.FixedSingle;
            UserIdTxt.Location = new Point(186, 163);
            UserIdTxt.Name = "UserIdTxt";
            UserIdTxt.Size = new Size(354, 30);
            UserIdTxt.TabIndex = 4;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(114, 165);
            label4.Name = "label4";
            label4.Size = new Size(64, 23);
            label4.TabIndex = 3;
            label4.Text = "User Id";
            // 
            // SqlServerTxt
            // 
            SqlServerTxt.BorderStyle = BorderStyle.FixedSingle;
            SqlServerTxt.Location = new Point(186, 83);
            SqlServerTxt.Name = "SqlServerTxt";
            SqlServerTxt.PlaceholderText = "default";
            SqlServerTxt.Size = new Size(157, 30);
            SqlServerTxt.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(19, 85);
            label3.Name = "label3";
            label3.Size = new Size(161, 23);
            label3.TabIndex = 0;
            label3.Text = "SQL Server Instance";
            // 
            // UseIntSecCk
            // 
            UseIntSecCk.AutoSize = true;
            UseIntSecCk.Checked = true;
            UseIntSecCk.CheckState = CheckState.Checked;
            UseIntSecCk.Location = new Point(186, 129);
            UseIntSecCk.Margin = new Padding(4);
            UseIntSecCk.Name = "UseIntSecCk";
            UseIntSecCk.Size = new Size(210, 27);
            UseIntSecCk.TabIndex = 2;
            UseIntSecCk.Text = "Use Integrated Security";
            UseIntSecCk.UseVisualStyleBackColor = true;
            UseIntSecCk.CheckedChanged += UseIntSecTxt_CheckedChanged;
            // 
            // ClientPanel
            // 
            ClientPanel.BorderStyle = BorderStyle.FixedSingle;
            ClientPanel.Controls.Add(pictureBox2);
            ClientPanel.Controls.Add(ClientServerPortNum);
            ClientPanel.Controls.Add(label7);
            ClientPanel.Controls.Add(ServerTxt);
            ClientPanel.Controls.Add(label8);
            ClientPanel.Location = new Point(25, 382);
            ClientPanel.Name = "ClientPanel";
            ClientPanel.Size = new Size(575, 112);
            ClientPanel.TabIndex = 4;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.button_cancel;
            pictureBox2.Location = new Point(41, 60);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(37, 32);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 8;
            pictureBox2.TabStop = false;
            pictureBox2.Visible = false;
            // 
            // ClientServerPortNum
            // 
            ClientServerPortNum.Location = new Point(186, 62);
            ClientServerPortNum.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
            ClientServerPortNum.Minimum = new decimal(new int[] { 50001, 0, 0, 0 });
            ClientServerPortNum.Name = "ClientServerPortNum";
            ClientServerPortNum.Size = new Size(141, 30);
            ClientServerPortNum.TabIndex = 1;
            ClientServerPortNum.Value = new decimal(new int[] { 50001, 0, 0, 0 });
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(137, 64);
            label7.Name = "label7";
            label7.Size = new Size(41, 23);
            label7.TabIndex = 2;
            label7.Text = "Port";
            // 
            // ServerTxt
            // 
            ServerTxt.BorderStyle = BorderStyle.FixedSingle;
            ServerTxt.Location = new Point(186, 17);
            ServerTxt.Name = "ServerTxt";
            ServerTxt.Size = new Size(354, 30);
            ServerTxt.TabIndex = 1;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(38, 19);
            label8.Name = "label8";
            label8.Size = new Size(142, 23);
            label8.TabIndex = 0;
            label8.Text = "Server IP Address";
            // 
            // TestServerBtn
            // 
            TestServerBtn.Location = new Point(24, 89);
            TestServerBtn.Margin = new Padding(4);
            TestServerBtn.Name = "TestServerBtn";
            TestServerBtn.Size = new Size(215, 44);
            TestServerBtn.TabIndex = 4;
            TestServerBtn.Text = "Verify Configuration";
            TestServerBtn.UseVisualStyleBackColor = true;
            TestServerBtn.Click += TestServerBtn_Click;
            // 
            // ContinueBtn
            // 
            ContinueBtn.Location = new Point(440, 89);
            ContinueBtn.Margin = new Padding(4);
            ContinueBtn.Name = "ContinueBtn";
            ContinueBtn.Size = new Size(160, 44);
            ContinueBtn.TabIndex = 5;
            ContinueBtn.Text = "Continue";
            ContinueBtn.UseVisualStyleBackColor = true;
            ContinueBtn.Click += ContinueBtn_Click;
            // 
            // LogLabel
            // 
            LogLabel.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            LogLabel.Location = new Point(24, 41);
            LogLabel.Name = "LogLabel";
            LogLabel.Size = new Size(576, 29);
            LogLabel.TabIndex = 0;
            LogLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ContentPanel
            // 
            ContentPanel.Controls.Add(ServerCk);
            ContentPanel.Controls.Add(ClientPanel);
            ContentPanel.Controls.Add(ClientCk);
            ContentPanel.Controls.Add(ServerPanel);
            ContentPanel.Controls.Add(label1);
            ContentPanel.Dock = DockStyle.Fill;
            ContentPanel.Location = new Point(0, 0);
            ContentPanel.Name = "ContentPanel";
            ContentPanel.Size = new Size(625, 655);
            ContentPanel.TabIndex = 7;
            // 
            // FooterPanel
            // 
            FooterPanel.Controls.Add(LogLabel);
            FooterPanel.Controls.Add(pictureBox3);
            FooterPanel.Controls.Add(ContinueBtn);
            FooterPanel.Controls.Add(TestServerBtn);
            FooterPanel.Dock = DockStyle.Bottom;
            FooterPanel.Location = new Point(0, 500);
            FooterPanel.Name = "FooterPanel";
            FooterPanel.Size = new Size(625, 155);
            FooterPanel.TabIndex = 8;
            // 
            // pictureBox3
            // 
            pictureBox3.BackColor = Color.White;
            pictureBox3.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBox3.Image = Properties.Resources.loading3;
            pictureBox3.Location = new Point(24, -27);
            pictureBox3.Margin = new Padding(3, 4, 3, 4);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(576, 106);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 6;
            pictureBox3.TabStop = false;
            pictureBox3.Visible = false;
            // 
            // backgroundWorker1
            // 
            backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
            // 
            // ConfigForm
            // 
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(625, 655);
            ControlBox = false;
            Controls.Add(FooterPanel);
            Controls.Add(ContentPanel);
            Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.DarkSlateGray;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4);
            Name = "ConfigForm";
            Text = "Configuring Services";
            TopMost = true;
            Load += ConfigForm_Load;
            ServerPanel.ResumeLayout(false);
            ServerPanel.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ServerPortNum).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ClientPanel.ResumeLayout(false);
            ClientPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)ClientServerPortNum).EndInit();
            ContentPanel.ResumeLayout(false);
            ContentPanel.PerformLayout();
            FooterPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private CheckBox ServerCk;
        private CheckBox ClientCk;
        private Label label1;
        private Panel ServerPanel;
        private TextBox PasswordTxt;
        private Label label5;
        private TextBox UserIdTxt;
        private Label label4;
        private TextBox SqlServerTxt;
        private Label label3;
        private CheckBox UseIntSecCk;
        private Panel ClientPanel;
        private Label label7;
        private TextBox ServerTxt;
        private Button TestServerBtn;
        private Label label8;
        private Button ContinueBtn;
        private Label label6;
        private Panel panel1;
        private NumericUpDown ServerPortNum;
        private NumericUpDown ClientServerPortNum;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private Label LogLabel;
        private Panel ContentPanel;
        private Panel FooterPanel;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private PictureBox pictureBox3;
        private Label label2;
    }
}

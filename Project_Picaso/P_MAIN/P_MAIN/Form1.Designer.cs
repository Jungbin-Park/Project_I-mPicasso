namespace P_MAIN
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel_Drawing = new System.Windows.Forms.Panel();
            this.listBox_ChatView = new System.Windows.Forms.ListBox();
            this.textBox_Chat = new System.Windows.Forms.TextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_Black = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_Red = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_Blue = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_Green = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_Yellow = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_P = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_N = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_E = new System.Windows.Forms.ToolStripButton();
            this.button_Link = new System.Windows.Forms.Button();
            this.button_exit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.button_RE = new System.Windows.Forms.Button();
            this.textBox_ID = new System.Windows.Forms.TextBox();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_Drawing
            // 
            this.panel_Drawing.BackColor = System.Drawing.Color.White;
            this.panel_Drawing.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel_Drawing.Location = new System.Drawing.Point(272, 86);
            this.panel_Drawing.Name = "panel_Drawing";
            this.panel_Drawing.Size = new System.Drawing.Size(900, 433);
            this.panel_Drawing.TabIndex = 0;
            this.panel_Drawing.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_Drawing_MouseDown);
            this.panel_Drawing.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel_Drawing_MouseMove);
            this.panel_Drawing.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel_Drawing_MouseUp);
            // 
            // listBox_ChatView
            // 
            this.listBox_ChatView.BackColor = System.Drawing.Color.White;
            this.listBox_ChatView.Font = new System.Drawing.Font("굴림", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.listBox_ChatView.ForeColor = System.Drawing.Color.Black;
            this.listBox_ChatView.FormattingEnabled = true;
            this.listBox_ChatView.ItemHeight = 20;
            this.listBox_ChatView.Location = new System.Drawing.Point(272, 525);
            this.listBox_ChatView.Name = "listBox_ChatView";
            this.listBox_ChatView.Size = new System.Drawing.Size(900, 104);
            this.listBox_ChatView.TabIndex = 1;
            // 
            // textBox_Chat
            // 
            this.textBox_Chat.BackColor = System.Drawing.Color.White;
            this.textBox_Chat.Font = new System.Drawing.Font("굴림", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox_Chat.Location = new System.Drawing.Point(410, 630);
            this.textBox_Chat.Name = "textBox_Chat";
            this.textBox_Chat.Size = new System.Drawing.Size(762, 30);
            this.textBox_Chat.TabIndex = 2;
            this.textBox_Chat.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_Chat_KeyDown);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.White;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_Black,
            this.toolStripButton_Red,
            this.toolStripButton_Blue,
            this.toolStripButton_Green,
            this.toolStripButton_Yellow,
            this.toolStripSeparator1,
            this.toolStripButton_P,
            this.toolStripButton_N,
            this.toolStripSeparator2,
            this.toolStripButton_E});
            this.toolStrip1.Location = new System.Drawing.Point(962, 55);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(239, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton_Black
            // 
            this.toolStripButton_Black.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripButton_Black.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_Black.Image = global::P_MAIN.Properties.Resources.black;
            this.toolStripButton_Black.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Black.Name = "toolStripButton_Black";
            this.toolStripButton_Black.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_Black.Text = "toolStripButton1";
            this.toolStripButton_Black.Click += new System.EventHandler(this.toolStripButton_Black_Click);
            // 
            // toolStripButton_Red
            // 
            this.toolStripButton_Red.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_Red.Image = global::P_MAIN.Properties.Resources.Red;
            this.toolStripButton_Red.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Red.Name = "toolStripButton_Red";
            this.toolStripButton_Red.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_Red.Text = "toolStripButton2";
            this.toolStripButton_Red.Click += new System.EventHandler(this.toolStripButton_Red_Click);
            // 
            // toolStripButton_Blue
            // 
            this.toolStripButton_Blue.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_Blue.Image = global::P_MAIN.Properties.Resources.blue;
            this.toolStripButton_Blue.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Blue.Name = "toolStripButton_Blue";
            this.toolStripButton_Blue.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_Blue.Text = "toolStripButton3";
            this.toolStripButton_Blue.Click += new System.EventHandler(this.toolStripButton_Blue_Click);
            // 
            // toolStripButton_Green
            // 
            this.toolStripButton_Green.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_Green.Image = global::P_MAIN.Properties.Resources.green1;
            this.toolStripButton_Green.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Green.Name = "toolStripButton_Green";
            this.toolStripButton_Green.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_Green.Text = "toolStripButton4";
            this.toolStripButton_Green.Click += new System.EventHandler(this.toolStripButton_Green_Click);
            // 
            // toolStripButton_Yellow
            // 
            this.toolStripButton_Yellow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_Yellow.Image = global::P_MAIN.Properties.Resources.yellow;
            this.toolStripButton_Yellow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Yellow.Name = "toolStripButton_Yellow";
            this.toolStripButton_Yellow.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_Yellow.Text = "toolStripButton5";
            this.toolStripButton_Yellow.Click += new System.EventHandler(this.toolStripButton_Yellow_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton_P
            // 
            this.toolStripButton_P.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_P.Image = global::P_MAIN.Properties.Resources.P;
            this.toolStripButton_P.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_P.Name = "toolStripButton_P";
            this.toolStripButton_P.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_P.Text = "toolStripButton6";
            this.toolStripButton_P.Click += new System.EventHandler(this.toolStripButton_P_Click);
            // 
            // toolStripButton_N
            // 
            this.toolStripButton_N.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_N.Image = global::P_MAIN.Properties.Resources.M1;
            this.toolStripButton_N.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_N.Name = "toolStripButton_N";
            this.toolStripButton_N.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_N.Text = "toolStripButton7";
            this.toolStripButton_N.Click += new System.EventHandler(this.toolStripButton_N_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton_E
            // 
            this.toolStripButton_E.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_E.Image = global::P_MAIN.Properties.Resources.E;
            this.toolStripButton_E.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_E.Name = "toolStripButton_E";
            this.toolStripButton_E.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_E.Text = "toolStripButton1";
            this.toolStripButton_E.Click += new System.EventHandler(this.toolStripButton_E_Click);
            // 
            // button_Link
            // 
            this.button_Link.Font = new System.Drawing.Font("굴림", 18F, System.Drawing.FontStyle.Bold);
            this.button_Link.Location = new System.Drawing.Point(45, 86);
            this.button_Link.Name = "button_Link";
            this.button_Link.Size = new System.Drawing.Size(165, 66);
            this.button_Link.TabIndex = 7;
            this.button_Link.Text = "Link Start!";
            this.button_Link.UseVisualStyleBackColor = true;
            this.button_Link.Click += new System.EventHandler(this.button_Link_Click);
            // 
            // button_exit
            // 
            this.button_exit.Font = new System.Drawing.Font("굴림", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_exit.Location = new System.Drawing.Point(45, 525);
            this.button_exit.Name = "button_exit";
            this.button_exit.Size = new System.Drawing.Size(165, 66);
            this.button_exit.TabIndex = 7;
            this.button_exit.Text = "Bye Bye~♪";
            this.button_exit.UseVisualStyleBackColor = true;
            this.button_exit.Click += new System.EventHandler(this.button_exit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(267, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 28);
            this.label1.TabIndex = 9;
            this.label1.Text = "HINT!";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.Location = new System.Drawing.Point(726, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 28);
            this.label3.TabIndex = 9;
            this.label3.Text = "글자수";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("굴림", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox1.Location = new System.Drawing.Point(543, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(255, 35);
            this.textBox1.TabIndex = 10;
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold);
            this.textBox2.Location = new System.Drawing.Point(340, 54);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(380, 26);
            this.textBox2.TabIndex = 10;
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox3
            // 
            this.textBox3.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold);
            this.textBox3.Location = new System.Drawing.Point(804, 53);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(114, 26);
            this.textBox3.TabIndex = 10;
            this.textBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // button_RE
            // 
            this.button_RE.Font = new System.Drawing.Font("굴림", 18F, System.Drawing.FontStyle.Bold);
            this.button_RE.Location = new System.Drawing.Point(45, 158);
            this.button_RE.Name = "button_RE";
            this.button_RE.Size = new System.Drawing.Size(165, 66);
            this.button_RE.TabIndex = 7;
            this.button_RE.Text = "RE?\r\n  ヾ(≧▽≦*)o";
            this.button_RE.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_RE.UseVisualStyleBackColor = true;
            this.button_RE.Click += new System.EventHandler(this.button_RE_Click);
            // 
            // textBox_ID
            // 
            this.textBox_ID.Font = new System.Drawing.Font("굴림", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox_ID.Location = new System.Drawing.Point(272, 630);
            this.textBox_ID.Name = "textBox_ID";
            this.textBox_ID.Size = new System.Drawing.Size(137, 30);
            this.textBox_ID.TabIndex = 11;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::P_MAIN.Properties.Resources.클라이_언트_1_원본;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1184, 661);
            this.Controls.Add(this.textBox_ID);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_exit);
            this.Controls.Add(this.button_RE);
            this.Controls.Add(this.button_Link);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.textBox_Chat);
            this.Controls.Add(this.listBox_ChatView);
            this.Controls.Add(this.panel_Drawing);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListBox listBox_ChatView;
        private System.Windows.Forms.TextBox textBox_Chat;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton_Black;
        private System.Windows.Forms.ToolStripButton toolStripButton_Red;
        private System.Windows.Forms.ToolStripButton toolStripButton_Blue;
        private System.Windows.Forms.ToolStripButton toolStripButton_Green;
        private System.Windows.Forms.ToolStripButton toolStripButton_Yellow;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton_P;
        private System.Windows.Forms.ToolStripButton toolStripButton_N;
        private System.Windows.Forms.Button button_Link;
        private System.Windows.Forms.Button button_exit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButton_E;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Panel panel_Drawing;
        private System.Windows.Forms.Button button_RE;
        private System.Windows.Forms.TextBox textBox_ID;
    }
}


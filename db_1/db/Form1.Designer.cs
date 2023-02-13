namespace db
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
            this.listBox_DBlog = new System.Windows.Forms.ListBox();
            this.button_DBConnect = new System.Windows.Forms.Button();
            this.button_DisConnect = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.listBox_Clientlog = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // listBox_DBlog
            // 
            this.listBox_DBlog.FormattingEnabled = true;
            this.listBox_DBlog.ItemHeight = 12;
            this.listBox_DBlog.Location = new System.Drawing.Point(12, 12);
            this.listBox_DBlog.Name = "listBox_DBlog";
            this.listBox_DBlog.Size = new System.Drawing.Size(324, 412);
            this.listBox_DBlog.TabIndex = 1;
            // 
            // button_DBConnect
            // 
            this.button_DBConnect.Location = new System.Drawing.Point(743, 35);
            this.button_DBConnect.Name = "button_DBConnect";
            this.button_DBConnect.Size = new System.Drawing.Size(131, 55);
            this.button_DBConnect.TabIndex = 2;
            this.button_DBConnect.Text = "DBConnect";
            this.button_DBConnect.UseVisualStyleBackColor = true;
            this.button_DBConnect.Click += new System.EventHandler(this.button_DBConnect_Click);
            // 
            // button_DisConnect
            // 
            this.button_DisConnect.Location = new System.Drawing.Point(743, 134);
            this.button_DisConnect.Name = "button_DisConnect";
            this.button_DisConnect.Size = new System.Drawing.Size(131, 55);
            this.button_DisConnect.TabIndex = 3;
            this.button_DisConnect.Text = "DisConnect";
            this.button_DisConnect.UseVisualStyleBackColor = true;
            this.button_DisConnect.Click += new System.EventHandler(this.button_DisConnect_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(743, 231);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(131, 53);
            this.button1.TabIndex = 4;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(743, 340);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(131, 69);
            this.button2.TabIndex = 5;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // listBox_Clientlog
            // 
            this.listBox_Clientlog.FormattingEnabled = true;
            this.listBox_Clientlog.ItemHeight = 12;
            this.listBox_Clientlog.Location = new System.Drawing.Point(366, 12);
            this.listBox_Clientlog.Name = "listBox_Clientlog";
            this.listBox_Clientlog.Size = new System.Drawing.Size(324, 412);
            this.listBox_Clientlog.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 450);
            this.Controls.Add(this.listBox_Clientlog);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button_DisConnect);
            this.Controls.Add(this.button_DBConnect);
            this.Controls.Add(this.listBox_DBlog);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListBox listBox_DBlog;
        private System.Windows.Forms.Button button_DBConnect;
        private System.Windows.Forms.Button button_DisConnect;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ListBox listBox_Clientlog;
    }
}


namespace Project_Picasso_Client_2
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox_ID = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label_word = new System.Windows.Forms.Label();
            this.label_Hint = new System.Windows.Forms.Label();
            this.label_long = new System.Windows.Forms.Label();
            this.label_ID = new System.Windows.Forms.Label();
            this.button_Reset = new System.Windows.Forms.Button();
            this.button_Answer = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(172, 88);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1000, 560);
            this.panel1.TabIndex = 0;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown_1);
            // 
            // textBox_ID
            // 
            this.textBox_ID.Font = new System.Drawing.Font("굴림", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox_ID.Location = new System.Drawing.Point(26, 42);
            this.textBox_ID.Name = "textBox_ID";
            this.textBox_ID.Size = new System.Drawing.Size(83, 27);
            this.textBox_ID.TabIndex = 6;
            this.textBox_ID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("굴림", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox1.Location = new System.Drawing.Point(490, 50);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(263, 32);
            this.textBox1.TabIndex = 7;
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("굴림", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox2.Location = new System.Drawing.Point(390, 11);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(333, 32);
            this.textBox2.TabIndex = 8;
            // 
            // textBox3
            // 
            this.textBox3.Font = new System.Drawing.Font("굴림", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox3.Location = new System.Drawing.Point(812, 11);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(53, 32);
            this.textBox3.TabIndex = 9;
            // 
            // label_word
            // 
            this.label_word.AutoSize = true;
            this.label_word.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_word.Location = new System.Drawing.Point(419, 56);
            this.label_word.Name = "label_word";
            this.label_word.Size = new System.Drawing.Size(52, 15);
            this.label_word.TabIndex = 10;
            this.label_word.Text = "제시어";
            // 
            // label_Hint
            // 
            this.label_Hint.AutoSize = true;
            this.label_Hint.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_Hint.Location = new System.Drawing.Point(336, 20);
            this.label_Hint.Name = "label_Hint";
            this.label_Hint.Size = new System.Drawing.Size(37, 15);
            this.label_Hint.TabIndex = 11;
            this.label_Hint.Text = "힌트";
            // 
            // label_long
            // 
            this.label_long.AutoSize = true;
            this.label_long.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_long.Location = new System.Drawing.Point(745, 19);
            this.label_long.Name = "label_long";
            this.label_long.Size = new System.Drawing.Size(55, 16);
            this.label_long.TabIndex = 12;
            this.label_long.Text = "글자수";
            // 
            // label_ID
            // 
            this.label_ID.AutoSize = true;
            this.label_ID.Font = new System.Drawing.Font("굴림", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_ID.Location = new System.Drawing.Point(56, 19);
            this.label_ID.Name = "label_ID";
            this.label_ID.Size = new System.Drawing.Size(25, 20);
            this.label_ID.TabIndex = 13;
            this.label_ID.Text = "ID";
            // 
            // button_Reset
            // 
            this.button_Reset.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_Reset.Location = new System.Drawing.Point(851, 50);
            this.button_Reset.Name = "button_Reset";
            this.button_Reset.Size = new System.Drawing.Size(65, 33);
            this.button_Reset.TabIndex = 15;
            this.button_Reset.Text = "리셋";
            this.button_Reset.UseVisualStyleBackColor = true;
            this.button_Reset.Click += new System.EventHandler(this.button_Reset_Click);
            // 
            // button_Answer
            // 
            this.button_Answer.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_Answer.Location = new System.Drawing.Point(764, 50);
            this.button_Answer.Name = "button_Answer";
            this.button_Answer.Size = new System.Drawing.Size(81, 33);
            this.button_Answer.TabIndex = 16;
            this.button_Answer.Text = "정답 확인";
            this.button_Answer.UseVisualStyleBackColor = true;
            this.button_Answer.Click += new System.EventHandler(this.button_Answer_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Project_Client2.Properties.Resources.KakaoTalk_20230214_173556069;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1184, 661);
            this.Controls.Add(this.button_Answer);
            this.Controls.Add(this.button_Reset);
            this.Controls.Add(this.label_ID);
            this.Controls.Add(this.label_long);
            this.Controls.Add(this.label_Hint);
            this.Controls.Add(this.label_word);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.textBox_ID);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Text = "0";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox_ID;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label_word;
        private System.Windows.Forms.Label label_Hint;
        private System.Windows.Forms.Label label_long;
        private System.Windows.Forms.Label label_ID;
        private System.Windows.Forms.Button button_Reset;
        private System.Windows.Forms.Button button_Answer;
    }
}


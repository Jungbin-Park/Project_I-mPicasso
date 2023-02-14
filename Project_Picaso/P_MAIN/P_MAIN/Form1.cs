using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using System.Threading;
using P_MAIN.Properties;
using System.Runtime.Remoting.Messaging;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;

namespace P_MAIN
{
    public partial class Form1 : Form
    {


        class CmdPacket
        {
            public string ID { get; set; }
            public char CMD { get; set; }
        }
        class ChatPacket : CmdPacket
        {
            public string CHATDATA { get; set; }
        }
        class PositionPacket : CmdPacket
        {
            // CMD = 'P';
            public int X { get; set; }
            public int Y { get; set; }
        }
        class ClickPacket : CmdPacket
        {
            // CMD = 'C';
            public bool CLICK { get; set; }
            public int X { get; set; }
            public int Y { get; set; }
        }

        class ColorPacket : CmdPacket
        {
            public int COLOR { get; set; }

            //public ColorPacket()
            //{
            //    COLOR = Color.Black; // 기본값
            //}
        }
        //서버 관련 필드
        Socket clientSocket;
        IPEndPoint ipep;        // 서버의 접속 주소
        bool isRunRecv = true;  // 수신 스레드의 계속 동작 여부       

        NetworkStream ns;
        StreamWriter sw;
        StreamReader sr;

        delegate void AddMsgLogData(string data);
        AddMsgLogData addMsgLogData = null;

        delegate void StopState();
        StopState stopButtonState = null;

        Dictionary<string, Point> playerLocation = new Dictionary<string, Point>();
        Dictionary<string, Point> playerLocation_End = new Dictionary<string, Point>();

        //색 변환
        Dictionary<string, int> player_int = new Dictionary<string, int>();
        Dictionary<string, Color> player_Color = new Dictionary<string, Color>();

        //정답
        string word = null;
        string hint = null;
        string Long = null;


        //그래픽 패널 필드
        //Graphics g;       
        int dr = 2;
        //Pen pen;
        Pen client_pen;
        Pen server_pen;
        Color server_color = Color.Black;
        Color cl;

        //타이머 필드
        int min = 0;
        int sec = 0;

        System.Windows.Forms.Timer tm = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer tm1 = new System.Windows.Forms.Timer();


        public Form1()
        {
            InitializeComponent();

            //드로잉
            // 봐야할부분
            //pen = new Pen(Color.Black, dr);
            client_pen = new Pen(Color.Black);
            server_pen = new Pen(Color.Black);

            //서버관련 
            this.DoubleBuffered = true;
            this.Load += Form1_Load;
            this.FormClosed += Form1_FormClosed;

            //클로즈 이미지


            //타이머
            tm.Interval = 1000;
            tm1.Interval = 1000;
            tm.Tick += Tm_Tick;
            tm1.Tick += Tm1_Tick;
            this.Paint += Form1_Paint;

        }

        private void Tm1_Tick(object sender, EventArgs e)
        {
            
            if (textBox1.Text == word)
            {
                tm.Start();
            }
            
        }

        // 타이머
        private void Tm_Tick(object sender, EventArgs e)
        {
            sec++;
            if (sec == 60)
            {
                min++;
                if (min == 2)
                {

                    tm.Stop();
                }
                sec = 0;

            }
            Invalidate();
        }

        private void button_RE_Click(object sender, EventArgs e)
        {
            //this.Hide();
            //Form2 form2 = new Form2();
            //form2.ShowDialog();
            //this.Close(); //폼2가리기
            //Application.Restart(); //앱 전체 다시 실행

            //텍스트랑 그림판만 지워보기
            //foreach (Control control in this.Controls)
            //{
            //    if (control is TextBox)
            //    {
            //        TextBox textbox = (TextBox)control;
            //        if (textbox != null) textbox.Text = string.Empty;
            //    }
            //    else if (control is Panel)
            //    {
            //        Refresh();
            //    }
            //}

            //this.Hide();
            //Form form1 = new Form1();  // 특정 폼 재실행 때
            //form1.Show();
            //return;
            Graphics g = panel_Drawing.CreateGraphics();
            sec = 0;
            min = 0;
            textBox1.Text = null;
            textBox2.Text = null;
            textBox3.Text = null;
            tm.Stop();
            tm1.Stop();
            // 2
            panel_Drawing.Invalidate(); 
            
            g.Clear(Color.White);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.isRunRecv = false; // 추가
            button_exit.PerformClick();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.addMsgLogData = AddMsgLogListBox;
            this.stopButtonState = StopButtonState;

            tm1.Start();

            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            button_exit.Enabled = false;
            textBox_Chat.Enabled = false;
        }

        void AddMsgLogListBox(string data)
        {
            if (listBox_ChatView.InvokeRequired)
            {
                Invoke(addMsgLogData, new object[] { data });
            }
            else
            {

                listBox_ChatView.Items.Add(data);
                listBox_ChatView.SelectedIndex = listBox_ChatView.Items.Count - 1;
                //listBox_ChatView.AutoScrollOffset = new Point(listBox_ChatView.listBox_ChatView.ColumnWidth);

            }
        }
        void ThreadRecv()
        {
            while (this.isRunRecv)
            {
                //string data = null;
                try
                {
                    string data = sr.ReadLine();

                    // DB
                    if (data.Contains('|'))
                    {
                        string[] rdata = data.Split('|');
                        hint = rdata[0];
                        Long = rdata[1];
                        word = rdata[2];
                        textBox1.Text = word;
                        textBox2.Text = hint;
                        textBox3.Text = Long;
                    }                    

                    //AddMsgLogListBox($"==> 수신 : {rdata},{data}");
                    if (data == null)
                    {
                        this.isRunRecv = false;
                    }                        
                    else
                    {
                        // json문자열을 원래의 객체로 복원
                        // 일단 모든 패킷 클래스의 부모 클래스로 변환하여
                        // 명령의 종류를 확인한 후 
                        // 어떤 자식 클래스로 변환할 지 결정함

                        CmdPacket cmd = JsonSerializer.Deserialize<CmdPacket>(data);
                        switch (cmd.CMD)
                        {
                            // 명령을 확인한 후 어떤 클래스로 변환할 지 결정함
                            case 'P':
                                PositionPacket pp = JsonSerializer.Deserialize<PositionPacket>(data);
                                ColorPacket colp = JsonSerializer.Deserialize<ColorPacket>(data); 
                                Graphics g = panel_Drawing.CreateGraphics();
                                g.SmoothingMode = SmoothingMode.AntiAlias;
                                                                
                                if (playerLocation.ContainsKey(textBox_ID.Text))
                                {
                                    //server_color = ColorTranslator.FromOle(colp.COLOR);
                                    if (player_Color.ContainsKey(cmd.ID) == false)
                                    {
                                        g.DrawLine(Pens.Black, playerLocation_End[cmd.ID].X, playerLocation_End[cmd.ID].Y, pp.X, pp.Y);
                                        playerLocation_End[cmd.ID] = new Point(pp.X, pp.Y);
                                        continue;
                                    }
                                    else if(player_Color[cmd.ID] == Color.White)
                                    {
                                        Pen erase_pen = new Pen(Color.White, 5);
                                        g.DrawLine(erase_pen, playerLocation_End[cmd.ID].X, playerLocation_End[cmd.ID].Y, pp.X, pp.Y);
                                        playerLocation_End[cmd.ID] = new Point(pp.X, pp.Y);
                                        continue;
                                    }
                                    //Console.WriteLine(player_Color[cmd.ID]);
                                    server_pen = new Pen(player_Color[cmd.ID]);
                                   // Console.WriteLine($"P if {player_Color[cmd.ID]}");
                                    g.DrawLine(server_pen, playerLocation_End[cmd.ID].X, playerLocation_End[cmd.ID].Y, pp.X, pp.Y);
                                    playerLocation_End[cmd.ID] = new Point(pp.X, pp.Y);
                                }
                                break;
                            case 'C':
                                ClickPacket cp = JsonSerializer.Deserialize<ClickPacket>(data);
                                if (playerLocation.ContainsKey(textBox_ID.Text))
                                {
                                    playerLocation_End[cmd.ID] = new Point(cp.X, cp.Y);
                                }
                                //else
                                //{
                                //    MessageBox.Show("색또는 아이디를 지정해주세요");
                                //}
                                break;
                            case 'R':
                                colp = JsonSerializer.Deserialize<ColorPacket>(data);
                                //if (playerLocation.ContainsKey(textBox_ID.Text))
                                //{
                                //    server_color = ColorTranslator.FromOle(colp.COLOR);
                                //    Console.WriteLine(server_color);
                                //    if (server_color == Color.Empty)
                                //    {
                                //        Console.WriteLine($"R if 색이 빈 값입니다.");
                                //        server_pen = new Pen(Color.Black, dr);  //검은색 기본값
                                //    }
                                //    else
                                //    {
                                //        Console.WriteLine($"R else {server_color}");
                                //        server_pen = new Pen(server_color, dr);
                                //        //pen = new Pen(colp.COLOR, dr);
                                //    }
                                //}
                                if (player_Color.ContainsKey(cmd.ID))
                                {
                                    player_Color[cmd.ID] = ColorTranslator.FromOle(colp.COLOR);
                                    Console.WriteLine(player_Color[cmd.ID]);
                                    if (player_Color[cmd.ID] == Color.Empty)
                                    {
                                        Console.WriteLine($"R if 색이 빈 값입니다.");
                                        server_pen = new Pen(Color.Black);  //검은색 기본값
                                    }
                                    else
                                    {
                                        Console.WriteLine($"R else {player_Color[cmd.ID]}");
                                        server_pen = new Pen(player_Color[cmd.ID]);
                                        //pen = new Pen(colp.COLOR, dr);
                                    }
                                }
                                else
                                {
                                    Color Trans_Color = ColorTranslator.FromOle(colp.COLOR);
                                    player_Color.Add(cmd.ID, Trans_Color);

                                }
                                break;
                            case 'T':
                                ChatPacket chatp = JsonSerializer.Deserialize<ChatPacket>(data);
                                //AddMsgLogListBox($"수신 >> {chatp.CHATDATA}");
                                AddMsgLogListBox($"{chatp.ID} >> {chatp.CHATDATA}");
                                break;
                        }
                    }
                }
                catch (JsonException ex)
                {
                    AddMsgLogListBox($"Json Error : {ex.Message}");
                    //AddMsgLogListBox($"Json Data : {data}");
                }
                catch (Exception ex)
                {
                    AddMsgLogListBox($"Recv Error : {ex.Message}");
                    // this.isRunRecv = false;
                }
            }
            Invoke(stopButtonState, null);
        }

        void StopButtonState()
        {
            button_Link.Enabled = true;
            button_exit.Enabled = false;
            textBox_Chat.Enabled = false;
        }
        private void button_Link_Click(object sender, EventArgs e)
        {
            this.clientSocket = new Socket(AddressFamily.InterNetwork,
                                           SocketType.Stream,
                                           ProtocolType.Tcp);
            this.ipep = new IPEndPoint(IPAddress.Parse("192.168.0.26"), 9000);
            AddMsgLogListBox("서버 접속 시도...");
            this.clientSocket.Connect(ipep);
            AddMsgLogListBox("서버 접속 연결!!!");

            this.ns = new NetworkStream(this.clientSocket);
            this.sw = new StreamWriter(this.ns);
            this.sr = new StreamReader(this.ns);
            this.ActiveControl = textBox_ID;

            this.isRunRecv = true;
            Thread threadRecv = new Thread(new ThreadStart(ThreadRecv));
            threadRecv.Start();

            button_Link.Enabled = false;
            button_exit.Enabled = true;
            textBox_Chat.Enabled = true;
        }
        private void button_exit_Click(object sender, EventArgs e)
        {
            if (this.clientSocket != null &&
               this.clientSocket.Connected)
            {
                this.clientSocket.Close();
            }

            button_Link.Enabled = true;
            button_exit.Enabled = false;
            textBox_Chat.Enabled = false;
        }

        
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Font ft = new Font("맑은고딕", 15, FontStyle.Bold);
            e.Graphics.DrawString($"{min}분{sec}초 지났습니다",
                ft, Brushes.Black, 270, 20);
        }

        

        // 패킷 전송
        void SendPositionPacket(string id, int x, int y)
        {
            PositionPacket pp = new PositionPacket();
            pp.CMD = 'P';
            pp.ID = id;
            pp.X = x;
            pp.Y = y;

            string data = JsonSerializer.Serialize(pp);
            sw.WriteLine(data);
            sw.Flush();
        }
        void SendClickPacket(bool isClick, string id, int x, int y)
        {
            // 서버로 ClickPacket 전송
            ClickPacket cp = new ClickPacket();
            cp.CMD = 'C'; // CMD
            cp.CLICK = isClick;
            cp.ID = id;
            cp.X = x;
            cp.Y = y;

            string data = JsonSerializer.Serialize(cp);
            sw.WriteLine(data);
            sw.Flush();
        }

        // 패널 그리기 [자신]
        private void panel_Drawing_MouseMove(object sender, MouseEventArgs e)
        {
            Graphics g = this.panel_Drawing.CreateGraphics();
            if (e.Button == MouseButtons.Left) // 왼클릭 상태일 때
            {
                
                string id;
                id = textBox_ID.Text;  // id

                if (playerLocation.ContainsKey(id))
                {
                    //Console.WriteLine($"if/move {id}, {e.Location}"); // 디버깅 출력용
                    //client_pen
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.DrawLine(client_pen, playerLocation[id], e.Location);
                    SendPositionPacket(id, playerLocation[id].X, playerLocation[id].Y);
                    playerLocation[id] = e.Location;
                }
            }
        }

        private void panel_Drawing_MouseDown(object sender, MouseEventArgs e)
        {
            Console.WriteLine("MouseDown");
            if (e.Button == MouseButtons.Left)
            {
                string id;
                id = textBox_ID.Text;  // id

                if (playerLocation.ContainsKey(id))
                {
                    //Console.WriteLine($"if/Down {id}, {e.Location}"); // 디버깅 출력용
                    playerLocation[id] = e.Location;
                    SendClickPacket(true, id, playerLocation[id].X, playerLocation[id].Y);
                }
                else
                {
                    //Console.WriteLine($"else/Down {id}, {e.Location}"); //디버깅 출력용
                    playerLocation.Add(id, e.Location);
                    //player_Color.Add(id, Color.Black); // 기본색 추가
                    SendClickPacket(true, id, playerLocation[id].X, playerLocation[id].Y);
                }
            }
          
        }

        private void panel_Drawing_MouseUp(object sender, MouseEventArgs e)
        {            
            if (e.Button == MouseButtons.Left)
            {
                string id;
                id = textBox_ID.Text;  // id

                if (playerLocation.ContainsKey(id))
                {
                    //Console.WriteLine($"if/UP {id}, {e.Location}"); //디버깅 출력용
                    playerLocation[id] = e.Location;

                    SendPositionPacket(id, playerLocation[id].X, playerLocation[id].Y);
                    SendClickPacket(false, id, playerLocation[id].X, playerLocation[id].Y); // id
                }
            }            
        }

        // 펜 색변경
        private void toolStripButton_Black_Click(object sender, EventArgs e)
        {
            //Graphics g = panel_Drawing.CreateGraphics();
            cl = Color.Black;
            client_pen = new Pen(cl, dr);
        }

        private void toolStripButton_Red_Click(object sender, EventArgs e)
        {
            //Graphics g = panel_Drawing.CreateGraphics();
            cl = Color.Red;
            client_pen = new Pen(cl, dr);
        }

        private void toolStripButton_Blue_Click(object sender, EventArgs e)
        {
            //Graphics g = panel_Drawing.CreateGraphics();
            cl = Color.Blue;
            client_pen = new Pen(cl, dr);
        }

        private void toolStripButton_Green_Click(object sender, EventArgs e)
        {
            //Graphics g = panel_Drawing.CreateGraphics();
            cl = Color.Green;
            client_pen = new Pen(cl, dr);
        }

       


        private void toolStripButton_Yellow_Click(object sender, EventArgs e)
        {
            ClickPacket cp = new ClickPacket();

            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
            {
                Color color = cd.Color;
                client_pen = new Pen(color, dr);

                int oleColor = ColorTranslator.ToOle(color);
                ColorPacket colp = new ColorPacket();
                colp.CMD = 'R';

                string id;
                id = textBox_ID.Text;  // id
                colp.ID = id;
                if (player_int.ContainsKey(id))
                {
                    colp.COLOR = oleColor;
                    player_int[id] = oleColor;
                    string data = JsonSerializer.Serialize(colp);
                    
                    sw.WriteLine(data);
                    sw.Flush();                    
                }
                else
                {
                    colp.COLOR = oleColor;
                    player_int.Add(id, oleColor);
                    string data = JsonSerializer.Serialize(colp);                    
                    sw.WriteLine(data);
                    sw.Flush();                    
                    
                }                
            }           
                           
        }

        private void toolStripButton_P_Click(object sender, EventArgs e)
        {
            // Graphics g = panel_Drawing.CreateGraphics();
            dr += 2;
            client_pen = new Pen(cl, dr);

        }
        private void toolStripButton_N_Click(object sender, EventArgs e)
        {
            // Graphicsg = panel_Drawing.CreateGraphics();
            dr -= 2;
            client_pen = new Pen(cl, dr);

        }

        private void toolStripButton_E_Click(object sender, EventArgs e)
        {
            Graphics g = panel_Drawing.CreateGraphics();            
            Color color = Color.White;
            client_pen = new Pen(color, 5);

            int oleColor = ColorTranslator.ToOle(color);
            ColorPacket colp = new ColorPacket();
            colp.CMD = 'R';
            string id;
            id = textBox_ID.Text;  // id
            colp.ID = id;
            if (player_int.ContainsKey(id))
            {
                colp.COLOR = oleColor;
                player_int[id] = oleColor;
                string data = JsonSerializer.Serialize(colp);

                sw.WriteLine(data);
                sw.Flush();
            }
            else
            {
                colp.COLOR = oleColor;
                player_int.Add(id, oleColor);
                string data = JsonSerializer.Serialize(colp);
                sw.WriteLine(data);
                sw.Flush();
            }

        }
        

        // 채팅
        private void textBox_Chat_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    ChatPacket chatp = new ChatPacket();

                    chatp.CMD = 'T';
                    chatp.ID = textBox_ID.Text;
                    chatp.CHATDATA = textBox_Chat.Text;

                    // json문자열로 변환
                    string data = JsonSerializer.Serialize(chatp);
                    this.sw.WriteLine(data);
                    this.sw.Flush();
                    AddMsgLogListBox($"{chatp.ID} : {chatp.CHATDATA}");
                    // AddMsgLogListBox($"전송 : {data}");

                    textBox_Chat.Clear();
                    break;
            }
        }
    }
}
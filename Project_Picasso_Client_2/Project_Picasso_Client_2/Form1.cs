using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Collections;
using System.Threading;
using System.Drawing.Drawing2D;
using System.Data.OracleClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Media;

namespace Project_Picasso_Client_2
{
    public partial class Form1 : Form
    {
        class CmdPacket
        {
            public string ID { get; set; }
            public char CMD { get; set; }
        }
        class PositionPacket : CmdPacket
        {
            // CMD = 'P';
            public int X { get; set; }
            public int Y { get; set; }

        }
        class ClickPacket : CmdPacket
        {
            public bool CLICK { get; set; }
            public int X { get; set; }
            public int Y { get; set; }
        }
        class ColorPacket : CmdPacket
        {
            public int COLOR { get; set; }

        }

        ////////////////////////서버 부분///////////////////////////////
        const string IP = "192.168.0.26";//"127.0.0.1";//"192.168.0.26";
        const int PORT = 9000;
        Socket clientSocket;
        IPEndPoint ipep; // 서버의 접속주소

        bool isRunRecv = true;
        NetworkStream ns;
        StreamWriter sw;
        StreamReader sr;
        ////////////////////////서버 부분///////////////////////////////

        ////////////////////////그리기 부분///////////////////////////////

        Dictionary<string, Point> playerLocation = new Dictionary<string, Point>();
        Dictionary<string, Point> playerLocation_End = new Dictionary<string, Point>();

        Dictionary<string, int> player_int = new Dictionary<string, int>();
        Dictionary<string, Color> player_Color = new Dictionary<string, Color>();


        ////////////////////////정답 부분///////////////////////////////
        string word;
        string hint;
        string Long;

        //그래픽 패널 필드
        //Graphics g;       
        int dr = 2;
        //Pen pen;
        Pen server_pen;
        Color server_color = Color.Black;


        ////////////////////////타이머 부분///////////////////////////////
        int min = 0;
        int sec = 0;

        System.Windows.Forms.Timer tm = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer tm1 = new System.Windows.Forms.Timer();


        ////////////////////////사운드 부분///////////////////////////////
        private SoundPlayer Player = new SoundPlayer();


        public Form1()
        {
            InitializeComponent();

            this.Load += Form1_Load;
            this.FormClosed += Form1_FormClosed;

            server_pen = new Pen(Color.Black, dr);


            // 타이머
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
                textBox1.Visible = true;
                textBox1.Text = "제시어가 출력되었습니다.";
            }
        }
        private void Tm_Tick(object sender, EventArgs e)
        {
            sec++;
            if (sec == 60)
            {
                min++;
                if (min == 1)
                {
                    textBox2.Visible = true;
                }
                if (min == 2)
                {
                    //textBox3.Visible = true;
                    tm.Stop();
                }
                sec = 0;

            }
            if(min == 1 && sec == 30)
            {
                textBox3.Visible = true;
            }
            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Font ft = new Font("맑은고딕", 13, FontStyle.Bold);
            e.Graphics.DrawString($"{min}:{sec} / (제한시간 2분)",
                ft, Brushes.Black, 180, 60);
        }

        

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.isRunRecv = false;
            this.clientSocket.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.clientSocket = new Socket(AddressFamily.InterNetwork,
                                        SocketType.Stream, ProtocolType.Tcp);
            this.ipep = new IPEndPoint(IPAddress.Parse(IP), (PORT));
            this.clientSocket.Connect(ipep);


            this.ns = new NetworkStream(this.clientSocket);
            this.sw = new StreamWriter(this.ns);
            this.sr = new StreamReader(this.ns);

            this.isRunRecv = true;
            Thread threadRecv = new Thread(new ThreadStart(ThreadRecv));
            threadRecv.IsBackground = true;
            threadRecv.Start();

            try
            {
                this.Player.SoundLocation = @"../../Resources/maple_BGM.wav";
                this.Player.PlayLooping();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error playing sound");
            }



            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            tm1.Start();
            textBox_ID.Text = "Admin";
        }

        void ThreadRecv()
        {
            while (this.isRunRecv)
            {
                try
                {
                    string data = sr.ReadLine();
                    Console.WriteLine(data);

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

                    //Console.WriteLine($"수신 : {data}");
                    if (data == null)
                    {
                        this.isRunRecv = false;
                    }
                    else
                    {
                        CmdPacket cmd = JsonSerializer.Deserialize<CmdPacket>(data);   //객체로바뀜

                        switch (cmd.CMD)
                        {
                            case 'P':
                                PositionPacket pp = JsonSerializer.Deserialize<PositionPacket>(data);
                                ColorPacket colp = JsonSerializer.Deserialize<ColorPacket>(data);
                                Graphics g = panel1.CreateGraphics();
                                g.SmoothingMode = SmoothingMode.AntiAlias;
                                if (playerLocation.ContainsKey(textBox_ID.Text))
                                {
                                    if (player_Color.ContainsKey(cmd.ID) == false)
                                    {
                                        g.DrawLine(Pens.Black, playerLocation_End[cmd.ID].X, playerLocation_End[cmd.ID].Y, pp.X, pp.Y);
                                        playerLocation_End[cmd.ID] = new Point(pp.X, pp.Y);
                                        continue;
                                    }
                                    else if (player_Color[cmd.ID] == Color.White)
                                    {
                                        Pen erase_pen = new Pen(Color.White, 20);
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
                                break;
                            case 'R':
                                colp = JsonSerializer.Deserialize<ColorPacket>(data);
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
                        }
                    }
                }
                catch (JsonException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine($"Json Data : {ex.Data}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Recv Error :{ex.Message}");
                    // if (ex.Source != "System.Text.Json") //
                    this.isRunRecv = false;
                }
            }
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

        private void panel1_MouseDown_1(object sender, MouseEventArgs e)
        {
            Console.WriteLine("MouseDown");
            if (e.Button == MouseButtons.Left)
            {
                string id;
                id = textBox_ID.Text;  // id

                if (playerLocation.ContainsKey(id))
                {
                    Console.WriteLine($"if/Down {id}, {e.Location}");
                    playerLocation[id] = e.Location;
                    SendClickPacket(true, id, playerLocation[id].X, playerLocation[id].Y);
                }
                else
                {
                    Console.WriteLine($"else/Down {id}, {e.Location}");
                    playerLocation.Add(id, e.Location);
                    SendClickPacket(true, id, playerLocation[id].X, playerLocation[id].Y);
                }
            }
        }

        private void button_Reset_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void button_Answer_Click(object sender, EventArgs e)
        {
            textBox1.Text = word;
            tm.Stop();
            tm1.Stop();
            min = 0;
            sec = 0;
            Invalidate();
        }
    }

}

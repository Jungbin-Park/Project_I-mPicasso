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

namespace Project_Client2
{
    public partial class Form1 : Form
    {
        ////////////////////////서버 부분///////////////////////////////
        const string IP = "192.168.0.18";//"127.0.0.1";//"192.168.0.26";
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
        Point previousPoint = new Point();

        ////////////////////////정답 부분///////////////////////////////
        string word = "";
        string hint = "";
        string Long = "";


        ////////////////////////타이머 부분///////////////////////////////
        int min = 0;
        int sec = 0;

        System.Windows.Forms.Timer tm = new System.Windows.Forms.Timer();

        public Form1()
        {
            InitializeComponent();

            this.Load += Form1_Load;
            this.FormClosed += Form1_FormClosed;


            // 타이머
            tm.Interval = 1000;
            tm.Tick += Tm_Tick;
            this.Paint += Form1_Paint;

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Font ft = new Font("맑은고딕", 15, FontStyle.Bold);
            e.Graphics.DrawString($"{min}분{sec}초 지났습니다",
                ft, Brushes.Black, 270, 20);
        }

        private void Tm_Tick(object sender, EventArgs e)
        {
            
            sec++;

            if (sec == 60)
            {
                min++;


                if (min == 2)
                {
                    textBox1.Text = word;
                    textBox2.Text = hint;
                    textBox3.Text = Long;
                    //버튼클릭창 추가예정
                    tm.Stop();
                }
                sec = 0;

            }
            Invalidate();
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
            tm.Start();

            textBox_ID.Text = "Admin";
        }

        void ThreadRecv()
        {
            while (this.isRunRecv)
            {
                try
                {
                    string data = sr.ReadLine();
                    string[] rdata = data.Split(',');
                    hint = rdata[0];
                    Long = rdata[1];
                    word = rdata[2];
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
                                Console.WriteLine(data);
                                PositionPacket pp = JsonSerializer.Deserialize<PositionPacket>(data);
                                PositionPacket_End pp_end = JsonSerializer.Deserialize<PositionPacket_End>(data);
                                Graphics g = panel1.CreateGraphics();
                                g.SmoothingMode = SmoothingMode.AntiAlias;
                                Console.WriteLine("test??");
                                if (playerLocation.ContainsKey(textBox_ID.Text))
                                {
                                    Console.WriteLine("test!");
                                    g.DrawLine(Pens.Black, previousPoint.X, previousPoint.Y, pp.X, pp.Y);
                                    previousPoint.X = pp.X; // Point
                                    previousPoint.Y = pp.Y;
                                }
                                else
                                {
                                    playerLocation.Add(textBox_ID.Text, previousPoint);
                                }
                                break;
                            case 'C':
                                Console.WriteLine(data);
                                ClickPacket cp = JsonSerializer.Deserialize<ClickPacket>(data);
                                ClickPacket_End cp_end = JsonSerializer.Deserialize<ClickPacket_End>(data);
                                previousPoint = new Point();
                                if (playerLocation.ContainsKey(textBox_ID.Text))
                                {
                                    previousPoint.X = cp.X; // Point
                                    previousPoint.Y = cp.Y;
                                }
                                else
                                {
                                    playerLocation.Add(textBox_ID.Text, previousPoint);
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
    }
    class CmdPacket
    {
        public char CMD { get; set; }
    }
    class PositionPacket : CmdPacket
    {
        // CMD = 'P';

        public string ID { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

    }
    class ClickPacket : CmdPacket
    {
        public bool CLICK { get; set; }
        public string ID { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }

    class BackCorlorPacket : CmdPacket
    {
        // CMD = 'B';
        public char CHCOLOR { get; set; }
    }

    class PositionPacket_End : CmdPacket
    {
        // CMD = 'P';
        public string ID { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }

    class ClickPacket_End : CmdPacket
    {
        // CMD = 'C';
        public bool CLICK { get; set; }
        public string ID { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}

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

namespace Project_Client2
{
    public partial class Form1 : Form
    {
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
        Point previousPoint = new Point();

        ////////////////////////그리기 부분///////////////////////////////

        public Form1()
        {
            InitializeComponent();

            this.Load += Form1_Load;
            this.FormClosed += Form1_FormClosed;

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

            textBox_ID.Text = "Admin";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Text = "hi";
            string qText = listBox1.Text;
            string aText = textBox1.Text;
            if(qText == aText)
            {
                textBox1.Text = "ang";
            }
        }


        void ThreadRecv()
        {
            while (this.isRunRecv)
            {
                try
                {
                    string data = sr.ReadLine();
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
                                Graphics g = panel1.CreateGraphics();
                                g.SmoothingMode = SmoothingMode.AntiAlias;
                                if (playerLocation.ContainsKey(textBox_ID.Text))
                                {
                                    g.DrawLine(Pens.Black, playerLocation_End[cmd.ID].X, playerLocation_End[cmd.ID].Y, pp.X, pp.Y);

                                    playerLocation_End[cmd.ID] = new Point(pp.X, pp.Y);
                                }
                                break;
                            case 'C':
                                ClickPacket cp = JsonSerializer.Deserialize<ClickPacket>(data);
                                previousPoint = new Point();
                                if (playerLocation.ContainsKey(textBox_ID.Text))
                                {
                                    playerLocation_End[cmd.ID] = new Point(cp.X, cp.Y);
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

        private void panel1_MouseDown(object sender, MouseEventArgs e)
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
    }
    class Player
    {
        public string PID { get; set; }
        public Point POS { get; set; }
    }

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

    class BackCorlorPacket : CmdPacket
    {
        // CMD = 'B';
        public char CHCOLOR { get; set; }
    }
}

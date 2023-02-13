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
using System.Threading;
using System.Text.Json;
using System.Drawing.Drawing2D;
using System.Collections;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Linq;
using static System.Windows.Forms.AxHost;
using System.Collections.Concurrent;

namespace _project_two_miltipen
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
            // CMD = 'C';
            public bool CLICK { get; set; }
            
            public int X { get; set; }
            public int Y { get; set; }            
        }

        const string IP = "192.168.0.26";//"127.0.0.1";
        const int PORT = 9000;
        Socket clientSocket;
        IPEndPoint ipep; // 서버의 접속주소
                         
        bool isRunRecv = true;
        NetworkStream ns;
        StreamWriter sw;
        StreamReader sr;


        Dictionary<string, Point> playerLocation = new Dictionary<string, Point>();
        Dictionary<string, Point> playerLocation_End = new Dictionary<string, Point>();
        Point previousPoint = new Point();


        public Form1()
        {            
            InitializeComponent();
            
            this.DoubleBuffered= true;
            this.Load += Form1_Load;
            this.FormClosed += Form1_FormClosed;
            this.Paint += Form1_Paint;    
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.isRunRecv = false;
            this.clientSocket.Close();
        }
        void ThreadRecv()
        {
            while (this.isRunRecv)
            {
                try
                {
                    string data = sr.ReadLine();
                    Console.WriteLine($"수신 : {data}");
                    if (data == null)
                    {
                        this.isRunRecv = false;
                    }
                    else
                    {
                        CmdPacket cmd = JsonSerializer.Deserialize<CmdPacket>(data);   //객체로바뀜
                        switch (cmd.CMD)
                        {
                           
                            // 채팅 데이터 이면
                            case 'P':
                                PositionPacket pp = JsonSerializer.Deserialize<PositionPacket>(data);
                                Graphics g = panel.CreateGraphics();
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
                    //AddMsgLogListBox($"==> 수신 : {data}");
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
        private void Form1_Load(object sender, EventArgs e)
        {
            this.clientSocket = new Socket(AddressFamily.InterNetwork,
                                SocketType.Stream, ProtocolType.Tcp);
            this.ipep = new IPEndPoint(IPAddress.Parse(IP),(PORT));            
            this.clientSocket.Connect(ipep);
            
            this.ns = new NetworkStream(this.clientSocket);
            this.sw = new StreamWriter(this.ns);
            this.sr = new StreamReader(this.ns);

            this.isRunRecv = true;
            Thread threadRecv = new Thread(new ThreadStart(ThreadRecv));
            threadRecv.IsBackground= true;
            threadRecv.Start();            
        }
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
        private void panel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias; 
            //없어도그려짐
        }
        private void panel_MouseMove(object sender, MouseEventArgs e)
        {
            
            Graphics g = this.panel.CreateGraphics();
            if (e.Button == MouseButtons.Left) // 왼클릭 상태일 때
            {
                string id;
                id = textBox_ID.Text;  // id

                if (playerLocation.ContainsKey(id))
                {
                    Console.WriteLine($"if/move {id}, {e.Location}");
                    g.DrawLine(Pens.Black, playerLocation[id], e.Location);
                    SendPositionPacket(id, playerLocation[id].X, playerLocation[id].Y);
                    playerLocation[id] = e.Location;
                }
            }            
        }
        private void panel_MouseUp(object sender, MouseEventArgs e)
        {
            Console.WriteLine("MouseUp");
            // 마우스 왼버튼이 MouseUp 이벤트를 발생시키면
            if (e.Button == MouseButtons.Left)
            {
                string id;
                id = textBox_ID.Text;  // id

                if (playerLocation.ContainsKey(id))
                {
                    Console.WriteLine($"if/UP {id}, {e.Location}");
                    playerLocation[id] = e.Location;

                    SendPositionPacket(id, playerLocation[id].X, playerLocation[id].Y);
                    SendClickPacket(false, id, playerLocation[id].X, playerLocation[id].Y); // id

                }
            }
        }
        private void panel_MouseDown(object sender, MouseEventArgs e)
        {
            
            Console.WriteLine("MouseDown");
            if (e.Button == MouseButtons.Left)
            {
                string id;
                id = textBox_ID.Text;  // id
                
                if(playerLocation.ContainsKey(id))
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
        private void Form1_Paint(object sender, PaintEventArgs e)
        {            
        }

    }
}

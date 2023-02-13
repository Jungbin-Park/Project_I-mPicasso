using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace db
{
    public partial class Form1 : Form
    {
        // DB연동
        OracleConnection conn = null;
        private string dbConnInfo = @"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))
                                    (CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xe))); User Id = meta; Password = 123456;";
        Random rand = new Random();


        // 서버구현
        Socket serverSocket;    // 클라이언트의 접속 연결을 담당하는
        IPEndPoint ipep;        // 서버의 주소(ip, port)

        Thread threadAccept;    // 연결 담당 스레드
        bool isRunAccept = true;

        IList<Socket> clientList = new List<Socket>();
        object keyObj = new object();

        delegate void AddMsgData(string log);
        AddMsgData addMsgData = null;
        string word_answer = "";


        public Form1()
        {
            InitializeComponent();

            this.Load += Form1_Load;
            this.FormClosed += Form1_FormClosed;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            button_DBConnect.PerformClick();
            serverSocket.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button_DisConnect.Enabled = false;

            this.addMsgData = AddClinetLogListBox;
        }

        void BroadCastData(Socket excludingSocket, string data)
        {
            foreach (var connSocket in this.clientList)
            {
                // 이 소켓은 제외하고 나머지에만 데이터를 보낸다.
                if (connSocket == excludingSocket)
                    continue;

                NetworkStream ns = new NetworkStream(connSocket);
                StreamWriter sw = new StreamWriter(ns);

                sw.WriteLine(data);
                
                sw.Flush();
            }
        }

        void BroadCastData_Word(string word)
        {
            foreach (var connSocket in this.clientList)
            {
                // 이 소켓은 제외하고 나머지에만 데이터를 보낸다.
                
                NetworkStream ns = new NetworkStream(connSocket);
                StreamWriter sw = new StreamWriter(ns);
                sw.WriteLine(word);
                sw.Flush();
            }
        }

        private void AddDBLogListBox(string result)
        {
            if (listBox_DBlog.InvokeRequired)
            {
                Invoke(addMsgData, new object[] { result });
            }
            else
            {
                listBox_DBlog.Items.Add(result);
                listBox_DBlog.SelectedIndex = listBox_DBlog.Items.Count - 1;
            }

        }

        void AddClinetLogListBox(string log)
        {
            if (listBox_Clientlog.InvokeRequired)
            {
                Invoke(addMsgData, new object[] { log });}
            else
            {
                listBox_Clientlog.Items.Add(log);
                listBox_Clientlog.SelectedIndex = listBox_Clientlog.Items.Count - 1;
            }
        }
                
        private void button_DBConnect_Click(object sender, EventArgs e)
        {
            string strconn = this.dbConnInfo;
            conn = new OracleConnection(strconn);
            try
            {
                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    AddDBLogListBox("Oracle Server 연결 성공");
                }
            }
            catch (Exception ex)
            {
                conn = null;
                AddDBLogListBox("DB Error : " + ex.Message);
            }

            this.serverSocket = new Socket(AddressFamily.InterNetwork,
                                            SocketType.Stream,
                                            ProtocolType.Tcp);
            this.ipep = new IPEndPoint(IPAddress.Any, 9000);
            this.serverSocket.Bind(this.ipep);
            this.serverSocket.Listen(100);

            AddClinetLogListBox("서버 시작");

            this.isRunAccept = true;
            this.threadAccept = new Thread(new ThreadStart(ThreadAccept));
            this.threadAccept.Start();

            button_DBConnect.Enabled = false;
            button_DisConnect.Enabled = true;
        }

        private void button_DisConnect_Click(object sender, EventArgs e)
        {
            if (conn != null &&
                conn.State == ConnectionState.Open ||
                conn.State == ConnectionState.Connecting)
            {
                conn.Close();
                conn = null;
                AddDBLogListBox("Oracle 연결 해제");
            }

            this.serverSocket.Close();

            button_DBConnect.Enabled = true;
            button_DisConnect.Enabled = false;

            foreach (var connSocket in clientList)
            {
                connSocket.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string selectSql = "SELECT * FROM CAT";
            try
            {
                OracleCommand cmd = new OracleCommand();
                // 연결객체와 sql문 전달
                cmd.Connection = this.conn;
                cmd.CommandText = selectSql;

                // sql문 실행후 결과를 reader객체에 저장
                OracleDataReader reader = cmd.ExecuteReader();

                //emp테이블의 컬럼 정보 얻기
                string[] columns = new string[reader.FieldCount];
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    columns[i] = reader.GetName(i);
                    AddDBLogListBox("컬럼명_" + i + ":" + columns[i]);
                }
                AddDBLogListBox("");

                // 행(레코드)데이터를 가져옴
                // 더 이상 읽을 레코드가 없으면 false를 반환
                while (reader.Read())
                {
                    string[] datas = new string[reader.FieldCount];
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        datas[i] = reader.GetValue(i).ToString();
                        string result = String.Format($"{columns[i]} : {datas[i]}");
                        AddDBLogListBox(result);
                    }
                    AddDBLogListBox("");
                }
                reader.Close();
                cmd.Dispose();  // 리소스 해제
            }
            catch (Exception ex)
            {
                AddDBLogListBox("DB Error : " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int rnum = rand.Next(15);

            string selectSql = $"SELECT * FROM CAT WHERE pno = {rnum}";
            
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = this.conn;
                cmd.CommandText = selectSql;

                

                OracleDataReader reader = cmd.ExecuteReader();

                
                string[] columns = new string[reader.FieldCount];

                for (int j = 0; j < reader.FieldCount; j++)
                {
                    columns[j] = reader.GetName(j);
                }
                
                while (reader.Read())
                {
                    string[] datas = new string[reader.FieldCount];
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        datas[i] = reader.GetValue(i).ToString();
                        string result = String.Format($"{columns[i]} : {datas[i]}");
                        AddDBLogListBox(result);
                    }
                    AddDBLogListBox("");
                    
                    //마지막 0 번호 , 1 힌트, 2 글자수 , 3 정답                    
                    
                    datas[3] = reader.GetValue(3).ToString();                    
                    word_answer = String.Format($"{datas[1]},{datas[2]},{datas[3]}");
                    AddDBLogListBox(word_answer);
                    BroadCastData_Word(word_answer);
                }


                reader.Close();
                cmd.Dispose();

            }
            catch (Exception ex)
            {
                AddDBLogListBox("DB Error : " + ex.Message);
            }

        }

        void ThreadRecv(object socket)
        {
            Socket connSocket = (Socket)socket;
            NetworkStream ns = new NetworkStream(connSocket);
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            while (true)
            {
                // 데이터 수신
                try
                {
                    AddClinetLogListBox("클라이언트 데이터 수신 대기...");
                    string data = sr.ReadLine();
                    AddClinetLogListBox($"수신 : {data}");
                    
                    //string rdata = 
                    if (data == null)
                    {

                        break;
                    }
                    // 받은 데이터를 연결된 나머지 클라이언트에 보낸다.
                    lock (this.keyObj)
                    {
                        BroadCastData(connSocket, data);
                        
                    }
                }

                catch (Exception ex)
                {
                    AddClinetLogListBox($"Recv Error : {ex.Message}");
                    break;
                }
            }


            AddClinetLogListBox($"{connSocket} 클라이언트 접속 종료");

            lock (this.keyObj)
            {
                clientList.Remove(connSocket);
            }
        }
        

        void ThreadAccept()
        {
            while (this.isRunAccept)
            {
                try
                {
                    AddClinetLogListBox("클라이언트 접속 대기...");
                    Socket connSocket = this.serverSocket.Accept();
                    AddClinetLogListBox("클라이언트 접속 연결!!!");
                    

                    // 새로 연결된 소켓을 클라이언트 소켓 리스트에 등록한다.
                    lock (this.keyObj)
                    {
                        clientList.Add(connSocket);
                    }

                    // 새로운 클라이언트가 접속하면 담당 스레드를 만들어서
                    // 입출력 처리하도록 한다
                    Thread threadRecv = new Thread(new ParameterizedThreadStart(ThreadRecv));
                    threadRecv.Start(connSocket);

                }
                catch (Exception ex)
                {
                    this.isRunAccept = false;
                    AddClinetLogListBox($"Accept Error : {ex.Message}");
                }
            }
        }
    }
}

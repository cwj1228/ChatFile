using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using ChatProgramClient;
using JsonObj;
using Packet;

namespace ChatProgramClient
{
    public partial class Login : Form
    {
        // 델리게이트로 함수 대리자 선언.
        delegate void AppendTextDelegateOnlyList(ListBox list, string s);
        delegate void ButtonDelegate(Button BT);
        delegate void BoxDelegate(TextBox TB);
        AppendTextDelegateOnlyList _listAppender;
        ButtonDelegate _button;
        BoxDelegate _box;
        static string myNick;
        Chanel chan = new Chanel();
        // 내가 지금 들어와 있는 방.
        public int room_number = -1;
        //static Login m_login = null;
        // 해당 클라이언트의 IP값
        public static string Client_IP = string.Empty;
        // 해당 클라이언트의 PORT값
        public static string Client_PORT = string.Empty;
        // 소켓 생성
        public static Socket Client_Socket = null;
        // 실행 상태 확인.
        public static bool IS_FIRST = true;

        // 델리게이트 선언.
        void ApeendList(ListBox list, string s)
        {
            if (list.InvokeRequired)
            {
                list.Invoke(_listAppender, list, s);
            }
            else
            {
                list.Items.Add(s);
            }
        }
        // 데이터 생성.
        public void InitData()
        {
            // 델리게이트
            _button = new ButtonDelegate(ButtonList);
            _box = new BoxDelegate(BoxList);
            // 정보 생성할 JsonDB 생성 후.
            JsonDB Client_JsonDB = new JsonDB();
            // 정보값을 Read.
            Client_JsonDB = Client_JsonDB.ReadJson();

            // IP와 PORT값 성정.
            Client_IP = Client_JsonDB.IP;
            Client_PORT = Client_JsonDB.PORT;
            // Socket에..
            Client_Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
        }
        void BoxList(TextBox box)
        {
            if (box.InvokeRequired)
            {
                box.Invoke(_box, box);
            }
            else
            {
                box.Enabled = false;
            }
        }
        void ButtonList(Button button)
        {
            if (button.InvokeRequired)
            {
                button.Invoke(_button, button);
            }
            else
            {
                button.Enabled = false;
            }
        }

        // 클라 -> 서버 연결 확인. ※ 반드시 서버가 먼저 열려있어야함.
        public void CS_Connet()
        {
            Console.WriteLine("※ 반드시 서버가 먼저 열려있어야함 ※");
            Console.WriteLine("서버 접속중");

            // 네트워크의 엔드포인트를 IP주소와 PORT로 표현.
            var ep = new IPEndPoint(IPAddress.Parse(Client_IP), Int32.Parse(Client_PORT));
            Client_Socket.Connect(ep);
            IS_FIRST = false;
            StartReceive();
        }
        // 클라 -> 서버 정보 보내기.
        public void SendMessage(string Message,int Room_Num, int Select)
        {
            MyPacket m_packet = new MyPacket();
            if (Select == -1)
            {
                byte[] buff = m_packet.InitPaket("Send_" + Room_Num + "_" + Message + "_" + myNick + "_" + Select);
                // 보냄.
                Client_Socket.Send(buff, SocketFlags.None);
            }
            else
            {
                byte[] buff = m_packet.InitPaket("Send_" + Room_Num + "_" + Message + "_" + myNick + "_" + Select);
                // 보냄.
                Client_Socket.Send(buff, SocketFlags.None);
            }
        }
        // 서버 -> 클라 정보 받아오기.
        public void StartReceive()
        {
            AsyncObject Server_Obj = new AsyncObject(1024);
            Server_Obj.Client_Socket = Client_Socket;
            Client_Socket.BeginReceive(Server_Obj.Buffer, 0, Server_Obj.BufferSize, 0, SC_DateReceived, Server_Obj);
        }

        void SC_DateReceived(IAsyncResult ar)
        {
            AsyncObject ServerData = (AsyncObject)ar.AsyncState;

            // 데이터 수신을 끝내고 받은 데이터의 바이트 수를 가져온다.
            int RecievdValue = ServerData.Client_Socket.EndReceive(ar);

            // 받은 바이트가 없을 경우 소켓을 닫아버린다.
            if (RecievdValue <= 0)
            {
                ServerData.Client_Socket.Close();
                return;
            }

            // 받은 데이터를 텍스트로 변환 시킨다. 
            //string SendDataText = Encoding.UTF8.GetString(ServerData.Buffer, 0, RecievdValue);
            // 역직렬화
            byte[] c_temp = new byte[ServerData.Buffer[2]];
            Array.Copy(ServerData.Buffer, ServerData.Buffer[0], c_temp, 0, ServerData.Buffer[2]);
            string SendDataText = Encoding.UTF8.GetString(c_temp, 0, ServerData.Buffer[2]);

            string[] DataCheck = SendDataText.Split('_');
            // 버퍼를 지움.
            ServerData.ClearBuffer();
            // 다시 수신 대기 상태로.
            ServerData.Client_Socket.BeginReceive(ServerData.Buffer, 0, 1024, 0, SC_DateReceived, ServerData);
            if (DataCheck[0] == "ServerChat")
            {
                Console.WriteLine(SendDataText);
                chan.ServerChatInfo(DataCheck[1]);
            }
            else if (DataCheck[0] == "ClientChats")
            {
                string MSG = DataCheck[2] + "님의 귓속말 : " + DataCheck[1];
                chan.RoomChatInfo(MSG);
            }
            else if (DataCheck[0] == "ClientChat")
            {
                string MSG = DataCheck[2] + " : " + DataCheck[1];
                chan.RoomChatInfo(MSG);
            }
            else if (DataCheck[0] == "Result")
            {
                if (DataCheck[1] == "LOGIN")
                {
                    Console.WriteLine("로그인이 되었다!");
                    EnableBT();
                    myNick = DataCheck[2];
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(chan);
                }
                else if (DataCheck[1] == "Join")
                {
                    Console.WriteLine("방 입장!");
                    chan.JoinInfo((Convert.ToInt32(DataCheck[2])), (Convert.ToInt32(DataCheck[3])));
                }
            }
            else if (DataCheck[0] == "Info")
            {
                if (DataCheck[1] != "Join")
                {
                    chan.Clear_PlayerList();
                    string[] userlist = DataCheck[1].Split('/');
                    for (int i = 0; i < Convert.ToInt32(DataCheck[2]); i++)
                    {
                        chan.Info_Set(userlist[i], this);
                    }
                    chan.Info_RoomSet(Convert.ToInt32(DataCheck[3]));
                }
                else
                {
                    chan.Clear_RoomPlayerList();
                    string[] roomuserlist = DataCheck[2].Split('/');
                    for (int i = 0; i < Convert.ToInt32(DataCheck[3]); i++)
                    {
                        chan.Info_SetRoom(roomuserlist[i]);
                    }
                }
            }

        }
        private void EnableBT()
        {
            BoxList(IDTextBox);
            BoxList(PWTextBox);
            ButtonList(CreateButton);
            ButtonList(LoginButton);
        }

        // 방 입장
        public void SelectJoin(char num, int roomnum, int playernum)
        {
            MyPacket m_packet = new MyPacket();
            byte[] JoinNum = null;
            JoinNum = m_packet.InitPaket("Join_" + num + "_" + roomnum + "_" + playernum + "_" + myNick);

            Client_Socket.Send(JoinNum, SocketFlags.None);
        }

        // 로그인
        private void LoginButton_Click(object sender, EventArgs e)
        {
            MyPacket m_packet = new MyPacket();
            byte[] ID_DATA = m_packet.InitPaket("Login_" + IDTextBox.Text + "_" + PWTextBox.Text);
            Client_Socket.Send(ID_DATA, SocketFlags.None);
        }

        // 회원가입
        public void SendID_DATA(string ID, string PW, string NAME)
        {
            MyPacket m_packet = new MyPacket();
            byte[] ID_DATA = m_packet.InitPaket("Create_" + ID + "_" + PW + "_" + NAME);
            Client_Socket.Send(ID_DATA, SocketFlags.None);
        }

        public Login()
        {
            InitializeComponent();
            InitData();
            CS_Connet();
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            newID frm = new newID();
            frm.GetLogIn(this);
            frm.Show();
        }
    }
}

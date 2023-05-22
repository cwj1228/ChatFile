using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using Packet;

namespace ChatProgramServer
{
    public partial class Server : Form
    {
        delegate void AppendTextDelegate(Control ctrl, string s);
        delegate void AppendTextDelegateOnlyList(ListBox list, string s);
        delegate void DeleteTextDelegateList(ListBox list, List<Socket> list_array);
        delegate void AddPlayerList(CheckedListBox list,string n);

        string LOG_PATH = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\bin\\Login.json";
        AppendTextDelegate _textAppender;
        AppendTextDelegateOnlyList _listAppender;
        DeleteTextDelegateList _listDelete;
        AddPlayerList _listadd;

        // 방 인원 설정
        int MAX_ROOM = 3;
        int MAX_PLAYER = 5;
        // 방 목록
        public Socket[,] RoomSocketList = new Socket[3, 5];
        // 해당 방 인원 닉네임 
        public string[,] RoomNickList = new String[3, 5];
        // 플레이어
        //public Socket[] PlayerSocketList = new Socket[5];
        // 플레이어 리스트
        public List<string> PlayerNameList = new List<String>();
        // 해당 서버의 IP 값
        public static string Server_IP = string.Empty;
        // 해당 서버의 PORT 값
        public static string Server_PORT = "60200";
        // 소켓 정보량을 받아올 수 있는 최대 수치
        static int SOCKET_MAXIMUM = 255;
        // 소켓 생성
        public static Socket Server_Socket = null;
        // 귓속말 모드
        public bool SecretChat = false;
        // 서버의 주소.
        static IPAddress ServerAddress;
        static IPEndPoint ServerEP;
        // 현재 내 클라이언트가 연결된 리스트
        static List<Socket> CurrentClientSocketList = new List<Socket>();
        static List<string> BlackList = new List<string>();

        // 패킷 받아올 때 써줄거.
        bool IsFirst = true;
        bool IsDone = false;
        byte[] Copy_Receivd;
        int ReceivdCount;
        int PACKET_SIZE;
        int PACKET_DATA_SIZE;
        int BuffCount;
        int HaveCount;
        public Server()
        {
            InitializeComponent();
            Server_Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            InitServer();
            // ? 
            _textAppender = new AppendTextDelegate(AppendText);
            _listAppender = new AppendTextDelegateOnlyList(ApeendList);
            _listDelete = new DeleteTextDelegateList(DeleteList);
            _listadd = new AddPlayerList(AddList);
        }
        void AddList(CheckedListBox _list,string _playername)
        {
            if(_list.InvokeRequired)
            {
                _list.Invoke(_listadd,_list,_playername);
            }
            else
            {
                _list.Items.Add(_playername);
            }
        }
        void AppendText(Control ctrl, string s)
        {
            if (ctrl.InvokeRequired)
            {
                ctrl.Invoke(_textAppender, ctrl, s);
            }
            else
            {
                string source = ctrl.Text;
                ctrl.Text = source + Environment.NewLine + s;
            }
        }

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

        private void SendButton_Click(object sender, EventArgs e)
        {
            if (ServerText.Text.ToString() == "" || ServerText.Text.ToString() == "\r\n")
                return;

            // 보낼 텍스트
            string TTS = ServerText.Text.Trim();
            MyPaket m_packet = new MyPaket();
            // 내 주소.
            string myAddress = Server_Socket.LocalEndPoint.ToString();
            // 버퍼에 담아줌.
            byte[] buff = m_packet.InitPaket("ServerChat_" + TTS);
            // 모든 서버에게 전달.
            for (int i = CurrentClientSocketList.Count - 1; i >= 0; i--)
            {
                Socket Client_Socket = CurrentClientSocketList[i];
                try
                {
                    // 보냄.
                    Client_Socket.Send(buff, SocketFlags.None);
                }
                catch
                {
                    try
                    {
                        Client_Socket.Dispose();
                    }
                    catch { }
                    CurrentClientSocketList.RemoveAt(i);
                }
            }
            ServerText.Clear();

        }
        void DeleteList(ListBox list, List<Socket> list_array)
        {
            if (list.InvokeRequired)
            {
                list.Invoke(_listDelete, list, list_array);
            }
            else
            {
                list.Items.Clear();
                for (int i = 0; i < list_array.Count; i++)
                {
                    list.Items.Add(list_array[i].RemoteEndPoint);
                }
            }
        }
        
        // 서버 생성.
        public void InitServer()
        {
            IPHostEntry he = Dns.GetHostEntry(Dns.GetHostName());

            // 처음으로 발견되는 ipv4 주소를 사용한다.
            foreach (IPAddress addr in he.AddressList)
            {
                if (addr.AddressFamily == AddressFamily.InterNetwork)
                {
                    ServerAddress = addr;
                    break;
                }
            }
            // 주소가 없다면..
            if (ServerAddress == null)
            {
                // 로컬호스트 주소를 사용한다.
                ServerAddress = IPAddress.Loopback;
            }

            // 서버의 엔드포인트 지정.
            ServerEP = new IPEndPoint(ServerAddress, int.Parse(Server_PORT));
            // 소켓의 로컬을 엔드포인트와 연결을 시킨다.
            Server_Socket.Bind(ServerEP);
            // 10개씩 읽어오겠다.
            Server_Socket.Listen(10);
            Server_Socket.BeginAccept(AceeptServer, null);
        }

        // 서버 연결을 확인 해줄거. 비동기화 방식.
        void AceeptServer(IAsyncResult ar)
        {
            // 새로운 소켓을 만들어, 클라이언트의 연결 요청을 수락한다.
            Socket Client = Server_Socket.EndAccept(ar);

            // 다른 클라이언트 연결 확인한다.
            Server_Socket.BeginAccept(AceeptServer, null);

            string copy_adress = Client.RemoteEndPoint.ToString();
            copy_adress.Split(':');
            // 만약 블랙리스트 안에 있다면..?
            for (int i = 0; i < BlackList.Count; i++)
            {
                if (BlackList[i] == copy_adress[0].ToString())
                {
                    Console.WriteLine("이미 블랙 당한 유저입니다.");
                    Client.Close();
                    return;
                }
            }
            // 연결된 클라이언트의 버퍼를 생성.
            AsyncObject Client_Obj = new AsyncObject(30);
            // 연결된 클라이언트의 소켓을 지정.
            Client_Obj.Client_Socket = Client;

            // 연결된 클라이언트를 리스트에 추가.
            Console.WriteLine("연결됨");

            Client.BeginReceive(Client_Obj.Buffer, 0, Client_Obj.BufferSize, 0, CS_DateReceived, Client_Obj);
        }

        // 클라 -> 서버 정보 받아오기.
        void CS_DateReceived(IAsyncResult ar)
        {
            // 넘어온 정보를 AsyncObject화 시켜서 가져온다.
            AsyncObject ClientData = (AsyncObject)ar.AsyncState;
            MyPaket m_packet = new MyPaket();

            try
            {
                // 데이터 수신을 끝내고 받은 데이터의 바이트 수를 가져온다.
                //int RecievdValue = ClientData.Client_Socket.EndReceive(ar);
                //int RecievdCount = ClientData.Client_Socket.
                // 받은 바이트가 없을 경우 소켓을 닫아버린다.
                //if (RecievdValue <= 0)
                //{
                //    ClientData.Client_Socket.Close();
                //    return;
                //}

                // 최초 체크 부분.
                if (IsFirst)
                {
                    // 일단 패킷 받아온거가 헤더 크기 이상으로 있는가? 
                    if (ClientData.Buffer.Length >= ClientData.Buffer[0] && ClientData.Buffer[1] < SOCKET_MAXIMUM)
                    {
                        // 패킷 사이즈와 데이터 사이즈를 가져옴.
                        PACKET_SIZE = ClientData.Buffer[1];
                        PACKET_DATA_SIZE = ClientData.Buffer[2];
                        BuffCount = 0;
                        // 받은 내용을 카피해서 넣어줄거
                        Copy_Receivd = new byte[PACKET_SIZE];
                        // 내가 지금 몇개를 받았는지 체크.
                        ReceivdCount = 0;
                        for (int i = 0; i < ClientData.Buffer.Length; i++)
                        {
                            if (ClientData.Buffer[i] != 0)
                            {
                                BuffCount++;
                                HaveCount++;
                            }
                        }
                        // 복사.
                        Array.Copy(ClientData.Buffer, 0, Copy_Receivd, ReceivdCount, HaveCount);
                        ReceivdCount = HaveCount;



                        if (HaveCount == PACKET_SIZE)
                            IsDone = true;
                        else
                        {
                            // 끝날때 다시 true로
                            IsFirst = false;
                            ClientData.Client_Socket.BeginReceive(ClientData.Buffer, 0, ClientData.BufferSize, 0, CS_DateReceived, ClientData);
                        }
                    }
                    else
                    {
                        Console.WriteLine("패킷 전송 실패");
                        for (int i = 0; i < CurrentClientSocketList.Count; i++)
                        {
                            if (CurrentClientSocketList[i] == ClientData.Client_Socket)
                            {
                                string copy_adress = CurrentClientSocketList[i].RemoteEndPoint.ToString();
                                copy_adress.Split(':');
                                BlackList.Add(copy_adress[0].ToString());
                                CurrentClientSocketList.RemoveAt(i);
                            }
                        }
                        ClientData.Client_Socket.Close();
                    }
                }
                else
                {
                    if (!IsDone)
                    {
                        for (int i = 0; i < ClientData.BufferSize; i++)
                        {
                            if (ClientData.Buffer[i] != 0)
                            {
                                if (HaveCount == PACKET_SIZE)
                                    break;
                                BuffCount++;
                                HaveCount++;
                                Copy_Receivd[HaveCount - 1] = ClientData.Buffer[i];
                            }
                        }

                        if (HaveCount == PACKET_SIZE)
                        {
                            Console.WriteLine("전송 완료.");
                            IsDone = true;
                        }
                        else
                        {
                            // 다 못받으면 다시 비동기 호출.
                            ClientData.Client_Socket.BeginReceive(ClientData.Buffer, 0, ClientData.BufferSize, 0, CS_DateReceived, ClientData);
                        }
                    }
                }

                if (IsDone)
                {
                    // 받은 데이터를 텍스트로 변환 시킨다.
                    //string SendDataText = Encoding.UTF8.GetString(ClientData.Buffer, 0, RecievdValue);
                    // 역직렬화

                    // 전체 데이터 크기만큼 생성
                    byte[] c_temp = new byte[PACKET_DATA_SIZE];
                    Array.Copy(Copy_Receivd, sizeof(int), c_temp, 0, PACKET_DATA_SIZE);
                    // 버퍼 [헤더크기 == 4] 부터 시작해서 c_temp[0]을 시작으로 버퍼 총크기만큼 복사.
                    //Array.Copy(ClientData.Buffer, ClientData.Buffer[0], c_temp, 0, ClientData.Buffer[2]);

                    string SendDataText = Encoding.UTF8.GetString(c_temp, 0, PACKET_DATA_SIZE);

                    string[] OnlyData = SendDataText.Split('_');
                    if (OnlyData[0] == "Create")
                    {
                        Console.WriteLine("계정이 생성됐습니다.");
                        if (!File.Exists(LOG_PATH))
                        {
                            File.Create(LOG_PATH);
                            //JObject jobject = JObject.Parse("{id : \"" + OnlyData[1] + "\", pw : \"" + OnlyData[2] + "\", name : \"" + OnlyData[3] + "\"}");
                            //string jsontext = System.IO.File.ReadAllText(LOG_PATH);
                            //File.WriteAllText(LOG_PATH, (jobject.ToString() + "\r\n" + jsontext));
                            var jarray = JObject.Parse("{\"id\":" + OnlyData[1] + "\"pw\": " + OnlyData[2] + ",name : \"" + OnlyData[3] + "\"}");
                            File.WriteAllText(LOG_PATH, (jarray.ToString() + "\r\n"));
                        }
                        else
                        {
                            var json = File.ReadAllText(LOG_PATH);
                            JObject obj = new JObject(JObject.Parse(json));
                            //var jarray = JObject.Parse("{\"" + OnlyData[1] + "\": \"" + OnlyData[2] + "\",name : \"" + OnlyData[3] + "\"}");
                            var jarray = JObject.Parse("{id : \"" + OnlyData[1] + "\", pw : \"" + OnlyData[2] + "\", name : \"" + OnlyData[3] + "\"}");

                            JArray temp = new JArray();

                            for (int i = 0; i < obj["user"].Count(); i++)
                            {
                                temp.Add(obj["user"][i]);
                            }
                            temp.Add(jarray);
                            string result = "{\n\t\"user\": " + temp.ToString() + "\n}";
                            JObject resultobj = JObject.Parse(result);
                            Console.WriteLine(resultobj);

                            File.WriteAllText(LOG_PATH, (resultobj.ToString()));
                            //JObject jobject = JObject.Parse("{id : \"" + OnlyData[1] + "\", pw : \"" + OnlyData[2] + "\", name : \"" + OnlyData[3] + "\"}");
                            //string jsontext = System.IO.File.ReadAllText(LOG_PATH);
                            //jobject.Add(jobject.ToString(), jsontext);
                            //File.WriteAllText(LOG_PATH, (jobject.ToString() + ",\r\n" + jsontext));
                        }
                    }
                    else if (OnlyData[0] == "Login")
                    {
                        var json = File.ReadAllText(LOG_PATH);
                        JObject obj = new JObject(JObject.Parse(json));

                        for (int i = 0; i < obj["user"].Count(); i++)
                        {
                            if (obj["user"][i]["id"].ToString() == OnlyData[1])
                            {
                                Console.WriteLine("아이디 찾음");
                                if (obj["user"][i]["pw"].ToString() == OnlyData[2])
                                {
                                    Console.WriteLine("로그인 성공");
                                    byte[] result = m_packet.InitPaket("Result_LOGIN_" + obj["user"][i]["name"].ToString());
                                    string s_info = "";
                                    PlayerNameList.Add(obj["user"][i]["name"].ToString());
                                    AddList(CheckPlayer,obj["user"][i]["name"].ToString() + "_" + ClientData.Client_Socket.RemoteEndPoint);
                                    CurrentClientSocketList.Add(ClientData.Client_Socket);
                                    for (int j = 0; j < PlayerNameList.Count; j++)
                                    {
                                        if (j != PlayerNameList.Count - 1)
                                            s_info += PlayerNameList[j] + "/";
                                        else
                                            s_info += PlayerNameList[j];
                                    }
                                    byte[] info = m_packet.InitPaket("Info_" + s_info + "_" + PlayerNameList.Count + "_" + RoomSocketList.GetLength(0));
                                    ClientData.Client_Socket.Send(result);

                                    for(int j = 0; j < CurrentClientSocketList.Count; j++)
                                    {
                                        CurrentClientSocketList[j].Send(info, SocketFlags.None);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("비밀번호가 틀림!");
                                }
                                break;
                            }
                        }

                        //JObject jObject = JObject.Parse(str);
                        //Console.WriteLine(jObject.ToString());
                        //Console.WriteLine(jObject["id1"].ToString());
                        //Console.WriteLine(jObject["id2"].ToString());
                    }
                    else if (OnlyData[0] == "Join")
                    {
                        if (OnlyData[1] == OnlyData[2])
                        {
                            IsDone = false;
                            IsFirst = true;
                            HaveCount = 0;
                            ReceivdCount = 0;
                            BuffCount = 0;

                            // 버퍼를 지움.
                            ClientData.ClearBuffer();
                            Copy_Receivd = null;
                            // 다시 수신 대기 상태로.
                            ClientData.Client_Socket.BeginReceive(ClientData.Buffer, 0, ClientData.BufferSize, 0, CS_DateReceived, ClientData);
                            return;
                        }

                        for (int i = 0; i < MAX_PLAYER;)
                        {
                            if(RoomSocketList[(Convert.ToInt32(OnlyData[1]) - 1),i] != null)
                            {
                                if (i == MAX_PLAYER - 1)
                                {
                                    Console.WriteLine("방이 꽉찼습니다.");
                                    byte[] error = m_packet.InitPaket("ERROR_FULL");
                                    ClientData.Client_Socket.Send(error);
                                    break;
                                }
                                i++;
                            }
                            else
                            {
                                RoomSocketList[(Convert.ToInt32(OnlyData[1]) - 1), i] = ClientData.Client_Socket;
                                Console.WriteLine("현재 " + (Convert.ToInt32(OnlyData[1])) + "번방에 " + (i + 1) + "번째 플레이어로 입장");
                                
                                if(Convert.ToInt32(OnlyData[2]) != -1)
                                {
                                    RoomSocketList[(Convert.ToInt32(OnlyData[2]) - 1), (Convert.ToInt32(OnlyData[3]))] = null;
                                    RoomNickList[(Convert.ToInt32(OnlyData[2]) - 1), (Convert.ToInt32(OnlyData[3]))] = null;
                                }
                                byte[] result = m_packet.InitPaket("Result_Join_" + OnlyData[1] + "_" + i);
                                ClientData.Client_Socket.Send(result);

                                // 여기서 부터 방 바뀌고 인원 교체.
                                string s_info = "";
                                int tempcount = 0;
                                RoomNickList[(Convert.ToInt32(OnlyData[1]) - 1), i] = OnlyData[4];
                                for (int j = 0; j < MAX_PLAYER; j++)
                                {
                                    if (RoomNickList[(Convert.ToInt32(OnlyData[1]) - 1), j] != "" && RoomNickList[(Convert.ToInt32(OnlyData[1]) - 1), j] != null)
                                    {
                                        tempcount++;
                                        s_info += RoomNickList[(Convert.ToInt32(OnlyData[1]) - 1), j];
                                        if (j != MAX_PLAYER - 1)
                                            s_info += "/";
                                    }
                                }

                                // 인포용 조인을 따로 만들까..
                                byte[] info = m_packet.InitPaket("Info_Join_" + s_info + "_" + tempcount);
                                ClientData.Client_Socket.Send(info);
                                for (int j = 0; j < MAX_PLAYER; j++)
                                {
                                    if (RoomSocketList[(Convert.ToInt32(OnlyData[1]) - 1), j] != null && j != i)
                                        RoomSocketList[(Convert.ToInt32(OnlyData[1]) - 1), j].Send(info);
                                    
                                }
                                s_info = "";
                                tempcount = 0;
                                if ((Convert.ToInt32(OnlyData[2]) != -1))
                                {
                                    // 여기서 부터 다시 방목록
                                    for (int j = 0; j < MAX_PLAYER; j++)
                                    {

                                        if (RoomNickList[(Convert.ToInt32(OnlyData[2]) - 1), j] != "" && RoomNickList[(Convert.ToInt32(OnlyData[2]) - 1), j] != null)
                                        {
                                            tempcount++;
                                            s_info += RoomNickList[(Convert.ToInt32(OnlyData[2]) - 1), j];
                                            if (j != MAX_PLAYER - 1)
                                                s_info += "/";
                                        }
                                    }
                                    info = m_packet.InitPaket("Info_Join_" + s_info + "_" + tempcount);
                                    for (int j = 0; j < MAX_PLAYER; j++)
                                    {
                                        if (Convert.ToInt32(OnlyData[2]) - 1 != -1 && RoomSocketList[(Convert.ToInt32(OnlyData[2]) - 1), j] != null)
                                            RoomSocketList[(Convert.ToInt32(OnlyData[2]) - 1), j].Send(info);
                                    }
                                }
                                break;
                            }
                        }
                    }
                    else if (OnlyData[0] == "Send")
                    {
                        if (Convert.ToInt32(OnlyData[4]) == -1)
                        {
                            string temp = "ClientChat_" + OnlyData[2] + "_" + OnlyData[3];
                            byte[] t_buff = m_packet.InitPaket(temp);
                            // 서버를 통해서 다른 클라이언트들에게 전송
                            for (int i = 0; i < MAX_PLAYER; i++)
                            {
                                if (RoomSocketList[Convert.ToInt32(OnlyData[1]), i] != null && RoomSocketList[Convert.ToInt32(OnlyData[1]), i].RemoteEndPoint != ClientData.Client_Socket.RemoteEndPoint)
                                {
                                    RoomSocketList[Convert.ToInt32(OnlyData[1]), i].Send(t_buff, SocketFlags.None);
                                }
                            }
                        }
                        else
                        {
                            string temp = "ClientChats_" + OnlyData[2] + "_" + OnlyData[3];
                            byte[] t_buff = m_packet.InitPaket(temp);
                            CurrentClientSocketList[Convert.ToInt32(OnlyData[4])].Send(t_buff);
                        }
                    }
                    IsDone = false;
                    IsFirst = true;
                    HaveCount = 0;
                    ReceivdCount = 0;
                    BuffCount = 0;

                    // 버퍼를 지움.
                    ClientData.ClearBuffer();
                    Copy_Receivd = null;
                    // 다시 수신 대기 상태로.
                    ClientData.Client_Socket.BeginReceive(ClientData.Buffer, 0, ClientData.BufferSize, 0, CS_DateReceived, ClientData);
                }
            }
            catch
            {
                for (int i = 0; i < CurrentClientSocketList.Count; i++)
                {
                    if (CurrentClientSocketList[i] == ClientData.Client_Socket)
                    {
                        CurrentClientSocketList.RemoveAt(i);
                        for (int x = 0; x < RoomNickList.GetLength(0); x++)
                        {
                            for (int y = 0; y < RoomNickList.GetLength(1); y++)
                            {
                                if (PlayerNameList[i] == RoomNickList[x, y])
                                {
                                    RoomNickList[x, y] = null;
                                }
                            }   
                        }
                        PlayerNameList.RemoveAt(i);
                    }
                }
                ClientData.Client_Socket.Close();
                DeleteList(CheckPlayer, CurrentClientSocketList);
            }
        }
    }
}

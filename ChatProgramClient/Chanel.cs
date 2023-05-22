using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatProgramClient
{
    public partial class Chanel : Form
    {
        delegate void AppendTextDelegateOnlyList(ListBox list, string s);
        AppendTextDelegateOnlyList _listAppender;
        delegate void ChatAllClear(ListBox list);
        ChatAllClear _chatallclear;

        Login m_Login;
        static bool Is_Join = false;
        static int Room_Number = -1;
        static int Player_Number = -1;
        public Chanel()
        {
            _chatallclear = new ChatAllClear(ChatList);
            _listAppender = new AppendTextDelegateOnlyList(ApeendList);
            InitializeComponent();
        }

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
        void ChatList(ListBox list)
        {
            if (list.InvokeRequired)
            {
                list.Invoke(_chatallclear, list);
            }
            else
            {
                list.Items.Clear();
            }
        }
        private void Chanel_Load(object sender, EventArgs e)
        {

        }

        // 전송 버튼
        private void SendButton_Click(object sender, EventArgs e)
        {
            if (SendTTS.Text.ToString() == "" || Room_Number == -1)
                return;

            string TTS = SendTTS.Text.Trim();

            for(int i = 0; i< PlayerList.Items.Count;i++)
            {
                if(PlayerList.GetItemChecked(i))
                {
                    m_Login.SendMessage(TTS, (Room_Number - 1), i);
                    RoomChatInfo("(귓속말 보냄) 나 : " + TTS);
                    SendTTS.Clear();
                    return;
                }
            }

            m_Login.SendMessage(TTS, (Room_Number - 1), -1);
            RoomChatInfo("나 : " + TTS);
            SendTTS.Clear();
        }

        // 화면 지우기용.
        public void Clear_PlayerList()
        {
            ChatList(PlayerList);
        }
        public void Clear_RoomPlayerList()
        {
            ChatList(RoomPlayerList);
        }

        // 플레이어 리스트 정보
        public void Info_Set(string userlist, Login temp)
        {
            ApeendList(PlayerList, userlist);
            m_Login = temp;
        }
        // 방 플레이어 리스트 정보
        public void Info_SetRoom(string roomuserlist)
        {
            ApeendList(RoomPlayerList, roomuserlist);
        }

        // 방 목록 리스트 정보
        public void Info_RoomSet(int num)
        {
            ChatList(RoomList);
            for (int i = 0; i < num; i++)
            {
                ApeendList(RoomList, i + 1 + "번방");
            }
        }

        public void JoinInfo(int roomnum, int playernum)
        {
            Room_Number = roomnum;
            Player_Number = playernum;
            Is_Join = true;
            ChatList(ChatLog);
            RoomChatInfo(Room_Number + "번방에 입장했습니다!");
        }
        // 서버 채팅 정보.
        public void ServerChatInfo(string server)
        {
            ApeendList(ServerChat, server);
        }
        // 방 채팅 정보.
        public void RoomChatInfo(string room)
        {
            ApeendList(ChatLog, room);
        }
        private void JoinRoom(object sender, EventArgs e)
        {
            if (RoomList.SelectedItem == null)
                return;

            Console.WriteLine(RoomList.SelectedItem.ToString()[0]);
            m_Login.SelectJoin(RoomList.SelectedItem.ToString()[0], Room_Number, Player_Number);
        }

        private void PlayerCheck(object sender, ItemCheckEventArgs e)
        {
            for(int i = 0; i < PlayerList.Items.Count; i++)
            {
                if(i != e.Index)
                {
                    PlayerList.SetItemChecked(i, false);
                }
            }
        }

        private void KeyBoardInput(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendButton_Click(sender, e);
            }
        }
    }
}

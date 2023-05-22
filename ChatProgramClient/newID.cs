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
    public partial class newID : Form
    {
        // 델리게이트로 함수 대리자 선언.
        delegate void AppendTextDelegateOnlyList(ListBox list, string s);
        AppendTextDelegateOnlyList _listAppender;

        public static Login m_login = null;
        public newID()
        {
            InitializeComponent();
        }
        public void GetLogIn(Login temp)
        {
            m_login = temp;
        }
        private void newIDCreate_Click(object sender, EventArgs e)
        {
            if (IDBOX.Text.ToString() == "" || PWBOX.Text.ToString() == "" || NICKNAMEBOX.Text.ToString() == "")
            {
                MessageBox.Show("빈 공간 없이 모두 작성해야 합니다!", "오류!");
                return;
            }
            m_login.SendID_DATA(IDBOX.Text.Trim(), PWBOX.Text.Trim(), NICKNAMEBOX.Text.Trim());
            this.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace ChatProgramClient
{
    class AsyncObject
    {
        // 어싱크로 받아온 객체의 버퍼.
        public byte[] Buffer;
        // 현재 소켓.
        public Socket Client_Socket;
        // 읽기 전용으로 만든 버퍼 크기.
        public readonly int BufferSize;
        // 받아온 크기 만큼의 버퍼 생성.
        public AsyncObject(int bufferSize)
        {
            BufferSize = bufferSize;
            Buffer = new byte[BufferSize];
        }

        // 버퍼 삭제용.
        public void ClearBuffer()
        {
            // 버퍼를 0~크기만큼 삭제.
            Array.Clear(Buffer, 0, BufferSize);
        }
    }
}

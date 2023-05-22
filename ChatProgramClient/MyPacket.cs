using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packet
{
    class MyPacket
    {
        // 해당 패킷 헤더의 길이.
        static int _headerLength = 4;
        // 해당 패킷 데이터 길이.
        static int _paketDataLength = 1024;
        // 총길이
        static int _paketTotalLength;
        // 헤더 생성.
        byte[] p_Header = new byte[_headerLength];
        // 데이터 바디.
        byte[] p_Body;

        public byte[] InitPaket(string Data)
        {
            // 헤더 크기.
            p_Header[0] = (byte)_headerLength;
            _paketDataLength = Encoding.UTF8.GetBytes(Data).Length;
            // 패킷할 전체 크기.
            _paketTotalLength = _paketDataLength + _headerLength;
            p_Header[1] = (byte)(_paketTotalLength);
            // 받은 데이터 전체 크기.
            p_Header[2] = (byte)_paketDataLength;
            p_Header[3] = 1;

            p_Body = Encoding.UTF8.GetBytes(Data);
            byte[] Packer = new byte[_paketTotalLength];
            Array.Copy(p_Header, 0, Packer, 0, _headerLength);
            Array.Copy(p_Body, 0, Packer, _headerLength, _paketDataLength);

            Console.WriteLine(Encoding.UTF8.GetString(Packer).ToString());
            return Packer;
        }
    }
}

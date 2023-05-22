using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace JsonObj
{
    class JsonDB
    {
        static int num = 0;
        public string IP;
        public string PORT;
        static string PATH = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\bin\\testjs.json";
        // Json 파일 생성
        static void CreateJson()
        {
            if (!File.Exists(PATH))
            {
                File.Create(PATH);
            }
            else
            {
                return;
            }
        }
        // 정보 저장
        void WriteJson()
        {
            if (File.Exists(PATH))
            {
                // 내가 진입할 서버 정보 이곳에 서버의 아이피를 적어야한다.
                JObject jobject = new JObject(new JProperty("IP", "[INPUT_SERVERIP]"), new JProperty("PORT", "60200"));
                File.WriteAllText(PATH, jobject.ToString());
            }
            else
            {
                Console.WriteLine("현재 파일이 없습니다...");
            }
        }
        // 정보 불러오기
        public JsonDB ReadJson()
        {
            if (!File.Exists(PATH))
            {
                CreateJson();
            }
            else
            {
                WriteJson();
            }

            // 인코딩 된 텍스트를 읽기용으로 불러옴
            StreamReader R_File = File.OpenText(PATH);
            // Json 파일을 읽기 위한 지정자.
            JsonTextReader R_Reader = new JsonTextReader(R_File);
            // 내가 원하는 인자값을 얻기위해 쪼개는 방식 ex) split 형식
            JObject JObj = (JObject)JToken.ReadFrom(R_Reader);

            // JsonDB 형식을 생성해서 반환해줌.
            JsonDB DB = new JsonDB();
            // 해당 인자값 추출.
            DB.IP = JObj["IP"].ToString();
            DB.PORT = JObj["PORT"].ToString();
            return DB;
        }
    }
}

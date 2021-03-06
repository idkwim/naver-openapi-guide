﻿using System;
using System.Net;
using System.Text;
using System.IO;
using System.Web;

namespace NaverAPI_Guide
{
    public class APIExamCafeJoin
    {
        static void Main(string[] args)
        {
            string token = "YOUR_ACCESS_TOKEN";// 네이버 로그인 접근 토큰;
            string header = "Bearer " + token; // Bearer 다음에 공백 추가
            string clubid = "28339939";// 카페의 고유 ID값 http://cafe.naver.com/apiexam
            string nickname = HttpUtility.UrlEncode("api 개발자");
            nickname =  HttpUtility.UrlEncode(nickname, Encoding.GetEncoding("EUC-KR"));
            string apiURL = "https://openapi.naver.com/v1/cafe/" + clubid + "/members";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiURL);
            request.Headers.Add("Authorization", header);
            request.Method = "POST";
            byte[] byteDataParams = Encoding.UTF8.GetBytes("nickname=" + nickname);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteDataParams.Length;
            Stream st = request.GetRequestStream();
            st.Write(byteDataParams, 0, byteDataParams.Length);
            st.Close();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string status = response.StatusCode.ToString();
            if (status == "OK")
            {
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                string text = reader.ReadToEnd();
                Console.WriteLine(text);
            }
            else
            {
                Console.WriteLine("Error 발생=" + status);
            }
            st.Close();
            response.Close();
        }
    }
}

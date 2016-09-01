using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Collections.Specialized;
using System.Text.RegularExpressions;


namespace TestTeleramm
{
    class Program
    {
        public static string Token = @"248500606:AAGKfZGqkSPW9Rk3r4FkDJD4XEBqOUk-lo0";
        public static Int32 LastUpdateID = 445254268;


        static void GetUpdate()
        {

            using (var webClient = new WebClient())
            {

                //Console.WriteLine("Запрос обновления {0}", LastUpdateID + 1);
                string response = webClient.DownloadString("https://api.telegram.org/bot" + Token + "/getUpdates" + "?offset=" + (LastUpdateID + 1));

                var N = JSON.Parse(response);




                foreach (JSONNode r in N["result"].AsArray)
                {
                    LastUpdateID = r["update_id"].AsInt;
                    Console.WriteLine("Пришло сообщение: {0}", r["message"]["text"]);


                    SendMessage(Computing.Comp(r["message"]["text"]).ToString(), r["message"]["chat"]["id"].AsInt);

                }


            }
        }

        static void SendMessage(string message, int chatID)
        {

            using (var webClient = new WebClient())
            {
                var pars = new NameValueCollection();

                pars.Add("text", message);
                pars.Add("chat_id", chatID.ToString());

                webClient.UploadValues("https://api.telegram.org/bot" + Token + "/sendMessage", pars);

            }


        }



        static void Main(string[] args)
        {

            while (true)
            {
                GetUpdate();

                Thread.Sleep(1000);
            }
        }
    }
}

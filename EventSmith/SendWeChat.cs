﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 定时事件
{
    public class SendWeChat
    {

        public SendWeChat(DealEvent dl)
        {
            dl.dealMsg += SentOndWeChat;
        }

        public void SentOndWeChat(string uid, string msg)
        {
            if (uid == "456")
            {
                Console.WriteLine("SendWeChat:" + msg);
            }
        }
    }
}

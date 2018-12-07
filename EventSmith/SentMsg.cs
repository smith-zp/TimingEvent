using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 定时事件
{
    public class SentMsg
    {

        public SentMsg(DealEvent dl)
        {
            dl.dealMsg += SentOndMsg;
        }

        public void SentOndMsg(string uid, string msg)
        {
            if (uid == "123")
            {
                Console.WriteLine("SentMsg:"  + msg);
            }
        }
    }
}

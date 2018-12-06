using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 定时事件
{
    public class SentMsg
    {
        public void SentOndMsg(string uid, string msg)
        {
            if (msg == "123")
            {
                Console.WriteLine("SentMsg:"  +uid);
            }
        }
    }
}
